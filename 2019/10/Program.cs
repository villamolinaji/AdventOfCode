var lines = File.ReadAllLinesAsync("Input.txt").Result;

var map = new List<List<char>>();

foreach (var line in lines)
{
	map.Add(line.ToCharArray().ToList());
}

var width = map[0].Count;
var height = map.Count;

var asteroids = new Dictionary<(int x, int y), int>();

for (int y = 0; y < height; y++)
{
	for (int x = 0; x < width; x++)
	{
		var line = map[y][x];

		if (line == '#')
		{
			var countAsteroidsDetected = CountAsteroids(x, y);
			asteroids.Add((x, y), countAsteroidsDetected);
		}
	}
}

var max = asteroids.Values.Max();

Console.WriteLine(max);

// Part 2
var station = asteroids.First(x => x.Value == max).Key;

var asteroidsList = asteroids.Where(x => x.Key != station).Select(x => (x.Key.x, x.Key.y)).ToList();

var queues = asteroidsList
	.Select(a =>
	{
		var xDist = a.x - station.x;
		var yDist = a.y - station.y;

		var angle = Math.Atan2(xDist, yDist);

		return (a.x, a.y, angle, dist: Math.Sqrt((xDist * xDist) + (yDist * yDist)));
	})
	.ToLookup(a => a.angle)
	.OrderByDescending(a => a.Key)
	.Select(a => new Queue<(int x, int y, double angle, double dist)>(a.OrderBy(b => b.dist)))
	.ToList();

var resultX = 0;
var resultY = 0;

for (int i = 0; i < 200; i++)
{
	var queue = queues[i];
	if (queue.Count > 0)
	{
		var dequeue = queue.Dequeue();
		resultX = dequeue.x;
		resultY = dequeue.y;
	}
}

var result = (resultX * 100) + resultY;

Console.WriteLine(result);


int CountAsteroids(int x, int y)
{
	int countAsteroids = 0;

	var angles = new HashSet<double>();

	for (int y2 = 0; y2 < height; y2++)
	{
		for (int x2 = 0; x2 < width; x2++)
		{
			if (x == x2 && y == y2)
			{
				continue;
			}
			if (map[y2][x2] == '#')
			{
				var angle = Math.Atan2(y2 - y, x2 - x);
				if (!angles.Contains(angle))
				{
					angles.Add(angle);
					countAsteroids++;
				}
			}
		}
	}

	return countAsteroids;
}
