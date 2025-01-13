using _16;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var samples = new List<Sample>();
var testProgram = new List<int[]>();

ReadInput();

var countValidSamples = 0;

foreach (var sample in samples)
{
	var matches = 0;

	var instructions = sample.Instruction;
	for (var i = 0; i < 16; i++)
	{
		instructions[0] = i;

		var regsActual = EvalutateSample(sample.Before, instructions);
		if (sample.After.SequenceEqual(regsActual))
		{
			matches++;
		}
	}

	if (matches >= 3)
	{
		countValidSamples++;
	}
}

Console.WriteLine(countValidSamples);

// Part 2
ReadInput();

var opcodeMapping = DeduceOpcodes();

var result = ExecuteProgram(opcodeMapping);

Console.WriteLine(result);

void ReadInput()
{
	samples = new List<Sample>();
	testProgram = new List<int[]>();
	var linesIndex = 0;

	while (linesIndex < lines.Length)
	{
		var line = lines[linesIndex];

		if (line.StartsWith("Before"))
		{
			var before = line.Substring(9, 10).Split(", ").Select(int.Parse).ToArray();
			var instruction = lines[linesIndex + 1].Split(" ").Select(int.Parse).ToArray();
			var after = lines[linesIndex + 2].Substring(9, 10).Split(", ").Select(int.Parse).ToArray();

			linesIndex += 4;

			samples.Add(new Sample(before, instruction, after));
		}
		else
		{
			linesIndex++;

			if (!string.IsNullOrEmpty(line))
			{
				testProgram.Add(line.Split(" ").Select(int.Parse).ToArray());
			}
		}
	}
}


int[] EvalutateSample(int[] before, int[] instructions)
{
	var regs = before.ToArray();

	switch (instructions[0])
	{
		case 0:
			regs[instructions[3]] = regs[instructions[1]] + regs[instructions[2]];
			break;
		case 1:
			regs[instructions[3]] = regs[instructions[1]] + instructions[2];
			break;
		case 2:
			regs[instructions[3]] = regs[instructions[1]] * regs[instructions[2]];
			break;
		case 3:
			regs[instructions[3]] = regs[instructions[1]] * instructions[2];
			break;
		case 4:
			regs[instructions[3]] = regs[instructions[1]] & regs[instructions[2]];
			break;
		case 5:
			regs[instructions[3]] = regs[instructions[1]] & instructions[2];
			break;
		case 6:
			regs[instructions[3]] = regs[instructions[1]] | regs[instructions[2]];
			break;
		case 7:
			regs[instructions[3]] = regs[instructions[1]] | instructions[2];
			break;
		case 8:
			regs[instructions[3]] = regs[instructions[1]];
			break;
		case 9:
			regs[instructions[3]] = instructions[1];
			break;
		case 10:
			regs[instructions[3]] = instructions[1] > regs[instructions[2]] ? 1 : 0;
			break;
		case 11:
			regs[instructions[3]] = regs[instructions[1]] > instructions[2] ? 1 : 0;
			break;
		case 12:
			regs[instructions[3]] = regs[instructions[1]] > regs[instructions[2]] ? 1 : 0;
			break;
		case 13:
			regs[instructions[3]] = instructions[1] == regs[instructions[2]] ? 1 : 0;
			break;
		case 14:
			regs[instructions[3]] = regs[instructions[1]] == instructions[2] ? 1 : 0;
			break;
		case 15:
			regs[instructions[3]] = regs[instructions[1]] == regs[instructions[2]] ? 1 : 0;
			break;
	}

	return regs;
}

Dictionary<int, int> DeduceOpcodes()
{
	var possibleOpcodes = new Dictionary<int, HashSet<int>>();

	foreach (var sample in samples)
	{
		int opcode = sample.Instruction[0];
		if (!possibleOpcodes.ContainsKey(opcode))
		{
			possibleOpcodes[opcode] = new HashSet<int>();
		}

		var instructions = sample.Instruction;
		for (var i = 0; i < 16; i++)
		{
			instructions[0] = i;

			var regsActual = EvalutateSample(sample.Before, instructions);
			if (sample.After.SequenceEqual(regsActual))
			{
				possibleOpcodes[opcode].Add(i);
			}
		}
	}

	var finalMapping = new Dictionary<int, int>();

	while (finalMapping.Count < 16)
	{
		var determined = possibleOpcodes.First(kv => kv.Value.Count == 1);
		var opcode = determined.Value.First();
		finalMapping[determined.Key] = opcode;

		foreach (var kv in possibleOpcodes)
		{
			kv.Value.Remove(opcode);
		}
	}

	return finalMapping;
}

int ExecuteProgram(Dictionary<int, int> opcodeMapping)
{
	var registers = new int[4];

	foreach (var instruction in testProgram)
	{
		var operation = opcodeMapping[instruction[0]];

		instruction[0] = operation;

		registers = EvalutateSample(registers, instruction);
	}
	return registers[0];
}
