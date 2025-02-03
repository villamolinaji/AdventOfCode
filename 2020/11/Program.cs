using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var directions = new (int dx, int dy)[] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };

Console.WriteLine(GetSeats(false));

// Part 2
Console.WriteLine(GetSeats(true));


int GetSeats(bool isPart2)
{
	var map = lines.ToArray();
	var mapString = GetMapString(map);

	var visitedSeats = new HashSet<string>();
	visitedSeats.Add(mapString);

	while (true)
	{
		map = DoRound(map, isPart2);
		mapString = GetMapString(map);

		if (visitedSeats.Contains(mapString))
		{
			break;
		}

		visitedSeats.Add(mapString);
	}

	return mapString.Count(c => c == '#');
}

string GetMapString(string[] map)
{
	return string.Join("\n", map);
}

string[] DoRound(string[] map, bool isPart2)
{
	var newMap = new string[map.Length];

	var maxOccupied = isPart2 ? 5 : 4;

	for (int y = 0; y < map.Length; y++)
	{
		var line = map[y];
		var newLine = new StringBuilder();

		for (int x = 0; x < line.Length; x++)
		{
			var seat = line[x];

			if (seat == '.')
			{
				newLine.Append('.');

				continue;
			}

			var occupied = GetOccupiedSeats(map, x, y, isPart2);

			if (seat == 'L' && occupied == 0)
			{
				newLine.Append('#');
			}
			else if (seat == '#' && occupied >= maxOccupied)
			{
				newLine.Append('L');
			}
			else
			{
				newLine.Append(seat);
			}
		}

		newMap[y] = newLine.ToString();
	}

	return newMap;
}

int GetOccupiedSeats(string[] map, int x, int y, bool isPart2)
{
	var occupied = 0;

	foreach (var direction in directions)
	{
		if (!isPart2)
		{
			var newX = x + direction.dx;
			var newY = y + direction.dy;

			if (newX < 0 ||
				newX >= map[0].Length ||
				newY < 0 ||
				newY >= map.Length)
			{
				continue;
			}

			if (map[newY][newX] == '#')
			{
				occupied++;
			}
		}
		else
		{
			var newX = x;
			var newY = y;

			while (true)
			{
				newX += direction.dx;
				newY += direction.dy;

				if (newX < 0 ||
					newX >= map[0].Length ||
					newY < 0 ||
					newY >= map.Length)
				{
					break;
				}

				if (map[newY][newX] == '#')
				{
					occupied++;
					break;
				}

				if (map[newY][newX] == 'L')
				{
					break;
				}
			}
		}
	}

	return occupied;
}