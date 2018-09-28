using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Hena
{
    public class DBUtility
    {
		private readonly static Type TYPE_BOOLEAN = typeof(bool);
		private readonly static Type TYPE_DATETIME = typeof(DateTime);
		private readonly static Type TYPE_TIMESPAN = typeof(TimeSpan);

		// 안전한 풀링처리가 만들어지기 전에는 풀링 처리하지 말것!!!
		public static T NewQuery<T>() where T : class, new()
		{
			return new T();
		}

        #region DataSet / DataRow

        public static bool ContainsRowData(DataSet ds, int tableIndex)
        {
            if (ds == null)
                return false;

            if (ds.Tables.Count <= tableIndex)
                return false;
			
            if (ds.Tables[tableIndex].Rows.Count == 0)
                return false;

            return true;
        }

		public static DataRow GetFirstRow(DataSet ds, int tableIndex)
		{
			return GetRow(ds, tableIndex, 0);
		}

		public static DataRow GetRow(DataSet ds, int tableIndex, int rowIndex)
		{
			if (ds.Tables.Count <= tableIndex)
				return null;

			DataTable table = ds.Tables[tableIndex];
			if (table.Rows.Count <= rowIndex)
				return null;

			return table.Rows[rowIndex];
		}

		public static int GetRowCount(DataSet ds, int tableIndex)
		{
			if (ds.Tables.Count <= tableIndex)
				return 0;

			return ds.Tables[tableIndex].Rows.Count;
		}

        public static byte ToDBBool(bool value)
        {
            return (byte)(value ? 1 : 0);
        }
        public static bool FromDBBool(byte value)
        {
            return value > 0;
        }

		public static bool AsValueEnum<T>(DataRow row, string key, out T outValue, T defaultValue = default(T)) where T : struct
		{
            outValue = defaultValue;

            object value = null;
			try
			{
				value = row[key];
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return false;
			}

			if (value == null)
				return false;

			try
			{
				if (value is string)
				{
					return Enum.TryParse<T>(value as string, true, out outValue);
				}
				else
				{
					outValue = (T)Convert.ChangeType(value, typeof(T));
				}
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return false;
			}
			
			return true;
		}

		public static bool AsValue(DataRow row, string key, out DateTime outValue, DateTime defaultValue, DateTimeKind kind)
		{
			DateTime value;
			bool result = AsValue<DateTime>(row, key, out value, defaultValue);
			outValue = new DateTime(value.Ticks, kind);
			return result;
		}
		public static bool AsValue(DataRow row, string key, out DateTime outValue, DateTime defaultValue = default(DateTime))
		{
			return AsValue(row, key, out outValue, defaultValue, DateTimeKind.Utc);
		}

        public static bool AsValue(DataRow row, string key, out TimeSpan outValue, TimeSpan defaultValue = default(TimeSpan))
        {
            string value;
            bool result = AsValue(row, key, out value, defaultValue.ToString());
            if( TimeSpan.TryParse(value, out outValue) == false )
            {
                outValue = defaultValue;
            }
            return result;
        }

        public static bool AsValue(DataRow row, string key, out DBKey outValue, DBKey defaultValue = default(DBKey))
        {
            long value;
            bool result = AsValue<long>(row, key, out value, defaultValue.Value);
            outValue = value;
            return result;
        }
		public static bool AsValue(DataRow row, string key, out bool outValue, bool defaultValue = false)
		{
			byte value;
			bool result = AsValue<byte>(row, key, out value, ToDBBool(defaultValue));
			outValue = FromDBBool(value);
			return result;
		}

		public static bool AsValue<T>(DataRow row, string key, out T outValue, T defaultValue = default(T)) where T : IConvertible
        {
			outValue = defaultValue;
			object value = null;
            try
            {
                value = row[key];
            }
            catch (Exception ex)
            {
				NLog.LogManager.GetCurrentClassLogger().Error(ex, "Not contains key");
				outValue = defaultValue;
				return false;
            }

			if( value == DBNull.Value )
				return true;

            if (value == null )
				return false;

            try
            {
				Type type = typeof(T);
				if (type == value.GetType())
                {
                    outValue = (T)value;
                }
                else if( type == typeof(string))
                {
                    outValue = (T)(object)value.ToString();
                }
				else
				{
					outValue = (T)Convert.ChangeType(value, typeof(T));
                }

            }
            catch (Exception ex)
            {
				NLog.LogManager.GetCurrentClassLogger().Error(ex, "Data Type Error");
				return false;
            }
            return true;
        }
        #endregion

        public static string ToProcedureQuery(string procedureName, List<object> parameters)
        {
			if (parameters.Count == 0)
			{
				string.Format("call {0}();", procedureName);
			}

			StringBuilder builder = new StringBuilder(1024);
            builder.Append("call ");
            builder.Append(procedureName);
            builder.Append("(");
            int count = parameters.Count;
            for (int i = 0; i < count - 1; ++i)
            {
				builder.AppendFormat("'{0}', ", ToQueryString(parameters[i]));
            }

            if (count > 0)
            {
				builder.AppendFormat("'{0}'", ToQueryString(parameters[count - 1]));
            }

            builder.Append(");");

            return builder.ToString();
        }

		private static string ToQueryString(object value)
		{
			try
			{
				Type type = value.GetType();
				if (type == TYPE_DATETIME)
				{
					return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
				}
				else if( type == TYPE_BOOLEAN )
				{
					return (bool)value ? "1" : "0";
				}
				else if( type == TYPE_TIMESPAN )
				{
                    var time = (TimeSpan)value;
                    return $"{time.Days:d2} {time.Hours:d2}:{time.Minutes:d2}:{time.Seconds:d2}";
				}
				return value.ToString();
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return "null";
			}
		}

        public static MySqlCommand ProcedureToMySqlCommand(string procedureName, List<object> parameters)
        {
            MySqlCommand command = new MySqlCommand(ToProcedureQuery(procedureName, parameters));
            command.CommandType = CommandType.Text;
            return command;
        }

        public static MySqlCommand ProcedureToMySqlCommand(string procedureName, List<object> parameters, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(ToProcedureQuery(procedureName, parameters), connection);
            command.CommandType = CommandType.Text;
            return command;
        }
    }
}
