using System.Numerics;
using System.Text.RegularExpressions;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

long numOfCards = 10007;
var cardPosition = 2019;
var times = 1L;

var (a, b) = DoShuffling(numOfCards, times);

var result = Mod(a * cardPosition + b, numOfCards);

Console.WriteLine(result);

// Part 2
numOfCards = 119315717514047;
times = 101741582076661;
cardPosition = 2020;

(a, b) = DoShuffling(numOfCards, times);

result = Mod(ModInv(a, numOfCards) * (cardPosition - b), numOfCards);

Console.WriteLine(result);


BigInteger Mod(BigInteger a, BigInteger m) => ((a % m) + m) % m;

BigInteger ModInv(BigInteger a, BigInteger m) => BigInteger.ModPow(a, m - 2, m);

(BigInteger a, BigInteger big) DoShuffling(long m, long n)
{
	var a = new BigInteger(1);
	var b = new BigInteger(0);

	foreach (var line in lines)
	{
		if (line.Contains("into new stack"))
		{
			a = -a;
			b = m - b - 1;
		}
		else if (line.Contains("cut"))
		{
			var i = long.Parse(Regex.Match(line, @"-?\d+").Value);
			b = m + b - i;
		}
		else if (line.Contains("increment"))
		{
			var i = long.Parse(Regex.Match(line, @"-?\d+").Value);
			a *= i;
			b *= i;
		}
	}

	var resultA = BigInteger.ModPow(a, n, m);

	var resultB = b * (BigInteger.ModPow(a, n, m) - 1) * ModInv(a - 1, m) % m;

	return (resultA, resultB);
}