var lines = File.ReadAllLinesAsync("Input.txt").Result;

var polymeterTemplate = lines[0];

var rules = lines[2..]
	.Select(line => line.Split(" -> "))
	.ToDictionary(parts => parts[0], parts => parts[1]);

var result = Iterate(10);

Console.WriteLine(result);

// Part2
result = Iterate(40);

Console.WriteLine(result);


long Iterate(int steps)
{
	var moleculeCount = new Dictionary<string, long>();

	foreach (var i in Enumerable.Range(0, polymeterTemplate.Length - 1))
	{
		var ab = polymeterTemplate.Substring(i, 2);
		moleculeCount[ab] = moleculeCount.GetValueOrDefault(ab) + 1;
	}

	for (var i = 0; i < steps; i++)
	{
		var updated = new Dictionary<string, long>();

		foreach (var (molecule, count) in moleculeCount)
		{
			var (a, n, b) = (molecule[0], rules[molecule], molecule[1]);

			updated[$"{a}{n}"] = updated.GetValueOrDefault($"{a}{n}") + count;
			updated[$"{n}{b}"] = updated.GetValueOrDefault($"{n}{b}") + count;
		}

		moleculeCount = updated;
	}

	var elementCounts = new Dictionary<char, long>();

	foreach (var (molecule, count) in moleculeCount)
	{
		var a = molecule[0];

		elementCounts[a] = elementCounts.GetValueOrDefault(a) + count;
	}

	elementCounts[polymeterTemplate.Last()]++;

	return elementCounts.Values.Max() - elementCounts.Values.Min();
}