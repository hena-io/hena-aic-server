using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace Hena.DB
{

	public class DBThread : SingletonThreadService<DBThread>
		, IJSONSerializable
		, IFileSerializable
	{
		#region Sub Classes
		public class ReqQueryData
		{
			public List<DBQuery> ReqQueries = new List<DBQuery>();
			public HashSet<DBQuery> FinishedQueries = new HashSet<DBQuery>();
			public Action<bool, DBQuery[]> OnComplete;
			public bool IsSuccess = false;
			public LinkedListNode<ReqQueryData> ContainerNode;
		}
		#endregion // Sub Classes

		// 1프레임에 처리 가능한 완료 쿼리 수
		public uint MaxProcessFinishedQueryPerFrame { get; set; } = 1000;

		// 대기중인 쿼리 갯수
		public int NumWaitingQuery { get; private set; } = 0;


		// 완료된 쿼리를 자체적으로 호출할지 여부( false 일 경우 외부에서 호출해줘야함. )
		public bool IsSelfProcessingFinishedQuery { get; set; } = true;

		private Dictionary<DBType, DBInstance> DBInstances { get; set; } = new Dictionary<DBType, DBInstance>();
		private LinkedList<ReqQueryData> ReqQueryGroups { get; set; } = new LinkedList<ReqQueryData>();
		private Queue<ReqQueryData> FinishedQueryGroups { get; set; } = new Queue<ReqQueryData>();

		public DBInstance GetDBInstance(DBType type)
		{
			DBInstance instance;
			DBInstances.TryGetValue(type, out instance);
			return instance;
		}

		#region Query Process
		// 쿼리 실행 완료 콜백
		private void OnExecutedQueries(DBInstance dBInstance, DBQueryBase[] queries, bool isSuccess)
		{
			foreach( DBQuery it in queries)
			{
				var qd = it.UserData as ReqQueryData;
				if (qd == null)
					continue;

				lock(qd)
				{
					qd.IsSuccess = isSuccess;
					qd.FinishedQueries.Add(it);
					if( qd.FinishedQueries.Count == qd.ReqQueries.Count )
					{
						lock(ReqQueryGroups)
						{
							ReqQueryGroups.Remove(qd.ContainerNode);
						}
						qd.ContainerNode = null;
						lock (FinishedQueryGroups)
						{
							FinishedQueryGroups.Enqueue(qd);
						}
					}
				}
			}
		}

		// 완료된 쿼리 처리
		public void ProcessFinishedQueries()
		{
			uint maxProcess = MaxProcessFinishedQueryPerFrame;
			for (uint numProcess = 0; numProcess < maxProcess; ++numProcess)
			{
				ReqQueryData qd = null;
				lock (FinishedQueryGroups)
				{
					if (FinishedQueryGroups.Count == 0)
						break;

					qd = FinishedQueryGroups.Dequeue();
				}

				if (qd == null)
					continue;

				try
				{
					Utility.InvokeSafe(qd.OnComplete, qd.IsSuccess, qd.ReqQueries.ToArray());
				}
				catch (Exception ex) { NLog.LogManager.GetCurrentClassLogger().Error(ex); }
			}
		}
		#endregion	// Query Process

		#region Request Queries
		// 쿼리 요청
		public void ReqQuery(DBQuery query, Action<bool, DBQuery[]> onComplete = null)
		{
			ReqQuery(new DBQuery[] { query }, onComplete);
		}

		// 쿼리 요청
		public void ReqQuery(DBQuery[] queries, Action<bool, DBQuery[]> onComplete = null)
		{
			var qd = new ReqQueryData();
			
			qd.OnComplete = onComplete;
			qd.ReqQueries.AddRange(queries);
			lock (ReqQueryGroups)
			{
				qd.ContainerNode = ReqQueryGroups.AddLast(qd);
			}

			foreach( var it in queries)
			{
				it.UserData = qd;
				var dbInstance = GetDBInstance(it.DBType);
				if( dbInstance != null)
				{
					dbInstance.RequestExecuteQuery(it);
				}
				else
				{
					Utility.InvokeSafe(onComplete, false, queries);
				}
			}
		}

		// 쿼리 요청( 비동기 )
		public async Task<bool> ReqQueryAsync(params DBQuery[] queries)
		{
			if (queries.Length == 0)
				return false;

			bool isSuccess = false;
			TaskCompletionSource<bool> taskCompletion = new TaskCompletionSource<bool>();
			ReqQuery(queries, (bool b, DBQuery[] resultQueries) =>
			{
				isSuccess = b;
				taskCompletion.TrySetResult(true);
			});

			await taskCompletion.Task;
			return isSuccess;
		}
		#endregion // Request Queries

		public void SetupToDefaultInstances()
		{
			DBInstances.Add(DBType.Hena_AIC_Config, new DBInstance() { Name = DBType.Hena_AIC_Config.ToString() });
			DBInstances.Add(DBType.Hena_AIC_Service, new DBInstance() { Name = DBType.Hena_AIC_Service.ToString() });
		}

		#region IService
		protected override void OnBeginService()
		{
			//if (LoadFromFile(ServerConfig.Instance.DB_CONFIG_FILEPATH) == false )
			//{
			//	if( File.Exists(ServerConfig.Instance.DB_CONFIG_FILEPATH) == false )
			//	{
			//		SetupToDefaultInstances();
			//		SaveToFile(ServerConfig.Instance.DB_CONFIG_FILEPATH);
			//	}
			//	StopService();
			//	return;
			//}

			foreach( var it in DBInstances)
			{
				var dbInstance = it.Value;
				dbInstance.OnExecuted += OnExecutedQueries;
				it.Value.StartService();
			}
		}

		protected override void OnUpdateService(double deltaTimeSec)
		{

			int numWaitingQuery = 0;
			foreach ( var it in DBInstances)
			{
				numWaitingQuery += it.Value.NumWaitingQuery;
			}
			NumWaitingQuery = numWaitingQuery;

			if(IsSelfProcessingFinishedQuery)
			{
				ProcessFinishedQueries();
			}
		}

		protected override void OnEndService()
		{
			foreach (var it in DBInstances)
			{
				it.Value.StopService();
			}
		}
		#endregion // IService

		#region IFileSerializable
		public bool LoadFromFile(string filePath)
		{
			return IFileSerializableExtension.LoadFromFile(this, filePath);
		}

		public bool SaveToFile(string filePath)
		{
			return IFileSerializableExtension.SaveToFile(this, filePath);
		}
		#endregion // IFileSerializable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			var dbInstances = new Dictionary<DBType, DBInstance>();
			var jConnections = token["connections"];
			foreach(var jConnection in jConnections)
			{
				DBInstance dbInstance = new DBInstance();
				if (dbInstance.FromJSON(jConnection) == false)
					return false;

				DBType type;
				if(Enum.TryParse(dbInstance.Name, true, out type))
				{
					dbInstances.Add(type, dbInstance);
				}
			}
			DBInstances = dbInstances;
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			var jConnections = new JArray();
			foreach (var it in DBInstances)
			{
				jConnections.Add(it.Value.ToJSON());
			}
			jObject["connections"] = jConnections;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
