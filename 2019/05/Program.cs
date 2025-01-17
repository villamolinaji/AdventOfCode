var input = File.ReadAllTextAsync("Input.txt").Result;

var positions = input.Split(',').Select(int.Parse).ToList();

RunProgram(1);

// Part 2
positions = input.Split(',').Select(int.Parse).ToList();

RunProgram(5);

void RunProgram(int input)
{
	var index = 0;

	while (index < positions.Count)
	{
		var opcode = positions[index];

		var de = opcode % 100;
		var c = (opcode / 100) % 10;
		var b = (opcode / 1000) % 10;

		var value1 = 0;
		var value2 = 0;

		if (de == 99)
		{
			break;
		}

		switch (de)
		{
			case 1:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				positions[positions[index + 3]] = value1 + value2;

				index += 4;

				break;

			case 2:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				positions[positions[index + 3]] = value1 * value2;
				index += 4;
				break;
			case 3:

				positions[positions[index + 1]] = input;
				index += 2;
				break;
			case 4:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];

				if (value1 != 0)
				{
					Console.WriteLine(value1);
				}

				index += 2;
				break;
			case 5:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				if (value1 != 0)
				{
					index = value2;
				}
				else
				{
					index += 3;
				}
				break;
			case 6:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				if (value1 == 0)
				{
					index = value2;
				}
				else
				{
					index += 3;
				}
				break;
			case 7:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				positions[positions[index + 3]] = value1 < value2 ? 1 : 0;

				index += 4;
				break;
			case 8:

				value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
				value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

				positions[positions[index + 3]] = value1 == value2 ? 1 : 0;

				index += 4;
				break;
		}
	}
}