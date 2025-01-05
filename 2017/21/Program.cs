using _21;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rules = new List<Rule>();

foreach (var line in lines)
{
	var parts = line.Split(" => ");
	var from = parts[0].Split("/").Select(x => x.ToCharArray().ToList()).ToList();
	var to = parts[1].Split("/").Select(x => x.ToCharArray().ToList()).ToList();

	for (int j = 0; j < 2; j++)
	{
		for (int i = 0; i < 4; i++)
		{
			rules.Add(new Rule { From = from, To = to });
			from = Rotate(from);
		}

		from = Flip(from);
	}
}

var grid = Iterate(5);
var result = grid.Sum(x => x.Count(x => x == '#'));

Console.WriteLine(result);

grid = Iterate(18);
result = grid.Sum(x => x.Count(x => x == '#'));

Console.WriteLine(result);


List<List<char>> Rotate(List<List<char>> input)
{
	var result = new List<List<char>>();
	for (int i = 0; i < input.Count; i++)
	{
		result.Add(new List<char>());
		for (int j = 0; j < input.Count; j++)
		{
			result[i].Add(input[input.Count - j - 1][i]);
		}
	}
	return result;
}

List<List<char>> Flip(List<List<char>> input)
{
	var result = new List<List<char>>();
	for (int i = 0; i < input.Count; i++)
	{
		result.Add(new List<char>());
		for (int j = 0; j < input.Count; j++)
		{
			result[i].Add(input[i][input.Count - j - 1]);
		}
	}
	return result;
}

List<List<char>> Iterate(int iterations)
{
	var grid = new List<List<char>>();
	grid.Add(new List<char> { '.', '#', '.' });
	grid.Add(new List<char> { '.', '.', '#' });
	grid.Add(new List<char> { '#', '#', '#' });

	for (int i = 0; i < iterations; i++)
	{
		var size = grid.Count % 2 == 0
			? 2
			: 3;
		var newSize = grid.Count / size * (size + 1);
		var newGrid = new List<List<char>>();

		for (int j = 0; j < newSize; j++)
		{
			newGrid.Add(new List<char>());
			for (int k = 0; k < newSize; k++)
			{
				newGrid[j].Add('.');
			}
		}

		for (int j = 0; j < grid.Count / size; j++)
		{
			for (int k = 0; k < grid.Count / size; k++)
			{
				var subGrid = new List<List<char>>();
				for (int l = 0; l < size; l++)
				{
					subGrid.Add(new List<char>());
					for (int m = 0; m < size; m++)
					{
						subGrid[l].Add(grid[j * size + l][k * size + m]);
					}
				}

				var subGridString = string.Join("/", subGrid.Select(x => string.Join("", x)));
				var rule = rules.First(x => x.FromString.SequenceEqual(subGridString));

				for (int l = 0; l < size + 1; l++)
				{
					for (int m = 0; m < size + 1; m++)
					{
						newGrid[j * (size + 1) + l][k * (size + 1) + m] = rule.To[l][m];
					}
				}
			}
		}

		grid = newGrid;
	}

	return grid;
}