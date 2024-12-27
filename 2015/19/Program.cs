var lines = File.ReadAllLinesAsync("Input.txt").Result;

string medicine = string.Empty;
var replacements = new List<(string from, string to)>();

foreach (var line in lines)
{
	if (line.Contains("=>"))
	{
		var parts = line.Split("=>");
		replacements.Add((parts[0].Trim(), parts[1].Trim()));
	}
	else if (!string.IsNullOrEmpty(line))
	{
		medicine = line;
	}
}

var molecules = new HashSet<string>();

foreach (var replacement in replacements)
{
	var index = 0;

	while (index < medicine.Length)
	{
		index = medicine.IndexOf(replacement.from, index);
		if (index == -1)
		{
			break;
		}

		var newMolecule = medicine.Substring(0, index) + replacement.to + medicine.Substring(index + replacement.from.Length);

		molecules.Add(newMolecule);

		index++;
	}
}

Console.WriteLine(molecules.Count);

// Part 2
Random random = new Random();
var currentMolecule = medicine;
var steps = 0;

while (currentMolecule != "e")
{
	var possibleReplacements = GetPossibleReplacements(currentMolecule).ToArray();

	if (possibleReplacements.Length == 0)
	{
		currentMolecule = medicine;
		steps = 0;

		continue;
	}

	var replacement = possibleReplacements[random.Next(possibleReplacements.Length)];

	currentMolecule = currentMolecule.Substring(0, replacement.from) + replacement.to + currentMolecule.Substring(replacement.from + replacement.length);

	steps++;
}

Console.WriteLine(steps);


IEnumerable<(int from, int length, string to)> GetPossibleReplacements(string molecule)
{
	var fromIndex = 0;

	while (fromIndex < molecule.Length)
	{
		foreach (var (to, from) in replacements)
		{
			if (fromIndex + from.Length <= molecule.Length)
			{
				var i = 0;

				while (i < from.Length)
				{
					if (molecule[fromIndex + i] != from[i])
					{
						break;
					}

					i++;
				}

				if (i == from.Length)
				{
					yield return (fromIndex, from.Length, to);
				}
			}
		}

		fromIndex++;
	}
}
