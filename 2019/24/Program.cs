var lines = File.ReadAllLinesAsync("Input.txt").Result;

var grid = lines.ToArray();

var visitedGrids = new HashSet<string>();

visitedGrids.Add(string.Join("", grid));

while (true)
{
	var newGrid = new string[grid.Length];

	for (var y = 0; y < grid.Length; y++)
	{
		var line = grid[y];

		var newLine = new char[line.Length];

		for (var x = 0; x < line.Length; x++)
		{
			var bugs = 0;

			if (y > 0 && grid[y - 1][x] == '#')
			{
				bugs++;
			}
			if (y < grid.Length - 1 && grid[y + 1][x] == '#')
			{
				bugs++;
			}
			if (x > 0 && grid[y][x - 1] == '#')
			{
				bugs++;
			}
			if (x < line.Length - 1 && grid[y][x + 1] == '#')
			{
				bugs++;
			}
			if (line[x] == '#' && bugs != 1)
			{
				newLine[x] = '.';
			}
			else if (line[x] == '.' && (bugs == 1 || bugs == 2))
			{
				newLine[x] = '#';
			}
			else
			{
				newLine[x] = line[x];
			}
		}

		newGrid[y] = new string(newLine);
	}

	grid = newGrid;

	var gridString = string.Join("", grid);

	if (visitedGrids.Contains(gridString))
	{
		break;
	}
	visitedGrids.Add(gridString);
}

var biodiversity = 0L;

for (var y = 0; y < grid.Length; y++)
{
	var line = grid[y];
	for (var x = 0; x < line.Length; x++)
	{
		if (line[x] == '#')
		{
			biodiversity += 1 << (y * line.Length + x);
		}
	}
}

Console.WriteLine(biodiversity);

// Part 2