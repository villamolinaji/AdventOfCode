using _17;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var recipients = new List<Recipient>();

int index = 1;
foreach (var line in lines)
{
	recipients.Add(new Recipient
	{
		Number = index,
		Capacity = int.Parse(line)
	});

	index++;
}

const int target = 150;

var combinations = 0;

var queue = new Queue<(List<int> used, int currentCapacity)>();

var queueVisited = new HashSet<string>();

foreach (var recipient in recipients)
{
	if (recipient.Capacity == target)
	{
		combinations++;

		queueVisited.Add(recipient.Number.ToString());
	}

	if (recipient.Capacity > target)
	{
		continue;
	}

	queue.Enqueue((new List<int> { recipient.Number }, recipient.Capacity));
}

while (queue.Count > 0)
{
	var (used, currentCapacity) = queue.Dequeue();

	foreach (var recipient in recipients.Where(r => !used.Contains(r.Number)))
	{
		var newCapacity = currentCapacity + recipient.Capacity;

		var newUsed = new List<int>(used) { recipient.Number };

		var key = string.Join(",", newUsed.Order());

		if (queueVisited.Contains(key))
		{
			continue;
		}
		queueVisited.Add(key);

		if (newCapacity == target)
		{
			combinations++;

		}
		else if (newCapacity > target)
		{
			continue;
		}
		else
		{
			queue.Enqueue((newUsed, newCapacity));
		}
	}
}

Console.WriteLine(combinations);


// Part 2
var minContainers = int.MaxValue;

queue = new Queue<(List<int> used, int currentCapacity)>();

queueVisited = new HashSet<string>();

foreach (var recipient in recipients)
{
	if (recipient.Capacity == target)
	{
		minContainers = Math.Min(minContainers, 1);

		queueVisited.Add(recipient.Number.ToString());
	}

	if (recipient.Capacity > target)
	{
		continue;
	}

	queue.Enqueue((new List<int> { recipient.Number }, recipient.Capacity));
}

while (queue.Count > 0)
{
	var (used, currentCapacity) = queue.Dequeue();

	foreach (var recipient in recipients.Where(r => !used.Contains(r.Number)))
	{
		var newCapacity = currentCapacity + recipient.Capacity;

		var newUsed = new List<int>(used) { recipient.Number };

		var key = string.Join(",", newUsed.Order());

		if (queueVisited.Contains(key))
		{
			continue;
		}
		queueVisited.Add(key);

		if (newCapacity == target)
		{
			minContainers = Math.Min(minContainers, newUsed.Count);

		}
		else if (newCapacity > target)
		{
			continue;
		}
		else
		{
			queue.Enqueue((newUsed, newCapacity));
		}
	}
}


combinations = 0;

queue = new Queue<(List<int> used, int currentCapacity)>();

queueVisited = new HashSet<string>();

foreach (var recipient in recipients)
{
	if (recipient.Capacity == target && minContainers == 1)
	{
		combinations++;

		queueVisited.Add(recipient.Number.ToString());
	}

	if (recipient.Capacity > target)
	{
		continue;
	}

	queue.Enqueue((new List<int> { recipient.Number }, recipient.Capacity));
}

while (queue.Count > 0)
{
	var (used, currentCapacity) = queue.Dequeue();

	foreach (var recipient in recipients.Where(r => !used.Contains(r.Number)))
	{
		var newCapacity = currentCapacity + recipient.Capacity;

		var newUsed = new List<int>(used) { recipient.Number };

		var key = string.Join(",", newUsed.Order());

		if (queueVisited.Contains(key))
		{
			continue;
		}
		queueVisited.Add(key);

		if (newCapacity == target && newUsed.Count == minContainers)
		{
			combinations++;

		}
		else if (newCapacity > target)
		{
			continue;
		}
		else
		{
			queue.Enqueue((newUsed, newCapacity));
		}
	}
}

Console.WriteLine(combinations);