var lines = File.ReadAllLinesAsync("Input.txt").Result;

var orbits = lines.Select(x => x.Split(')')).ToDictionary(x => x[1], x => x[0]);

var totalOrbits = orbits.Keys.Sum(x => CountOrbits(orbits, x));

Console.WriteLine(totalOrbits);

// Part 2
var steps = 0;

var youPath = GetOrbitPath(orbits, "YOU");
var sanPath = GetOrbitPath(orbits, "SAN");

for (var i = 0; i < youPath.Count; i++)
{
	if (sanPath.Contains(youPath[i]))
	{
		steps = i + sanPath.IndexOf(youPath[i]);
		break;
	}
}

Console.WriteLine(steps);


int CountOrbits(Dictionary<string, string> orbits, string key)
{
	if (orbits.TryGetValue(key, out var value))
	{
		return 1 + CountOrbits(orbits, value);
	}

	return 0;
}

List<string> GetOrbitPath(Dictionary<string, string> orbits, string key)
{
	var path = new List<string>();

	while (orbits.TryGetValue(key, out var value))
	{
		path.Add(value);
		key = value;
	}

	return path;
}