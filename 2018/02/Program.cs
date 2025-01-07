using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var countTwoLetters = 0;
var countThreeLetters = 0;

foreach (var line in lines)
{
	var letterCounts = line.GroupBy(c => c).Select(g => g.Count()).ToList();

	if (letterCounts.Contains(2))
	{
		countTwoLetters++;
	}

	if (letterCounts.Contains(3))
	{
		countThreeLetters++;
	}
}

var checksum = countTwoLetters * countThreeLetters;

Console.WriteLine(checksum);

// Part 2
var maxDiff = 0;
var commonLetters = string.Empty;

for (int i = 0; i < lines.Length - 1; i++)
{
	for (int j = i + 1; j < lines.Length; j++)
	{
		var line1 = lines[i];
		var line2 = lines[j];

		var common = new StringBuilder();

		for (int k = 0; k < line1.Length; k++)
		{
			if (line1[k] == line2[k])
			{
				common.Append(line1[k]);
			}
		}

		var commonString = common.ToString();
		if (commonString.Length > maxDiff)
		{
			maxDiff = commonString.Length;
			commonLetters = commonString;
		}
	}
}

Console.WriteLine(commonLetters);