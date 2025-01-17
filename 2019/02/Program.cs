var input = File.ReadAllTextAsync("Input.txt").Result;

var positions = input.Split(',').Select(int.Parse).ToList();

positions[1] = 12;
positions[2] = 2;

RunProgram();

Console.WriteLine(positions[0]);

// Part 2
for (int i = 0; i < 100; i++)
{
	for (int j = 0; j < 100; j++)
	{
		positions = input.Split(',').Select(int.Parse).ToList();

		positions[1] = i;
		positions[2] = j;

		RunProgram();

		if (positions[0] == 19690720)
		{
			Console.WriteLine(100 * i + j);
			return;
		}
	}
}


void RunProgram()
{
	var index = 0;

	while (index < positions.Count)
	{
		var opcode = positions[index];

		if (opcode == 99)
		{
			break;
		}

		var value1 = positions[positions[index + 1]];
		var value2 = positions[positions[index + 2]];

		if (opcode == 1)
		{
			positions[positions[index + 3]] = value1 + value2;
		}
		else if (opcode == 2)
		{
			positions[positions[index + 3]] = value1 * value2;
		}

		index += 4;
	}
}