var lines = File.ReadAllLinesAsync("Input.txt").Result;

var a = 0;
var b = 0;
var c = 0;
var d = 0;

Evaluate();

Console.WriteLine(a);

// Part 2
a = 0;
b = 0;
c = 1;
d = 0;

Evaluate();

Console.WriteLine(a);


void Evaluate()
{
	int index = 0;
	while (index >= 0 && index < lines.Length)
	{
		var line = lines[index];

		var parts = line.Split(' ');

		if (line.StartsWith("cpy"))
		{
			var value = 0;
			if (int.TryParse(parts[1], out value))
			{
				if (parts[2] == "a")
				{
					a = value;
				}
				else if (parts[2] == "b")
				{
					b = value;
				}
				else if (parts[2] == "c")
				{
					c = value;
				}
				else if (parts[2] == "d")
				{
					d = value;
				}
			}
			else
			{
				if (parts[1] == "a")
				{
					value = a;
				}
				else if (parts[1] == "b")
				{
					value = b;
				}
				else if (parts[1] == "c")
				{
					value = c;
				}
				else if (parts[1] == "d")
				{
					value = d;
				}

				if (parts[2] == "a")
				{
					a = value;
				}
				else if (parts[2] == "b")
				{
					b = value;
				}
				else if (parts[2] == "c")
				{
					c = value;
				}
				else if (parts[2] == "d")
				{
					d = value;
				}
			}
		}
		else if (line.StartsWith("inc"))
		{
			if (parts[1] == "a")
			{
				a++;
			}
			else if (parts[1] == "b")
			{
				b++;
			}
			else if (parts[1] == "c")
			{
				c++;
			}
			else if (parts[1] == "d")
			{
				d++;
			}
		}
		else if (line.StartsWith("dec"))
		{
			if (parts[1] == "a")
			{
				a--;
			}
			else if (parts[1] == "b")
			{
				b--;
			}
			else if (parts[1] == "c")
			{
				c--;
			}
			else if (parts[1] == "d")
			{
				d--;
			}
		}
		else if (line.StartsWith("jnz"))
		{
			var value = 0;

			if (!int.TryParse(parts[1], out value))
			{
				if (parts[1] == "a")
				{
					value = a;
				}
				else if (parts[1] == "b")
				{
					value = b;
				}
				else if (parts[1] == "c")
				{
					value = c;
				}
				else if (parts[1] == "d")
				{
					value = d;
				}
			}

			if (value != 0)
			{
				var offset = int.Parse(parts[2]);
				index += offset - 1;
			}
		}

		index++;
	}
}