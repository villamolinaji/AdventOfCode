var input = "712643589";

var result = string.Join("", Solve(9, 100).Take(8));

Console.WriteLine(result);

// Part 2
var solve2Array = Solve(1000000, 10000000).Take(2).ToArray();

var result2 = solve2Array[0] * solve2Array[1];

Console.WriteLine(result2);


IEnumerable<long> Solve(int maxLabel, int rotate)
{
	var digits = input.Select(d => int.Parse(d.ToString())).ToArray();

	int[] next = Enumerable.Range(1, maxLabel + 1).ToArray();
	next[0] = -1;

	for (var i = 0; i < digits.Length; i++)
	{
		next[digits[i]] = digits[(i + 1) % digits.Length];
	}

	if (maxLabel > input.Length)
	{
		next[maxLabel] = next[digits[digits.Length - 1]];
		next[digits[digits.Length - 1]] = input.Length + 1;
	}

	var current = digits[0];

	for (var i = 0; i < rotate; i++)
	{
		var removed1 = next[current];
		var removed2 = next[removed1];
		var removed3 = next[removed2];

		next[current] = next[removed3];

		var destination = current;
		do destination = destination == 1
				? maxLabel
				: destination - 1;
		while (destination == removed1 || destination == removed2 || destination == removed3);

		next[removed3] = next[destination];
		next[destination] = removed1;
		current = next[current];
	}

	var cup = next[1];

	for (int i = 0; i < maxLabel - 1; i++)
	{
		yield return cup;
		cup = next[cup];
	}
}
