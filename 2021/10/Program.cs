var lines = File.ReadAllLinesAsync("Input.txt").Result;

var opens = new char[] { '(', '[', '{', '<' };
var closes = new char[] { ')', ']', '}', '>' };

var totalPoints = 0;

foreach (var line in lines)
{
	totalPoints += GetLinePoints(line);
}

Console.WriteLine(totalPoints);

// Part 2
var scores = new List<long>();

foreach (var line in lines)
{
	var score = GetLinePoints2(line);

	if (score > 0)
	{
		scores.Add(score);
	}
}

scores.Sort();

var result = scores[scores.Count / 2];

Console.WriteLine(result);


int GetLinePoints(string line)
{
	var points = 0;

	var stack = new Stack<char>();

	foreach (var l in line)
	{
		if (opens.Contains(l))
		{
			stack.Push(l);
		}
		else if (closes.Contains(l))
		{
			var latestOpen = stack.Pop();

			if ((latestOpen == '(' && l != ')') ||
				(latestOpen == '[' && l != ']') ||
				(latestOpen == '{' && l != '}') ||
				(latestOpen == '<' && l != '>'))
			{
				switch (l)
				{
					case ')':
						points = 3;
						break;
					case ']':
						points = 57;
						break;
					case '}':
						points = 1197;
						break;
					case '>':
						points = 25137;
						break;
				}
			}
		}
	}

	return points;
}

long GetLinePoints2(string line)
{
	var points = 0L;

	var stack = new Stack<char>();

	foreach (var l in line)
	{
		if (opens.Contains(l))
		{
			stack.Push(l);
		}
		else if (closes.Contains(l))
		{
			var latestOpen = stack.Pop();

			if ((latestOpen == '(' && l != ')') ||
				(latestOpen == '[' && l != ']') ||
				(latestOpen == '{' && l != '}') ||
				(latestOpen == '<' && l != '>'))
			{
				return 0;
			}
		}
	}

	while (stack.Count > 0)
	{
		var latestOpen = stack.Pop();
		var closePoints = 0;

		switch (latestOpen)
		{
			case '(':
				closePoints = 1;
				break;
			case '[':
				closePoints = 2;
				break;
			case '{':
				closePoints = 3;
				break;
			case '<':
				closePoints = 4;
				break;
		}

		points *= 5;

		points += closePoints;
	}

	return points;
}