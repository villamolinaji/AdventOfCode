var lines = File.ReadAllLinesAsync("Input.txt").Result;

var numbers = lines.Select(x => int.Parse(x)).ToList();

var target = 2020;

var result = 0;

for (int i = 0; i < numbers.Count - 1; i++)
{
	var a = numbers[i];

	var b = numbers.FirstOrDefault(b => a + b == target);

	if (b != 0)
	{
		result = a * b;
		break;

	}
}

Console.WriteLine(result);

// Part 2
bool endLoop = false;

for (int i = 0; i < numbers.Count - 1 && !endLoop; i++)
{
	for (int j = i + 1; j < numbers.Count - 2 && !endLoop; j++)
	{
		for (int k = j + 1; k < numbers.Count - 1; k++)
		{
			var a = numbers[i];
			var b = numbers[j];
			var c = numbers[k];

			if (a + b + c == target)
			{
				result = a * b * c;

				endLoop = true;

				break;
			}
		}
	}
}

Console.WriteLine(result);