namespace _18
{
	public class Cloner
	{
		private Node? currentNode;

		public Node? Clone(Node? node)
		{
			if (node == null)
			{
				return null;
			}

			if (node.Number != null)
			{
				var clone = new Node
				{
					Number = node.Number,
					LeftNode = currentNode,
				};

				if (currentNode != null)
				{
					currentNode.RightNode = clone;
				}

				currentNode = clone;

				return clone;
			}
			else
			{
				return new Node
				{
					LeftChild = Clone(node.LeftChild),
					RightChild = Clone(node.RightChild),
				};
			}
		}
	}
}
