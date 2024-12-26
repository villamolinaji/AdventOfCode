var lines = File.ReadAllLinesAsync("Input.txt").Result;

var grid = new bool[1000, 1000];

foreach (var line in lines)
{
	if (line.StartsWith("turn on"))
	{
		var lineReplace = line.Replace("turn on ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid[x, y] = true;
			}
		}
	}
	else if (line.StartsWith("turn off"))
	{
		var lineReplace = line.Replace("turn off ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid[x, y] = false;
			}
		}
	}
	else if (line.StartsWith("toggle"))
	{
		var lineReplace = line.Replace("toggle ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid[x, y] = !grid[x, y];
			}
		}
	}
}

var result = 0;
for (int x = 0; x < 1000; x++)
{
	for (int y = 0; y < 1000; y++)
	{
		if (grid[x, y])
		{
			result++;
		}
	}
}

Console.WriteLine(result);

// Part 2
var grid2 = new int[1000, 1000];

for (int x = 0; x < 1000; x++)
{
	for (int y = 0; y < 1000; y++)
	{
		grid2[x, y] = 0;
	}
}

foreach (var line in lines)
{
	if (line.StartsWith("turn on"))
	{
		var lineReplace = line.Replace("turn on ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid2[x, y]++;
			}
		}
	}
	else if (line.StartsWith("turn off"))
	{
		var lineReplace = line.Replace("turn off ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid2[x, y]--;

				if (grid2[x, y] < 0)
				{
					grid2[x, y] = 0;
				}
			}
		}
	}
	else if (line.StartsWith("toggle"))
	{
		var lineReplace = line.Replace("toggle ", "");

		var parts = lineReplace.Split(" through ");
		var start = parts[0].Split(",");
		var end = parts[1].Split(",");
		var startX = int.Parse(start[0]);
		var startY = int.Parse(start[1]);
		var endX = int.Parse(end[0]);
		var endY = int.Parse(end[1]);

		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				grid2[x, y] += 2;
			}
		}
	}
}

var totalBrightness = 0;
for (int x = 0; x < 1000; x++)
{
	for (int y = 0; y < 1000; y++)
	{
		totalBrightness += grid2[x, y];
	}
}

Console.WriteLine(totalBrightness);