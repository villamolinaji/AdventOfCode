using System.Collections.Immutable;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var caves = new Dictionary<string, List<string>>();

foreach (var line in lines)
{
	var parts = line.Split('-');

	if (!caves.ContainsKey(parts[0]))
	{
		caves[parts[0]] = new List<string>();
	}
	caves[parts[0]].Add(parts[1]);

	if (!caves.ContainsKey(parts[1]))
	{
		caves[parts[1]] = new List<string>();
	}
	caves[parts[1]].Add(parts[0]);
}

Console.WriteLine(GetPaths(false));

Console.WriteLine(GetPaths(true));


int GetPaths(bool isPart2)
{
	return PathCount("start", ImmutableHashSet.Create<string>("start"), false, isPart2);
}

int PathCount(string currentCave, ImmutableHashSet<string> visitedCaves, bool anySmallCaveWasVisitedTwice, bool isPart2)
{

	if (currentCave == "end")
	{
		return 1;
	}

	var pathCount = 0;

	foreach (var cave in caves[currentCave])
	{
		var isBigCave = cave.ToUpper() == cave;

		var seen = visitedCaves.Contains(cave);

		if (!seen ||
			isBigCave)
		{
			pathCount += PathCount(cave, visitedCaves.Add(cave), anySmallCaveWasVisitedTwice, isPart2);
		}
		else if (isPart2 &&
			!isBigCave &&
			cave != "start" &&
			!anySmallCaveWasVisitedTwice)
		{
			pathCount += PathCount(cave, visitedCaves, true, isPart2);
		}
	}

	return pathCount;
}
