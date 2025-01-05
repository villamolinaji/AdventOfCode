var lines = File.ReadAllLinesAsync("Input.txt").Result;

var grid = new Dictionary<(int x, int y), char>();

for (var y = 0; y < lines.Length; y++)
{
	for (var x = 0; x < lines[y].Length; x++)
	{
		grid[(x, y)] = lines[y][x];
	}
}

var iterations = 10000;

var infections = 0;

var posX = lines[0].Length / 2;
var posY = lines.Length / 2;

var directions = new (int dx, int dy)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };
var direction = 0;

for (int i = 0; i < iterations; i++)
{
	var current = grid.GetValueOrDefault((posX, posY), '.');
	var nextState = '.';

	if (current == '#')
	{
		direction = (direction + 1) % 4;
		nextState = '.';
	}
	else
	{
		direction = (direction + 3) % 4;
		nextState = '#';
		infections++;
	}

	grid[(posX, posY)] = nextState;

	var d = directions[direction];
	(posX, posY) = (posX + d.dx, posY + d.dy);
}

Console.WriteLine(infections);

// Part 2
grid.Clear();

for (var y = 0; y < lines.Length; y++)
{
	for (var x = 0; x < lines[y].Length; x++)
	{
		grid[(x, y)] = lines[y][x];
	}
}

iterations = 10000000;

infections = 0;

posX = lines[0].Length / 2;
posY = lines.Length / 2;

direction = 0;

for (int i = 0; i < iterations; i++)
{
	var current = grid.GetValueOrDefault((posX, posY), '.');
	var nextState = '.';

	if (current == '.')
	{
		direction = (direction + 3) % 4;
		nextState = 'W';
	}
	else if (current == 'W')
	{
		nextState = '#';
		infections++;
	}
	else if (current == '#')
	{
		direction = (direction + 1) % 4;
		nextState = 'F';
	}
	else if (current == 'F')
	{
		direction = (direction + 2) % 4;
		nextState = '.';
	}

	grid[(posX, posY)] = nextState;

	var d = directions[direction];
	(posX, posY) = (posX + d.dx, posY + d.dy);
}

Console.WriteLine(infections);
