var lines = File.ReadAllLinesAsync("Input.txt").Result;

var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

var minRisk = Solve(GetRiskLevelMap());

Console.WriteLine(minRisk);

// Part 2
minRisk = Solve(ScaleUp(GetRiskLevelMap()));

Console.WriteLine(minRisk);


Dictionary<(int x, int y), int> GetRiskLevelMap()
{
	return new Dictionary<(int x, int y), int>(
		from y in Enumerable.Range(0, lines.Length)
		from x in Enumerable.Range(0, lines[0].Length)
		select new KeyValuePair<(int x, int y), int>((x, y), lines[y][x] - '0')
	);
}

int Solve(Dictionary<(int x, int y), int> riskMap)
{
	var topLeft = (0, 0);
	var bottomRight = (riskMap.Keys.MaxBy(p => p.x).x, riskMap.Keys.MaxBy(p => p.y).y);

	var queue = new PriorityQueue<(int x, int y), int>();

	var totalRiskMap = new Dictionary<(int x, int y), int>();

	totalRiskMap[topLeft] = 0;

	queue.Enqueue(topLeft, 0);

	while (true)
	{
		var position = queue.Dequeue();

		if (position == bottomRight)
		{
			break;
		}

		foreach (var direction in directions)
		{
			var newPosition = (position.x + direction.x, position.y + direction.y);

			if (riskMap.ContainsKey(newPosition))
			{
				var totalRiskThroughP = totalRiskMap[position] + riskMap[newPosition];

				if (totalRiskThroughP < totalRiskMap.GetValueOrDefault(newPosition, int.MaxValue))
				{
					totalRiskMap[newPosition] = totalRiskThroughP!;

					queue.Enqueue(newPosition, totalRiskThroughP);
				}
			}
		}
	}

	return totalRiskMap[bottomRight];
}

Dictionary<(int x, int y), int> ScaleUp(Dictionary<(int x, int y), int> map)
{
	var (ccol, crow) = (map.Keys.MaxBy(p => p.x).x + 1, map.Keys.MaxBy(p => p.y).y + 1);

	var res = new Dictionary<(int x, int y), int>(
		from y in Enumerable.Range(0, crow * 5)
		from x in Enumerable.Range(0, ccol * 5)

		let tileY = y % crow
		let tileX = x % ccol
		let tileRiskLevel = map[(tileX, tileY)]
		let tileDistance = (y / crow) + (x / ccol)

		let riskLevel = (tileRiskLevel + tileDistance - 1) % 9 + 1

		select new KeyValuePair<(int x, int y), int>((x, y), riskLevel)
	);

	return res;
}
