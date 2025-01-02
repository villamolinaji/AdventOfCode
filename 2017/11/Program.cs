var input = File.ReadAllText("Input.txt");

var distances = GetDistances().ToList();

Console.WriteLine(distances.Last());

Console.WriteLine(distances.Max());


IEnumerable<int> GetDistances()
{
	foreach (var w in GetPositions())
	{
		yield return (Math.Abs(w.x) + Math.Abs(w.y) + Math.Abs(w.z)) / 2;
	}
}

IEnumerable<(int x, int y, int z)> GetPositions()
{
	int x = 0;
	int y = 0;
	int z = 0;

	foreach (var direction in input.Split(','))
	{
		switch (direction)
		{
			case "n":
				y++;
				z--;
				break;
			case "ne":
				x++;
				z--;
				break;
			case "se":
				x++;
				y--;
				break;
			case "s":
				y--;
				z++;
				break;
			case "sw":
				x--;
				z++;
				break;
			case "nw":
				x--;
				y++;
				break;
		}

		yield return (x, y, z);
	}
}