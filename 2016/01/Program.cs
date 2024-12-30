var instructions = File.ReadAllTextAsync("Input.txt").Result;

var currentX = 0;
var currentY = 0;
var currentDirection = 1;

foreach (var instruction in instructions.Split(", "))
{
	var turn = instruction[0];
	var distance = int.Parse(instruction.Substring(1));

	if (turn == 'R')
	{
		currentDirection++;
	}
	else
	{
		currentDirection--;
	}

	if (currentDirection == 0)
	{
		currentDirection = 4;
	}
	else if (currentDirection == 5)
	{
		currentDirection = 1;
	}

	switch (currentDirection)
	{
		case 1:
			currentY += distance;
			break;
		case 2:
			currentX += distance;
			break;
		case 3:
			currentY -= distance;
			break;
		case 4:
			currentX -= distance;
			break;
	}
}

var totalDistance = Math.Abs(currentX) + Math.Abs(currentY);

Console.WriteLine(totalDistance);

// Part 2
currentX = 0;
currentY = 0;
currentDirection = 1;

var visitedLocations = new HashSet<(int x, int y)>();
visitedLocations.Add((currentX, currentY));

var instructionIndex = 0;
var instructionsSplit = instructions.Split(", ");

while (true)
{
	var instruction = instructionsSplit[instructionIndex];

	var turn = instruction[0];
	var distance = int.Parse(instruction.Substring(1));

	if (turn == 'R')
	{
		currentDirection++;
	}
	else
	{
		currentDirection--;
	}

	if (currentDirection == 0)
	{
		currentDirection = 4;
	}
	else if (currentDirection == 5)
	{
		currentDirection = 1;
	}

	bool isVisited = false;
	for (int i = 0; i < distance; i++)
	{
		switch (currentDirection)
		{
			case 1:
				currentY++;
				break;
			case 2:
				currentX++;
				break;
			case 3:
				currentY--;
				break;
			case 4:
				currentX--;
				break;
		}

		if (visitedLocations.Contains((currentX, currentY)))
		{
			isVisited = true;
			break;
		}
		visitedLocations.Add((currentX, currentY));
	}

	if (isVisited)
	{
		break;
	}

	instructionIndex++;

	if (instructionIndex == instructionsSplit.Length)
	{
		instructionIndex = 0;
	}
}

totalDistance = Math.Abs(currentX) + Math.Abs(currentY);

Console.WriteLine(totalDistance);