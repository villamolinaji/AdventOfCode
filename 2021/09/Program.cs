var lines = File.ReadAllLinesAsync("Input.txt").Result;

var directions = new (int dx, int dy)[] { (-1, 0), (0, -1), (1, 0), (0, 1) };

var lowestPoints = new List<int>();
var basins = new List<int>();

var maxY = lines.Length;
var maxX = lines[0].Length;

for (int y = 0; y < maxY; y++)
{
	for (int x = 0; x < maxX; x++)
	{
		var pointValue = int.Parse(lines[y][x].ToString());

		var isLowest = true;

		foreach (var direction in directions)
		{
			var newX = x + direction.dx;
			var newY = y + direction.dy;

			if (newX >= 0 && newY >= 0 && newX < maxX && newY < maxY)
			{
				var neighbour = int.Parse(lines[newY][newX].ToString());

				if (neighbour <= pointValue)
				{
					isLowest = false;
					break;
				}
			}
		}

		if (isLowest)
		{
			lowestPoints.Add(pointValue);

			basins.Add(CalculateBasin(x, y));
		}
	}
}

var sum = lowestPoints.Sum(p => p + 1);

Console.WriteLine(sum);

// Part 2
var largestBasins = basins.OrderByDescending(b => b).Take(3);
var result2 = 1;
foreach (var basin in largestBasins)
{
	result2 *= basin;
}

Console.WriteLine(result2);


int CalculateBasin(int x, int y)
{
	var basin = 0;

	var visited = new HashSet<(int x, int y)>();
	var queue = new Queue<(int x, int y)>();
	queue.Enqueue((x, y));

	while (queue.Count > 0)
	{
		(var currentX, var currentY) = queue.Dequeue();

		if (visited.Contains((currentX, currentY)))
		{
			continue;
		}
		visited.Add((currentX, currentY));

		basin++;

		var currentValue = int.Parse(lines[y][x].ToString());

		foreach (var direction in directions)
		{
			var newX = currentX + direction.dx;
			var newY = currentY + direction.dy;

			if (newX >= 0 && newY >= 0 && newX < maxX && newY < maxY)
			{
				var neighbour = int.Parse(lines[newY][newX].ToString());

				if (neighbour > currentValue && neighbour != 9)
				{
					queue.Enqueue((newX, newY));
				}
			}
		}
	}

	return basin;
}