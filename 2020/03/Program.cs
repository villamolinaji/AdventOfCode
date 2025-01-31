var lines = File.ReadAllLinesAsync("Input.txt").Result;

var cols = lines[0].Length;

var row = 0;
var col = 0;

var countTrees = 0;

while (row < lines.Length)
{
	if (lines[row][col] == '#')
	{
		countTrees++;
	}
	row++;

	col = (col + 3) % cols;
}

Console.WriteLine(countTrees);

// Part 2
var slopes = new (int, int)[]
{
	(1, 1),
	(3, 1),
	(5, 1),
	(7, 1),
	(1, 2)
};

var result = slopes
	.Select(slope =>
	{
		var right = slope.Item1;
		var down = slope.Item2;
		var row = 0;
		var col = 0;
		var countTrees = 0L;

		while (row < lines.Length)
		{
			if (lines[row][col] == '#')
			{
				countTrees++;
			}

			row += down;

			col = (col + right) % cols;
		}

		return countTrees;
	})
	.Aggregate((a, b) => a * b);

Console.WriteLine(result);