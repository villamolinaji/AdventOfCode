using System.Text;
using Newtonsoft.Json.Linq;

var lines = File.ReadAllLinesAsync("input.txt").Result;

var charIndex = 0;

int sumNumbers = 0;

foreach (var line in lines)
{
	charIndex = 0;

	while (charIndex < line.Length)
	{
		if (line[charIndex] == '-')
		{
			charIndex++;
			var number = 0;
			while (charIndex < line.Length && line[charIndex] >= '0' && line[charIndex] <= '9')
			{
				number = number * 10 + (line[charIndex] - '0');
				charIndex++;
			}
			sumNumbers -= number;
		}
		else if (line[charIndex] >= '0' && line[charIndex] <= '9')
		{
			var number = 0;
			while (charIndex < line.Length && line[charIndex] >= '0' && line[charIndex] <= '9')
			{
				number = number * 10 + (line[charIndex] - '0');
				charIndex++;
			}
			sumNumbers += number;
		}
		else
		{
			charIndex++;
		}
	}
}

Console.WriteLine(sumNumbers);

// Part 2
var inputString = new StringBuilder();
foreach (var line in lines)
{
	inputString.Append(line);
}

var parsedJson = JToken.Parse(inputString.ToString());

sumNumbers = SumNumbersExcludingRed(parsedJson);

Console.WriteLine(sumNumbers);


int SumNumbersExcludingRed(JToken token)
{
	int sum = 0;

	if (token.Type == JTokenType.Object)
	{
		var obj = (JObject)token;
		if (obj.Properties().Any(property => property.Value.Type == JTokenType.String && property.Value.ToString() == "red"))
		{
			return 0;
		}

		sum += obj.Properties()
			.Select(property => SumNumbersExcludingRed(property.Value))
			.Sum();
	}
	else if (token.Type == JTokenType.Array || token.Type == JTokenType.Property)
	{
		foreach (var child in token.Children())
		{
			sum += SumNumbersExcludingRed(child);
		}
	}
	else if (token.Type == JTokenType.Integer)
	{
		sum += token.Value<int>();
	}

	return sum;
}
