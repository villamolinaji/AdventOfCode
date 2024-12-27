using _13;

var lines = File.ReadAllLinesAsync("input.txt").Result;

var attendees = new List<Attendee>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var from = parts[0];
	var to = parts[10].TrimEnd('.');
	var happiness = int.Parse(parts[3]) * (parts[2] == "gain" ? 1 : -1);

	attendees.Add(new Attendee { From = from, To = to, Happiness = happiness });
}

var persons = attendees.Select(a => a.From).Distinct().ToList();

int maxHappiness = GetMaxHappiness();

Console.WriteLine(maxHappiness);

// Part 2
foreach (var person in persons)
{
	attendees.Add(new Attendee { From = "Me", To = person, Happiness = 0 });
	attendees.Add(new Attendee { From = person, To = "Me", Happiness = 0 });
}

persons = attendees.Select(a => a.From).Distinct().ToList();

maxHappiness = GetMaxHappiness();

Console.WriteLine(maxHappiness);


int GetMaxHappiness()
{
	int maxHappiness = 0;

	var queue = new Queue<(string person, List<string> table, int happiness)>();

	foreach (var person in persons)
	{
		queue.Enqueue((person, new List<string> { person }, 0));
	}

	while (queue.Count > 0)
	{
		var (person, table, happiness) = queue.Dequeue();

		if (table.Count == persons.Count)
		{
			var firstPerson = table[0];
			var lastPerson = table[table.Count - 1];

			var nextHappinessR = attendees.First(a => a.From == firstPerson && a.To == lastPerson).Happiness;
			var nextHappinessL = attendees.First(a => a.To == firstPerson && a.From == lastPerson).Happiness;
			var nextHappiness = nextHappinessR + nextHappinessL;

			happiness += nextHappiness;

			maxHappiness = Math.Max(maxHappiness, happiness);

			continue;
		}

		foreach (var nextPerson in persons)
		{
			if (table.Contains(nextPerson))
			{
				continue;
			}

			var nextHappinessR = attendees.First(a => a.From == person && a.To == nextPerson).Happiness;
			var nextHappinessL = attendees.First(a => a.To == person && a.From == nextPerson).Happiness;
			var nextHappiness = nextHappinessR + nextHappinessL;

			var nextTable = new List<string>(table) { nextPerson };

			queue.Enqueue((nextPerson, nextTable, happiness + nextHappiness));
		}
	}

	return maxHappiness;
}