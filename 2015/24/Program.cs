using System.Collections.Immutable;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var packages = lines.Select(int.Parse).ToArray();

var result = Solve(3);

Console.WriteLine(result);

// Part 2
result = Solve(4);

Console.WriteLine(result);


long Solve(int groups)
{
	for (var i = 0; i < packages.Length; i++)
	{
		var solutionPackages = PickPackages(i, 0, packages.Sum() / groups);

		if (solutionPackages.Any())
		{
			return solutionPackages.Select(p => p.Aggregate(1L, (m, x) => m * x)).Min();
		}
	}

	return 0;
}

IEnumerable<ImmutableList<int>> PickPackages(int count, int packageIndex, int packagesSumPerGroup)
{
	if (packagesSumPerGroup == 0)
	{
		yield return ImmutableList.Create<int>();
		yield break;
	}

	if (count < 0 ||
		packagesSumPerGroup < 0 ||
		packageIndex >= packages.Length)
	{
		yield break;
	}

	if (packages[packageIndex] <= packagesSumPerGroup)
	{
		foreach (var x in PickPackages(count - 1, packageIndex + 1, packagesSumPerGroup - packages[packageIndex]))
		{
			yield return x.Add(packages[packageIndex]);
		}
	}

	foreach (var x in PickPackages(count, packageIndex + 1, packagesSumPerGroup))
	{
		yield return x;
	}
}
