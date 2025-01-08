var lines = File.ReadAllLinesAsync("Input.txt").Result;

var coordinates = lines
			.Select(line => line.Split(',').Select(int.Parse).ToArray())
			.Select(arr => (x: arr[0], y: arr[1]))
			.ToList();

int maxX = coordinates.Max(c => c.x);
int maxY = coordinates.Max(c => c.y);

var grid = new int[maxX + 1, maxY + 1];
var areaCount = new Dictionary<int, int>();
var infiniteAreas = new HashSet<int>();

for (int x = 0; x <= maxX; x++)
{
	for (int y = 0; y <= maxY; y++)
	{
		var distances = coordinates
			.Select((c, index) => (index, distance: ManhattanDistance(x, y, c.x, c.y)))
			.OrderBy(t => t.distance)
			.ToList();

		if (distances[0].distance != distances[1].distance)
		{
			grid[x, y] = distances[0].index;

			if (!areaCount.ContainsKey(distances[0].index))
			{
				areaCount[distances[0].index] = 0;
			}
			areaCount[distances[0].index]++;

			if (x == 0 ||
				y == 0 ||
				x == maxX ||
				y == maxY)
			{
				infiniteAreas.Add(distances[0].index);
			}
		}
	}
}

int largestFiniteArea = areaCount
	.Where(kvp => !infiniteAreas.Contains(kvp.Key))
	.Max(kvp => kvp.Value);

Console.WriteLine(largestFiniteArea);

// Part 2
var totalDistance = 10000;
var region2 = new List<(int x, int y)>();

for (int x = 0; x <= maxX; x++)
{
	for (int y = 0; y <= maxY; y++)
	{
		var distances = coordinates
			.Select((c, index) => (index, distance: ManhattanDistance(x, y, c.x, c.y)))
			.ToList();

		var sumDistances = distances.Sum(d => d.distance);

		if (sumDistances < totalDistance)
		{
			region2.Add((x, y));
		}
	}
}

Console.WriteLine(region2.Count);


int ManhattanDistance(int x1, int y1, int x2, int y2)
{
	return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}
