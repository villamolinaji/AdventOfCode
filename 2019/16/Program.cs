using System.Numerics;
using System.Text;

var inputSignal = File.ReadAllTextAsync("Input.txt").Result;

var pattern = new int[] { 0, 1, 0, -1 };

var phases = 100;

var signal = inputSignal.Select(c => int.Parse(c.ToString())).ToArray();

for (var phase = 0; phase < phases; phase++)
{
	var newSignal = new int[signal.Length];

	for (var i = 0; i < signal.Length; i++)
	{
		var patternIndex = 1;
		var sum = 0;

		var patternAux = pattern.SelectMany(p => Enumerable.Repeat(p, i + 1)).ToArray();

		for (var j = 0; j < signal.Length; j++)
		{
			sum += signal[j] * patternAux[patternIndex % patternAux.Length];

			patternIndex++;
		}

		newSignal[i] = Math.Abs(sum) % 10;
	}

	signal = newSignal;
}

Console.WriteLine(string.Join("", signal.Take(8)));

// Part 2
signal = inputSignal.Select(c => int.Parse(c.ToString())).ToArray();
var result = new StringBuilder();

var offset = int.Parse(inputSignal.Substring(0, 7));

var rows = 8;
var cols = inputSignal.Length * 10000 - offset;

var bijMods = new int[cols + 1];
var bij = new BigInteger(1);

for (var c = 1; c <= cols; c++)
{
	bijMods[c] = (int)(bij % 10);
	bij = bij * (c + 99) / c;
}

for (var r = 1; r <= rows; r++)
{
	var s = 0;

	for (var c = r; c <= cols; c++)
	{
		var x = signal[(offset + c - 1) % inputSignal.Length];

		s += x * bijMods[c - r + 1];
	}

	result.Append((s % 10).ToString());
}

Console.WriteLine(result.ToString());