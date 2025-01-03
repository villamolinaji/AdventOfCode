var inputStartA = 699;
var inputStartB = 124;

var startA = inputStartA;
var startB = inputStartB;

var factorA = 16807;
var factorB = 48271;

var div = 2147483647;

var evaluations = 40000000;

int matches = 0;

for (int i = 0; i < evaluations; i++)
{
	startA = (int)((long)startA * factorA % div);
	startB = (int)((long)startB * factorB % div);

	var binaryA = Convert.ToString(startA, 2).PadLeft(32, '0');
	var binaryB = Convert.ToString(startB, 2).PadLeft(32, '0');

	if (binaryA.Substring(16) == binaryB.Substring(16))
	{
		matches++;
	}
}

Console.WriteLine(matches);

// Part 2
evaluations = 5000000;
matches = 0;
startA = inputStartA;
startB = inputStartB;


for (int i = 0; i < evaluations; i++)
{
	startA = (int)((long)startA * factorA % div);
	while (startA % 4 != 0)
	{
		startA = (int)((long)startA * factorA % div);
	}

	startB = (int)((long)startB * factorB % div);
	while (startB % 8 != 0)
	{
		startB = (int)((long)startB * factorB % div);
	}

	var binaryA = Convert.ToString(startA, 2).PadLeft(32, '0');
	var binaryB = Convert.ToString(startB, 2).PadLeft(32, '0');

	if (binaryA.Substring(16) == binaryB.Substring(16))
	{
		matches++;
	}
}

Console.WriteLine(matches);