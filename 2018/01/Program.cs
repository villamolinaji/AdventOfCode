var lines = File.ReadAllLinesAsync("Input.txt").Result;

var frequency = 0;


foreach (var line in lines)
{
	frequency += int.Parse(line);
}

Console.WriteLine(frequency);

// Part 2
var frequencies = new HashSet<int>();

frequency = 0;
frequencies.Add(frequency);

var index = 0;

while (true)
{
	frequency += int.Parse(lines[index]);

	if (frequencies.Contains(frequency))
	{
		Console.WriteLine(frequency);
		break;
	}
	frequencies.Add(frequency);

	index = (index + 1) % lines.Length;
}
