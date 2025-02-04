var lines = File.ReadAllLinesAsync("Input.txt").Result;

var cycles = 6;

var iterations = new[] { -1, 0, 1 };

var neighbourIterations = (
	from dx in iterations
	from dy in iterations
	from dz in iterations
	where dx != 0 || dy != 0 || dz != 0
	select (dx, dy, dz))
	.ToArray();

var result = GetActivePoints(
	(x, y) => (x: x, y: y, z: 0),
	(p) => neighbourIterations.Select(d => (p.x + d.dx, p.y + d.dy, p.z + d.dz)));

Console.WriteLine(result);

// Part 2
var neighbourIterations2 = (
	from dx in iterations
	from dy in iterations
	from dz in iterations
	from dw in iterations
	where dx != 0 || dy != 0 || dz != 0 || dw != 0
	select (dx, dy, dz, dw))
	.ToArray();

result = GetActivePoints(
	(x, y) => (x: x, y: y, z: 0, w: 0),
	(p) => neighbourIterations2.Select(i => (p.x + i.dx, p.y + i.dy, p.z + i.dz, p.w + i.dw)));

Console.WriteLine(result);


int GetActivePoints<T>(Func<int, int, T> createDimension, Func<T, IEnumerable<T>> getNeighbours)
{
	var height = lines.Length;
	var width = lines[0].Length;

	var activePoints = new HashSet<T>(
		from x in Enumerable.Range(0, width)
		from y in Enumerable.Range(0, height)
		where lines[y][x] == '#'
		select createDimension(x, y)
	);

	for (var i = 0; i < cycles; i++)
	{
		var newActivePoints = new HashSet<T>();
		var inactivePoints = new Dictionary<T, int>();

		foreach (var point in activePoints)
		{
			var activeNeighbours = 0;

			foreach (var neighbour in getNeighbours(point))
			{
				if (activePoints.Contains(neighbour))
				{
					activeNeighbours++;
				}
				else
				{
					inactivePoints[neighbour] = inactivePoints.GetValueOrDefault(neighbour) + 1;
				}
			}

			if (activeNeighbours == 2 || activeNeighbours == 3)
			{
				newActivePoints.Add(point);
			}
		}

		foreach (var (point, activeNeighbours) in inactivePoints)
		{
			if (activeNeighbours == 3)
			{
				newActivePoints.Add(point);
			}
		}

		activePoints = newActivePoints;
	}

	return activePoints.Count();
}