using _10;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var nodes = new Dictionary<string, Node>();

foreach (var line in lines)
{
	var parts = line.Split(' ');

	if (parts[0] == "value")
	{
		var id = parts[5];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { id = id, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}

		nodes[id].values.Add(int.Parse(parts[1]));
	}
	else
	{
		var id = parts[1];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { id = id, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}

		nodes[id].outLow = parts[6];
		nodes[id].outHigh = parts[11];
	}
}

bool isDone = false;
while (!isDone)
{
	foreach (var node in nodes.Values)
	{
		if (node.values.Count == 2 &&
			node.outHigh != null)
		{
			var min = node.values.Min();
			var max = node.values.Max();

			nodes[node.outLow].values.Add(min);
			nodes[node.outHigh].values.Add(max);

			node.values.Clear();

			if (min == 17 && max == 61)
			{
				Console.WriteLine(node.id);
				isDone = true;
				break;
			}
		}
	}
}

// Part 2
nodes = new Dictionary<string, Node>();

foreach (var line in lines)
{
	var parts = line.Split(' ');

	if (parts[0] == "value")
	{
		var id = parts[4] + " " + parts[5];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { id = id, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}

		nodes[id].values.Add(int.Parse(parts[1]));
	}
	else
	{
		var id = parts[0] + " " + parts[1];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { id = id, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}

		var lowId = parts[5] + " " + parts[6];
		if (!nodes.ContainsKey(lowId))
		{
			nodes[lowId] = new Node { id = lowId, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}
		nodes[id].outLow = lowId;

		var highId = parts[10] + " " + parts[11];
		if (!nodes.ContainsKey(highId))
		{
			nodes[highId] = new Node { id = highId, values = new List<int>(), outLow = string.Empty, outHigh = string.Empty };
		}
		nodes[id].outHigh = highId;

	}
}

var machines = Execute(nodes).Last().machine;

var result = machines["output 0"].values.Single() *
	machines["output 1"].values.Single() *
	machines["output 2"].values.Single();

Console.WriteLine(result);


IEnumerable<(Dictionary<string, Node> machine, string id, int min, int max)> Execute(Dictionary<string, Node> machine)
{
	bool any = true;
	while (any)
	{
		any = false;
		foreach (var node in machine.Values)
		{
			if (node.values.Count == 2 && node.outHigh != null)
			{
				any = true;

				var min = node.values.Min();
				var max = node.values.Max();

				machine[node.outLow].values.Add(min);
				machine[node.outHigh].values.Add(max);

				node.values.Clear();

				yield return (machine, node.id, min, max);
			}
		}
	}
}
