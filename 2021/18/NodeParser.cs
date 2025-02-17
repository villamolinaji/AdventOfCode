namespace _18
{
	public class NodeParser
	{
		private Node? parserCurrent;

		public (Node node, int index) ParseNode(ReadOnlySpan<char> txt)
		{
			var node = new Node();

			if (char.IsNumber(txt[0]))
			{
				node.LeftNode = parserCurrent;

				if (parserCurrent != null)
				{
					parserCurrent.RightNode = node;
				}

				parserCurrent = node;

				node.Number = txt[0] - '0';

				return (node, 1);
			}
			else
			{
				(node.LeftChild, var leftIndex) = ParseNode(txt[1..]);

				(node.RightChild, var rightIndex) = ParseNode(txt[(1 + leftIndex + 1)..]);

				return (node, 1 + leftIndex + 1 + rightIndex + 1);
			}
		}
	}
}
