using _13;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var layers = new List<Layer>();

foreach (var line in lines)
{
	var parts = line.Split(": ");
	var depth = int.Parse(parts[0]);
	var range = int.Parse(parts[1]);
	layers.Add(new Layer { Depth = depth, Range = range, ScannerPosition = 0, ScannerDirection = 1, IsCaught = false });
}

var severity = 0;

int totalDepth = layers.Max(l => l.Depth);

for (int i = 0; i <= totalDepth; i++)
{
	foreach (var layer in layers)
	{
		if (layer.Depth == i &&
			layer.ScannerPosition == 0)
		{
			layer.IsCaught = true;
		}

		layer.ScannerPosition += layer.ScannerDirection;
		if (layer.ScannerPosition == 0 ||
			layer.ScannerPosition == layer.Range - 1)
		{
			layer.ScannerDirection = -layer.ScannerDirection;
		}
	}
}

foreach (var layer in layers.Where(l => l.IsCaught))
{
	severity += layer.Depth * layer.Range;
}

Console.WriteLine(severity);

// Part 2
int delay = Enumerable.Range(0, int.MaxValue).First(delay => IsSafe(delay));

Console.WriteLine(delay);


bool IsSafe(int delay)
{
	var packetPos = 0;

	foreach (var layer in layers)
	{
		delay += layer.Depth - packetPos;

		packetPos = layer.Depth;

		var scannerPos = delay % (2 * layer.Range - 2);

		if (scannerPos == 0)
		{
			return false;
		}
	}

	return true;
}