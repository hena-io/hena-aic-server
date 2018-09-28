using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace Hena.DB
{
	public class DBInstance : ThreadService, IJSONSerializable
	{
		private readonly static DBInstance Default = new DBInstance();

		#region Events
		public event Action<DBInstance, DBQueryBase[], bool> OnExecuted;
		#endregion	// Events

		// 이름
		public string Name { get; set; } = string.Empty;

		// 한 프레임에 실행할 쿼리 수
		public uint MaxExecuteQueryPerFrame { get; set; } = 100;

		// 대기중인 쿼리 갯수
		public int NumWaitingQuery { get; private set; } = 0;

		// 연결 데이터
		public DBConnectionData ConnectionData { get; private set; } = new DBConnectionData();

		// MySql 접속 인스턴스
		public MySqlConnection Connection { get; private set; } = new MySqlConnection();

		// 핑 주기
		public TimeSpan PingInterval { get; set; } = TimeSpan.FromSeconds(5.0);

		// 연결중
		public bool IsConnecting { get { return Connection.State == ConnectionState.Connecting; } }

		// 연결된 상태
		public bool IsConnected { get { return Connection.State == ConnectionState.Open; } }

		// 호출 대기중인 쿼리
		private Queue<DBQueryBase> WaitingQueries { get; set; } = new Queue<DBQueryBase>();

		// 마지막으로 핑 호출한 시점
		private DateTime LastPingTime { get; set; } = DateTime.UtcNow;

        // 다음에 접속을 시도할 시간
        private DateTime NextTryConnectTime { get; set; } = DateTime.UtcNow;

		// 쿼리 실행 요청
		public void RequestExecuteQuery(DBQueryBase query)
		{
			lock(WaitingQueries)
			{
				++NumWaitingQuery;
				WaitingQueries.Enqueue(query);
			}
		}

		// 쿼리 실행
		public DBQueryBase[] ProcessExecuteQueries()
		{
			if (IsConnected == false)
				return new DBQueryBase[0];

			List<DBQueryBase> queries = new List<DBQueryBase>();
			uint maxProcess = MaxExecuteQueryPerFrame;
			lock (WaitingQueries)
			{
				for (uint numProcess = 0; numProcess < maxProcess; ++numProcess)
				{
					if (WaitingQueries.Count == 0)
						break;

					queries.Add(WaitingQueries.Dequeue());
					--NumWaitingQuery;
				}
			}

			var queryArray = queries.ToArray();
			if(queryArray.Length > 0 )
			{
				bool result = DBQueryBase.Execute(Connection, queryArray);

				Utility.InvokeSafe(OnExecuted, this, queryArray, result);
			}
			return queryArray;
		}
			

		#region ThreadService
		protected override void OnBeginService()
		{
			Connect();
		}

		protected override void OnUpdateService(double deltaTimeSec)
		{
			var nowTime = DateTime.UtcNow;
			if (Connection.State == ConnectionState.Closed)
			{
                if(nowTime >= NextTryConnectTime)
                {
                    Connect();
                    NextTryConnectTime = nowTime.AddSeconds(1);
                }
                return;
			}

			if (Connection.State != ConnectionState.Open)
				return;

			TimeSpan deltaTimeForPing = nowTime - LastPingTime;
			if (deltaTimeForPing >= PingInterval)
			{
				LastPingTime = nowTime;

				if (Connection.Ping() == false)
					return;
			}

			// 쿼리 실행
			ProcessExecuteQueries();
		}

		protected override void OnEndService()
		{
			Disconnect();
		}
		#endregion // ThreadService

		#region Connect & Disconnect
		// 데이터베이스 연결
		protected bool Connect()
		{
			if (IsConnecting || IsConnected)
				return false;

			try
			{
				Connection.ConnectionString = ConnectionData.ConnectionString;
				Connection.Open();

				if (IsConnected)
				{
					NLog.LogManager.GetCurrentClassLogger().Info("Database Connected - Name : {0}, Host : {1}, Port : {2}, Database : {3}"
						, Name, ConnectionData.Host, ConnectionData.Port, ConnectionData.Database);
					return true;
				}
				else
				{
					NLog.LogManager.GetCurrentClassLogger().Error("Failed to connect database - Name : {0}, Host : {1}, Port : {2}, Database : {3}"
					, Name, ConnectionData.Host, ConnectionData.Port, ConnectionData.Database);
					return false;
				}
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex, "Failed to connect database - Name : {0}, Host : {1}, Port : {2}, Database : {3}"
					, Name, ConnectionData.Host, ConnectionData.Port, ConnectionData.Database);

				return false;
			}
		}

		// 데이터베이스 연결(비동기)
		protected async Task ConnectAsync()
		{
			if (IsConnecting || IsConnected)
				return;

			Connection.ConnectionString = ConnectionData.ConnectionString;
			await Connection.OpenAsync();
		}

		// 데이터베이스 연결종료
		protected void Disconnect()
		{
			if (IsConnected)
			{
				Connection.Close();
			}
		}

		// 데이터베이스 연결종료(비동기)
		protected async Task DisconnectAsync()
		{
			if( IsConnected )
			{
				await Connection.CloseAsync();
			}
		}
		#endregion // Connect & Disconnect

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			Name = JSONUtility.GetValue(token, "name", string.Empty);
			MaxExecuteQueryPerFrame = JSONUtility.GetValue(token, "max_execute_query_per_frame", Default.MaxExecuteQueryPerFrame);
			double pingIntervalMS = JSONUtility.GetValue(token, "ping_interval", Default.PingInterval.TotalMilliseconds);
			PingInterval = TimeSpan.FromMilliseconds(pingIntervalMS);

			if (ConnectionData.FromJSON(token["connection_data"]) == false)
				return false;

			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["name"] = Name;
			jObject["max_execute_query_per_frame"] = MaxExecuteQueryPerFrame;
			jObject["ping_interval"] = PingInterval.TotalMilliseconds;
			jObject["connection_data"] = ConnectionData.ToJSON();
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
