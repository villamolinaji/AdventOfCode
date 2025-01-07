using _03;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rectangles = lines.Select(line =>
{
	var parts = line.Split(' ');
	var id = int.Parse(parts[0].Substring(1));
	var col = int.Parse(parts[2].Split(',')[0]);
	var row = int.Parse(parts[2].Split(',')[1].TrimEnd(':'));
	var width = int.Parse(parts[3].Split('x')[0]);
	var height = int.Parse(parts[3].Split('x')[1]);
	return new Rectangle
	{
		Id = id,
		Col = col,
		Row = row,
		Width = width,
		Height = height
	};
}).ToList();

var grid = new Dictionary<(int, int), int>();
foreach (var rectangle in rectangles)
{
	for (int i = 0; i < rectangle.Height; i++)
	{
		for (int j = 0; j < rectangle.Width; j++)
		{
			var key = (rectangle.Row + i, rectangle.Col + j);
			if (grid.ContainsKey(key))
			{
				grid[key]++;
			}
			else
			{
				grid[key] = 1;
			}
		}
	}
}

var result = grid.Values.Count(x => x > 1);
Console.WriteLine(result);

// Par 2
var grid2 = new Dictionary<(int, int), List<int>>();
foreach (var rectangle in rectangles)
{
	for (int i = 0; i < rectangle.Height; i++)
	{
		for (int j = 0; j < rectangle.Width; j++)
		{
			var key = (rectangle.Row + i, rectangle.Col + j);
			if (grid2.ContainsKey(key))
			{
				grid2[key].Add(rectangle.Id);
			}
			else
			{
				grid2[key] = new List<int>() { rectangle.Id };
			}
		}
	}
}

var duplicates = grid2.Where(x => x.Value.Count > 1).Select(x => x.Value).SelectMany(x => x).Distinct().ToList();
var result2 = rectangles.Where(x => !duplicates.Contains(x.Id)).Select(x => x.Id).ToList();
Console.WriteLine(result2[0]);
