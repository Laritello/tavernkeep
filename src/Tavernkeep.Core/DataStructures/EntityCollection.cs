using System.Collections;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.DataStructures
{
	public class EntityCollection<T> : ICollection<T> where T : INamedProperty
	{
		private readonly List<T> _items = [];

		public T this[int index]
		{
			get => _items[index];
			set => _items[index] = value;
		}

		public T this[string name] => _items.First(x => x.Name == name);

		public int Count => _items.Count;

		public bool IsReadOnly => false;

		public void Add(T item) => _items.Add(item);

		public void Clear() => _items.Clear();

		public bool Contains(T item) => _items.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

		public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

		public bool Remove(T item) => _items.Remove(item);

		IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
	}
}
