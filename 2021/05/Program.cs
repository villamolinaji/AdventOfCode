var inputLines = File.ReadAllLinesAsync("Input.txt").Result;

var lines = new List<(int x1, int y1, int x2, int y2)>();

foreach (var inputLine in inputLines)
{
	var parts = inputLine.Split("->");
	var coordinates1 = parts[0].Split(",");
	var coordinates2 = parts[1].Split(",");

	lines.Add((int.Parse(coordinates1[0]), int.Parse(coordinates1[1]), int.Parse(coordinates2[0]), int.Parse(coordinates2[1])));
}

var points = GetPoints(false);

var pointsOverlap = points.GroupBy(p => p).Count(g => g.Count() > 1);

Console.WriteLine(pointsOverlap);

// Part 2
points = GetPoints(true);

pointsOverlap = points.GroupBy(p => p).Count(g => g.Count() > 1);

Console.WriteLine(pointsOverlap);


List<(int x, int y)> GetPoints(bool isPart2)
{
	var points = new List<(int x, int y)>();

	foreach (var line in lines)
	{
		if (line.x1 == line.x2)
		{
			for (int y = Math.Min(line.y1, line.y2); y <= Math.Max(line.y1, line.y2); y++)
			{
				points.Add((line.x1, y));
			}
		}
		else if (line.y1 == line.y2)
		{
			for (int x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x1, line.x2); x++)
			{
				points.Add((x, line.y1));
			}
		}
		else if (isPart2)
		{
			var diff = Math.Abs(line.x1 - line.x2);

			for (int i = 0; i <= diff; i++)
			{
				var newX = line.x1 + i;
				var newY = line.y1 + i;

				if (line.x1 > line.x2)
				{
					newX = line.x1 - i;
				}
				if (line.y1 > line.y2)
				{
					newY = line.y1 - i;
				}

				points.Add((newX, newY));
			}
		}
	}

	return points;
}