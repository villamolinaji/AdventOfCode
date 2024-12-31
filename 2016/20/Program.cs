using _20;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var blocks = new List<Block>();

foreach (var line in lines)
{
	var parts = line.Split('-');
	blocks.Add(new Block { From = long.Parse(parts[0]), To = long.Parse(parts[1]) });
}

blocks = blocks.OrderBy(b => b.From).ToList();

long lowest = 0;

foreach (var block in blocks)
{
	if (block.From > lowest)
	{
		break;
	}

	lowest = Math.Max(lowest, block.To + 1);
}

Console.WriteLine(lowest);

// Part 2
lowest = 0;
long allowedCount = 0;

for (int i = 0; i < blocks.Count; i++)
{
	if (blocks[i].From > lowest)
	{
		allowedCount += blocks[i].From - lowest;
	}
	lowest = Math.Max(lowest, blocks[i].To + 1);
}

allowedCount += 4294967295 - lowest + 1;

Console.WriteLine(allowedCount);