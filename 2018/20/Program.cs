string input = File.ReadAllTextAsync("input.txt").Result;

var graph = BuildGraph(input);

var distances = BFS(graph, (0, 0));

int maxDistance = distances.Values.Max();

Console.WriteLine(maxDistance);

// Part 2
var rooms = distances.Count(d => d.Value >= 1000);

Console.WriteLine(rooms);


Dictionary<(int, int), List<(int, int)>> BuildGraph(string input)
{
	var graph = new Dictionary<(int, int), List<(int, int)>>();
	var stack = new Stack<(int, int)>();
	var current = (x: 0, y: 0);

	foreach (char c in input)
	{
		switch (c)
		{
			case 'N':
			case 'S':
			case 'E':
			case 'W':
				var next = Move(current, c);

				if (!graph.ContainsKey(current))
				{
					graph[current] = new List<(int, int)>();
				}
				if (!graph.ContainsKey(next))
				{
					graph[next] = new List<(int, int)>();
				}

				graph[current].Add(next);
				graph[next].Add(current);

				current = next;

				break;
			case '(':
				stack.Push(current);

				break;
			case ')':
				current = stack.Pop();

				break;
			case '|':
				current = stack.Peek();

				break;
		}
	}

	return graph;
}

(int, int) Move((int x, int y) pos, char direction)
{
	switch (direction)
	{
		case 'N':
			return (pos.x, pos.y - 1);
		case 'S':
			return (pos.x, pos.y + 1);
		case 'E':
			return (pos.x + 1, pos.y);
		case 'W':
			return (pos.x - 1, pos.y);
		default:
			throw new ArgumentException($"Invalid direction: {direction}");
	}
}

Dictionary<(int, int), int> BFS(Dictionary<(int, int), List<(int, int)>> graph, (int, int) start)
{
	var distances = new Dictionary<(int, int), int>
	{
		[start] = 0
	};
	var queue = new Queue<(int, int)>();

	queue.Enqueue(start);

	while (queue.Count > 0)
	{
		var current = queue.Dequeue();

		foreach (var neighbor in graph[current])
		{
			if (!distances.ContainsKey(neighbor))
			{
				distances[neighbor] = distances[current] + 1;

				queue.Enqueue(neighbor);
			}
		}
	}

	return distances;
}