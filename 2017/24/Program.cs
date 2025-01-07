var lines = File.ReadAllLinesAsync("Input.txt").Result;

var bridges = new List<(int a, int b)>();

foreach (var line in lines)
{
	var parts = line.Split('/');
	bridges.Add((int.Parse(parts[0]), int.Parse(parts[1])));
}

var strongest = 0;

var queue = new Queue<(int strength, List<(int a, int b)> bridges, (int a, int b) usedBridge)>();

foreach (var bridge in bridges)
{
	if (bridge.a == 0)
	{
		var newBridges = new List<(int a, int b)>(bridges);
		newBridges.RemoveAt(newBridges.IndexOf(bridge));
		queue.Enqueue((bridge.a + bridge.b, newBridges, bridge));
	}
	else if (bridge.b == 0)
	{
		var newBridges = new List<(int a, int b)>(bridges);
		newBridges.Remove(bridge);
		queue.Enqueue((bridge.a + bridge.b, newBridges, (bridge.b, bridge.a)));
	}
}

while (queue.Count > 0)
{
	var (currentStrength, currentBridges, currentUsedBridge) = queue.Dequeue();

	var nextBridges = currentBridges.Where(b => (b.a == currentUsedBridge.b || b.b == currentUsedBridge.b)).ToList();

	if (nextBridges.Count == 0)
	{

		strongest = Math.Max(currentStrength, strongest);
	}
	else
	{
		foreach (var nextBridge in nextBridges)
		{
			var newBridges = new List<(int a, int b)>(currentBridges);
			newBridges.RemoveAt(newBridges.IndexOf(nextBridge));

			if (nextBridge.a == currentUsedBridge.b)
			{
				queue.Enqueue((currentStrength + nextBridge.a + nextBridge.b, newBridges, nextBridge));
			}
			else
			{
				queue.Enqueue((currentStrength + nextBridge.a + nextBridge.b, newBridges, (nextBridge.b, nextBridge.a)));
			}
		}
	}
}

Console.WriteLine(strongest);

// Part 2
var solutions = new Dictionary<int, int>();

var queue2 = new Queue<(int strength, List<(int a, int b)> bridges, List<(int a, int b)> usedBridges)>();

foreach (var bridge in bridges)
{
	if (bridge.a == 0)
	{
		var newBridges = new List<(int a, int b)>(bridges);
		newBridges.RemoveAt(newBridges.IndexOf(bridge));
		queue2.Enqueue((bridge.a + bridge.b, newBridges, new List<(int a, int b)> { bridge }));
	}
	else if (bridge.b == 0)
	{
		var newBridges = new List<(int a, int b)>(bridges);
		newBridges.Remove(bridge);
		queue2.Enqueue((bridge.a + bridge.b, newBridges, new List<(int a, int b)> { (bridge.b, bridge.a) }));
	}
}

while (queue2.Count > 0)
{
	var (currentStrength, currentBridges, currentUsedBridges) = queue2.Dequeue();

	var lastUsedBridge = currentUsedBridges[currentUsedBridges.Count - 1];

	var nextBridges = currentBridges.Where(b => (b.a == lastUsedBridge.b || b.b == lastUsedBridge.b)).ToList();

	if (nextBridges.Count == 0)
	{
		var length = currentUsedBridges.Count;
		if (solutions.ContainsKey(length))
		{
			solutions[length] = Math.Max(solutions[length], currentStrength);
		}
		else
		{
			solutions.Add(length, currentStrength);
		}
	}
	else
	{
		foreach (var nextBridge in nextBridges)
		{
			var newBridges = new List<(int a, int b)>(currentBridges);
			newBridges.RemoveAt(newBridges.IndexOf(nextBridge));

			var newUsedBridges = new List<(int a, int b)>(currentUsedBridges);

			if (nextBridge.a == lastUsedBridge.b)
			{
				newUsedBridges.Add(nextBridge);
				queue2.Enqueue((currentStrength + nextBridge.a + nextBridge.b, newBridges, newUsedBridges));
			}
			else
			{
				newUsedBridges.Add((nextBridge.b, nextBridge.a));
				queue2.Enqueue((currentStrength + nextBridge.a + nextBridge.b, newBridges, newUsedBridges));
			}
		}
	}
}

strongest = solutions.OrderByDescending(s => s.Key).First().Value;

Console.WriteLine(strongest);