var lines = File.ReadAllLinesAsync("Input.txt").Result;

var numbers = lines.Select(l => long.Parse(l)).ToList();

var previousNumbers = 25;

var numbersQ = new Queue<long>();

var invalidNumber = 0L;

for (int i = 0; i < numbers.Count; i++)
{
	var currentNumber = numbers[i];

	if (numbersQ.Count < previousNumbers)
	{
		numbersQ.Enqueue(currentNumber);

		continue;
	}

	var isValid = false;

	var numbersAux = new List<long>();

	foreach (var number in numbersQ)
	{
		numbersAux.Add(number);
	}

	foreach (var number in numbersAux)
	{
		if (numbersAux.Any(n => n + number == currentNumber))
		{
			isValid = true;
		}
	}

	if (!isValid)

	{
		invalidNumber = currentNumber;

		break;
	}
	else
	{
		numbersQ.Dequeue();

		numbersQ.Enqueue(currentNumber);
	}
}

Console.WriteLine(invalidNumber);

// Part 2
var smallest = 0L;
var largest = 0L;

for (int i = 0; i < numbers.Count; i++)
{
	var sum = numbers[i];

	for (int j = i + 1; j < lines.Length; j++)
	{
		sum += numbers[j];

		if (sum > invalidNumber)
		{
			break;
		}
		else if (sum == invalidNumber)
		{
			var range = numbers.ToList().GetRange(i, j - i + 1);

			smallest = range.Min();
			largest = range.Max();

			break;
		}
	}
}

Console.WriteLine(smallest + largest);