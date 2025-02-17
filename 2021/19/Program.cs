using _19;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var scanners = new HashSet<Scanner>();
var scanner = new Scanner();

foreach (var line in lines)
{
	if (string.IsNullOrEmpty(line))
	{
		scanners.Add(scanner);
	}
	else if (line.Contains("scanner"))
	{
		scanner = new Scanner();
	}
	else
	{
		var parts = line.Split(',');
		scanner.Beacons.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
	}
}
scanners.Add(scanner);

var locatedScanners = LocateScanners();

var countBeacons = locatedScanners
	.SelectMany(scanner => scanner.GetBeaconsInWorld())
	.Distinct()
	.Count();

Console.WriteLine(countBeacons);

// Part 2
var maxDistance = (
	from sA in locatedScanners
	from sB in locatedScanners
	where sA != sB
	select
		Math.Abs(sA.Center.x - sB.Center.x) +
		Math.Abs(sA.Center.y - sB.Center.y) +
		Math.Abs(sA.Center.z - sB.Center.z))
	.Max();

Console.WriteLine(maxDistance);


HashSet<Scanner> LocateScanners()
{
	var locatedScanners = new HashSet<Scanner>();
	var queue = new Queue<Scanner>();

	locatedScanners.Add(scanners.First());

	queue.Enqueue(scanners.First());

	scanners.Remove(scanners.First());

	while (queue.Any())
	{
		var scannerA = queue.Dequeue();

		foreach (var scannerB in scanners.ToArray())
		{
			var maybeLocatedScanner = TryToLocate(scannerA, scannerB);

			if (maybeLocatedScanner != null)
			{

				locatedScanners.Add(maybeLocatedScanner);

				queue.Enqueue(maybeLocatedScanner);

				scanners.Remove(scannerB);
			}
		}
	}

	return locatedScanners;
}

Scanner? TryToLocate(Scanner scannerA, Scanner scannerB)
{
	var beaconsInA = scannerA.GetBeaconsInWorld().ToArray();

	foreach (var (beaconInA, beaconInB) in CommonBeacons(scannerA, scannerB))
	{
		var rotatedB = scannerB;

		for (var rotation = 0; rotation < 24; rotation++, rotatedB = rotatedB.Rotate())
		{
			var beaconInRotatedB = rotatedB.Transform(beaconInB);

			var locatedB = rotatedB.Translate((beaconInA.x - beaconInRotatedB.x, beaconInA.y - beaconInRotatedB.y, beaconInA.z - beaconInRotatedB.z));

			if (locatedB.GetBeaconsInWorld().Intersect(beaconsInA).Count() >= 12)
			{
				return locatedB;
			}
		}
	}

	return null;
}

IEnumerable<((int x, int y, int z) beaconInA, (int x, int y, int z) beaconInB)> CommonBeacons(Scanner scannerA, Scanner scannerB)
{
	foreach (var beaconInA in Take(scannerA.GetBeaconsInWorld()))
	{
		var absA = AbsCoordinates(scannerA.Translate((-beaconInA.x, -beaconInA.y, -beaconInA.z)))
			.ToHashSet();

		foreach (var beaconInB in Take(scannerB.GetBeaconsInWorld()))
		{
			var absB = AbsCoordinates(scannerB.Translate((-beaconInB.x, -beaconInB.y, -beaconInB.z)));

			if (absB.Count(d => absA.Contains(d)) >= 3 * 12)
			{
				yield return (beaconInA, beaconInB);
			}
		}
	}
}

IEnumerable<T> Take<T>(IEnumerable<T> ts) => ts.Take(ts.Count() - 11);

IEnumerable<int> AbsCoordinates(Scanner scanner) =>
	from coord in scanner.GetBeaconsInWorld()
	from v in new[] { coord.x, coord.y, coord.z }
	select Math.Abs(v);
