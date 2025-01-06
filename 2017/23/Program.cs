var lines = File.ReadAllLinesAsync("Input.txt").Result;

var registers = new Dictionary<string, int>();

long mulInvoked = 0;
long index = 0;

while (index >= 0 &&
	index < lines.Length)
{
	var line = lines[index];
	var parts = line.Split(' ');

	var valueX = 0;
	if (!int.TryParse(parts[2], out valueX) &&
		!registers.ContainsKey(parts[1]))
	{
		registers.Add(parts[1], 0);
	}

	switch (parts[0])
	{
		case "set":
			registers[parts[1]] = GetRegisterValue(parts[2]);
			index++;
			break;
		case "sub":
			registers[parts[1]] = GetRegisterValue(parts[1]) - GetRegisterValue(parts[2]);
			index++;
			break;
		case "mul":
			registers[parts[1]] = GetRegisterValue(parts[1]) * GetRegisterValue(parts[2]);
			mulInvoked++;
			index++;
			break;
		case "jnz":
			index += GetRegisterValue(parts[1]) != 0
				? GetRegisterValue(parts[2])
				: 1;
			break;
	}
}

Console.WriteLine(mulInvoked);

// Part 2

var initial = (Convert.ToInt32(lines[0].Split(' ')[2]) * 100) + 100000;
var max = initial - Convert.ToInt32(lines[7].Split(' ')[2]);
var maxFactor = (int)Math.Sqrt(max);
var increment = -Convert.ToInt32(lines[30].Split(' ')[2]);

var composites = 0;
for (var x = initial; x <= max; x += increment)
{
	if (x % 2 == 0)
	{
		composites++;
		continue;
	}

	for (var n = 3; n <= maxFactor; n += 2)
	{
		if (x % n == 0)
		{
			composites++;
			break;
		}
	}
}

Console.WriteLine(composites);


int GetRegisterValue(string reg)
{
	if (int.TryParse(reg, out var n))
	{
		return n;
	}
	else
	{
		return registers.ContainsKey(reg)
			? registers[reg]
			: 0;
	}
}