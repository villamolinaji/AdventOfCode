using _19;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var map = Enumerable.Range(0, 50)
		.Select(y => new long[50])
		.ToArray();

int x = 0, y = 0;
for (y = 0; y < 50; y++)
{
	for (x = 0; x < 50; x++)
	{
		map[y][x] = Run(x, y);
	}
}

var result = map
	.SelectMany(r => r)
	.Count(x => x == 1);

Console.WriteLine(result);

// Part 2
var result2 = 0;

x = 101;
y = 0;

while (true)
{
	while (true)
	{
		if (Run(x, y) == 1)
		{
			break;
		}

		y++;
	}

	if (Run(x - 99, y + 99) == 1)
	{
		result2 = (((x - 99) * 10000) + y);

		break;
	}

	x++;
}

Console.WriteLine(result2);


long Run(int x, int y)
{
	var computer = new IntcodeComputer(program);

	computer.Run(new long[] { x, y });

	return computer.Output.Dequeue();
}