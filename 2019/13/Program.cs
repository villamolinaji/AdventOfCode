using _13;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

computer.Run(1);

var chunks = ReadOutput();

var blocks = chunks.Count(x => x[2] == 2);

Console.WriteLine(blocks);

// Part 2
program[0] = 2;

computer = new IntcodeComputer(program);

var score = 0;
var icolBall = -1;
var icolPaddle = -1;
var dir = 0;

while (true)
{
	computer.Run(dir);

	chunks = ReadOutput();
	foreach (var chunk in chunks)
	{
		int x = (int)chunk[0];
		int y = (int)chunk[1];
		int value = (int)chunk[2];

		if (x == -1 && y == 0)
		{
			score = value;
		}
		else if (value == 3)
		{
			icolPaddle = (int)x;
		}
		else if (value == 4)
		{
			icolBall = (int)x;
		}
	}

	if (computer.IsHalt)
	{
		break;
	}

	if (icolBall < icolPaddle)
	{
		dir = -1;
	}
	else if (icolBall > icolPaddle)
	{
		dir = 1;
	}
	else
	{
		dir = 0;
	}

	computer.Output.Clear();
}

Console.WriteLine(score);


long[][] ReadOutput()
{
	var result = new List<long[]>();

	while (computer.Output.Any())
	{
		var outputValues = new long[3];

		for (int i = 0; i < 3; i++)
		{
			outputValues[i] = computer.Output.Dequeue();
		}

		result.Add(outputValues);
	}

	return result.ToArray();
}