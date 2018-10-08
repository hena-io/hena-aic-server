using System;
using System.Collections.Generic;
using System.Text;

namespace Hena.Library.Misc
{
	public class PropertyCopier<TParent, TChild> where TParent : class
											where TChild : class
	{
		public static void Copy(TParent source, TChild target)
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
	}
}
