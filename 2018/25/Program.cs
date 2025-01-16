using _25;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var points = new List<Point>();

foreach (var line in lines)
{
	var parts = line.Split(',');

	points.Add(new Point
	{
		X = int.Parse(parts[0]),
		Y = int.Parse(parts[1]),
		Z = int.Parse(parts[2]),
		T = int.Parse(parts[3])
	});
}

var constellations = new List<List<Point>>();

foreach (var point in points)
{
	var matchingConstellations = new List<List<Point>>();
	foreach (var constellation in constellations)
	{
		foreach (var otherPoint in constellation)
		{
			if (GetManhattanDistance(otherPoint, point) <= 3)
			{
				matchingConstellations.Add(constellation);
				break;
			}
		}
	}

	if (matchingConstellations.Count == 0)
	{
		constellations.Add(new List<Point> { point });
	}
	else
	{
		var newConstellation = new List<Point> { point };

		foreach (var matchingConstellation in matchingConstellations)
		{
			newConstellation.AddRange(matchingConstellation);
			constellations.Remove(matchingConstellation);
		}

		constellations.Add(newConstellation);
	}
}

Console.WriteLine(constellations.Count);


int GetManhattanDistance(Point a, Point b)
{
	return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z) + Math.Abs(a.T - b.T);
}
