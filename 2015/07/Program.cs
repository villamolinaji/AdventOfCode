var lines = File.ReadAllLinesAsync("input.txt").Result;

var wires = new Dictionary<string, string>();

foreach (var line in lines)
{
	var parts = line.Split(" -> ");
	wires[parts[1]] = parts[0];
}

var visited = new Dictionary<string, int>();

var wireAValue = GetWireValue("a");

Console.WriteLine(wireAValue);

// Part 2
wires["b"] = wireAValue.ToString();
visited.Clear();

wireAValue = GetWireValue("a");

Console.WriteLine(wireAValue);


int GetWireValue(string wire)
{
	if (visited.ContainsKey(wire))
	{
		return visited[wire];
	}

	if (int.TryParse(wire, out var value))
	{
		visited[wire] = value;

		return value;
	}

	if (wires.ContainsKey(wire))
	{
		var wireValue = wires[wire];
		var parts = wireValue.Split(" ");

		if (parts.Length == 1)
		{
			if (visited.ContainsKey(parts[0]))
			{
				return visited[parts[0]];
			}

			return GetWireValue(parts[0]);
		}
		else if (parts.Length == 2)
		{
			if (visited.ContainsKey(parts[1]))
			{
				return ~visited[parts[1]];
			}

			return ~GetWireValue(parts[1]);
		}
		else if (parts.Length == 3)
		{
			var op1 = GetWireValue(parts[0]);

			visited[parts[0]] = op1;

			var op2 = GetWireValue(parts[2]);

			visited[parts[2]] = op2;

			if (parts[1] == "AND")
			{
				return op1 & op2;
			}
			else if (parts[1] == "OR")
			{
				return op1 | op2;
			}
			else if (parts[1] == "LSHIFT")
			{
				return op1 << op2;
			}
			else if (parts[1] == "RSHIFT")
			{
				return op1 >> op2;
			}
		}
	}

	return 0;
}
