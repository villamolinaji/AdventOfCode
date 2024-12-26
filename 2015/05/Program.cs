var lines = File.ReadAllLinesAsync("Input.txt").Result;

int countValid = 0;
int countValidPart2 = 0;

foreach (var line in lines)
{
	if (IsValid(line))
	{
		countValid++;
	}

	if (IsValidPart2(line))
	{
		countValidPart2++;
	}
}

Console.WriteLine(countValid);
Console.WriteLine(countValidPart2);


bool IsValid(string line)
{
	if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
	{
		return false;
	}

	var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
	var vowelCount = line.Count(l => vowels.Contains(l));

	if (vowelCount < 3)
	{
		return false;
	}

	bool hasDouble = false;
	for (int i = 0; i < line.Length - 1; i++)
	{
		if (line[i] == line[i + 1])
		{
			hasDouble = true;
			break;
		}
	}

	return hasDouble;
}

bool IsValidPart2(string line)
{
	bool hasPair = false;
	for (int i = 0; i < line.Length - 3; i++)
	{
		var pair = line.Substring(i, 2);

		if (line.IndexOf(pair, i + 2) != -1)
		{
			hasPair = true;
		}
	}

	bool hasRepeat = false;
	for (int i = 0; i < line.Length - 2; i++)
	{
		if (line[i] == line[i + 2])
		{
			hasRepeat = true;
		}
	}

	return hasPair && hasRepeat;
}