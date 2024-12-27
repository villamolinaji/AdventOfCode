using _14;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var reindeers = new List<Reindeer>();

const int TotalSeconds = 2503;

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var name = parts[0];
	var speed = parts[3];
	var flyseconds = parts[6];
	var restseconds = parts[13];

	reindeers.Add(new Reindeer
	{
		Name = name,
		Speed = int.Parse(speed),
		FlySeconds = int.Parse(flyseconds),
		RestSeconds = int.Parse(restseconds),
		Distance = 0,
		CurrentFly = 0,
		CurrentRest = 0,
		Points = 0
	});
}

for (int i = 0; i < TotalSeconds; i++)
{
	foreach (var reindeer in reindeers)
	{
		if (reindeer.CurrentFly < reindeer.FlySeconds)
		{
			reindeer.Distance += reindeer.Speed;
			reindeer.CurrentFly++;
		}
		else if (reindeer.CurrentRest < reindeer.RestSeconds)
		{
			reindeer.CurrentRest++;

			if (reindeer.CurrentRest == reindeer.RestSeconds)
			{
				reindeer.CurrentFly = 0;
				reindeer.CurrentRest = 0;
			}
		}
	}
}

var maxDistance = reindeers.Max(r => r.Distance);

Console.WriteLine(maxDistance);


// Part 2
foreach (var reindeer in reindeers)
{
	reindeer.Distance = 0;
	reindeer.CurrentFly = 0;
	reindeer.CurrentRest = 0;
}

for (int i = 0; i < TotalSeconds; i++)
{
	foreach (var reindeer in reindeers)
	{
		if (reindeer.CurrentFly < reindeer.FlySeconds)
		{
			reindeer.Distance += reindeer.Speed;
			reindeer.CurrentFly++;
		}
		else if (reindeer.CurrentRest < reindeer.RestSeconds)
		{
			reindeer.CurrentRest++;

			if (reindeer.CurrentRest == reindeer.RestSeconds)
			{
				reindeer.CurrentFly = 0;
				reindeer.CurrentRest = 0;
			}
		}
	}

	var currentMaxDistance = reindeers.Max(r => r.Distance);
	var winningReindeers = reindeers.Where(r => r.Distance == currentMaxDistance);

	foreach (var winner in winningReindeers)
	{
		winner.Points++;
	}
}

var maxPoints = reindeers.Max(r => r.Points);

Console.WriteLine(maxPoints);

