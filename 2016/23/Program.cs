var lines = File.ReadAllLinesAsync("Input.txt").Result;

var a = 7;
var b = 0;
var c = 0;
var d = 0;

Evaluate();

Console.WriteLine(a);

// Part 2
lines = File.ReadAllLinesAsync("Input.txt").Result;
lines[5] = "cpy c a";
lines[6] = "mul d a";
lines[7] = "cpy 0 d";
lines[8] = "cpy 0 c";

a = 12;
b = 0;
c = 0;
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
		else if (line.StartsWith("mul"))
		{
			var value = 0;

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
				a *= value;
			}
			else if (parts[2] == "b")
			{
				b *= value;
			}
			else if (parts[2] == "c")
			{
				c *= value;
			}
			else if (parts[2] == "d")
			{
				d *= value;
			}
		}
		else if (line.StartsWith("jnz"))
		{
			var value = 0;
			var offset = 0;

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
				if (!int.TryParse(parts[2], out offset))
				{
					if (parts[2] == "a")
					{
						offset = a;
					}
					else if (parts[2] == "b")
					{
						offset = b;
					}
					else if (parts[2] == "c")
					{
						offset = c;
					}
					else if (parts[2] == "d")
					{
						offset = d;
					}
				}

				index += offset - 1;
			}
		}
		else if (line.StartsWith("tgl"))
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

			var targetIndex = index + value;
			if (targetIndex >= 0 &&
				targetIndex < lines.Length)
			{
				var targetLine = lines[targetIndex];
				var targetParts = targetLine.Split(' ');

				if (targetParts.Length == 2)
				{
					if (targetParts[0] == "inc")
					{
						lines[targetIndex] = "dec " + targetParts[1];
					}
					else
					{
						lines[targetIndex] = "inc " + targetParts[1];
					}
				}
				else if (targetParts.Length == 3)
				{
					if (targetParts[0] == "jnz")
					{
						lines[targetIndex] = "cpy " + targetParts[1] + " " + targetParts[2];
					}
					else
					{
						lines[targetIndex] = "jnz " + targetParts[1] + " " + targetParts[2];
					}
				}
			}
		}

		index++;
	}
}