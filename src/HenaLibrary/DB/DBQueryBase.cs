using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.DB
{
	public abstract class DBQueryBase
	{
		#region Interfaces
		// 입력 데이터 인터페이스
		public interface IIN
		{
			void FillParameters(List<object> parameters);
			string ToQueryString(string procedureName);
		}
		
		// 결과 데이터 인터페이스
		public interface IOUT
		{
			bool FromDataTable(DataTable table);
		}
		#endregion // Interfaces

		public abstract class IN_BASE : IIN
		{
			public virtual void FillParameters(List<object> parameters)
			{

			}

			public virtual string ToQueryString(string procedureName)
			{
				List<object> parameters = new List<object>();
				FillParameters(parameters);
				return DBUtility.ToProcedureQuery(procedureName, parameters);
			}
		}


		// 사용자 데이터
		public object UserData { get; set; }

		// 프로시저 이름
		public abstract string ProcedureName { get; }

		// 입력 데이터
		public abstract IIN GetInData();
		public abstract IOUT GetOutData();

		// 쿼리 문자열 조합
		public virtual string ToQueryString()
		{
			return GetInData().ToQueryString(ProcedureName);
			//List<object> parameters = new List<object>();
			//IIN inData = GetInData();
			//if (inData != null)
			//{
			//	inData.FillParameters(parameters);
			//}

			//return DBUtility.ToProcedureQuery(ProcedureName, parameters);
		}

		#region Execute
		// 쿼리 실행
		public virtual bool Execute(MySqlConnection connection)
		{
			return Execute(connection, this);
		}

		// 쿼리 실행(비동기)
		public virtual async Task<bool> ExecuteAsync(MySqlConnection connection)
		{
			return await ExecuteAsync(connection, this);
		}

		// 쿼리 실행(비동기)
		public static async Task<bool> ExecuteAsync(MySqlConnection connection, params DBQueryBase[] queries)
		{
			bool result = false;
			await Task.Run(() =>
			{
				result = Execute(connection, queries);
			});
			return result;
		}
		
		// 쿼리 실행 (비동기 + 콜백)
		public static async Task<bool> ExecuteAsync(MySqlConnection connection, Action<bool, MySqlConnection, DBQueryBase[]> onComplete, params DBQueryBase[] queries)
		{
			bool result = false;
			await Task.Run(() =>
			{
				result = Execute(connection, queries);
			});

			Utility.InvokeSafe(onComplete, result, connection, queries);
			return result;
		}

		// 쿼리 실행
		public static bool Execute(MySqlConnection connection, params DBQueryBase[] queries)
		{
			if (connection == null || queries.Length == 0)
				return false;

            string finalQueryString = "";
            StringBuilder queryStringBuilder = new StringBuilder(1024);
			List<DBQueryBase> selectQueries = new List<DBQueryBase>();

			try
			{
				foreach (var it_query in queries)
				{
					var queryString = it_query.ToQueryString();
					queryStringBuilder.Append(queryString);
					if (it_query.GetOutData() != null)
					{
						selectQueries.Add(it_query);
					}
				}
				finalQueryString = queryStringBuilder.ToString();

				MySqlCommand command = new MySqlCommand(finalQueryString, connection);
				command.CommandType = CommandType.Text;

				if (selectQueries.Count == 0)
				{
					command.ExecuteNonQuery();
					return true;
				}
				else
				{
					var ds = new DataSet();
					MySqlDataAdapter da = new MySqlDataAdapter();
					da.SelectCommand = command;
					da.Fill(ds);

					if (selectQueries.Count != ds.Tables.Count)
					{
						NLog.LogManager.GetCurrentClassLogger().Warn("MySqlQuery different to result count. NumQuery : {0}, NumResult : {1} Query = {2}"
							, selectQueries.Count
							, ds.Tables.Count
							, finalQueryString);

						return false;
					}

					bool result = true;
					for (int idx = 0; idx < selectQueries.Count; ++idx)
					{
						var outData = selectQueries[idx].GetOutData();
						result = outData.FromDataTable(ds.Tables[idx]) && result;
					}
					return result;
				}
			}
            catch (Exception ex)
            {
				NLog.LogManager.GetCurrentClassLogger().Error(ex, finalQueryString);
                return false;
            }
        }
		#endregion // Execute
	}
}
