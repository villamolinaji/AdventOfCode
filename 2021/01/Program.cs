var lines = File.ReadAllLinesAsync("Input.txt").Result;

var depths = lines.Select(l => int.Parse(l)).ToList();

var countIncrements = 0;
for (int i = 0; i < depths.Count - 1; i++)
{
	if (depths[i] < depths[i + 1])
	{
		countIncrements++;
	}
}

Console.WriteLine(countIncrements);

// Part 2
countIncrements = 0;

var queueA = new Queue<int>();
var queueB = new Queue<int>();

for (int i = 0; i < depths.Count - 1; i++)
{
	queueA.Enqueue(depths[i]);
	queueB.Enqueue(depths[i + 1]);

	if (queueA.Count > 3)
	{
		queueA.Dequeue();
	}

	if (queueB.Count > 3)
	{
		queueB.Dequeue();
	}

	if (queueA.Count == 3 && queueB.Count == 3)
	{
		var sumA = queueA.Sum();
		var sumB = queueB.Sum();
		if (sumA < sumB)
		{
			countIncrements++;
		}
	}
}

Console.WriteLine(countIncrements);