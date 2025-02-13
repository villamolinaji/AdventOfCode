var lines = File.ReadAllLinesAsync("Input.txt").Result;

var outputs = new List<string>();

foreach (var line in lines)
{
	var parts = line.Split('|');

	outputs.AddRange(parts[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)));
}

var segmentCounts = new[] { "cf", "acf", "bcdf", "abcdefg" }.Select(x => x.Length).ToHashSet();

var result = outputs.Count(o => segmentCounts.Contains(o.Length));

Console.WriteLine(result);

// Part 2
var result2 = 0;

foreach (var line in lines)
{
	var parts = line.Split('|');

	var patterns = parts[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(x => x.ToHashSet()).ToArray();

	var digits = new HashSet<char>[10];

	digits[1] = patterns.Single(pattern => pattern.Count == "cf".Length);
	digits[4] = patterns.Single(pattern => pattern.Count == "bcdf".Length);

	var lookup = (int segmentCount, int commonWithOne, int commonWithFour) =>
		patterns.Single(pattern =>
			pattern.Count == segmentCount &&
			pattern.Intersect(digits[1]).Count() == commonWithOne &&
			pattern.Intersect(digits[4]).Count() == commonWithFour
		);

	digits[0] = lookup(6, 2, 3);
	digits[2] = lookup(5, 1, 2);
	digits[3] = lookup(5, 2, 3);
	digits[5] = lookup(5, 1, 3);
	digits[6] = lookup(6, 1, 3);
	digits[7] = lookup(3, 2, 2);
	digits[8] = lookup(7, 2, 4);
	digits[9] = lookup(6, 2, 4);

	var decode = (string v) =>
		Enumerable.Range(0, 10).Single(i => digits[i].SetEquals(v));

	result2 += parts[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Aggregate(0, (n, digit) => n * 10 + decode(digit));
}

Console.WriteLine(result2);
