int target = 29000000;

var presents = CalculatePresents(1000000, 10);

var house = GetHouse();

Console.WriteLine(house);

// Part 2
presents = CalculatePresents(50, 11);

house = GetHouse();

Console.WriteLine(house);


int[] CalculatePresents(int maxVisits, int presentsPerHouse)
{
	var presents = new int[1000000];

	for (var i = 1; i < presents.Length; i++)
	{
		var j = i;
		var step = 0;

		while (j < presents.Length &&
			step < maxVisits)
		{
			presents[j] += presentsPerHouse * i;

			j += i;

			step++;
		}
	}

	return presents;
}

int GetHouse()
{
	for (var i = 0; i < presents.Length; i++)
	{
		if (presents[i] >= target)
		{
			return i;
		}
	}

	return -1;
}