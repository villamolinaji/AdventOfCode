var input = 361527;

var currentRow = 1;
var currentCol = 1;
int direction = 2;

for (int i = 3; i < input; i++)
{
	switch (direction)
	{
		case 0:
			currentCol++;
			if (currentCol > -currentRow)
			{
				direction = 1;
			}
			break;
		case 1:
			currentRow++;
			if (currentRow == currentCol)
			{
				direction = 2;
			}
			break;
		case 2:
			currentCol--;
			if (currentCol == -currentRow)
			{
				direction = 3;
			}
			break;
		case 3:
			currentRow--;
			if (currentRow == currentCol)
			{
				direction = 0;
			}
			break;
	}
}

var steps = currentRow + currentCol;
Console.WriteLine(steps);

// Part 2
currentRow = 1;
currentCol = 1;
direction = 2;

var grid = new Dictionary<(int row, int col), int>();
grid[(0, 0)] = 1;
grid[(0, 1)] = 1;
grid[(1, 1)] = 2;

var result = 0;

var directions = new List<(int row, int col)>
{
	(0, 1),
	(1, 1),
	(1, 0),
	(1, -1),
	(0, -1),
	(-1, -1),
	(-1, 0),
	(-1, 1)
};

while (true)
{
	switch (direction)
	{
		case 0:
			currentCol++;
			if (currentCol > -currentRow)
			{
				direction = 1;
			}
			break;
		case 1:
			currentRow++;
			if (currentRow == currentCol)
			{
				direction = 2;
			}
			break;
		case 2:
			currentCol--;
			if (currentCol == -currentRow)
			{
				direction = 3;
			}
			break;
		case 3:
			currentRow--;
			if (currentRow == currentCol)
			{
				direction = 0;
			}
			break;
	}

	var sum = 0;
	foreach (var dir in directions)
	{
		var newRow = currentRow + dir.row;
		var newCol = currentCol + dir.col;

		if (grid.ContainsKey((newRow, newCol)))
		{
			sum += grid[(newRow, newCol)];
		}
	}

	grid[(currentRow, currentCol)] = sum;

	if (sum > input)
	{
		result = sum;
		break;
	}
}

Console.WriteLine(result);