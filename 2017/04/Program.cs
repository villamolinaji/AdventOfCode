var lines = File.ReadAllLinesAsync("Input.txt").Result;

var valids = 0;
var valids2 = 0;

foreach (var line in lines)
{
	if (IsValid(line))
	{
		valids++;
	}

	if (IsValid2(line))
	{
		valids2++;
	}
}

Console.WriteLine(valids);
Console.WriteLine(valids2);


bool IsValid(string line)
{
	var parts = line.Split(' ');
	var set = new HashSet<string>();

	foreach (var part in parts)
	{
		if (!set.Add(part))
		{
			return false;
		}
	}
	return true;
}

bool IsValid2(string line)
{
	var parts = line.Split(' ');

	for (int i = 0; i < parts.Length; i++)
	{
		for (int j = i + 1; j < parts.Length; j++)
		{
			if (IsAnagram(parts[i], parts[j]))
			{
				return false;
			}
		}
	}

	return true;
}

bool IsAnagram(string a, string b)
{
	if (a.Length != b.Length)
	{
		return false;
	}

	var aChars = a.ToCharArray();
	var bChars = b.ToCharArray();

	Array.Sort(aChars);
	Array.Sort(bChars);

	for (int i = 0; i < aChars.Length; i++)
	{
		if (aChars[i] != bChars[i])
		{
			return false;
		}
	}

	return true;
}