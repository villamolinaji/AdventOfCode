var input = File.ReadAllTextAsync("Input.txt").Result;

var banks = input.Split('\t').Select(int.Parse).ToList();

var visited = new HashSet<string>();

while (true)
{
	var key = string.Join(",", banks);
	if (visited.Contains(key))
	{
		break;
	}
	visited.Add(key);

	var max = banks.Max();

	var index = banks.IndexOf(max);

	banks[index] = 0;

	for (var i = 0; i < max; i++)
	{
		index = (index + 1) % banks.Count;
		banks[index]++;
	}
}

Console.WriteLine(visited.Count);

// Part 2
banks = input.Split('\t').Select(int.Parse).ToList();

visited = new HashSet<string>();
var sameState = string.Empty;

var steps = 0;

while (true)
{
	var key = string.Join(",", banks);
	if (visited.Contains(key))
	{
		if (sameState == key)
		{
			break;
		}
		else if (string.IsNullOrEmpty(sameState))
		{
			sameState = key;
			steps = 0;
		}
	}
	visited.Add(key);

	var max = banks.Max();

	var index = banks.IndexOf(max);

	banks[index] = 0;

	for (var i = 0; i < max; i++)
	{
		index = (index + 1) % banks.Count;
		banks[index]++;
	}

	steps++;
}

Console.WriteLine(steps);