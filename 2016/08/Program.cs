var lines = File.ReadAllLinesAsync("Input.txt").Result;

var grid = new bool[6, 50];

foreach (var line in lines)
{
	if (line.StartsWith("rect"))
	{
		var parts = line.Split(' ')[1].Split('x');
		var width = int.Parse(parts[0]);
		var height = int.Parse(parts[1]);

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				grid[i, j] = true;
			}
		}
	}
	else if (line.StartsWith("rotate row"))
	{
		var parts = line.Split(' ');
		var row = int.Parse(parts[2].Split('=')[1]);
		var amount = int.Parse(parts[4]);
		var newRow = new bool[50];

		for (int i = 0; i < 50; i++)
		{
			newRow[(i + amount) % 50] = grid[row, i];
		}

		for (int i = 0; i < 50; i++)
		{
			grid[row, i] = newRow[i];
		}
	}
	else if (line.StartsWith("rotate column"))
	{
		var parts = line.Split(' ');
		var column = int.Parse(parts[2].Split('=')[1]);
		var amount = int.Parse(parts[4]);
		var newColumn = new bool[6];

		for (int i = 0; i < 6; i++)
		{
			newColumn[(i + amount) % 6] = grid[i, column];
		}

		for (int i = 0; i < 6; i++)
		{
			grid[i, column] = newColumn[i];
		}
	}
}

var countPixelsLit = 0;

for (int i = 0; i < grid.GetLength(0); i++)
{
	for (int j = 0; j < grid.GetLength(1); j++)
	{
		if (grid[i, j])
		{
			countPixelsLit++;
		}
	}
}

Console.WriteLine(countPixelsLit);

// Part 2
for (int i = 0; i < grid.GetLength(0); i++)
{
	for (int j = 0; j < grid.GetLength(1); j++)
	{
		Console.Write(grid[i, j] ? '#' : ' ');
	}
	Console.WriteLine();
}
