using System.Text.RegularExpressions;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var memory = new Dictionary<long, long>();
var orMask = 0L;
var andMask = 0xffffffffffffffL;

foreach (var line in lines)
{
	if (line.StartsWith("mask"))
	{
		var mask = line.Split(" = ")[1];

		andMask = Convert.ToInt64(mask.Replace("X", "1"), 2);
		orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
	}
	else
	{
		var num = Regex.Matches(line, "\\d+").Select(match => long.Parse(match.Value)).ToArray();

		memory[num[0]] = num[1] & andMask | orMask;
	}
}

var result = memory.Values.Sum();

Console.WriteLine(result);

// Part 2
memory = new Dictionary<long, long>();

var mask2 = string.Empty;

foreach (var line in lines)
{
	if (line.StartsWith("mask"))
	{
		mask2 = line.Split(" = ")[1];
	}
	else
	{
		var num = Regex.Matches(line, "\\d+").Select(match => long.Parse(match.Value)).ToArray();

		var (memoryNumber, value) = (num[0], num[1]);

		foreach (var address in GetCombinations(memoryNumber, mask2, 35))
		{
			memory[address] = value;
		}
	}
}

result = memory.Values.Sum();

Console.WriteLine(result);


IEnumerable<long> GetCombinations(long memoryNumber, string mask, int i)
{
	if (i == -1)
	{
		yield return 0;
	}
	else
	{
		foreach (var prefix in GetCombinations(memoryNumber, mask, i - 1))
		{
			if (mask[i] == '0')
			{
				yield return (prefix << 1) + ((memoryNumber >> 35 - i) & 1);
			}
			else if (mask[i] == '1')
			{
				yield return (prefix << 1) + 1;
			}
			else
			{
				yield return (prefix << 1);

				yield return (prefix << 1) + 1;
			}
		}
	}
}