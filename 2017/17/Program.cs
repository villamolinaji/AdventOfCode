var times = 2018;
var steps = 366;

var buffer = new List<int>();
buffer.Add(0);

int currentPosition = 0;

for (int i = 1; i < times; i++)
{
	currentPosition = (currentPosition + steps) % buffer.Count + 1;

	buffer.Insert(currentPosition, i);
}

Console.WriteLine(buffer[(currentPosition + 1) % buffer.Count]);

// Part 2
currentPosition = 0;

var result = 0;

var bufferCount = 1;

for (int i = 1; i <= 50000000; i++)
{
	currentPosition = (currentPosition + steps) % bufferCount + 1;

	if (currentPosition == 1)
	{
		result = i;
	}

	bufferCount++;
}

Console.WriteLine(result);