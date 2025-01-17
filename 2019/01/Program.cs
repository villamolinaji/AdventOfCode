var lines = File.ReadAllLinesAsync("Input.txt").Result;

int totalFuel = 0;

foreach (var line in lines)
{
	var mass = int.Parse(line);

	var fuel = (mass / 3) - 2;

	totalFuel += fuel;
}

Console.WriteLine(totalFuel);

// Part 2
totalFuel = 0;

foreach (var line in lines)
{
	var mass = int.Parse(line);

	var fuel = CalculateFuel(mass);

	totalFuel += fuel;
}

Console.WriteLine(totalFuel);


int CalculateFuel(int mass)
{
	var fuel = (mass / 3) - 2;

	if (fuel <= 0)
	{
		return 0;
	}

	return fuel + CalculateFuel(fuel);
}