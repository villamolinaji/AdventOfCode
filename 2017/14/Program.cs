var input = "hxtvlmkl";

var grid = new List<string>();

for (int i = 0; i < 128; i++)
{
	var keyString = $"{input}-{i}";

	var knotHash = GetKnotHash(keyString);

	grid.Add(HexToBinary(knotHash));
}

var squaresUsed = grid.Sum(g => g.Count(gg => gg == '1'));

Console.WriteLine(squaresUsed);

// Part 2
var currentRegion = 1;
var grid2 = grid.Select(g => g.Select(c => c == '1' ? '#' : '.').ToArray()).ToArray();

while (grid2.Any(row => row.Contains('#')))
{
	var start = (grid2.Select((row, i) => (row, i)).First(r => r.row.Contains('#')).i, grid2.First(row => row.Contains('#')).ToList().IndexOf('#'));
	var queue = new Queue<(int x, int y)>();

	queue.Enqueue(start);

	while (queue.Count > 0)
	{
		var current = queue.Dequeue();

		if (current.x < 0 ||
			current.x >= 128 ||
			current.y < 0 ||
			current.y >= 128 ||
			grid2[current.x][current.y] != '#')
		{
			continue;
		}

		grid2[current.x][current.y] = (char)(currentRegion + '0');

		queue.Enqueue((current.x - 1, current.y));
		queue.Enqueue((current.x + 1, current.y));
		queue.Enqueue((current.x, current.y - 1));
		queue.Enqueue((current.x, current.y + 1));
	}

	currentRegion++;
}

Console.WriteLine(currentRegion - 1);


string HexToBinary(string text)
{
	string binary = String.Join("", text.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

	return binary;
}

string GetKnotHash(string key)
{
	var lengths = new List<int>();

	foreach (var c in key)
	{
		lengths.Add((int)c);
	}
	lengths.AddRange(new[] { 17, 31, 73, 47, 23 });

	int elementCount = 256;

	var list = Enumerable.Range(0, elementCount).ToArray();

	var position = 0;
	var skipSize = 0;

	for (int t = 0; t < 64; t++)
	{
		foreach (var length in lengths)
		{
			var sublist = new List<int>();

			for (int i = 0; i < length; i++)
			{
				sublist.Add(list[(i + position) % list.Length]);
			}

			sublist.Reverse();

			for (int i = 0; i < sublist.Count; i++)
			{
				list[(i + position) % list.Length] = sublist[i];
			}

			position += length + skipSize;

			skipSize++;
		}
	}

	var denseHash = new List<int>();
	for (int i = 0; i < 16; i++)
	{
		var hash = list.Skip(i * 16).Take(16).Aggregate((a, b) => a ^ b);
		denseHash.Add(hash);
	}

	var knotHash = string.Join("", denseHash.Select(x => x.ToString("x2")));

	return knotHash;
}