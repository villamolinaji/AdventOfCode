var lines = File.ReadAllLinesAsync("Input.txt").Result;

var imageEnhancementAlgorithm = lines[0];

var grid = ReadImage();

var countPixels = EnhanceImage(2);

Console.WriteLine(countPixels);

// Part 2
grid = ReadImage();

countPixels = EnhanceImage(50);

Console.WriteLine(countPixels);


Dictionary<(int x, int y), int> ReadImage()
{
	var linesGrid = lines.Skip(2).ToArray();

	var grid = new Dictionary<(int x, int y), int>(
		from y in Enumerable.Range(0, linesGrid.Length)
		from x in Enumerable.Range(0, linesGrid[0].Length)
		select new KeyValuePair<(int x, int y), int>((x, y), linesGrid[y][x] == '#' ? 1 : 0));

	return grid;
}


int EnhanceImage(int stpes)
{
	var minX = 0;
	var minY = 0;
	var maxX = grid.Keys.MaxBy(p => p.x).x;
	var maxY = grid.Keys.MaxBy(p => p.y).y;

	for (var i = 0; i < stpes; i++)
	{
		var newGrid = new Dictionary<(int x, int y), int>();

		for (var y = minY - 1; y <= maxY + 1; y++)
		{
			for (var x = minX - 1; x <= maxX + 1; x++)
			{

				var point = (x, y);

				var index = 0;

				foreach (var neighbour in GetNeighbours(point))
				{
					index = index * 2 + grid.GetValueOrDefault(neighbour, i % 2);
				}

				newGrid[point] = imageEnhancementAlgorithm[index] == '#'
					? 1
					: 0;
			}
		}

		minX = minX - 1;
		minY = minY - 1;
		maxX = maxX + 1;
		maxY = maxY + 1;

		grid = newGrid;
	}

	return grid.Count(x => x.Value == 1);
}

IEnumerable<(int x, int y)> GetNeighbours((int x, int y) pos) =>
	from y in Enumerable.Range(-1, 3)
	from x in Enumerable.Range(-1, 3)
	select (pos.x + x, pos.y + y);