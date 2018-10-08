using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Hena
{
    public interface IDictionaryDataContainer<TContainer, TKey, TValue>
    {
        void Add(TKey key, TValue value);
        void AddRange(Dictionary<TKey, TValue> items);
        void Remove(TKey key);
		bool ContainsKey(TKey key);
		bool ContainsValue(TValue value);
		TValue Find(TKey key);
        bool Find(TKey key, out TValue value);
        void Clear();
        bool Swap(ref TContainer target);

		TKey[] ToArrayKeys();
		TValue[] ToArrayValues();
	}

	[Serializable]
	public abstract class DictionaryDataContainer<TContainer, TKey, TValue>
        : IResetable
        , IDictionaryDataContainer<TContainer, TKey, TValue>
        where TContainer : class, IDictionaryDataContainer<TContainer, TKey, TValue>, new()
        where TValue : class, new()
	{
		public virtual Dictionary<TKey, TValue> Items { get; protected set; } = new Dictionary<TKey, TValue>();

		public virtual int Count { get { return Items.Count; } }


		public Dictionary<TKey, TValue> NewItemContainer()
		{
			return new Dictionary<TKey, TValue>();
		}

		#region Public Methods
		public virtual void Add(TKey key, TValue value)
        {
            Items.Add_LockThis(key, value);
        }

        public virtual void AddRange(Dictionary<TKey, TValue> values)
        {
			lock(Items)
			{
				Utility.AddRange(Items, values);
			}
		}

		public virtual void Remove(TKey key)
		{
			lock(Items)
			{
				Items.Remove(key);
			}
		}

		public virtual bool ContainsKey(TKey key)
		{
			return Items.ContainsKey_LockThis(key);
		}

		public virtual bool ContainsValue(TValue value)
		{
			return Items.ContainsValue_LockThis(value);
		}

		public virtual TValue Find(TKey key)
        {
			lock(Items)
			{
				TValue value;
				Items.TryGetValue(key, out value);
				return value;
			}
        }

        public virtual bool Find(TKey key, out TValue value)
        {
			lock(Items)
			{
				return Items.TryGetValue(key, out value);
			}
		}

		public virtual TValue FindOrAdd(TKey key)
		{
			lock (Items)
			{
				TValue value;
				if( Items.TryGetValue(key, out value) == false )
				{
					value = NewValue(key);
					Items.Add(key, value);
				}
				return value;
			}
		}

		protected virtual TValue NewValue(TKey key)
		{
			TValue value = new TValue();
			return value;
		}

		public virtual void Clear()
		{
			Items.Clear_LockThis();
		}

        public virtual bool Swap(ref TContainer target)
        {
            var p = target as DictionaryDataContainer<TContainer, TKey, TValue>;
            if (p == null)
                return false;

			var temp = p.Items;
			p.Items = Items;
			Items = temp;
            return true;
        }

		public TKey[] ToArrayKeys()
		{
			return Items.ToArrayKey_LockThis();
		}

		public TValue[] ToArrayValues()
		{
			return Items.ToArrayValue_LockThis();
		}
		#endregion // Public Methods

		#region IResetable
		public virtual void Reset()
		{
			Items.Clear_LockThis();
		}
		#endregion // IResetable
	}

}
