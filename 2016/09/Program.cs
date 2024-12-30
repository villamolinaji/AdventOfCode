using System.Text;

var line = File.ReadAllTextAsync("Input.txt").Result;

var decompressedLine = Decompress(line);

Console.WriteLine(decompressedLine.Length);

// Part 2
long result = Decompress2(line);

Console.WriteLine(result);


string Decompress(string line)
{
	var decompressed = new StringBuilder();

	int i = 0;
	while (i < line.Length)
	{
		if (line[i] == '(')
		{
			var marker = new StringBuilder();

			for (i++; line[i] != ')'; i++)
			{
				marker.Append(line[i]);
			}
			var parts = marker.ToString().Split('x');
			var length = int.Parse(parts[0]);
			var repeat = int.Parse(parts[1]);
			var toRepeat = new StringBuilder();

			for (int j = 0; j < length; j++)
			{
				i++;
				toRepeat.Append(line[i]);
			}

			for (int j = 0; j < repeat; j++)
			{
				decompressed.Append(toRepeat);
			}
		}
		else
		{
			decompressed.Append(line[i]);
		}

		i++;
	}

	return decompressed.ToString();
}

long Decompress2(string line)
{
	long result = 0;

	int i = 0;

	while (i < line.Length)
	{
		if (line[i] == '(')
		{
			int markerEnd = line.IndexOf(')', i);
			string marker = line.Substring(i + 1, markerEnd - i - 1);
			var parts = marker.Split('x');
			int length = int.Parse(parts[0]);
			int repeat = int.Parse(parts[1]);

			var decompressLenght = Decompress2(line.Substring(markerEnd + 1, length));

			result += decompressLenght * repeat;

			i = markerEnd + length;
		}
		else
		{
			result++;
		}

		i++;
	}

	return result;
}