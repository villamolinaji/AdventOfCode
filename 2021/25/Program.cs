var lines = File.ReadAllLinesAsync("Input.txt").Result;

var map = lines.Select(line => line.ToCharArray()).ToArray();

var rows = map.Length;
var cols = map[0].Length;

var steps = 1;

while (true)
{
	var isMoved = false;

	var newMap = GetNewMap(map);

	for (var r = 0; r < rows; r++)
	{
		for (var c = 0; c < cols; c++)
		{
			if (map[r][c] == '>' &&
				CanMoveRight(map, r, c))
			{
				MoveRight(newMap, r, c);

				isMoved = true;
			}
		}
	}

	map = newMap;

	newMap = GetNewMap(map);

	for (var r = 0; r < rows; r++)
	{
		for (var c = 0; c < cols; c++)
		{
			if (map[r][c] == 'v' &&
				CanMoveDown(map, r, c))
			{
				MoveDown(newMap, r, c);

				isMoved = true;
			}
		}
	}

	if (!isMoved)
	{
		break;
	}

	map = newMap;

	steps++;
}

Console.WriteLine(steps);


char[][] GetNewMap(char[][] map)
{
	var newMap = new char[rows][];
	for (var i = 0; i < rows; i++)
	{
		newMap[i] = new char[cols];
		Array.Copy(map[i], newMap[i], cols);
	}

	return newMap;
}


bool CanMoveRight(char[][] map, int r, int c)
{
	if (c + 1 >= map[0].Length)
	{
		c = 0;
	}
	else
	{
		c++;
	}

	return map[r][c] == '.';
}

void MoveRight(char[][] map, int r, int c)
{
	map[r][c] = '.';

	if (c + 1 >= map[0].Length)
	{
		c = 0;
	}
	else
	{
		c++;
	}

	map[r][c] = '>';
}

bool CanMoveDown(char[][] map, int r, int c)
{
	if (r + 1 >= map.Length)
	{
		r = 0;
	}
	else
	{
		r++;
	}

	return map[r][c] == '.';
}

void MoveDown(char[][] map, int r, int c)
{
	map[r][c] = '.';

	if (r + 1 >= map.Length)
	{
		r = 0;
	}
	else
	{
		r++;
	}

	map[r][c] = 'v';
}
