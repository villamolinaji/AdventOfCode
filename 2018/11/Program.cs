var serialNumber = 9306;

var largestPowerLevelX = 0;
var largestPowerLevelY = 0;
var largestPowerLevelSize = 0;

(largestPowerLevelX, largestPowerLevelY, largestPowerLevelSize) = Resolve(3);

Console.WriteLine($"{largestPowerLevelX},{largestPowerLevelY},{largestPowerLevelSize}");

// Part 2
(largestPowerLevelX, largestPowerLevelY, largestPowerLevelSize) = Resolve(300);

Console.WriteLine($"{largestPowerLevelX},{largestPowerLevelY},{largestPowerLevelSize}");


int GetPowerLevel(int x, int y)
{
	var rackId = x + 10;

	var powerLevel = rackId * y;

	powerLevel += serialNumber;

	powerLevel *= rackId;

	powerLevel = (powerLevel / 100) % 10;

	powerLevel -= 5;

	return powerLevel;
}

(int largestPowerLevelX, int largestPowerLevelY, int largestPowerLevelSize) Resolve(int squareSize)
{
	var largestPowerLevel = int.MinValue;
	var largestPowerLevelX = 0;
	var largestPowerLevelY = 0;
	var largestPowerLevelSize = 0;

	var gridPowerLevel = new int[300, 300];
	for (var x = 0; x < 300; x++)
	{
		for (var y = 0; y < 300; y++)
		{
			gridPowerLevel[x, y] = GetPowerLevel(x + 1, y + 1);
		}
	}

	var gridPowerLevelSize = new int[300, 300];

	for (int size = 1; size <= squareSize; size++)
	{
		for (int x = 0; x < 300 - size; x++)
		{
			for (int y = 0; y < 300 - size; y++)
			{
				gridPowerLevelSize[x, y] += gridPowerLevel[x + size - 1, y];
			}
		}

		for (int x = 01; x < 300 - size; x++)
		{
			for (int y = 0; y < 300 - size; y++)
			{
				var powerLevel = 0;

				for (int i = 0; i < size; i++)
				{
					powerLevel += gridPowerLevelSize[x, y + i];

				}

				if (powerLevel > largestPowerLevel)
				{
					largestPowerLevel = powerLevel;
					largestPowerLevelX = x + 1;
					largestPowerLevelY = y + 1;
					largestPowerLevelSize = size;
				}
			}
		}
	}

	return (largestPowerLevelX, largestPowerLevelY, largestPowerLevelSize);
}