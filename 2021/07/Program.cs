var input = File.ReadAllTextAsync("Input.txt").Result;

var horizontalPositions = input.Split(',').Select(x => int.Parse(x)).ToList();

var minPosition = horizontalPositions.Min();
var maxPosition = horizontalPositions.Max();

var minFuel = int.MaxValue;
var minFuel2 = int.MaxValue;

for (int i = minPosition; i <= maxPosition; i++)
{
	var newFuel = CalculateFuel(i, false);

	var newFuel2 = CalculateFuel(i, true);

	if (newFuel < minFuel)
	{
		minFuel = newFuel;
	}

	if (newFuel2 < minFuel2)
	{
		minFuel2 = newFuel2;
	}
}

Console.WriteLine(minFuel);

Console.WriteLine(minFuel2);


int CalculateFuel(int position, bool isPart2)
{
	var fuel = 0;

	foreach (var horizontalPosition in horizontalPositions)
	{
		if (!isPart2)
		{
			fuel += Math.Abs(position - horizontalPosition);
		}
		else
		{
			fuel += Sumatory(Math.Abs(position - horizontalPosition));
		}
	}

	return fuel;
}

int Sumatory(int n)
{
	if (n == 0)
	{
		return 0;
	}

	return n + Sumatory(n - 1);
}