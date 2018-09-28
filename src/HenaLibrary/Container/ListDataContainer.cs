using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Hena
{

    public interface IListDataContainer<TContainer, TData>
    {
        void Add(TData item);
        void Add(TData item, bool addToClone);
        void AddRange(ICollection<TData> items);
        void AddRange(ICollection<TData> items, bool addToClone);
        void Remove(TData item);
        void RemoveAt(int idx);
        void Clear();
        bool Swap(ref TContainer target);

		TData FirstItem { get; }
		TData LastItem { get; }
    }


	[Serializable]
	public class ListDataContainer<TContainer, TData>
        : IResetable
		, IJSONSerializable
		, ICloneable<TContainer>
        , ICopyable<TContainer>
        , IListDataContainer<TContainer, TData>
        where TContainer : class, IListDataContainer<TContainer, TData>, ICloneable<TContainer>, new()
        where TData : class, ICloneable<TData>, IJSONSerializable, new()
	{
		public virtual List<TData> Items { get; protected set; } = new List<TData>();

		public virtual int Count { get { return Items.Count; } }

		public List<TData> NewItemContainer()
		{
			return new List<TData>();
		}

		public TData FirstItem { get { lock (Items) { return Items.Count == 0 ? null : Items[0]; } } }
		public TData LastItem { get { lock (Items) { return Items.Count == 0 ? null : Items[Items.Count - 1]; } } }


		#region Public Methods
		public TData[] ToArray()
		{
			return Items.ToArray_LockThis();
		}

        public TData[] ToArrayWithClear()
        {
            lock(Items)
            {
                TData[] items = Items.ToArray();
                Items.Clear();
                return items;
            }
        }

		public virtual void Add(TData item)
        {
            Items.Add_LockThis(item);
        }

        public virtual void Add(TData item, bool addToClone)
		{
			Items.Add_LockThis(addToClone ? item.Clone() : item);
		}

        public virtual void AddRange(ICollection<TData> items)
        {
			lock(Items)
			{
				Items.AddRangeSafe(items);
			}
		}

        public virtual void AddRange(ICollection<TData> items, bool addToClone)
		{
			if (addToClone)
			{
				lock(Items)
				{
					foreach(var it in items )
					{
						Items.Add(it.Clone());
					}
				}
			}
			else
			{
				lock(Items)
				{
					Items.AddRange(items);
				}
			}
		}

		public virtual void Remove(TData item)
		{
			Items.Remove_LockThis(item);
		}

        public virtual void RemoveRange(int index, int count)
        {
            lock(Items)
            {
                Items.RemoveRange(index, count);
            }
        }

		public virtual void RemoveAt(int idx)
		{
			lock (Items)
			{
				Items.RemoveAt(idx);
			}
		}

		public virtual TData GetItemAt(int idx)
		{
			lock (Items)
			{
				return Items[idx];
			}
		}

		public virtual void Clear()
		{
			Items.Clear_LockThis();
		}

        public bool Swap(ref TContainer target)
        {
            var p = target as ListDataContainer<TContainer, TData>;
            if (p == null)
                return false;

			var temp = p.Items;
			p.Items = Items;
			Items = temp;
			return true;
        }
		#endregion // Public Methods

		#region IJSONSerializable
		public virtual JToken ToJSON()
		{
			JObject jObject = new JObject();
			lock (Items)
			{
				JArray jArray = new JArray();
				foreach (var it in Items)
				{
					jArray.Add(it.ToJSON());
				}
				jObject["Items"] = jArray;
			}
			return jObject;
		}

		public virtual bool FromJSON(JToken token)
		{
			if (token == null)
				return false;

			JToken jArray = token["Items"];
			if (jArray == null)
				return false;

			List<TData> newItems = new List<TData>();
			foreach(var it in jArray)
			{
				var item = new TData();
				if (item.FromJSON(it) == false)
					return false;

				newItems.Add(item);
			}
			Items = newItems;
			return true;
		}
		#endregion // IJSONSerializable

		#region ICloneable
		public virtual void CopyTo(ref TContainer target)
		{
			lock(Items)
			{
				lock(target)
				{
					target.Clear();
					target.AddRange(Items, true);
				}
			}
		}

		public virtual TContainer Clone()
		{
			return this.Clone<TContainer>();
		}

        public TContainer CloneWithClear()
        {
            lock(Items)
            {
                var newItem = Clone();
                Clear();
                return newItem;
            }
        }
		#endregion // ICloneable

		#region IResetable
		public virtual void Reset()
		{
			Items.Clear_LockThis();
		}
		#endregion // IResetable
	}
}
