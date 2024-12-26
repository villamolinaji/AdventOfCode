var originalInput = "1113122113";
var input = originalInput;

for (int i = 0; i < 40; i++)
{
	input = GetSequence(input);
}

Console.WriteLine(input.Length);

// Par 2
input = originalInput;

for (int i = 0; i < 50; i++)
{
	input = GetSequence(input);
}

Console.WriteLine(input.Length);


string GetSequence(string input)
{
	var count = 1;
	var previous = input[0];
	var sequence = new List<(int number, int count)>();
	for (int i = 1; i < input.Length; i++)
	{
		if (input[i] == previous)
		{
			count++;
		}
		else
		{
			sequence.Add((int.Parse(previous.ToString()), count));
			count = 1;
			previous = input[i];
		}
	}
	sequence.Add((int.Parse(previous.ToString()), count));

	return string.Join("", sequence.Select(x => $"{x.count}{x.number}"));
}
