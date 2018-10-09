using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Hena.Library.Extensions
{
	public static class ObjectExtension
	{
		public static void Copy<TTarget>(this object source, TTarget target)
		{
			var sourceProperties = source.GetType().GetProperties();
			var targetProperties = target.GetType().GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var targetProperty = Array.Find(targetProperties, (item) => item.Name == sourceProperty.Name && sourceProperty.PropertyType == item.PropertyType);
				if( targetProperty != null && targetProperty.CanWrite)
				{
					targetProperty.SetValue(target, sourceProperty.GetValue(source));
				}
			}
		}

		public static TTarget Clone<TTarget>(this object source) where TTarget : class, new()
		{
			var target = new TTarget();
			source.Copy(target);
			return target;
		}


		public static void Copy<TTarget>(this DataRow source, TTarget target)
		{
			var targetProperties = target.GetType().GetProperties();
			var columns = source.Table.Columns;
			foreach (DataColumn it in columns)
			{
				var targetProperty = Array.Find(targetProperties, (item) => item.Name == it.ColumnName);
				if (targetProperty != null && targetProperty.CanWrite)
				{
					var value = source[it];
					var targetType = targetProperty.PropertyType;
					object convertedValue;
					try
					{
						if (targetType.IsEnum)
						{
							convertedValue = Enum.Parse(targetType, value.ToString());
						}
						else if (targetType == typeof(DBKey))
						{
							convertedValue = new DBKey(value.ToString());
						}
						else if(value.GetType() == typeof(DBNull))
						{
							try
							{
								convertedValue = System.Convert.ChangeType(string.Empty, targetType);
							}
							catch
							{
								convertedValue = Activator.CreateInstance(targetType);
							}
						}
						else
						{
							convertedValue = System.Convert.ChangeType(value, targetType);
						}

						targetProperty.SetValue(target, convertedValue);
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
					}
					
				}
			}
		}


	}


}
