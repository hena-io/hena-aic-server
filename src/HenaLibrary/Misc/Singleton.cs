using System.Collections;
using System.Collections.Generic;

namespace Hena
{
	public enum eSingletonMethod
	{
		CreateInstance,
		DestroyInstance,
	}
	public interface ISingleton
	{
		void CallMethod(eSingletonMethod method);
	}

	public class SingletonBase<T> where T : class, new()
	{
		private static object _lockObject = new object();
		private static bool _isDestroy = false;
		private static T _instance;
		public static T Instance { get { return CreateInstance(); } }
		public static T Instanced { get { return _instance; } }

		protected SingletonBase()
		{
		}

		public static T CreateInstance()
		{
			if (_instance == null && _isDestroy == false)
			{
				lock (_lockObject)
				{
					if (_instance == null && _isDestroy == false)
					{
						_instance = new T();
						(_instance as SingletonBase<T>).OnCreateInstance();
					}
				}
			}
			return _instance;
		}

		public static void DestroyInstance()
		{
			if (_instance != null)
			{
				_isDestroy = true;
				(_instance as SingletonBase<T>).OnDestroyInstance();
			}
			_instance = null;
		}

		protected virtual void OnCreateInstance()
		{

		}

		protected virtual void OnDestroyInstance()
		{

		}
	}

	public class Singleton<T> : SingletonBase<T>, ISingleton where T : class, new()
	{
		public void CallMethod(eSingletonMethod method)
		{

			switch (method)
			{
				case eSingletonMethod.CreateInstance:
					{
						CreateInstance();
					}
					break;
				case eSingletonMethod.DestroyInstance:
					{
						DestroyInstance();
					}
					break;
			}

		}

		// 초기화
		protected virtual void OnInitialize()
		{

		}

		// 해제
		protected virtual void OnRelease()
		{

		}

		protected override void OnCreateInstance()
		{
			// 초기화
			OnInitialize();
		}

		protected override void OnDestroyInstance()
		{
			// 해제
			OnRelease();
		}
	}

	public class SingletonContainer<T> : Singleton<T> where T : class, new()
	{
		private List<ISingleton> _instances = new List<ISingleton>();

		protected List<ISingleton> Instances
		{
			get { return _instances; }
		}

		protected void AddSingletonInstance(ISingleton singleton)
		{
			_instances.Add(singleton);
		}

		protected void ReleaseChildSingleton()
		{
			for (int i = _instances.Count - 1; i >= 0; --i)
			{
				if (_instances[i] != null)
				{
					_instances[i].CallMethod(eSingletonMethod.DestroyInstance);
				}
			}
			_instances.Clear();
		}

		protected override void OnDestroyInstance()
		{
			base.OnDestroyInstance();
			ReleaseChildSingleton();
		}
	}
}
