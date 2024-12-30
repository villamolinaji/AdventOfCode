var lines = File.ReadAllLinesAsync("Input.txt").Result;

var sum = lines
	.Where(line => IsRealRoom(line))
	.Sum(line => int.Parse(line.Split('-')[^1].Split('[')[0]));

Console.WriteLine(sum);

// Part 2
foreach (var line in lines)
{
	var parts = line.Split('-');
	var name = string.Join(" ", parts.Take(parts.Length - 1));
	var sector = int.Parse(parts[parts.Length - 1].Split('[')[0]);
	var decrypted = new string(name.Select(c => c == ' ' ? ' ' : (char)('a' + (c - 'a' + sector) % 26)).ToArray());

	if (decrypted.Contains("northpole object storage"))
	{
		Console.WriteLine(sector);
		break;
	}
}


bool IsRealRoom(string line)
{
	var parts = line.Split('-');
	var name = string.Join("", parts.Take(parts.Length - 1));
	var checksum = parts[parts.Length - 1].Split('[')[1].TrimEnd(']');
	var counts = name
		.GroupBy(c => c)
		.OrderByDescending(g => g.Count())
		.ThenBy(g => g.Key)
		.Take(5)
		.Select(g => g.Key)
		.ToArray();

	return new string(counts) == checksum;
}
