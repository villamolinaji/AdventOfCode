var lines = File.ReadAllLinesAsync("Input.txt").Result;

var checksum = 0;

foreach (var line in lines)
{
	var numbers = line.Split('\t', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

	checksum += numbers.Max() - numbers.Min();
}

Console.WriteLine(checksum);

// Part 2
var sum = 0;
foreach (var line in lines)
{
	var numbers = line.Split('\t', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

	numbers.Sort();

	for (var i = 0; i < numbers.Count; i++)
	{
		for (var j = i + 1; j < numbers.Count; j++)
		{
			if (numbers[j] % numbers[i] == 0)
			{
				sum += numbers[j] / numbers[i];
			}
		}
	}
}

Console.WriteLine(sum);