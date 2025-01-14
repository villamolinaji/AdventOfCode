var lines = File.ReadAllLinesAsync("Input.txt").Result;

Console.WriteLine(Simulate(10));

Console.WriteLine(Simulate(1000000000));


long Simulate(int minutes)
{
	var rows = lines.Length;
	var cols = lines[0].Length;
	var map = new char[rows, cols];

	for (int r = 0; r < rows; r++)
	{
		for (int c = 0; c < cols; c++)
		{
			map[r, c] = lines[r][c];
		}
	}

	var directions = new (int dr, int dc)[]
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

	var minute = 0;

	var visited = new Dictionary<string, int>();

	var isCycleAplied = false;

	while (minute < minutes)
	{
		var mapString = string.Join("", map.Cast<char>());

		if (visited.ContainsKey(mapString) &&
			!isCycleAplied)
		{
			var cycleLength = minute - visited[mapString];
			var remainingMinutes = minutes - minute - 1;
			var remainingCycles = remainingMinutes / cycleLength;

			minute += remainingCycles * cycleLength;

			isCycleAplied = true;
		}
		else
		{
			visited[mapString] = minute;
		}

		var newMap = new char[rows, cols];

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				var open = 0;
				var trees = 0;
				var lumberyards = 0;

				foreach (var (dr, dc) in directions)
				{
					var nr = r + dr;
					var nc = c + dc;

					if (nr < 0 ||
						nr >= rows ||
						nc < 0 ||
						nc >= cols)
					{
						continue;
					}

					switch (map[nr, nc])
					{
						case '.':
							open++;
							break;
						case '|':
							trees++;
							break;
						case '#':
							lumberyards++;
							break;
					}
				}

				switch (map[r, c])
				{
					case '.':
						newMap[r, c] = trees >= 3
							? '|'
							: '.';
						break;
					case '|':
						newMap[r, c] = lumberyards >= 3
							? '#'
							: '|';
						break;
					case '#':
						newMap[r, c] = lumberyards >= 1 && trees >= 1
							? '#'
							: '.';
						break;
				}
			}
		}

		map = newMap;

		minute++;
	}

	long treeCount = 0;
	long lumberyardCount = 0;

	for (int r = 0; r < rows; r++)
	{
		for (int c = 0; c < cols; c++)
		{
			switch (map[r, c])
			{
				case '|':
					treeCount++;
					break;
				case '#':
					lumberyardCount++;
					break;
			}
		}
	}

	return treeCount * lumberyardCount;
}