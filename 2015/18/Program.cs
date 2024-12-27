var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rows = lines.Count();
var cols = lines[0].Count();

var grid = new bool[rows, cols];

for (var row = 0; row < rows; row++)
{
	for (var col = 0; col < cols; col++)
	{
		grid[row, col] = lines[row][col] == '#';
	}
}

const int maxSteps = 100;

var directions = new List<(int row, int col)>
{
	(-1, -1),
	(-1, 0),
	(-1, 1),
	(0, -1),
	(0, 1),
	(1, -1),
	(1, 0),
	(1, 1)
};

for (int i = 0; i < maxSteps; i++)
{
	grid = OperateGrid(false);
}

var lights = 0;
for (var row = 0; row < rows; row++)
{
	for (var col = 0; col < cols; col++)
	{
		if (grid[row, col])
		{
			lights++;
		}
	}
}
Console.WriteLine(lights);


// Part 2
grid = new bool[rows, cols];

for (var row = 0; row < rows; row++)
{
	for (var col = 0; col < cols; col++)
	{
		grid[row, col] = lines[row][col] == '#';
	}
}

grid[0, 0] = true;
grid[0, cols - 1] = true;
grid[rows - 1, 0] = true;
grid[rows - 1, cols - 1] = true;

for (int i = 0; i < maxSteps; i++)
{
	grid = OperateGrid(true);
}

lights = 0;
for (var row = 0; row < rows; row++)
{
	for (var col = 0; col < cols; col++)
	{
		if (grid[row, col])
		{
			lights++;
		}
	}
}
Console.WriteLine(lights);


bool[,] OperateGrid(bool isPart2)
{
	var newGrid = new bool[rows, cols];

	for (var row = 0; row < rows; row++)
	{
		for (var col = 0; col < cols; col++)
		{
			var neighbors = 0;
			foreach (var direction in directions)
			{
				var newRow = row + direction.row;
				var newCol = col + direction.col;
				if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
				{
					continue;
				}
				if (grid[newRow, newCol])
				{
					neighbors++;
				}
			}

			if (isPart2 &&
				(row == 0 || row == rows - 1) && (col == 0 || col == cols - 1))
			{
				newGrid[row, col] = true;
				continue;
			}

			if (grid[row, col])
			{
				newGrid[row, col] = neighbors == 2 || neighbors == 3;
			}
			else
			{
				newGrid[row, col] = neighbors == 3;
			}
		}
	}
	return newGrid;
}