var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rows = lines.Length;
var cols = lines[0].Length;

var map = new char[rows, cols];

for (var i = 0; i < rows; i++)
{
	for (var j = 0; j < cols; j++)
	{
		map[i, j] = lines[i][j];
	}
}

var letters = new List<char>();

var topCol = 0;

for (var i = 0; i < cols; i++)
{
	if (map[0, i] == '|')
	{
		topCol = i;
		break;
	}
}

var row = 0;
var col = topCol;

var direction = 2;

var steps = 0;

while (true)
{
	if (map[row, col] == ' ')
	{
		break;
	}

	if (map[row, col] == '+')
	{
		if (direction != 0 && row + 1 < rows && map[row + 1, col] != ' ')
		{
			direction = 2;
		}
		else if (direction != 2 && row - 1 >= 0 && map[row - 1, col] != ' ')
		{
			direction = 0;
		}
		else if (direction != 1 && col + 1 < cols && map[row, col + 1] != ' ')
		{
			direction = 3;
		}
		else if (direction != 3 && col - 1 >= 0 && map[row, col - 1] != ' ')
		{
			direction = 1;
		}
	}

	if (map[row, col] != '|' && map[row, col] != '-' && map[row, col] != '+')
	{
		letters.Add(map[row, col]);
	}

	if (direction == 0)
	{
		row--;
	}
	else if (direction == 1)
	{
		col--;
	}
	else if (direction == 2)
	{
		row++;
	}
	else if (direction == 3)
	{
		col++;
	}

	steps++;
}

Console.WriteLine(string.Join("", letters));

Console.WriteLine(steps);