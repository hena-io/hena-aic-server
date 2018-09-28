using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hena
{
	public class ThreadService : IService
	{
		#region Events
		public event Action onBeginService;
		public event Action onUpdateService;
		public event Action onEndService;
		#endregion

		public long UpdateIntervalMS { get; set; } = 1000 / 30;

		public bool IsRunning
		{
			get { return ThreadInstance != null && ThreadInstance.IsAlive; }
		}

		public virtual TimeSpan StopServiceTimeout { get; set; } = TimeSpan.FromSeconds(10);

		private Thread ThreadInstance { get; set; }
		private bool IsStopping { get; set; } = false;


		// 서비스 시작
		public void StartService()
		{
			if (IsRunning)
				return;

			IsStopping = false;
			ThreadInstance = new Thread(RunThread);
			ThreadInstance.IsBackground = true;
			ThreadInstance.Start();
		}

		// 서비스 종료
		public void StopService()
		{
			if (IsRunning == false || IsStopping)
				return;

			IsStopping = true;
			TimeUtility.WaitForThreadStop(ThreadInstance, StopServiceTimeout.TotalSeconds);
		}

		// 서비스 업데이트
		public void UpdateService(double deltaTimeSec)
		{
			if (Thread.CurrentThread != ThreadInstance)
				return;

			OnUpdateService(deltaTimeSec);
		}

		private void RunThread()
		{
			OnBeginService();
			Utility.InvokeSafe(onBeginService);

			DateTime lastTime = DateTime.UtcNow;
			while (true)
			{
				if (IsStopping)
					break;

				try
				{
					DateTime nowTime = DateTime.UtcNow;
					double deltaTimeMS = TimeUtility.GetDeltaTimeMS(lastTime, nowTime);
					if (deltaTimeMS >= UpdateIntervalMS)
					{
						lastTime = nowTime;
						double deltaTimeSec = TimeUtility.MSToSec(deltaTimeMS);
						UpdateService(deltaTimeSec);
						Utility.InvokeSafe(onUpdateService);
					}
				}
				catch (Exception ex)
				{
					NLog.LogManager.GetCurrentClassLogger().Error("Exception - {0} {1}", ex.Message, ex.StackTrace);
				}

				Thread.Sleep(1);
			}
			Utility.InvokeSafe(onEndService);
			OnEndService();

			IsStopping = false;
		}

		#region IService
		protected virtual void OnBeginService() { }
		protected virtual void OnUpdateService(double deltaTimeSec) { }
		protected virtual void OnEndService() { }
		#endregion // IService
	}
}
