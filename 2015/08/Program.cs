using System.Text;

var lines = File.ReadAllLinesAsync("input.txt").Result;

var total = 0;
foreach (var line in lines)
{
	var code = line.Length;
	var memory = GetMemoryLength(line);

	total += code - memory;
}

Console.WriteLine(total);

// Part 2
total = 0;
foreach (var line in lines)
{
	var code = line.Length;
	var encodedLine = EncodeLine(line);

	total += (encodedLine.Length + 2) - code;
}

Console.WriteLine(total);


int GetMemoryLength(string line)
{
	var memory = 0;
	var i = 0;
	while (i < line.Length - 2)
	{
		if (line[i] == '\\')
		{
			if (line[i + 1] == '\\' ||
				line[i + 1] == '"')
			{
				memory++;
				i++;
			}
			else if (line[i + 1] == 'x')
			{
				memory++;
				i += 3;
			}
		}
		else
		{
			memory++;
		}

		i++;
	}

	return memory - 1;
}

string EncodeLine(string line)
{
	var sb = new StringBuilder();

	var i = 1;

	sb.Append("\\\"");

	while (i < line.Length - 1)
	{
		if (line[i] == '"')
		{
			sb.Append("\\\"");
		}
		else if (line[i] == '\\')
		{
			if (line[i + 1] == '"')
			{
				sb.Append("\\\\\\\"");

				i++;
			}
			else if (line[i + 1] == 'x')
			{
				sb.Append("\\\\x");

				i++;
			}
			else
			{
				sb.Append("\\\\\\\\");

				i++;
			}
		}
		else
		{
			sb.Append(line[i]);
		}

		i++;
	}

	sb.Append("\\\"");

	return sb.ToString();
}