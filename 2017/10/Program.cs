var lengths = File.ReadAllText("Input.txt")
	.Split(',')
	.Select(int.Parse)
	.ToArray();

int elementCount = 256;

var list = Enumerable.Range(0, elementCount).ToArray();

var position = 0;
var skipSize = 0;

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

var result = list[0] * list[1];
Console.WriteLine(result);

// Part 2
var lengthsString = File.ReadAllText("Input.txt");
var lengths2 = new List<int>();

foreach (var c in lengthsString)
{
	lengths2.Add((int)c);
}
lengths2.AddRange(new[] { 17, 31, 73, 47, 23 });

list = Enumerable.Range(0, elementCount).ToArray();

position = 0;
skipSize = 0;

for (int t = 0; t < 64; t++)
{
	foreach (var length in lengths2)
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

Console.WriteLine(knotHash);