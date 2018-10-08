using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
				foreach (var targetProperty in targetProperties)
				{
					if (sourceProperty.Name == targetProperty.Name && sourceProperty.PropertyType == targetProperty.PropertyType)
					{
						targetProperty.SetValue(target, sourceProperty.GetValue(source));
						break;
					}
				}
			}
		}

		public static void Copy<TTarget>(this DataRow source, TTarget target)
		{
			var targetProperties = target.GetType().GetProperties();
			var columns = source.Table.Columns;
			foreach (DataColumn it in columns)
			{
				var targetProperty = Array.Find(targetProperties, (item) => item.Name == it.ColumnName);
				if (targetProperty != null)
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
							convertedValue = Activator.CreateInstance(targetType);
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
