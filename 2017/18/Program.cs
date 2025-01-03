using _18;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var registers = new Dictionary<string, long>();

long frequencyPlayed = 0;
long index = 0;
var endWhile = false;

while (!endWhile)
{
	var line = lines[index];
	var parts = line.Split(' ');

	if (!registers.ContainsKey(parts[1]))
	{
		registers.Add(parts[1], 0);
	}

	switch (parts[0])
	{
		case "snd":
			frequencyPlayed = registers[parts[1]];
			break;
		case "set":
			registers[parts[1]] = int.TryParse(parts[2], out var value) ? value : registers[parts[2]];
			break;
		case "add":
			registers[parts[1]] = registers[parts[1]] + (int.TryParse(parts[2], out value) ? value : registers[parts[2]]);
			break;
		case "mul":
			registers[parts[1]] = registers[parts[1]] * (int.TryParse(parts[2], out value) ? value : registers[parts[2]]);
			break;
		case "mod":
			registers[parts[1]] = registers[parts[1]] % (int.TryParse(parts[2], out value) ? value : registers[parts[2]]);
			break;
		case "rcv":
			if (registers[parts[1]] != 0)
			{
				endWhile = true;
			}
			break;
		case "jgz":
			if (registers[parts[1]] > 0)
			{
				var offset = int.TryParse(parts[2], out value) ? value : registers[parts[2]];
				long newIndex = index + offset - 1;

				if (newIndex < 0 ||
					newIndex >= lines.Length)
				{
					endWhile = true;
				}
				index = newIndex;
			}
			break;
	}

	index++;
}

Console.WriteLine(frequencyPlayed);

// Part 2
var q0 = new Queue<long>();
var q1 = new Queue<long>();

var timesSentProgram1 = Enumerable
	.Zip(
		new Machine(0, q0, q1).Execute(lines),
		new Machine(1, q1, q0).Execute(lines),
		(state0, state1) => (state0: state0, state1: state1))
	.First(x => !x.state0.running && !x.state1.running)
	.state1.valueSent;

Console.WriteLine(timesSentProgram1);

