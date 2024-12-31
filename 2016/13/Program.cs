var favoriteNumber = 1352;

var targetX = 31;
var targetY = 39;

var minSteps = int.MaxValue;

var directions = new[]
{
	(1, 0), (-1, 0), (0, 1), (0, -1)
};

var queue = new Queue<(int x, int y, int steps)>();
var visited = new Dictionary<(int x, int y), int>();

queue.Enqueue((1, 1, 0));

while (queue.Count > 0)
{
	var (x, y, steps) = queue.Dequeue();

	if (x == targetX && y == targetY)
	{
		minSteps = Math.Min(minSteps, steps);

		continue;
	}

	if (steps >= minSteps)
	{
		continue;
	}

	if (x < 0 ||
		y < 0 ||
		!IsOpenSpace((x, y)))
	{
		continue;
	}

	if (visited.ContainsKey((x, y)) &&
		visited[(x, y)] <= steps)
	{
		continue;
	}
	visited[(x, y)] = steps;

	foreach (var (dx, dy) in directions)
	{
		queue.Enqueue((x + dx, y + dy, steps + 1));
	}
}

Console.WriteLine(minSteps);

// Part 2
var maxSteps = 50;

queue = new Queue<(int x, int y, int steps)>();
var visited2 = new HashSet<(int, int)>();

queue.Enqueue((1, 1, 0));
visited2.Add((1, 1));

while (queue.Count > 0)
{
	var (x, y, steps) = queue.Dequeue();

	if (steps < maxSteps)
	{
		foreach (var (dx, dy) in directions)
		{
			var nextX = x + dx;
			var nextY = y + dy;

			if (nextX < 0 ||
				nextY < 0 ||
				!IsOpenSpace((nextX, nextY)) ||
				visited2.Contains((nextX, nextY)))
			{
				continue;
			}

			visited2.Add((nextX, nextY));
			queue.Enqueue((nextX, nextY, steps + 1));
		}
	}
}

Console.WriteLine(visited2.Count);


bool IsOpenSpace((int x, int y) pos)
{
	var (x, y) = pos;
	int value = x * x + 3 * x + 2 * x * y + y + y * y + favoriteNumber;
	int bitCount = Convert.ToString(value, 2).Count(c => c == '1');

	return bitCount % 2 == 0;
}