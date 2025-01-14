var lines = File.ReadAllLinesAsync("Input.txt").Result;

var ipReg = int.Parse(lines[0].Split(' ')[1]);

var instructions = lines.Skip(1).Select(x => x.Split(' ')).Select(x => (x[0], int.Parse(x[1]), int.Parse(x[2]), int.Parse(x[3]))).ToArray();

var registers = new int[6];

EvaluateInstructions();

Console.WriteLine(registers[0]);

//Part2
//registers = new int[6];
//registers[0] = 1;

//EvaluateInstructions();

//Console.WriteLine(registers[0]);
// Run commented code and get value on reg3 after some seconds: 10551348

var reg3 = 10551348;
var reg0 = 0;

for (var x = 1; x <= reg3; x++)
{
	if (reg3 % x == 0)
	{
		reg0 += x;
	}
}

Console.WriteLine(reg0);


void EvaluateInstructions()
{
	var instructionIndex = 0;

	while (instructionIndex >= 0 && instructionIndex < instructions.Length)
	{
		var instruction = instructions[instructionIndex];

		registers[ipReg] = instructionIndex;

		switch (instruction.Item1)
		{
			case "addr":
				registers[instruction.Item4] = registers[instruction.Item2] + registers[instruction.Item3];
				break;
			case "addi":
				registers[instruction.Item4] = registers[instruction.Item2] + instruction.Item3;
				break;
			case "mulr":
				registers[instruction.Item4] = registers[instruction.Item2] * registers[instruction.Item3];
				break;
			case "muli":
				registers[instruction.Item4] = registers[instruction.Item2] * instruction.Item3;
				break;
			case "banr":
				registers[instruction.Item4] = registers[instruction.Item2] & registers[instruction.Item3];
				break;
			case "bani":
				registers[instruction.Item4] = registers[instruction.Item2] & instruction.Item2;
				break;
			case "borr":
				registers[instruction.Item4] = registers[instruction.Item2] | registers[instruction.Item3];
				break;
			case "bori":
				registers[instruction.Item4] = registers[instruction.Item2] | instruction.Item3;
				break;
			case "setr":
				registers[instruction.Item4] = registers[instruction.Item2];
				break;
			case "seti":
				registers[instruction.Item4] = instruction.Item2;
				break;
			case "gtir":
				registers[instruction.Item4] = instruction.Item2 > registers[instruction.Item3] ? 1 : 0;
				break;
			case "gtri":
				registers[instruction.Item4] = registers[instruction.Item2] > instruction.Item3 ? 1 : 0;
				break;
			case "gtrr":
				registers[instruction.Item4] = registers[instruction.Item2] > registers[instruction.Item3] ? 1 : 0;
				break;
			case "eqir":
				registers[instruction.Item4] = instruction.Item2 == registers[instruction.Item3] ? 1 : 0;
				break;
			case "eqri":
				registers[instruction.Item4] = registers[instruction.Item2] == instruction.Item3 ? 1 : 0;
				break;
			case "eqrr":
				registers[instruction.Item4] = registers[instruction.Item2] == registers[instruction.Item3] ? 1 : 0;
				break;
		}

		instructionIndex = registers[ipReg] + 1;
	}
}