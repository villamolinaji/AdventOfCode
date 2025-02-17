using _18;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var numbers = lines.Select(ParseLine).ToList();

Node ParseLine(string l) => new NodeParser().ParseNode(l).node;

var result = numbers
	.Aggregate(Sum)
	.GetMagnitude()
	.ToString();

Console.WriteLine(result);

//Part 2
result = (
	from a in numbers
	from b in numbers
	where a != b
	select Sum(a, b).GetMagnitude())
	.Max()
	.ToString();

Console.WriteLine(result);


Node Sum(Node? left, Node? right)
{
	left = left!.Clone();
	right = right!.Clone();

	var leftLink = left;

	while (leftLink?.RightChild != null)
	{
		leftLink = leftLink.RightChild;
	}

	var rightLink = right;

	while (rightLink?.LeftChild != null)
	{
		rightLink = rightLink.LeftChild;
	}

	(leftLink!.RightNode, rightLink!.LeftNode) = (rightLink, leftLink);

	var node = new Node
	{
		LeftChild = left,
		RightChild = right
	};

	Reduce(node);

	return node;
}

void Reduce(Node root)
{
	while (true)
	{
		if (NeedExplode(root, out var node))
		{
			Explode(node!);
		}
		else if (NeedSplit(root, out node))
		{
			Split(node!);
		}
		else
		{
			break;
		}
	}
}

bool NeedExplode(Node root, out Node? node)
{
	node = FindNode(root, 0, static (n, l) =>
		l >= 4 &&
		n?.Number == null &&
		n?.LeftChild?.Number != null &&
		n?.RightChild?.Number != null);

	return node != null;
}

Node? FindNode(Node? root, int level, Func<Node?, int, bool> predicate)
{
	if (predicate(root, level))
	{
		return root;
	}

	if (root?.Number != null)
	{
		return null;
	}

	return FindNode(root?.LeftChild, level + 1, predicate) ?? FindNode(root?.RightChild, level + 1, predicate);
}

void Explode(Node node)
{
	var left = node.LeftChild!.Number!.Value;
	var right = node.RightChild!.Number!.Value;

	node.Number = 0;
	node.LeftNode = node.LeftChild.LeftNode;

	if (node.LeftNode != null)
	{
		node.LeftNode.RightNode = node;
		node.LeftNode.Number += left;
	}

	node.LeftChild = null;

	node.RightNode = node.RightChild.RightNode;

	if (node.RightNode != null)
	{
		node.RightNode.LeftNode = node;
		node.RightNode.Number += right;
	}

	node.RightChild = null;
}

bool NeedSplit(Node root, out Node? node)
{
	node = FindNode(root, 0, static (n, _) => n!.Number >= 10);

	return node != null;
}

void Split(Node node)
{
	var leftNumber = node!.Number / 2;
	var rightNumber = node.Number - leftNumber;

	node.Number = default;
	node.LeftChild = new Node
	{
		LeftNode = node.LeftNode,
		Number = leftNumber,
	};
	node.RightChild = new Node
	{
		RightNode = node.RightNode,
		Number = rightNumber,
	};

	node.LeftChild.RightNode = node.RightChild;
	node.RightChild.LeftNode = node.LeftChild;

	if (node.LeftNode != null)
	{
		node.LeftNode.RightNode = node.LeftChild;
	}

	if (node.RightNode != null)
	{
		node.RightNode.LeftNode = node.RightChild;
	}

	node.LeftNode = node.RightNode = null;
}
