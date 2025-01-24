using _12;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

List<Moon> moons = ReadInput(lines);

var steps = 1000;

for (var i = 0; i < steps; i++)
{
	MoveMoons();
}

var totalEnergy = 0;
foreach (var moon in moons)
{
	var potentialEnergy = Math.Abs(moon.PosX) + Math.Abs(moon.PosY) + Math.Abs(moon.PosZ);
	var kineticEnergy = Math.Abs(moon.VelX) + Math.Abs(moon.VelY) + Math.Abs(moon.VelZ);

	totalEnergy += potentialEnergy * kineticEnergy;
}

Console.WriteLine(totalEnergy);

// Part 2
var stepsByDim = new long[3];

// X
var steps2 = 0;
var visisted = new HashSet<(int, int, int, int, int, int, int, int)>();
moons = ReadInput(lines);
while (true)
{
	steps2++;

	MoveMoons();

	var state = (moons[0].PosX, moons[1].PosX, moons[2].PosX, moons[3].PosX, moons[0].VelX, moons[1].VelX, moons[2].VelX, moons[3].VelX);

	if (visisted.Contains(state))
	{
		break;
	}

	visisted.Add(state);

	stepsByDim[0] = steps2;
}

// Y
steps2 = 0;
visisted = new HashSet<(int, int, int, int, int, int, int, int)>();
moons = ReadInput(lines);

while (true)
{
	steps2++;

	MoveMoons();

	var state = (moons[0].PosY, moons[1].PosY, moons[2].PosY, moons[3].PosY, moons[0].VelY, moons[1].VelY, moons[2].VelY, moons[3].VelY);

	if (visisted.Contains(state))
	{
		break;
	}

	visisted.Add(state);

	stepsByDim[1] = steps2;
}

// Z
steps2 = 0;
visisted = new HashSet<(int, int, int, int, int, int, int, int)>();
moons = ReadInput(lines);
while (true)
{
	steps2++;

	MoveMoons();

	var state = (moons[0].PosZ, moons[1].PosZ, moons[2].PosZ, moons[3].PosZ, moons[0].VelZ, moons[1].VelZ, moons[2].VelZ, moons[3].VelZ);

	if (visisted.Contains(state))
	{
		break;
	}

	visisted.Add(state);

	stepsByDim[2] = steps2;
}

var result = LCM(stepsByDim[0], LCM(stepsByDim[1], stepsByDim[2]));

Console.WriteLine(result);


static List<Moon> ReadInput(string[] lines)
{
	var moons = new List<Moon>();

	foreach (var line in lines)
	{
		var parts = line.Split(',');

		var moon = new Moon();
		moon.PosX = int.Parse(parts[0].Split('=')[1]);
		moon.PosY = int.Parse(parts[1].Split('=')[1]);
		moon.PosZ = int.Parse(parts[2].Split('=')[1].TrimEnd('>'));

		moons.Add(moon);
	}

	return moons;
}

void MoveMoons()
{
	foreach (var moon in moons)
	{
		foreach (var otherMoon in moons)
		{
			if (moon == otherMoon)
			{
				continue;
			}

			if (moon.PosX < otherMoon.PosX)
			{
				moon.VelX++;
			}
			else if (moon.PosX > otherMoon.PosX)
			{
				moon.VelX--;
			}

			if (moon.PosY < otherMoon.PosY)
			{
				moon.VelY++;
			}
			else if (moon.PosY > otherMoon.PosY)
			{
				moon.VelY--;
			}

			if (moon.PosZ < otherMoon.PosZ)
			{
				moon.VelZ++;
			}
			else if (moon.PosZ > otherMoon.PosZ)
			{
				moon.VelZ--;
			}
		}
	}

	foreach (var moon in moons)
	{
		moon.PosX += moon.VelX;
		moon.PosY += moon.VelY;
		moon.PosZ += moon.VelZ;
	}
}

long GCD(long a, long b)
{
	while (b != 0)
	{
		var temp = b;
		b = a % b;
		a = temp;
	}
	return a;
}

long LCM(long a, long b)
{
	return a * b / GCD(a, b);
}
