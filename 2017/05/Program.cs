var lines = File.ReadAllLinesAsync("Input.txt").Result;

var index = 0;
var steps = 0;

var instructions = lines.Select(int.Parse).ToList();

while (index >= 0 &&
	index < instructions.Count)
{
	var offset = instructions[index];

	instructions[index] = offset + 1;

	index += offset;
	steps++;
}

Console.WriteLine(steps);

// Part 2
index = 0;
steps = 0;

instructions = lines.Select(int.Parse).ToList();

while (index >= 0 &&
	index < instructions.Count)
{
	var offset = instructions[index];

	instructions[index] = offset >= 3
		? offset - 1
		: offset + 1;

	index += offset;
	steps++;
}

Console.WriteLine(steps);