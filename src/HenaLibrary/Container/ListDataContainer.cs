using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Hena
{

    public interface IListDataContainer<TContainer, TData>
    {
        void Add(TData item);
        void AddRange(ICollection<TData> items);
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
		, ICloneable<TContainer>
        , ICopyable<TContainer>
        , IListDataContainer<TContainer, TData>
        where TContainer : class, IListDataContainer<TContainer, TData>, ICloneable<TContainer>, new()
        where TData : class, new()
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

        public virtual void AddRange(ICollection<TData> items)
        {
			lock(Items)
			{
				Items.AddRangeSafe(items);
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

		public virtual TData Find(Predicate<TData> match)
		{
			lock (Items)
			{
				return Items.Find(match);
			}
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

		#region ICloneable
		public virtual void CopyTo(ref TContainer target)
		{
			lock(Items)
			{
				lock(target)
				{
					target.Clear();
					target.AddRange(Items);
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
