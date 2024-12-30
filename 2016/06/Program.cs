using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var correctedMessage = new StringBuilder();

var cols = lines[0].Length;

for (var c = 0; c < cols; c++)
{
	var line = lines.Select(l => l[c]);

	var characterMostFrequent = line
		.GroupBy(c => c)
		.OrderByDescending(g => g.Count())
		.First()
		.Key;

	correctedMessage.Append(characterMostFrequent);
}

Console.WriteLine(correctedMessage.ToString());

// Part 2
correctedMessage = new StringBuilder();

for (var c = 0; c < cols; c++)
{
	var line = lines.Select(l => l[c]);

	var characterMostFrequent = line
		.GroupBy(c => c)
		.OrderBy(g => g.Count())
		.First()
		.Key;

	correctedMessage.Append(characterMostFrequent);
}

Console.WriteLine(correctedMessage.ToString());