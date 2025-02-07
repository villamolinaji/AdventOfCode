using System.Text;
using _20;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

const int ImageSize = 10;

var tiles = new List<Tile>();
var tile = new Tile(0, new string[ImageSize]);
var gridIndex = 0;

foreach (var line in lines)
{
	if (line.StartsWith("Tile"))
	{
		tile = new Tile(long.Parse(line.Split(' ')[1].TrimEnd(':')), new string[ImageSize]);

		gridIndex = 0;
	}
	else if (string.IsNullOrWhiteSpace(line))
	{
		tiles.Add(tile);
	}
	else
	{
		tile.Image[gridIndex] = line;
		gridIndex++;
	}
}
tiles.Add(tile);

var tilesResolved = Resolve();

var result = tilesResolved[0][0].Id *
	tilesResolved[0][tilesResolved[0].Length - 1].Id *
	tilesResolved[tilesResolved.Length - 1][0].Id *
	tilesResolved[tilesResolved.Length - 1][tilesResolved[tilesResolved.Length - 1].Length - 1].Id;

Console.WriteLine(result);

// Part 2
var monster = new string[]{
	"                  # ",
	"#    ##    ##    ###",
	" #  #  #  #  #  #   "
};

var image = MergeTiles(tilesResolved);

var result2 = 0;

while (true)
{
	var monsterCount = MatchCount(image, monster);

	if (monsterCount > 0)
	{
		var hashCountInImage = image.ToString().Count(ch => ch == '#');

		var hashCountInMonster = string.Join("\n", monster).Count(ch => ch == '#');

		result2 = hashCountInImage - monsterCount * hashCountInMonster;

		break;
	}

	image.ChangeOrientation();
}

Console.WriteLine(result2);


Tile[][] Resolve()
{
	var pairs = new Dictionary<string, List<Tile>>();

	foreach (var tile in tiles)
	{
		for (var i = 0; i < 8; i++)
		{
			var pattern = tile.Top();

			if (!pairs.ContainsKey(pattern))
			{
				pairs[pattern] = new List<Tile>();
			}

			pairs[pattern].Add(tile);

			tile.ChangeOrientation();
		}
	}

	bool isEdge(string pattern) => pairs[pattern].Count == 1;

	Tile? getNeighbour(Tile? tile, string pattern) => pairs[pattern].SingleOrDefault(other => other != tile);

	Tile putTileInPlace(Tile? above, Tile? left)
	{
		if (above == null &&
			left == null)
		{
			foreach (var tile in tiles)
			{
				for (var i = 0; i < 8; i++)
				{
					if (isEdge(tile.Top()) &&
						isEdge(tile.Left()))
					{
						return tile;
					}

					tile.ChangeOrientation();
				}
			}
		}
		else
		{
			var tile = above != null
				? getNeighbour(above, above.Bottom())
				: getNeighbour(left, left!.Right());

			while (true)
			{
				var topMatch = above == null
					? isEdge(tile!.Top())
					: tile!.Top() == above.Bottom();

				var leftMatch = left == null
					? isEdge(tile.Left())
					: tile.Left() == left.Right();

				if (topMatch &&
					leftMatch)
				{
					return tile;
				}

				tile.ChangeOrientation();
			}
		}

		throw new InvalidOperationException();
	}

	var size = (int)Math.Sqrt(tiles.Count);
	var puzzle = new Tile[size][];

	for (var row = 0; row < size; row++)
	{
		puzzle[row] = new Tile[size];

		for (var col = 0; col < size; col++)
		{
			var above = row == 0
				? null
				: puzzle[row - 1][col];

			var left = col == 0
				? null
				: puzzle[row][col - 1];

			puzzle[row][col] = putTileInPlace(above, left);
		}
	}

	return puzzle;
}

Tile MergeTiles(Tile[][] tiles)
{
	var image = new List<string>();
	var tileSize = tiles[0][0].Size;
	var tileCount = tiles.Length;
	for (var irow = 0; irow < tileCount; irow++)
	{
		for (var i = 1; i < tileSize - 1; i++)
		{
			var sb = new StringBuilder();
			for (var icol = 0; icol < tileCount; icol++)
			{
				sb.Append(tiles[irow][icol].Row(i).Substring(1, tileSize - 2));
			}
			image.Add(sb.ToString());
		}
	}

	return new Tile(42, image.ToArray());
}

int MatchCount(Tile image, params string[] pattern)
{
	var res = 0;
	var (ccolP, crowP) = (pattern[0].Length, pattern.Length);

	for (var irow = 0; irow < image.Size - crowP; irow++)
	{
		for (var icol = 0; icol < image.Size - ccolP; icol++)
		{
			bool match()
			{
				for (var icolP = 0; icolP < ccolP; icolP++)
					for (var irowP = 0; irowP < crowP; irowP++)
					{
						if (pattern[irowP][icolP] == '#' &&
							image.GetPixel(irow + irowP, icol + icolP) != '#')
						{
							return false;
						}
					}
				return true;
			}

			if (match())
			{
				res++;
			}
		}
	}

	return res;
}
