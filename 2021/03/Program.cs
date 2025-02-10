using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var length = lines[0].Length;

var gammaRate = new StringBuilder();
var epsilonRate = new StringBuilder();

for (int i = 0; i < length; i++)
{
	var count0 = lines.Count(l => l[i] == '0');
	var count1 = lines.Count(l => l[i] == '1');

	gammaRate.Append(count0 > count1 ? '0' : '1');
	epsilonRate.Append(count0 < count1 ? '0' : '1');
}

var gammaDecimal = Convert.ToInt32(gammaRate.ToString(), 2);
var epsilonDecimal = Convert.ToInt32(epsilonRate.ToString(), 2);

var result = gammaDecimal * epsilonDecimal;

Console.WriteLine(result);

// Part 2
var oxygenNumbers = lines.ToList();
var scrubberNumbers = lines.ToList();

for (int i = 0; i < length; i++)
{
	if (oxygenNumbers.Count > 1)
	{
		var countOxygen0 = oxygenNumbers.Count(l => l[i] == '0');
		var countOxygen1 = oxygenNumbers.Count(l => l[i] == '1');

		if (countOxygen0 > countOxygen1)
		{
			oxygenNumbers = oxygenNumbers.Where(l => l[i] == '0').ToList();
		}
		else
		{
			oxygenNumbers = oxygenNumbers.Where(l => l[i] == '1').ToList();
		}
	}

	if (scrubberNumbers.Count > 1)
	{
		var countScrubber0 = scrubberNumbers.Count(l => l[i] == '0');
		var countScrubber1 = scrubberNumbers.Count(l => l[i] == '1');

		if (countScrubber0 <= countScrubber1)
		{
			scrubberNumbers = scrubberNumbers.Where(l => l[i] == '0').ToList();
		}
		else
		{
			scrubberNumbers = scrubberNumbers.Where(l => l[i] == '1').ToList();
		}
	}
}

var oxygenDecimal = Convert.ToInt32(oxygenNumbers[0].ToString(), 2);
var scrubbernDecimal = Convert.ToInt32(scrubberNumbers[0].ToString(), 2);

result = oxygenDecimal * scrubbernDecimal;

Console.WriteLine(result);