var input = File.ReadAllTextAsync("Input.txt").Result;

var result = 0;

for (var i = 0; i < input.Length; i++)
{
	if (input[i] == input[(i + 1) % input.Length])
	{
		result += int.Parse(input[i].ToString());
	}
}

Console.WriteLine(result);

// Part 2
result = 0;

for (var i = 0; i < input.Length; i++)
{
	var nextIndex = (i + input.Length / 2) % input.Length;

	if (input[i] == input[nextIndex])
	{
		result += int.Parse(input[i].ToString());
	}
}

Console.WriteLine(result);
