var input = File.ReadAllTextAsync("Input.txt").Result;

var result = input.Count(c => c == '(') - input.Count(c => c == ')');

Console.WriteLine(result);

// Par 2
int currentFloor = 0;
int position = 0;

foreach (var c in input)
{
	position++;

	if (c == '(')
	{
		currentFloor++;
	}
	else
	{
		currentFloor--;
	}

	if (currentFloor == -1)
	{
		break;
	}
}

Console.WriteLine(position);
