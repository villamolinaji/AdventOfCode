using _22;

var modulo = 20183;

var depth = 11739;
(int x, int y) target = (11, 718);

var erosionLevelCache = new Dictionary<(int, int), int>();

var riskLevel = 0;

for (int y = 0; y <= target.y; y++)
{
	for (int x = 0; x <= target.x; x++)
	{

		riskLevel += (int)GetRegionType(x, y);
	}
}

Console.WriteLine(riskLevel);

// Part 2
var queue = new PriorityQueue<((int x, int y) pos, Tool tool, int t)>();
var visited = new HashSet<((int x, int y), Tool tool)>();

var minutes = int.MaxValue;

queue.Enqueue(0, ((0, 0), Tool.Torch, 0));

while (queue.Any())
{
	var state = queue.Dequeue();
	var (pos, tool, t) = state;

	if (pos.x == target.x &&
		pos.y == target.y &&
		tool == Tool.Torch)
	{
		minutes = t;

		Console.WriteLine(minutes);

		break;
	}

	var hash = (pos, tool);

	if (visited.Contains(hash))
	{
		continue;
	}

	visited.Add(hash);

	foreach (var (newPos, newTool, dt) in GetNeighbours(pos, tool))
	{
		queue.Enqueue(
			t + dt + Math.Abs(newPos.x - target.x) + Math.Abs(newPos.y - target.y),
			(newPos, newTool, t + dt)
		);
	}
}

int GetErosionLevel(int x, int y)
{
	var key = (x, y);
	if (!erosionLevelCache.ContainsKey(key))
	{
		if (x == target.x && y == target.y)
		{
			erosionLevelCache[key] = depth;
		}
		else if (x == 0 && y == 0)
		{
			erosionLevelCache[key] = depth;
		}
		else if (x == 0)
		{
			erosionLevelCache[key] = ((y * 48271) + depth) % modulo;
		}
		else if (y == 0)
		{
			erosionLevelCache[key] = ((x * 16807) + depth) % modulo;
		}
		else
		{
			erosionLevelCache[key] = ((GetErosionLevel(x, y - 1) * GetErosionLevel(x - 1, y)) + depth) % modulo;
		}
	}
	return erosionLevelCache[key];
}

RegionType GetRegionType(int x, int y)
{
	return (RegionType)(GetErosionLevel(x, y) % 3);
}

IEnumerable<((int x, int y) pos, Tool tool, int dt)> GetNeighbours((int x, int y) pos, Tool tool)
{
	yield return GetRegionType(pos.x, pos.y) switch
	{
		RegionType.Rocky => (pos, tool == Tool.ClimbingGear ? Tool.Torch : Tool.ClimbingGear, 7),
		RegionType.Narrow => (pos, tool == Tool.Torch ? Tool.Nothing : Tool.Torch, 7),
		RegionType.Wet => (pos, tool == Tool.ClimbingGear ? Tool.Nothing : Tool.ClimbingGear, 7),
		_ => throw new ArgumentException("Invalid region type", nameof(pos))
	};

	foreach (var dx in new[] { -1, 0, 1 })
	{
		foreach (var dy in new[] { -1, 0, 1 })
		{
			if (Math.Abs(dx) + Math.Abs(dy) != 1)
			{
				continue;
			}

			var posNew = (x: pos.x + dx, y: pos.y + dy);
			if (posNew.x < 0 || posNew.y < 0)
			{
				continue;
			}

			switch (GetRegionType(posNew.x, posNew.y))
			{
				case RegionType.Rocky when tool == Tool.ClimbingGear || tool == Tool.Torch:
				case RegionType.Narrow when tool == Tool.Torch || tool == Tool.Nothing:
				case RegionType.Wet when tool == Tool.ClimbingGear || tool == Tool.Nothing:
					yield return (posNew, tool, 1);
					break;
			}
		}
	}
}
