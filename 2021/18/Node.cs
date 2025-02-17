namespace _18
{
	public class Node
	{
		public int? Number { get; set; }

		public Node? LeftChild { get; set; }

		public Node? RightChild { get; set; }

		public Node? LeftNode { get; set; }

		public Node? RightNode { get; set; }

		public Node? Clone() => new Cloner().Clone(this);

		public long GetMagnitude() =>
			Number ?? ((LeftChild!.GetMagnitude() * 3) + (RightChild!.GetMagnitude() * 2));

		public override string? ToString() =>
			Number != null
				? Number.ToString()
				: $"[{LeftChild},{RightChild}]";
	}
}
