var lines = File.ReadAllLinesAsync("Input.txt").Result;

var groups = new List<List<string>>();

var persons = new List<string>();

foreach (var line in lines)
{
	if (string.IsNullOrWhiteSpace(line))
	{
		groups.Add(persons);
		persons = new List<string>();
	}
	else
	{
		persons.Add(line);
	}
}

groups.Add(persons);

var sum = 0;

var sum2 = 0;

foreach (var group in groups)
{
	var answers = group.SelectMany(x => x).Distinct().ToList();
	sum += answers.Count;

	var answers2 = answers.Where(x => group.All(y => y.Contains(x))).ToList();
	sum2 += answers2.Count;
}

Console.WriteLine(sum);

Console.WriteLine(sum2);