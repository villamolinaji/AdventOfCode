var lines = File.ReadAllLinesAsync("Input.txt").Result;

var ipReg = int.Parse(lines[0].Split(' ')[1]);

var instructions = lines.Skip(1).Select(x => x.Split(' ')).Select(x => (x[0], int.Parse(x[1]), int.Parse(x[2]), int.Parse(x[3]))).ToArray();

var registers = new int[6];

Solve();


void Solve()
{
	HashSet<int> seenHaltValues = new HashSet<int>();
	int firstHaltValue = -1;
	int lastHaltValue = -1;

	while (true)
	{
		int instructionIndex = registers[ipReg];

		if (instructionIndex < 0 ||
			instructionIndex >= instructions.Length)
		{
			break;
		}

		var (op, a, b, c) = instructions[instructionIndex];

		ExecuteInstruction(op, a, b, c);

		// halt instruction (eqrr 3 0 5 in input)
		if (instructionIndex == 28)
		{
			if (firstHaltValue == -1)
			{
				firstHaltValue = registers[3];
			}

			if (!seenHaltValues.Add(registers[3]))
			{
				Console.WriteLine($"Part 1: {firstHaltValue}");

				Console.WriteLine($"Part 2: {lastHaltValue}");

				return;
			}

			lastHaltValue = registers[3];
		}

		registers[ipReg]++;
	}
}

void ExecuteInstruction(string op, int a, int b, int c)
{
	switch (op)
	{
		case "addr":
			registers[c] = registers[a] + registers[b];
			break;
		case "addi":
			registers[c] = registers[a] + b;
			break;
		case "mulr":
			registers[c] = registers[a] * registers[b];
			break;
		case "muli":
			registers[c] = registers[a] * b;
			break;
		case "banr":
			registers[c] = registers[a] & registers[b];
			break;
		case "bani":
			registers[c] = registers[a] & b;
			break;
		case "borr":
			registers[c] = registers[a] | registers[b];
			break;
		case "bori":
			registers[c] = registers[a] | b;
			break;
		case "setr":
			registers[c] = registers[a];
			break;
		case "seti":
			registers[c] = a;
			break;
		case "gtir":
			registers[c] = a > registers[b] ? 1 : 0;
			break;
		case "gtri":
			registers[c] = registers[a] > b ? 1 : 0;
			break;
		case "gtrr":
			registers[c] = registers[a] > registers[b] ? 1 : 0;
			break;
		case "eqir":
			registers[c] = a == registers[b] ? 1 : 0;
			break;
		case "eqri":
			registers[c] = registers[a] == b ? 1 : 0;
			break;
		case "eqrr":
			registers[c] = registers[a] == registers[b] ? 1 : 0;
			break;
	}
}