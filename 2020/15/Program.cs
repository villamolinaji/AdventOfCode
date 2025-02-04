var input = "10,16,6,0,1,17";

var numbers = input.Split(',').Select(int.Parse).ToList();

var lastSpoken = numbers.Last();

var spoken = new Dictionary<int, int>();

foreach (var number in numbers)
{
	spoken[number] = numbers.IndexOf(number) + 1;
}

for (var i = numbers.Count + 1; i <= 2020; i++)
{
	if (spoken.ContainsKey(lastSpoken))
	{
		var next = i - spoken[lastSpoken] - 1;
		spoken[lastSpoken] = i - 1;
		lastSpoken = next;
	}
	else
	{
		spoken[lastSpoken] = i - 1;
		lastSpoken = 0;
	}
}

Console.WriteLine(lastSpoken);

// Part 2
lastSpoken = numbers.Last();

spoken = new Dictionary<int, int>();

foreach (var number in numbers)
{
	spoken[number] = numbers.IndexOf(number) + 1;
}

for (var i = numbers.Count + 1; i <= 30000000; i++)
{
	if (spoken.ContainsKey(lastSpoken))
	{
		var next = i - spoken[lastSpoken] - 1;
		spoken[lastSpoken] = i - 1;
		lastSpoken = next;
	}
	else
	{
		spoken[lastSpoken] = i - 1;
		lastSpoken = 0;
	}
}

Console.WriteLine(lastSpoken);