using _21;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

var stringInputList = new string[]
{
	"OR A T",
	"AND B T",
	"AND C T",
	"NOT T J",
	"AND D J",
	"WALK"
};

computer.Run(stringInputList);

var result = computer.Output.Last();

Console.WriteLine(result);

// Part 2
computer = new IntcodeComputer(program);

stringInputList = new string[]
{
	"OR A T",
	"AND B T",
	"AND C T",
	"NOT T J",
	"AND D J",
	"OR H T",
	"OR E T",
	"AND T J",
	"RUN"
};

computer.Run(stringInputList);

result = computer.Output.Last();

Console.WriteLine(result);