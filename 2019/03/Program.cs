var lines = File.ReadAllLinesAsync("Input.txt").Result;

var instructions1 = lines[0].Split(',').ToArray();
var instructions2 = lines[1].Split(',').ToArray();

var visitedPoints = new Dictionary<(int x, int y), int>();

var colisions = new List<(int x, int y, int steps)>();

IterateInstructions(instructions1);

IterateInstructions(instructions2);

var closestColision = colisions.OrderBy(c => GetManhattanDistance(c.x, c.y)).First();

Console.WriteLine(GetManhattanDistance(closestColision.x, closestColision.y));

// Part 2
var closestColisionSteps = colisions.OrderBy(c => c.steps).First().steps;

Console.WriteLine(closestColisionSteps);


int GetManhattanDistance(int x, int y)
{
	return Math.Abs(x) + Math.Abs(y);
}

void IterateInstructions(string[] instructions)
{
	var currentX = 0;
	var currentY = 0;

	int steps = 0;

	foreach (var instruction in instructions)
	{
		var direction = instruction[0];
		var distance = int.Parse(instruction.Substring(1));

		var xIncrement = 0;
		var yIncrement = 0;

		switch (direction)
		{
			case 'U':
				yIncrement = 1;
				break;
			case 'D':
				yIncrement = -1;
				break;
			case 'L':
				xIncrement = -1;
				break;
			case 'R':
				xIncrement = 1;
				break;
		}

		for (int i = 0; i < distance; i++)
		{
			currentX += xIncrement;
			currentY += yIncrement;

			steps++;

			if (visitedPoints.ContainsKey((currentX, currentY)))
			{
				colisions.Add((currentX, currentY, visitedPoints[(currentX, currentY)] + steps));
			}
			else
			{
				visitedPoints.Add((currentX, currentY), steps);
			}
		}
	}
}