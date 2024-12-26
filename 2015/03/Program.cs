var input = File.ReadAllTextAsync("Input.txt").Result;

var visited = new HashSet<(int x, int y)>();

var currentX = 0;
var currentY = 0;

visited.Add((currentX, currentY));

foreach (var c in input)
{
	switch (c)
	{
		case '^':
			currentY++;
			break;
		case 'v':
			currentY--;
			break;
		case '>':
			currentX++;
			break;
		case '<':
			currentX--;
			break;
	}

	visited.Add((currentX, currentY));
}

Console.WriteLine(visited.Count);

// Part 2
currentX = 0;
currentY = 0;

var robotX = 0;
var robotY = 0;

visited.Clear();
visited.Add((currentX, currentY));

for (var i = 0; i < input.Length; i += 2)
{
	var c = input[i];

	switch (c)
	{
		case '^':
			currentY++;
			break;
		case 'v':
			currentY--;
			break;
		case '>':
			currentX++;
			break;
		case '<':
			currentX--;
			break;
	}

	visited.Add((currentX, currentY));

	if (i + 1 >= input.Length)
	{
		break;
	}

	c = input[i + 1];

	switch (c)
	{
		case '^':
			robotY++;
			break;
		case 'v':
			robotY--;
			break;
		case '>':
			robotX++;
			break;
		case '<':
			robotX--;
			break;
	}

	visited.Add((robotX, robotY));
}

Console.WriteLine(visited.Count);