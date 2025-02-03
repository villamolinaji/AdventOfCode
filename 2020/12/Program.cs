var lines = File.ReadAllLinesAsync("Input.txt").Result;

var (currentX, currentY) = MoveBoat(false);

var distance = Math.Abs(currentX) + Math.Abs(currentY);

Console.WriteLine(distance);

// Part 2
(currentX, currentY) = MoveBoat(true);

distance = Math.Abs(currentX) + Math.Abs(currentY);

Console.WriteLine(distance);


(int x, int y) MoveBoat(bool isPart2)
{
	var currentX = 0;
	var currentY = 0;
	var currentDirection = 0;

	var relativeX = 10;
	var relativeY = 1;

	foreach (var line in lines)
	{
		var action = line[0];
		var value = int.Parse(line[1..]);

		switch (action)
		{
			case 'N':
				if (isPart2)
				{
					relativeY += value;
				}
				else
				{
					currentY += value;
				}
				break;
			case 'S':
				if (isPart2)
				{
					relativeY -= value;
				}
				else
				{
					currentY -= value;
				}
				break;
			case 'E':
				if (isPart2)
				{
					relativeX += value;
				}
				else
				{
					currentX += value;
				}
				break;
			case 'W':
				if (isPart2)
				{
					relativeX -= value;
				}
				else
				{
					currentX -= value;
				}
				break;
			case 'L':
				if (isPart2)
				{
					var times = value / 90;
					for (var i = 0; i < times; i++)
					{
						var temp = relativeX;
						relativeX = -relativeY;
						relativeY = temp;
					}
				}
				else
				{
					currentDirection = (currentDirection - value + 360) % 360;
				}
				break;
			case 'R':
				if (isPart2)
				{
					var times = value / 90;
					for (var i = 0; i < times; i++)
					{
						var temp = relativeX;
						relativeX = relativeY;
						relativeY = -temp;
					}
				}
				else
				{
					currentDirection = (currentDirection + value) % 360;
				}
				break;
			case 'F':
				if (isPart2)
				{
					currentX += relativeX * value;
					currentY += relativeY * value;
				}
				else
				{
					switch (currentDirection)
					{
						case 0:
							currentX += value;
							break;
						case 90:
							currentY -= value;
							break;
						case 180:
							currentX -= value;
							break;
						case 270:
							currentY += value;
							break;
					}
				}
				break;
		}
	}

	return (currentX, currentY);
}