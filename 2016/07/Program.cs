using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var countSupportTls = lines.Count(line => SupportTls(line));

Console.WriteLine(countSupportTls);

// Part 2
var countSupportSsl = lines.Count(line => SupportSsl(line));

Console.WriteLine(countSupportSsl);


bool SupportTls(string line)
{
	var inBrackets = false;
	var hasAbba = false;
	var hasAbbaInBrackets = false;

	for (int i = 0; i < line.Length - 3; i++)
	{
		if (line[i] == '[')
		{
			inBrackets = true;
		}
		else if (line[i] == ']')
		{
			inBrackets = false;
		}
		else if (line[i] == line[i + 3] &&
			line[i + 1] == line[i + 2] &&
			line[i] != line[i + 1])
		{
			if (inBrackets)
			{
				hasAbbaInBrackets = true;
			}
			else
			{
				hasAbba = true;
			}
		}
	}

	return hasAbba && !hasAbbaInBrackets;
}

bool SupportSsl(string line)
{
	var split = SplitBrackets(line);

	var noBracketsLine = split.Where(s => !s.inBrackets).Select(s => s.line).ToList();
	var inBracketsLine = split.Where(s => s.inBrackets).Select(s => s.line).ToList();

	for (int i = 0; i < noBracketsLine.Count; i++)
	{
		var noBracketsLineAux = noBracketsLine[i];

		for (int j = 0; j < noBracketsLineAux.Length - 2; j++)
		{
			if (noBracketsLineAux[j] == noBracketsLineAux[j + 2] &&
				noBracketsLineAux[j] != noBracketsLineAux[j + 1])
			{
				var bab = $"{noBracketsLineAux[j + 1]}{noBracketsLineAux[j]}{noBracketsLineAux[j + 1]}";

				if (inBracketsLine.Any(s => s.Contains(bab)))
				{
					return true;
				}
			}
		}
	}

	return false;
}

List<(string line, bool inBrackets)> SplitBrackets(string line)
{
	var result = new List<(string line, bool inBrackets)>();
	var inBrackets = false;
	var currentLine = new StringBuilder();

	foreach (var c in line)
	{
		if (c == '[')
		{
			result.Add((currentLine.ToString(), inBrackets));
			currentLine.Clear();
			inBrackets = true;
		}
		else if (c == ']')
		{
			result.Add((currentLine.ToString(), inBrackets));
			currentLine.Clear();
			inBrackets = false;
		}
		else
		{
			currentLine.Append(c);
		}
	}

	result.Add((currentLine.ToString(), inBrackets));

	return result;
}