var lines = File.ReadAllLinesAsync("Input.txt").Result;

var clay = new HashSet<(int x, int y)>();

foreach (var line in lines)
{
	var parts = line.Split(", ");
	var first = parts[0].Split('=');
	var second = parts[1].Split('=');
	var range = second[1].Split("..").Select(int.Parse).ToArray();

	if (first[0] == "x")
	{
		for (var y = range[0]; y <= range[1]; y++)
		{
			clay.Add((int.Parse(first[1]), y));
		}
	}
	else
	{
		for (var x = range[0]; x <= range[1]; x++)
		{
			clay.Add((x, int.Parse(first[1])));
		}
	}
}

var (width, height) = (2000, 2000);
var grid = new char[width, height];

for (var y = 0; y < height; y++)
{
	for (var x = 0; x < width; x++)
	{
		if (clay.Contains((x, y)))
		{
			grid[x, y] = '#';
		}
		else
		{
			grid[x, y] = '.';
		}
	}
}

SimulateFallDown(500, 0);

var (minY, maxY) = (int.MaxValue, int.MinValue);
for (var y = 0; y < height; y++)
{
	for (var x = 0; x < width; x++)
	{
		if (grid[x, y] == '#')
		{
			minY = Math.Min(minY, y);
			maxY = Math.Max(maxY, y);
		}
	}
}

var count = 0;
var countWater = 0;

for (var y = minY; y <= maxY; y++)
{
	for (var x = 0; x < width; x++)
	{
		var cell = grid[x, y];
		if (cell == '|' || cell == '~')
		{
			count++;

			if (cell == '~')
			{
				countWater++;
			}
		}
	}
}

Console.WriteLine(count);

Console.WriteLine(countWater);


void SimulateFallDown(int x, int y)
{
	if (grid[x, y] != '.')
	{
		return;
	}

	grid[x, y] = '|';

	if (y == height - 1)
	{
		return;
	}

	SimulateFallDown(x, y + 1);

	if (grid[x, y + 1] == '#' ||
		grid[x, y + 1] == '~')
	{
		if (x > 0)
		{
			SimulateFallDown(x - 1, y);
		}
		if (x < width - 1)
		{
			SimulateFallDown(x + 1, y);
		}
	}

	if (IsInClay(x, y))
	{
		foreach (var dx in new[] { -1, 1 })
		{
			for (var xT = x; xT >= 0 && xT < width && grid[xT, y] == '|'; xT += dx)
			{
				grid[xT, y] = '~';
			}
		}
	}
}

bool IsInClay(int x, int y)
{
	foreach (var dx in new[] { -1, 1 })
	{
		for (var xT = x; xT >= 0 && xT < width && grid[xT, y] != '#'; xT += dx)
		{
			if (grid[xT, y] == '.' ||
				grid[xT, y + 1] == '|')
			{
				return false;
			}
		}
	}

	return true;
}