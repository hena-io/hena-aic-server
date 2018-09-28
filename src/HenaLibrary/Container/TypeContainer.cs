using System;
using System.Collections.Generic;
using System.Text;

namespace Hena
{
	public class TypeContainer : Singleton<TypeContainer>
	{
		private Dictionary<Type, object> Types = new Dictionary<Type, object>();

		public T Get<T>()
		{
			return (T)Types.TryGetValueEx(typeof(T));
		}

		public void Register<T>(T value)
		{
			Types.Remove(typeof(T));
			Types.Add(typeof(T), value);
		}
	}
}
