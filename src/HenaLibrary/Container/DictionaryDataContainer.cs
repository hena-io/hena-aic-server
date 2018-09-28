using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Hena
{
    public interface IDictionaryDataContainer<TContainer, TKey, TValue>
    {
        void Add(TKey key, TValue value);
        void Add(TKey key, TValue value, bool addToClone);
        void AddRange(Dictionary<TKey, TValue> items);
        void AddRange(Dictionary<TKey, TValue> items, bool addToClone);
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
		, IJSONSerializable
		, ICloneable<TContainer>
        , IDictionaryDataContainer<TContainer, TKey, TValue>
        where TContainer : class, IDictionaryDataContainer<TContainer, TKey, TValue>, ICloneable<TContainer>, new()
        where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
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

        public virtual void Add(TKey key, TValue value, bool addToClone)
		{
			Items.Add_LockThis(key, addToClone ? value.Clone() : value);
		}

        public virtual void AddRange(Dictionary<TKey, TValue> values)
        {
			lock(Items)
			{
				Utility.AddRange(Items, values);
			}
		}

        public virtual void AddRange(Dictionary<TKey, TValue> values, bool addToClone)
		{
			lock(Items)
			{
				if (addToClone)
				{
					Utility.AddRangeClone(Items, values);
				}
				else
				{
					Utility.AddRange(Items, values);
				}
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

		#region IJSONSerializable
		protected abstract JToken ToJSON_Key(TKey key);
		protected abstract bool FromJSON_Key(JToken token, out TKey outKey);

		public virtual JToken ToJSON()
		{
			JObject jObject = new JObject();
			JArray jArray = new JArray();
			lock(Items)
			{
				foreach (var it in Items)
				{
					var jItem = new JObject();
					jItem["Key"] = ToJSON_Key(it.Key);
					jItem["Value"] = it.Value.ToJSON();
					jArray.Add(jItem);
				}
			}
			
			jObject["Items"] = jArray;
			return jObject;
		}
		public virtual bool FromJSON(JToken token)
		{
			if (token == null)
				return false;

			JToken jArray = token["Items"];
			if (jArray == null)
				return false;

			var newItems = NewItemContainer();
			foreach (var it in jArray)
			{
				TKey key;
				if (FromJSON_Key(it["Key"], out key) == false)
					return false;

				var value = NewValue(key);
				if (value.FromJSON(it["Value"]) == false)
					return false;

				newItems.Add(key, value);
			}
			Items = newItems;
			return true;
		}
		#endregion // IJSONSerializable

		#region ICloneable
		public virtual TContainer Clone()
		{
			return this.Clone<TContainer>();
		}

		public virtual void CopyTo(ref TContainer target)
		{
            target.Clear();
            target.AddRange(Items, true);
		}
		#endregion // ICloneable

		#region IResetable
		public virtual void Reset()
		{
			Items.Clear_LockThis();
		}
		#endregion // IResetable
	}

	public class DictionaryDataContainerKeyEnum<TKey, TValue>
		: DictionaryDataContainer<DictionaryDataContainerKeyEnum<TKey, TValue>, TKey, TValue>
		, IJSONSerializable
		where TKey : struct
		where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
	{
		#region IJSONSerializable
		protected override JToken ToJSON_Key(TKey key)
		{
			return new JValue(key.ToString());
		}

		protected override bool FromJSON_Key(JToken token, out TKey outKey)
		{
			outKey = default(TKey);
			if (token == null)
				return false;

			outKey = token.AsEnum<TKey>();

			return true;
		}
		#endregion // IJSONSerializable
	}

	public class DictionaryDataContainerKeyDBKey<TValue>
		: DictionaryDataContainer<DictionaryDataContainerKeyDBKey<TValue>, DBKey, TValue>
		, IJSONSerializable
		where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
	{
		#region IJSONSerializable
		protected override JToken ToJSON_Key(DBKey key)
		{
			return new JValue(key.Value);
		}

		protected override bool FromJSON_Key(JToken token, out DBKey outKey)
		{
			outKey = 0;
			if (token == null)
				return false;

			outKey = token.AsDBKey();

			return true;
		}
		#endregion // IJSONSerializable
	}

	public class DictionaryDataContainerKeyString<TValue>
		: DictionaryDataContainer<DictionaryDataContainerKeyString<TValue>, string, TValue>
		, IJSONSerializable
		where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
	{
		#region IJSONSerializable
		protected override JToken ToJSON_Key(string key)
		{
			return new JValue(key);
		}

		protected override bool FromJSON_Key(JToken token, out string outKey)
		{
			outKey = string.Empty;
			if (token == null)
				return false;

			outKey = token.AsString();

			return true;
		}
		#endregion // IJSONSerializable
	}

	public class DictionaryDataContainerKeyLong<TValue>
        : DictionaryDataContainer<DictionaryDataContainerKeyLong<TValue>, long, TValue>
        , IJSONSerializable
        where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
    {
		#region IJSONSerializable
		protected override JToken ToJSON_Key(long key)
		{
			return new JValue(key);
		}

		protected override bool FromJSON_Key(JToken token, out long outKey)
		{
			outKey = 0L;
			if (token == null)
				return false;

			outKey = token.AsLong();

			return true;
		}
		#endregion // IJSONSerializable
	}

	public class DictionaryDataContainerKeyInt<TValue>
		: DictionaryDataContainer<DictionaryDataContainerKeyInt<TValue>, int, TValue>
		, IJSONSerializable
		where TValue : class, ICloneable<TValue>, IJSONSerializable, new()
	{
		#region IJSONSerializable
		protected override JToken ToJSON_Key(int key)
		{
			return new JValue(key);
		}

		protected override bool FromJSON_Key(JToken token, out int outKey)
		{
			outKey = 0;
			if (token == null)
				return false;

			outKey = token.AsInt();

			return true;
		}
		#endregion // IJSONSerializable
	}
}
