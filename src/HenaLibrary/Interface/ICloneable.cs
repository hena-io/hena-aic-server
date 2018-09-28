
namespace Hena
{
	public interface ICloneable
	{
	}

	public interface ICloneable<T> : ICopyable<T>
	{
		T Clone();
	}

	public static class ICloneableExtension
	{
		public static T Clone<T>(this ICloneable<T> item) where T : ICloneable<T>, new()
		{
			var newItem = new T();
			item.CopyTo(ref newItem);
			return newItem;
		}
	}
}
