var lines = File.ReadAllLinesAsync("Input.txt").Result;

var result = Move(false);

Console.WriteLine(result);

// Part 2
result = Move(true);

Console.WriteLine(result);


int Move(bool isPart2)
{
	var horizontal = 0;
	var depth = 0;
	var aim = 0;

	foreach (var line in lines)
	{
		var parts = line.Split(" ");
		var command = parts[0];
		var value = int.Parse(parts[1]);

		switch (command)
		{
			case "forward":
				horizontal += value;
				if (isPart2)
				{
					depth += value * aim;
				}
				break;
			case "down":
				if (!isPart2)
				{
					depth += value;
				}
				else
				{
					aim += value;
				}
				break;
			case "up":
				if (!isPart2)
				{
					depth -= value;
				}
				else
				{
					aim -= value;
				}
				break;
		}
	}

	return horizontal * depth;
}