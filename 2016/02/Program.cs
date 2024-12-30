var lines = File.ReadAllLinesAsync("Input.txt").Result;

var pad = new Dictionary<(int, int), char>
{
	[(-1, -1)] = '1',
	[(0, -1)] = '2',
	[(1, -1)] = '3',
	[(-1, 0)] = '4',
	[(0, 0)] = '5',
	[(1, 0)] = '6',
	[(-1, 1)] = '7',
	[(0, 1)] = '8',
	[(1, 1)] = '9',
};

var currentX = 0;
var currentY = 0;

var code = new List<char>();

foreach (var line in lines)
{
	foreach (var c in line)
	{
		switch (c)
		{
			case 'U':
				if (pad.ContainsKey((currentX, currentY - 1)))
				{
					currentY--;
				}

				break;
			case 'D':
				if (pad.ContainsKey((currentX, currentY + 1)))
				{
					currentY++;
				}

				break;
			case 'L':
				if (pad.ContainsKey((currentX - 1, currentY)))
				{
					currentX--;
				}

				break;
			case 'R':
				if (pad.ContainsKey((currentX + 1, currentY)))
				{
					currentX++;
				}

				break;
		}
	}

	code.Add(pad[(currentX, currentY)]);
}

Console.WriteLine(string.Join("", code));

// Part 2
pad = new Dictionary<(int, int), char>
{
	[(0, -2)] = '1',
	[(-1, -1)] = '2',
	[(0, -1)] = '3',
	[(1, -1)] = '4',
	[(-2, 0)] = '5',
	[(-1, 0)] = '6',
	[(0, 0)] = '7',
	[(1, 0)] = '8',
	[(2, 0)] = '9',
	[(-1, 1)] = 'A',
	[(0, 1)] = 'B',
	[(1, 1)] = 'C',
	[(0, 2)] = 'D',
};

currentX = -2;
currentY = 0;

code = new List<char>();

foreach (var line in lines)
{
	foreach (var c in line)
	{
		switch (c)
		{
			case 'U':
				if (pad.ContainsKey((currentX, currentY - 1)))
				{
					currentY--;
				}

				break;
			case 'D':
				if (pad.ContainsKey((currentX, currentY + 1)))
				{
					currentY++;
				}

				break;
			case 'L':
				if (pad.ContainsKey((currentX - 1, currentY)))
				{
					currentX--;
				}

				break;
			case 'R':
				if (pad.ContainsKey((currentX + 1, currentY)))
				{
					currentX++;
				}

				break;
		}
	}

	code.Add(pad[(currentX, currentY)]);
}

Console.WriteLine(string.Join("", code));
