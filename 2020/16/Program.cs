var lines = File.ReadAllLinesAsync("Input.txt").Result;

var myTicket = new List<int>();
var nearbyTickets = new List<List<int>>();
var validRanges = new Dictionary<string, List<(int, int)>>();

var indexLine = 0;
while (indexLine < lines.Length)
{
	var line = lines[indexLine];

	if (line == string.Empty)
	{
		indexLine++;
		continue;
	}
	else if (line == "your ticket:")
	{
		indexLine++;
		myTicket = lines[indexLine].Split(",").Select(int.Parse).ToList();
		indexLine++;
	}
	else if (line == "nearby tickets:")
	{
		indexLine++;

		while (indexLine < lines.Length)
		{
			nearbyTickets.Add(lines[indexLine].Split(",").Select(int.Parse).ToList());
			indexLine++;
		}
	}
	else
	{
		var parts = line.Split(": ");
		var ranges = parts[1].Split(" or ");
		var rangeList = new List<(int, int)>();

		foreach (var range in ranges)
		{
			var rangeParts = range.Split("-");
			rangeList.Add((int.Parse(rangeParts[0]), int.Parse(rangeParts[1])));
		}

		validRanges[parts[0]] = rangeList;

		indexLine++;
	}
}

var errorRate = 0;

foreach (var ticket in nearbyTickets)
{
	foreach (var value in ticket)
	{
		var isValid = IsValid(value);

		if (!isValid)
		{
			errorRate += value;
		}
	}
}

Console.WriteLine(errorRate);

// Part 2
var validTickets = new List<List<int>>();
foreach (var ticket in nearbyTickets)
{
	var isValidTicket = true;
	foreach (var value in ticket)
	{
		var isValid = IsValid(value);
		if (!isValid)
		{
			isValidTicket = false;
			break;
		}
	}

	if (isValidTicket)
	{
		validTickets.Add(ticket);
	}
}

var fieldPositions = new Dictionary<string, List<int>>();

foreach (var range in validRanges)
{
	var positions = new List<int>();
	for (var i = 0; i < myTicket.Count; i++)
	{
		var isValid = true;

		foreach (var ticket in validTickets)
		{
			if (!IsValid2(ticket[i], range.Value))
			{
				isValid = false;
				break;
			}
		}

		if (isValid)
		{
			positions.Add(i);
		}
	}

	fieldPositions[range.Key] = positions;
}

var fieldPositionsOrdered = fieldPositions.OrderBy(x => x.Value.Count).ToList();

var fieldPositionsFinal = new Dictionary<string, int>();

foreach (var fieldPosition in fieldPositionsOrdered)
{
	var position = fieldPosition.Value.Except(fieldPositionsFinal.Values).First();
	fieldPositionsFinal[fieldPosition.Key] = position;
}

var result = 1L;

foreach (var fieldPosition in fieldPositionsFinal)
{
	if (fieldPosition.Key.StartsWith("departure"))
	{
		result *= myTicket[fieldPosition.Value];
	}
}

Console.WriteLine(result);


bool IsValid(int value)
{
	var isValid = false;

	foreach (var range in validRanges.Values.SelectMany(x => x))
	{
		if (value >= range.Item1 && value <= range.Item2)
		{
			isValid = true;
			break;
		}
	}

	return isValid;
}

bool IsValid2(int value, List<(int, int)> ranges)
{
	var isValid = false;

	foreach (var range in ranges)
	{
		if (value >= range.Item1 && value <= range.Item2)
		{
			isValid = true;
			break;
		}
	}

	return isValid;
}