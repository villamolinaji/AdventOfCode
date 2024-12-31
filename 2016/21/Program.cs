var startPassword = "abcdefgh";

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var password = startPassword;

foreach (var line in lines)
{
	var parts = line.Split(' ');

	if (parts[0] == "swap")
	{
		if (parts[1] == "position")
		{
			var x = int.Parse(parts[2]);
			var y = int.Parse(parts[5]);
			var xChar = password[x];
			var yChar = password[y];

			password = password.Remove(x, 1).Insert(x, yChar.ToString());
			password = password.Remove(y, 1).Insert(y, xChar.ToString());
		}
		else
		{
			var x = parts[2][0];
			var y = parts[5][0];
			var xIndex = password.IndexOf(x);
			var yIndex = password.IndexOf(y);

			password = password.Remove(xIndex, 1).Insert(xIndex, y.ToString());
			password = password.Remove(yIndex, 1).Insert(yIndex, x.ToString());
		}
	}
	else if (parts[0] == "rotate")
	{
		if (parts[1] == "based")
		{
			var x = parts[6][0];
			var xIndex = password.IndexOf(x);
			var rotations = 1 + xIndex + (xIndex >= 4 ? 1 : 0);

			password = Rotate(password, rotations);
		}
		else
		{
			var x = int.Parse(parts[2]);

			if (parts[1] == "left")
			{
				for (var i = 0; i < x; i++)
				{
					password = password[1..] + password[0];
				}
			}
			else
			{
				for (var i = 0; i < x; i++)
				{
					password = password[^1] + password[..^1];
				}
			}
		}
	}
	else if (parts[0] == "reverse")
	{
		var x = int.Parse(parts[2]);
		var y = int.Parse(parts[4]);

		password = password[..x] + string.Concat(password[x..(y + 1)].Reverse()) + password[(y + 1)..];
	}
	else if (parts[0] == "move")
	{
		var x = int.Parse(parts[2]);
		var y = int.Parse(parts[5]);

		var xChar = password[x];
		password = password.Remove(x, 1);
		password = password.Insert(y, xChar.ToString());
	}
}

Console.WriteLine(password);

// Part 2
var endPassword = "fbgdceah";

password = endPassword;

for (int i = lines.Length - 1; i >= 0; i--)
{
	var line = lines[i];
	var parts = line.Split(' ');
	if (parts[0] == "swap")
	{
		if (parts[1] == "position")
		{
			var x = int.Parse(parts[2]);
			var y = int.Parse(parts[5]);
			var xChar = password[x];
			var yChar = password[y];
			password = password.Remove(x, 1).Insert(x, yChar.ToString());
			password = password.Remove(y, 1).Insert(y, xChar.ToString());
		}
		else
		{
			var x = parts[2][0];
			var y = parts[5][0];
			var xIndex = password.IndexOf(x);
			var yIndex = password.IndexOf(y);
			password = password.Remove(xIndex, 1).Insert(xIndex, y.ToString());
			password = password.Remove(yIndex, 1).Insert(yIndex, x.ToString());
		}
	}
	else if (parts[0] == "rotate")
	{
		if (parts[1] == "based")
		{
			var x = parts[6][0];
			int index = password.IndexOf(x);
			int rotations = 0;

			for (int j = 0; j < password.Length; j++)
			{
				if ((j + 1 + j + (j >= 4 ? 1 : 0)) % password.Length == index)
				{
					rotations = j - index;
					break;
				}
			}

			password = Rotate(password, rotations);
		}
		else
		{
			var x = int.Parse(parts[2]);
			if (parts[1] == "right")
			{
				for (var j = 0; j < x; j++)
				{
					password = password[1..] + password[0];
				}
			}
			else
			{
				for (var j = 0; j < x; j++)
				{
					password = password[^1] + password[..^1];
				}
			}
		}
	}
	else if (parts[0] == "reverse")
	{
		var x = int.Parse(parts[2]);
		var y = int.Parse(parts[4]);
		password = password[..x] + string.Concat(password[x..(y + 1)].Reverse()) + password[(y + 1)..];
	}
	else if (parts[0] == "move")
	{
		var y = int.Parse(parts[2]);
		var x = int.Parse(parts[5]);

		var xChar = password[x];
		password = password.Remove(x, 1);
		password = password.Insert(y, xChar.ToString());
	}
}

Console.WriteLine(password);


string Rotate(string password, int steps)
{
	int len = password.Length;
	steps = (steps % len + len) % len;

	return string.Join("", password.Skip(len - steps).Concat(password.Take(len - steps)));
}
