var lines = File.ReadAllLinesAsync("Input.txt").Result;

var bagRules = new Dictionary<string, List<(string, int)>>();

foreach (var line in lines)
{
	var parts = line.Split(" bags contain ");
	var bagColor = parts[0];
	var contains = parts[1].Split(", ");
	var containsColors = new List<(string, int)>();

	foreach (var contain in contains)
	{
		if (contain == "no other bags.")
		{
			continue;
		}

		var containParts = contain.Split(" ");
		var count = int.Parse(containParts[0]);
		var color = containParts[1] + " " + containParts[2];

		containsColors.Add((color, count));
	}

	bagRules.Add(bagColor, containsColors);
}

var bagsUsed = new HashSet<string>();

foreach (var bagColor in bagRules.Where(r => r.Key != "shiny gold").Select(r => r.Key))
{
	var containsShinyGold = false;
	var queue = new Queue<string>();
	queue.Enqueue(bagColor);

	while (queue.Count > 0)
	{
		var currentColor = queue.Dequeue();

		if (currentColor == "shiny gold")
		{
			containsShinyGold = true;

			break;
		}

		foreach (var color in bagRules[currentColor])
		{
			queue.Enqueue(color.Item1);
		}
	}

	if (containsShinyGold)
	{
		bagsUsed.Add(bagColor);
	}
}

Console.WriteLine(bagsUsed.Count);

// Part 2
var totalBags = 0;

var queue2 = new Queue<(string, int)>();

queue2.Enqueue(("shiny gold", 1));

while (queue2.Count > 0)
{
	var (currentColor, currentCount) = queue2.Dequeue();

	foreach (var color in bagRules[currentColor])
	{
		totalBags += currentCount * color.Item2;

		queue2.Enqueue((color.Item1, currentCount * color.Item2));
	}
}

Console.WriteLine(totalBags);