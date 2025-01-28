using System.Collections.Immutable;
using System.Text;
using _18;

var directions = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

var input = File.ReadAllTextAsync("Input.txt").Result;

var map1 = new Map(input);

var steps = Resolve(map1);

Console.WriteLine(steps);

// Part 2
var steps2 = 0;

foreach (var subMap in GenerateSubMaps(input))
{
	var map2 = new Map(subMap);

	steps2 += Resolve(map2);
}

Console.WriteLine(steps2);


Dictionary<char, ImmutableHashSet<char>> GenerateDependencies(Map map)
{
	var queue = new Queue<((int row, int col) pos, string dependsOn)>();
	var pos = Find('@', map);
	var dependsOn = "";

	queue.Enqueue((pos, dependsOn));

	var result = new Dictionary<char, ImmutableHashSet<char>>();
	var visited = new HashSet<(int row, int col)>();
	visited.Add(pos);

	while (queue.Any())
	{
		(pos, dependsOn) = queue.Dequeue();

		foreach (var (drow, dcol) in directions)
		{
			var posT = (pos.row + drow, pos.col + dcol);
			var ch = GetCell(posT, map);

			if (visited.Contains(posT) || ch == '#')
			{
				continue;
			}

			visited.Add(posT);
			var dependsOnT = dependsOn;

			if (char.IsLower(ch))
			{
				result[ch] = ImmutableHashSet.CreateRange(dependsOn);
			}

			if (char.IsLetter(ch))
			{
				dependsOnT += char.ToLower(ch);
			}
			queue.Enqueue((posT, dependsOnT));
		}
	}
	return result;
}

(int row, int col) Find(char ch, Map map)
{
	if (!map.PositionCache.ContainsKey(ch))
	{
		for (var r = 0; r < map.Rows; r++)
		{
			for (var c = 0; c < map.Cols; c++)
			{
				if (map.Grid[r][c] == ch)
				{
					map.PositionCache[ch] = (r, c);

					return map.PositionCache[ch];
				}
			}
		}

		throw new InvalidOperationException();
	}
	else
	{
		return map.PositionCache[ch];
	}
}

char GetCell((int row, int col) pos, Map map)
{
	var (row, col) = pos;

	if (row < 0 ||
		row >= map.Rows ||
		col < 0 ||
		col >= map.Cols)
	{
		return '#';
	}

	return map.Grid[row][col];

}

int Resolve(Map map)
{
	var dependencies = GenerateDependencies(map);

	var cache = new Dictionary<string, int>();

	return ResolveRecursive('@', dependencies.Keys.ToImmutableHashSet(), cache, dependencies, map);
}

int ResolveRecursive(char currentItem, ImmutableHashSet<char> keys, Dictionary<string, int> cache, Dictionary<char, ImmutableHashSet<char>> dependencies, Map map)
{
	if (keys.Count == 0)
	{
		return 0;
	}

	var cacheKey = currentItem + string.Join("", keys);

	if (!cache.ContainsKey(cacheKey))
	{
		var result = int.MaxValue;

		foreach (var key in keys)
		{
			if (dependencies[key].Intersect(keys).Count == 0)
			{
				var d = GetDistance(currentItem, key, map) + ResolveRecursive(key, keys.Remove(key), cache, dependencies, map);

				result = Math.Min(d, result);
			}
		}

		cache[cacheKey] = result;
	}

	return cache[cacheKey];
}

int GetDistance(char a, char b, Map map)
{
	var key = (a, b);

	if (!map.DistanceCache.ContainsKey(key))
	{
		map.DistanceCache[key] = ComputeDistance(a, b, map);
	}

	return map.DistanceCache[key];
}

int ComputeDistance(char a, char b, Map map)
{
	if (a == b)
	{
		return 0;
	}
	var pos = Find(a, map);

	var queue = new Queue<((int row, int col) pos, int distance)>();
	int distance = 0;

	queue.Enqueue((pos, distance));

	var seen = new HashSet<(int row, int col)>();
	seen.Add(pos);

	while (queue.Any())
	{
		(pos, distance) = queue.Dequeue();

		foreach (var (drow, dcol) in directions)
		{
			var newPos = (pos.row + drow, pos.col + dcol);

			var cell = GetCell(newPos, map);

			if (seen.Contains(newPos) ||
				cell == '#')
			{
				continue;
			}

			seen.Add(newPos);

			var newDistance = distance + 1;

			if (cell == b)
			{
				return newDistance;
			}
			else
			{
				queue.Enqueue((newPos, newDistance));
			}
		}
	}

	throw new InvalidOperationException();
}

IEnumerable<string> GenerateSubMaps(string input)
{
	var grid = input.Split("\n").Select(x => x.ToCharArray()).ToArray();
	var rows = grid.Length;
	var cols = grid[0].Length;
	var hrow = rows / 2;
	var hcol = cols / 2;
	var pattern = "@#@\n###\n@#@".Split();

	foreach (var drow in new[] { -1, 0, 1 })
	{
		foreach (var dcol in new[] { -1, 0, 1 })
		{
			grid[hrow + drow][hcol + dcol] = pattern[1 + drow][1 + dcol];
		}
	}

	foreach (var (drow, dcol) in new[] { (0, 0), (0, hcol + 1), (hrow + 1, 0), (hrow + 1, hcol + 1) })
	{
		var res = new StringBuilder();

		for (var irow = 0; irow < hrow; irow++)
		{
			res.Append(string.Join("", grid[irow + drow].Skip(dcol).Take(hcol)) + "\n");
		}

		for (var ch = 'A'; ch <= 'Z'; ch++)
		{
			if (!res.ToString().Contains(char.ToLower(ch)))
			{
				res = res.Replace(ch, '.');
			}
		}

		res = new StringBuilder(res.ToString().Substring(0, res.Length - 1));

		yield return res.ToString();
	}
}
