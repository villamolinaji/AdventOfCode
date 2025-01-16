using _23;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var nanobots = new List<Nanobot>();

foreach (var line in lines)
{
	var parts = line.Split(new[] { ' ', ',', '<', '>', '=', 'r' }, StringSplitOptions.RemoveEmptyEntries);

	nanobots.Add(new Nanobot
	{
		X = int.Parse(parts[1]),
		Y = int.Parse(parts[2]),
		Z = int.Parse(parts[3]),
		Radius = int.Parse(parts[4])
	});
}

var strongestNanobot = nanobots.OrderByDescending(n => n.Radius).First();

var inRange = nanobots
	.Count(n => GetManhattanDistance(n, strongestNanobot) <= strongestNanobot.Radius);

Console.WriteLine(inRange);

// Part 2
var xmin = nanobots.Min(b => b.X);
var xdiff = nanobots.Max(b => b.X) - xmin;
var ymin = nanobots.Min(b => b.Y);
var ydiff = nanobots.Max(b => b.Y) - ymin;
var zmin = nanobots.Min(b => b.Z);
var zdiff = nanobots.Max(b => b.Z) - zmin;
var length = (xdiff + ydiff + zdiff) / 10;

var boxes = (
		from x in Enumerable.Range(0, 11)
		from y in Enumerable.Range(0, 11)
		from z in Enumerable.Range(0, 11)
		select new Box(
			xmin + (xdiff * x / 10),
			ymin + (ydiff * y / 10),
			zmin + (zdiff * z / 10),
			length,
			nanobots))
	.OrderByDescending(b => b.Count)
	.Take(200)
	.ToList();

while (length >= 10)
{
	length = Math.Max(5, length / 10);

	boxes = boxes
		.SelectMany(b =>
			from x in Enumerable.Range(0, 11)
			from y in Enumerable.Range(0, 11)
			from z in Enumerable.Range(0, 11)
			select new Box(
				b.X - length + (length * x / 5),
				b.Y - length + (length * y / 5),
				b.Z - length + (length * z / 5),
				length,
				b.Nanobots)
		)
		.OrderByDescending(b => b.Count)
		.Take(200)
		.ToList();
}

boxes = boxes
	.SelectMany(b =>
		from x in Enumerable.Range(0, 11)
		from y in Enumerable.Range(0, 11)
		from z in Enumerable.Range(0, 11)
		select (
			x: b.X - 5 + x,
			y: b.Y - 5 + y,
			z: b.Z - 5 + z))
	.Distinct()
	.Select(l => new Box(
		l.x,
		l.y,
		l.z,
		0,
		nanobots))
	.OrderByDescending(b => b.Count)
	.ThenBy(b => Math.Abs(b.X) + Math.Abs(b.Y) + Math.Abs(b.Z))
	.Take(5)
	.ToList();

var result = boxes
	.Select(b => Math.Abs(b.X) + Math.Abs(b.Y) + Math.Abs(b.Z))
	.First()
	.ToString();

Console.WriteLine(result);


int GetManhattanDistance(Nanobot a, Nanobot b)
{
	return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
}
