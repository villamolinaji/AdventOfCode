var lines = File.ReadAllLinesAsync("Input.txt").Result;

var steps = 0;

var directions = new (int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0), (-1, -1), (1, -1), (-1, 1), (1, 1) };

int[][] octopuses = lines
	.Select(line => line.Select(ch => int.Parse(ch.ToString())).ToArray())
	.ToArray();

var maxY = octopuses.Length;
var maxX = octopuses[0].Length;

var countFlashes = 0;

var stepAllFlash = 0;

var queue = new Queue<(int x, int y)>();

while (true)
{
	var newOctopuses = CopyOctopuses(octopuses);

	var flashed = new HashSet<(int x, int y)>();

	for (int y = 0; y < maxY; y++)
	{
		newOctopuses[y] = new int[maxX];
		Array.Copy(octopuses[y], newOctopuses[y], maxX);

		for (int x = 0; x < maxX; x++)
		{
			newOctopuses[y][x] = newOctopuses[y][x] + 1;

			if (newOctopuses[y][x] == 10)
			{
				queue.Enqueue((x, y));
			}
		}
	}

	while (queue.Count > 0)
	{
		(var x, var y) = queue.Dequeue();

		flashed.Add((x, y));

		foreach (var (dx, dy) in directions)
		{
			var nx = x + dx;
			var ny = y + dy;

			if (nx >= 0 && nx < maxX && ny >= 0 && ny < maxY)
			{
				newOctopuses[ny][nx] = newOctopuses[ny][nx] + 1;

				if (newOctopuses[ny][nx] == 10)
				{
					queue.Enqueue((nx, ny));
				}
			}
		}
	}

	foreach ((var x, var y) in flashed)
	{
		newOctopuses[y][x] = 0;
	}

	if (steps < 100)
	{
		countFlashes += flashed.Count;
	}

	if (flashed.Count == maxX * maxY)
	{
		stepAllFlash = steps + 1;
		break;
	}

	octopuses = newOctopuses;

	steps++;
}

Console.WriteLine(countFlashes);

Console.WriteLine(stepAllFlash);


int[][] CopyOctopuses(int[][] source)
{
	var copy = new int[source.Length][];

	for (int i = 0; i < source.Length; i++)
	{
		copy[i] = new int[source[i].Length];
		Array.Copy(source[i], copy[i], source[i].Length);
	}

	return copy;
}
