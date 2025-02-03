using System.Collections.Immutable;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var joltages = lines.Select(l => int.Parse(l)).ToList();

var countDifference1 = 0;
var countDifference3 = 0;

var higher = joltages.Max() + 3;

for (var i = 0; i < higher; i++)
{
	if (joltages.Contains(i + 1))
	{
		countDifference1++;
	}
	else if (joltages.Contains(i + 3))
	{
		countDifference3++;

		i += 2;
	}
}

countDifference3++;

var result = countDifference1 * countDifference3;

Console.WriteLine(result);

// Part 2
joltages.Sort();
joltages.Add(higher);
var joltages2 = ImmutableList.Create(0).AddRange(joltages);

var a = 1L;
var b = 0L;
var c = 0L;

for (var i = joltages2.Count - 2; i >= 0; i--)
{
	var s = (i + 1 < joltages2.Count && joltages2[i + 1] - joltages2[i] <= 3 ? a : 0) +
		(i + 2 < joltages2.Count && joltages2[i + 2] - joltages2[i] <= 3 ? b : 0) +
		(i + 3 < joltages2.Count && joltages2[i + 3] - joltages2[i] <= 3 ? c : 0);

	//c = b;
	//b = a;
	//a = s;
	(a, b, c) = (s, a, b);
}

Console.WriteLine(a);