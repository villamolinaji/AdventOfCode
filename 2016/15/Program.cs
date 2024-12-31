using _15;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var discs = new List<Disc>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var number = int.Parse(parts[1].Substring(1));
	var positions = int.Parse(parts[3]);
	var initialPosition = int.Parse(parts[^1].Trim('.'));

	discs.Add(new Disc(number, positions, initialPosition));
}
Console.WriteLine(GetTime());

// Part 2
discs.Add(new Disc(7, 11, 0));

Console.WriteLine(GetTime());


int GetTime()
{
	int time = 0;

	while (true)
	{
		var success = true;

		for (int i = 0; i < discs.Count; i++)
		{
			var disc = discs[i];
			var position = (disc.InitialPosition + time + i + 1) % disc.Positions;

			if (position != 0)
			{
				success = false;
				break;
			}
		}

		if (success)
		{
			break;
		}

		time++;
	}

	return time;
}