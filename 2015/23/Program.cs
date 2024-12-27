var lines = File.ReadAllLinesAsync("Input.txt").Result;

var a = 0;
var b = 0;

RunProgram();

Console.WriteLine(b);

// Part 2
a = 1;
b = 0;

RunProgram();

Console.WriteLine(b);


void RunProgram()
{
	int i = 0;

	while (i < lines.Length)
	{
		var line = lines[i];

		if (line.StartsWith("inc"))
		{
			var parts = line.Split(" ");
			if (parts[1] == "a")
			{
				a++;
			}
			else
			{
				b++;
			}
		}
		else if (line.StartsWith("tpl"))
		{
			var parts = line.Split(" ");
			if (parts[1] == "a")
			{
				a *= 3;
			}
			else
			{
				b *= 3;
			}
		}
		else if (line.StartsWith("hlf"))
		{
			var parts = line.Split(" ");
			if (parts[1] == "a")
			{
				a /= 2;
			}
			else
			{
				b /= 2;
			}
		}
		else if (line.StartsWith("jmp"))
		{
			var parts = line.Split(" ");

			i += int.Parse(parts[1]);

			continue;
		}
		else if (line.StartsWith("jie"))
		{
			var parts = line.Split(" ");

			if (parts[1].Contains("a"))
			{
				if (a % 2 == 0)
				{
					i += int.Parse(parts[2]);
					continue;
				}
			}
			else
			{
				if (b % 2 == 0)
				{
					i += int.Parse(parts[2]);
					continue;
				}
			}
		}
		else if (line.StartsWith("jio"))
		{
			var parts = line.Split(" ");

			if (parts[1].Contains("a"))
			{
				if (a == 1)
				{
					i += int.Parse(parts[2]);
					continue;
				}
			}
			else
			{
				if (b == 1)
				{
					i += int.Parse(parts[2]);
					continue;
				}
			}
		}

		i++;
	}
}
