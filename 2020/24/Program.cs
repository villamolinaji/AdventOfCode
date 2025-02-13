var lines = File.ReadAllLinesAsync("Input.txt").Result;

var directions = new Dictionary<string, (int x, int y)> {
	{"o",  ( 0,  0)},
	{"ne", ( 1,  1)},
	{"nw", (-1,  1)},
	{"e",  ( 2,  0)},
	{"w",  (-2,  0)},
	{"se", ( 1, -1)},
	{"sw", (-1, -1)},
};

var result = ParseBlackTiles().Count;

Console.WriteLine(result);

// Part 2
result = Enumerable.Range(0, 100)
	.Aggregate(ParseBlackTiles(), (blackTiles, _) => Flip(blackTiles))
	.Count;

Console.WriteLine(result);


HashSet<(int x, int y)> ParseBlackTiles()
{
	var tiles = new Dictionary<(int x, int y), bool>();

	foreach (var line in lines)
	{
		var tile = Move(line);

		tiles[tile] = !tiles.GetValueOrDefault(tile);
	}

	return (from kvp in tiles where kvp.Value select kvp.Key).ToHashSet();
}

(int x, int y) Move(string line)
{
	var (x, y) = (0, 0);

	while (line != "")
	{
		foreach (var kvp in directions.Where(d => line.StartsWith(d.Key)))
		{
			line = line.Substring(kvp.Key.Length);

			(x, y) = (x + kvp.Value.x, y + kvp.Value.y);
		}
	}
	return (x, y);
}

HashSet<(int x, int y)> Flip(HashSet<(int x, int y)> blackTiles)
{
	var tiles = (
		from black in blackTiles
		from tile in GetNeighbours(black)
		select tile
	).ToHashSet();

	return (
		from tile in tiles
		let blacks = GetNeighbours(tile).Count(n => blackTiles.Contains(n))
		where blacks == 2 ||
			(blacks == 3 && blackTiles.Contains(tile))
		select tile
	).ToHashSet();
}

IEnumerable<(int x, int y)> GetNeighbours((int x, int y) tile) =>
		from dir in directions.Values
		select (tile.x + dir.x, tile.y + dir.y);