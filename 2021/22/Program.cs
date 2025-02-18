using _22;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var steps = new List<(bool isOn, Region region)>();

foreach (var line in lines)
{
	var parts = line.Split(' ');

	var parts2 = parts[1].Split(',');

	var parts2X = parts2[0].Replace("x=", "").Split("..");
	var parts2Y = parts2[1].Replace("y=", "").Split("..");
	var parts2Z = parts2[2].Replace("z=", "").Split("..");

	steps.Add((
		parts[0] == "on",
		new Region(
			new Segment(int.Parse(parts2X[0]), int.Parse(parts2X[1])),
			new Segment(int.Parse(parts2Y[0]), int.Parse(parts2Y[1])),
			new Segment(int.Parse(parts2Z[0]), int.Parse(parts2Z[1])))));
}

var cubesOn = GetCubesOn(50);

Console.WriteLine(cubesOn);

// Part 2
cubesOn = GetCubesOn(int.MaxValue);

Console.WriteLine(cubesOn);


long GetCubesOn(int range)
{
	return GetCubesOnRecursive(
		steps.Count - 1,
		new Region(new Segment(-range, range), new Segment(-range, range), new Segment(-range, range)));
}

long GetCubesOnRecursive(int indexSteps, Region region)
{
	if (region.IsEmpty || indexSteps < 0)
	{
		return 0;
	}
	else
	{
		var intersection = region.Intersect(steps[indexSteps].region);

		var activeInRegion = GetCubesOnRecursive(indexSteps - 1, region);

		var activeInIntersection = GetCubesOnRecursive(indexSteps - 1, intersection);

		var activeOutsideIntersection = activeInRegion - activeInIntersection;

		return steps[indexSteps].isOn
			? activeOutsideIntersection + intersection.Volume
			: activeOutsideIntersection;
	}
}
