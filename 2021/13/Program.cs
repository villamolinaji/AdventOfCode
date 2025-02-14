using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var dots = new HashSet<(int x, int y)>();

var folds = new List<(int x, int y)>();

ReadInput();

Console.WriteLine(GetFolds().First().Count);

// Part 2
ReadInput();

Console.WriteLine(GetImage(GetFolds().Last()));


void ReadInput()
{
	dots = new HashSet<(int x, int y)>();
	folds = new List<(int x, int y)>();

	foreach (var line in lines)
	{
		if (string.IsNullOrEmpty(line))
		{
			continue;
		}

		if (line.StartsWith("fold"))
		{
			var parts = line.Split(' ');

			var parts2 = parts[2].Split('=');

			var x = parts2[0] == "x"
				? int.Parse(parts2[1])
				: 0;

			var y = parts2[0] == "y"
				? int.Parse(parts2[1])
				: 0;

			folds.Add((x, y));
		}
		else
		{
			var parts = line.Split(',');
			dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
		}
	}
}

IEnumerable<HashSet<(int x, int y)>> GetFolds()
{
	foreach (var fold in folds)
	{
		if (fold.x > 0)
		{
			dots = FoldX(fold.x, dots);
		}
		else
		{
			dots = FoldY(fold.y, dots);
		}

		yield return dots;
	}
}

HashSet<(int x, int y)> FoldX(int x, HashSet<(int x, int y)> d) =>
	d.Select(p => p.x > x ? p with { x = 2 * x - p.x } : p).ToHashSet();

HashSet<(int x, int y)> FoldY(int y, HashSet<(int x, int y)> d) =>
	d.Select(p => p.y > y ? p with { y = 2 * y - p.y } : p).ToHashSet();

string GetImage(HashSet<(int x, int y)> d)
{
	var image = new StringBuilder();
	var height = d.MaxBy(p => p.y).y;
	var width = d.MaxBy(p => p.x).x;

	for (var y = 0; y <= height; y++)
	{
		for (var x = 0; x <= width; x++)
		{
			image.Append(d.Contains((x, y)) ? '#' : ' ');
		}
		image.Append("\n");
	}

	return image.ToString();
}