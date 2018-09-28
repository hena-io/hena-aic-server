using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hena
{
	public class SingletonThreadService<T> : Singleton<T>, IService where T : class, new()
	{

		#region Events
		public event Action onBeginService;
		public event Action onUpdateService;
		public event Action onEndService;
		#endregion

		#region Private Variables
		private object _lock = new object();
        private CancellationTokenSource CancellationToken { get; set; }
        private TaskCompletionSource<bool> ThreadCompletion { get; set; }
        private Thread _thread = null;
		private bool _isActiveService = false;
		#endregion

		#region Public Properties
		public long UpdateIntervalMS { get; set; } = 1000 / 30;
		public bool IsActiveService { get { return _isActiveService; } }

		public int ThreadId
		{
			get
			{
				if (_thread == null)
					return 0;
				return _thread.ManagedThreadId;
			}
		}
		public bool IsAliveThread
		{
			get
			{
				if (_thread == null)
					return false;
				return _thread.IsAlive;
			}
		}

		public bool IsCurrentThread
		{
			get
			{
				return _thread == Thread.CurrentThread;
			}
		}
		#endregion


		#region Protected Properties
		protected object LockInstance { get { return _lock; } }
		public virtual TimeSpan StopServiceTimeout { get; set; } = TimeSpan.FromSeconds(10);
		#endregion


		// 초기화
		protected override void OnInitialize()
		{
		}

		// 해제
		protected override void OnRelease()
		{
			
		}


        // 서비스 시작
        public void StartService()
        {
            Task.WaitAll(Task.Run(async () => await StartServiceAsync()));
        }

        public async Task StartServiceAsync()
		{
			await StopServiceAsync();

            CancellationToken = new CancellationTokenSource();
            ThreadCompletion = new TaskCompletionSource<bool>();
            _thread = new Thread(RunThread);
			_thread.IsBackground = true;
			_thread.Start();
			_isActiveService = true;
		}
		
		// 서비스 업데이트( 외부 스레드 호출 사용 불가 )
		public void UpdateService(double deltaTimeSec)
		{
			if (Thread.CurrentThread != _thread)
				return;

			OnUpdateService(deltaTimeSec);
		}

        // 서비스 종료
        public void StopService()
        {
            Task.WaitAll(Task.Run(async () => await StopServiceAsync()));
        }

        public async Task StopServiceAsync()
		{
			_isActiveService = false;
			if( _thread != null )
			{
                CancellationToken.Cancel();
                await Task.WhenAll(ThreadCompletion.Task);
				_thread = null;
                ThreadCompletion = null;
                CancellationToken = null;
            }
		}

		private void RunThread()
		{
			OnBeginService();
			Utility.InvokeSafe(onBeginService);

			DateTime lastTime = DateTime.UtcNow;
			while (true)
			{
				if(CancellationToken.IsCancellationRequested)
					break;

                try
                {
					DateTime nowTime = DateTime.UtcNow;
					double deltaTimeMS = TimeUtility.GetDeltaTimeMS(lastTime, nowTime);
					if ( deltaTimeMS >= UpdateIntervalMS )
					{
						lastTime = nowTime;
						double deltaTimeSec = TimeUtility.MSToSec(deltaTimeMS);
						UpdateService(deltaTimeSec);
						Utility.InvokeSafe(onUpdateService);
					}
                }
                catch(Exception ex)
                {
					NLog.LogManager.GetCurrentClassLogger().Error("Exception - {0} {1}", ex.Message, ex.StackTrace);
                }
				
				Thread.Sleep(1);
			}
			Utility.InvokeSafe(onEndService);
			OnEndService();

            ThreadCompletion.TrySetResult(true);
        }

		#region IService
		protected virtual void OnBeginService() { }
		protected virtual void OnUpdateService(double deltaTimeSec) { }
		protected virtual void OnEndService() { }
		#endregion // IService
	}
}
