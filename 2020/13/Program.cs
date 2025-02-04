using System.Numerics;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var timestamp = int.Parse(lines[0]);
var buses = lines[1]
	.Split(',')
	.Where(x => x != "x")
	.Select(int.Parse)
	.ToList();

var earliestBus = buses.OrderBy(b => b - timestamp % b).First();

var minWaintingTime = earliestBus - timestamp % earliestBus;

Console.WriteLine(earliestBus * minWaintingTime);

// Part 2
var buses2 = lines[1]
	.Split(',')
	.Select((x, i) => (x, i))
	.Where(x => x.x != "x")
	.Select(x => (busId: int.Parse(x.x), index: x.i))
	.ToList();

var timeBuses = buses2.Select(x => (x.busId, x.busId - x.index)).ToList();

var result = ChineseRemainderTheorem(timeBuses);

Console.WriteLine(result);


long ChineseRemainderTheorem(List<(int mod, int a)> buses)
{
	var prod = buses.Aggregate(1L, (acc, item) => acc * item.mod);

	var sum = buses.Select((item, i) =>
	{
		var p = prod / item.mod;

		return item.a * ModInv(p, item.mod) * p;
	}).Sum();

	return sum % prod;
}

long ModInv(long a, long m) => (long)BigInteger.ModPow(a, m - 2, m);