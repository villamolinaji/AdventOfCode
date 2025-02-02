var lines = File.ReadAllLinesAsync("Input.txt").Result;

var index = 0;
var accumulator = 0;

(accumulator, index) = Run(lines);

Console.WriteLine(accumulator);

// Part 2
for (int i = 0; i < lines.Length; i++)
{
	var line = lines[i];
	var parts = line.Split(" ");
	var operation = parts[0];

	if (operation == "acc")
	{
		continue;
	}

	var instructions = lines.ToArray();

	if (operation == "nop")
	{
		instructions[i] = line.Replace("nop", "jmp");
	}
	else
	{
		instructions[i] = line.Replace("jmp", "nop");
	}

	(accumulator, index) = Run(instructions);

	if (index == instructions.Length)
	{
		Console.WriteLine(accumulator);

		break;
	}
}


(int accumulator, int index) Run(string[] instructions)
{
	var index = 0;
	var accumulator = 0;

	var visitedInstructionIndex = new HashSet<int>();

	while (index < instructions.Length)
	{
		if (visitedInstructionIndex.Contains(index))
		{
			break;
		}
		visitedInstructionIndex.Add(index);

		var instruction = instructions[index];
		var parts = instruction.Split(' ');
		var operation = parts[0];
		var value = int.Parse(parts[1]);

		switch (operation)
		{
			case "acc":
				accumulator += value;
				index++;

				break;
			case "jmp":
				index += value;

				break;
			case "nop":
				index++;

				break;
		}
	}

	return (accumulator, index);
}