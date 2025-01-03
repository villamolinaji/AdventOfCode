var input = File.ReadAllTextAsync("Input.txt").Result;

var programs = new char[16];
for (var i = 0; i < programs.Length; i++)
{
	programs[i] = (char)('a' + i);
}

var moves = input.Split(',');

foreach (var move in moves)
{
	switch (move[0])
	{
		case 's':
			var spin = int.Parse(move.Substring(1));

			programs = programs.Skip(programs.Length - spin).Concat(programs.Take(programs.Length - spin)).ToArray();

			break;
		case 'x':
			var positions = move.Substring(1).Split('/').Select(int.Parse).ToArray();

			var temp = programs[positions[0]];
			programs[positions[0]] = programs[positions[1]];
			programs[positions[1]] = temp;

			break;
		case 'p':
			var names = move.Substring(1).Split('/');
			var index1 = Array.IndexOf(programs, names[0][0]);
			var index2 = Array.IndexOf(programs, names[1][0]);

			programs[index1] = names[1][0];
			programs[index2] = names[0][0];

			break;
	}
}

Console.WriteLine(string.Join("", programs));

// Part 2
programs = new char[16];
for (var i = 0; i < programs.Length; i++)
{
	programs[i] = (char)('a' + i);
}

var seen = new HashSet<string>();

var cycle = 0;
var index = 0;

while (index < 1000000000)
{
	foreach (var move in moves)
	{
		switch (move[0])
		{
			case 's':
				var spin = int.Parse(move.Substring(1));
				programs = programs.Skip(programs.Length - spin).Concat(programs.Take(programs.Length - spin)).ToArray();
				break;
			case 'x':
				var positions = move.Substring(1).Split('/').Select(int.Parse).ToArray();
				var temp = programs[positions[0]];
				programs[positions[0]] = programs[positions[1]];
				programs[positions[1]] = temp;
				break;
			case 'p':
				var names = move.Substring(1).Split('/');
				var index1 = Array.IndexOf(programs, names[0][0]);
				var index2 = Array.IndexOf(programs, names[1][0]);
				programs[index1] = names[1][0];
				programs[index2] = names[0][0];
				break;
		}
	}

	if (cycle == 0)
	{
		var current = string.Join("", programs);

		if (seen.Contains(current))
		{
			cycle = index;

			index = 1000000000 - 1000000000 % cycle;
		}
		else
		{
			seen.Add(current);
		}
	}

	index++;
}

Console.WriteLine(string.Join("", programs));