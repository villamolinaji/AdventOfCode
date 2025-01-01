var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rows = lines.Length;
var columns = lines[0].Length;
var pointsOfInterest = new Dictionary<int, (int, int)>();

for (int r = 0; r < rows; r++)
{
	for (int c = 0; c < columns; c++)
	{
		if (char.IsDigit(lines[r][c]))
		{
			pointsOfInterest[lines[r][c] - '0'] = (r, c);
		}
	}
}

int minSteps = GetMinSteps(false);
Console.WriteLine(minSteps);

// Part 2
minSteps = GetMinSteps(true);
Console.WriteLine(minSteps);


int GetMinSteps(bool isPart2)
{
	var distances = new Dictionary<(int, int), int>();

	foreach (var start in pointsOfInterest.Keys)
	{
		var distancesBFS = BFS(pointsOfInterest[start]);

		foreach (var end in pointsOfInterest.Keys)
		{
			if (start != end)
			{
				distances[(start, end)] = distancesBFS[pointsOfInterest[end]];
			}
		}
	}

	var allPoints = pointsOfInterest.Keys.ToList();
	allPoints.Remove(0);

	int shortestPath = TSP(0, allPoints, distances, isPart2);

	return shortestPath;
}

Dictionary<(int, int), int> BFS((int, int) start)
{
	var queue = new Queue<((int, int), int)>();
	var distances = new Dictionary<(int, int), int>();
	var directions = new (int, int)[]
	{
		(1, 0),
		(-1, 0),
		(0, 1),
		(0, -1)
	};

	queue.Enqueue((start, 0));

	distances[start] = 0;

	while (queue.Count > 0)
	{
		var ((r, c), dist) = queue.Dequeue();

		foreach (var (dr, dc) in directions)
		{
			int nr = r + dr;
			int nc = c + dc;

			if (nr >= 0 &&
				nr < rows &&
				nc >= 0 &&
				nc < columns
				&& lines[nr][nc] != '#'
				&& !distances.ContainsKey((nr, nc)))
			{
				distances[(nr, nc)] = dist + 1;

				queue.Enqueue(((nr, nc), dist + 1));
			}
		}
	}

	return distances;
}

int TSP(int current, List<int> remaining, Dictionary<(int, int), int> distances, bool isPart2)
{
	if (remaining.Count == 0)
	{
		return isPart2
			? distances[(current, 0)]
			: 0;
	}

	int shortest = int.MaxValue;

	foreach (var next in remaining)
	{
		var newRemaining = new List<int>(remaining);

		newRemaining.Remove(next);

		var distancesValue = 0;
		if (distances.TryGetValue((current, next), out distancesValue))
		{
			shortest = Math.Min(shortest, distancesValue + TSP(next, newRemaining, distances, isPart2));
		}
	}

	return shortest;
}