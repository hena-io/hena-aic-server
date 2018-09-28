using Hena;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Hena.Threads
{
	internal class ThreadWorker
	{
		private LinkedList<Action> WorkActions = new LinkedList<Action>();
		private Thread ThreadInstance;

		public int NumWork
		{
			get { lock (WorkActions) { return WorkActions.Count; } }
		}

		public void AddLast(Action action)
		{
			bool startThread = false;
			lock (WorkActions)
			{
				WorkActions.AddLast(action);

				if (ThreadInstance == null)
				{
					ThreadInstance = new Thread(RunThread);
					ThreadInstance.Name = "ThreadWorker-" + ThreadInstance.ManagedThreadId;
					startThread = true;
				}
			}

			if( startThread)
			{
				ThreadInstance.Start();
			}
		}

		private void RunThread()
		{
			while(true)
			{
				Action action = null;
				lock(WorkActions)
				{
					if( WorkActions.Count == 0 )
					{
						ThreadInstance = null;
						return;
					}

					action = WorkActions.First.Value;
					WorkActions.RemoveFirst();
				}

				Utility.InvokeSafe(action);
				Thread.Sleep(1);
			}
		}
	}
	
}
