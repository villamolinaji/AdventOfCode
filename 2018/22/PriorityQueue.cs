namespace _22
{
	public class PriorityQueue<T>
	{
		private SortedDictionary<int, Queue<T>> sortedDictionary { get; set; } = new SortedDictionary<int, Queue<T>>();

		public bool Any()
		{
			return sortedDictionary.Any();
		}

		public void Enqueue(int p, T t)
		{
			if (!sortedDictionary.ContainsKey(p))
			{
				sortedDictionary[p] = new Queue<T>();
			}
			sortedDictionary[p].Enqueue(t);
		}

		public T Dequeue()
		{
			var p = sortedDictionary.Keys.First();
			var items = sortedDictionary[p];
			var t = items.Dequeue();
			if (!items.Any())
			{
				sortedDictionary.Remove(p);
			}
			return t;
		}
	}
}
