using MoreLinq;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var groups = lines
	.Where(x => !string.IsNullOrWhiteSpace(x))
	.Batch(18)
	.Select(g =>
	{
		var a = Convert.ToInt32(g[4].Split()[^1]);
		var b = Convert.ToInt32(g[5].Split()[^1]);
		var c = Convert.ToInt32(g[15].Split()[^1]);

		return (a: a == 26, b, c);
	})
	.Select((value, index) => (index, value));

var largestModelNumber = new int[14];
var smallestModelNumber = new int[14];

var stack = new Stack<(int i, int c)>();

foreach (var (i, (a, b, c)) in groups)
{
	if (a)
	{
		var (j, d) = stack.Pop();

		var diff = b + d;

		if (diff > 0)
		{
			largestModelNumber[i] = 9;
			largestModelNumber[j] = 9 - diff;

			smallestModelNumber[j] = 1;
			smallestModelNumber[i] = 1 + diff;
		}
		else
		{
			largestModelNumber[j] = 9;
			largestModelNumber[i] = 9 + diff;

			smallestModelNumber[i] = 1;
			smallestModelNumber[j] = 1 - diff;
		}
	}
	else
	{
		stack.Push((i, c));
	}
}

Console.WriteLine(string.Join("", largestModelNumber));

Console.WriteLine(string.Join("", smallestModelNumber));
