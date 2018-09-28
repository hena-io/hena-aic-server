using System;
using System.Collections.Generic;


namespace Hena.Threads
{
	public class ThreadWorkerPool
	{
		public static ThreadWorkerPool Global = new ThreadWorkerPool(4);

		List<ThreadWorker> WorkerThreads = new List<ThreadWorker>();
		public ThreadWorkerPool(int maxThread = 4)
		{
			SetMaxThread(maxThread);
		}

		public void AddWork(Action work)
		{
			GetWorker().AddLast(work);
		}

		private ThreadWorker GetWorker()
		{
			try
			{
				lock (WorkerThreads)
				{
					ThreadWorker selectedWorker = WorkerThreads[0];
					int count = WorkerThreads.Count;
					for (int i = 1; i < count; ++i)
					{
						if (WorkerThreads[i].NumWork < selectedWorker.NumWork)
						{
							selectedWorker = WorkerThreads[i];
						}
					}
					return selectedWorker;
				}
			}
			catch(Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return null;
			}
			
		}

		public void SetMaxThread(int maxThread)
		{
			maxThread = Mathf.Clamp(maxThread, 1, 1024);
			lock(WorkerThreads)
			{
				while (WorkerThreads.Count > maxThread)
				{
					WorkerThreads.RemoveAt(0);
				}

				while (WorkerThreads.Count < maxThread)
				{
					WorkerThreads.Add(new ThreadWorker());
				}
			}
		}

		
	}

}
