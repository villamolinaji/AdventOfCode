var input = File.ReadAllTextAsync("Input.txt").Result;



Console.WriteLine(CalculateFished(80));

//Part 2
Console.WriteLine(CalculateFished(256));


long CalculateFished(int days)
{
	var fishCount = new long[9];

	foreach (var fish in input.Split(',').Select(x => int.Parse(x)))
	{
		fishCount[fish]++;
	}

	for (var i = 0; i < days; i++)
	{
		fishCount[(i + 7) % 9] += fishCount[i % 9];
	}

	return fishCount.Sum();
}