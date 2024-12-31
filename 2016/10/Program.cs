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
			nodes[id] = new Node { Id = id, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}

		nodes[id].Values.Add(int.Parse(parts[1]));
	}
	else
	{
		var id = parts[1];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { Id = id, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}

		nodes[id].OutLow = parts[6];
		nodes[id].OutHigh = parts[11];
	}
}

bool isDone = false;
while (!isDone)
{
	foreach (var node in nodes.Values)
	{
		if (node.Values.Count == 2 &&
			node.OutHigh != null)
		{
			var min = node.Values.Min();
			var max = node.Values.Max();

			nodes[node.OutLow].Values.Add(min);
			nodes[node.OutHigh].Values.Add(max);

			node.Values.Clear();

			if (min == 17 && max == 61)
			{
				Console.WriteLine(node.Id);
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
			nodes[id] = new Node { Id = id, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}

		nodes[id].Values.Add(int.Parse(parts[1]));
	}
	else
	{
		var id = parts[0] + " " + parts[1];

		if (!nodes.ContainsKey(id))
		{
			nodes[id] = new Node { Id = id, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}

		var lowId = parts[5] + " " + parts[6];
		if (!nodes.ContainsKey(lowId))
		{
			nodes[lowId] = new Node { Id = lowId, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}
		nodes[id].OutLow = lowId;

		var highId = parts[10] + " " + parts[11];
		if (!nodes.ContainsKey(highId))
		{
			nodes[highId] = new Node { Id = highId, Values = new List<int>(), OutLow = string.Empty, OutHigh = string.Empty };
		}
		nodes[id].OutHigh = highId;

	}
}

var machines = Execute(nodes).Last().machine;

var result = machines["output 0"].Values.Single() *
	machines["output 1"].Values.Single() *
	machines["output 2"].Values.Single();

Console.WriteLine(result);


IEnumerable<(Dictionary<string, Node> machine, string id, int min, int max)> Execute(Dictionary<string, Node> machine)
{
	bool any = true;
	while (any)
	{
		any = false;
		foreach (var node in machine.Values)
		{
			if (node.Values.Count == 2 && node.OutHigh != null)
			{
				any = true;

				var min = node.Values.Min();
				var max = node.Values.Max();

				machine[node.OutLow].Values.Add(min);
				machine[node.OutHigh].Values.Add(max);

				node.Values.Clear();

				yield return (machine, node.Id, min, max);
			}
		}
	}
}
