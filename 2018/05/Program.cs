var inputOriginal = File.ReadAllTextAsync("Input.txt").Result;

var input = ApplyReactions(inputOriginal);

Console.WriteLine(input.Length);

// Part 2
input = inputOriginal;

var visited = new Dictionary<string, int>();

var minLength = int.MaxValue;

for (char c = 'a'; c <= 'z'; c++)
{
	var inputTemp = input.Replace(c.ToString(), "").Replace(char.ToUpper(c).ToString(), "");

	if (visited.ContainsKey(inputTemp))
	{
		minLength = Math.Min(minLength, visited[inputTemp]);
		continue;
	}

	var inputReaction = ApplyReactions(inputTemp);

	visited[inputTemp] = inputReaction.Length;

	minLength = Math.Min(minLength, inputReaction.Length);
}

Console.WriteLine(minLength);


string ApplyReactions(string inputOriginal)
{
	var input = inputOriginal;

	var endWhile = false;

	while (!endWhile)
	{
		var hasRemoved = false;
		for (int i = 0; i < input.Length; i++)
		{
			if (i + 1 < input.Length &&
				input[i] != input[i + 1] &&
				char.ToLower(input[i]) == char.ToLower(input[i + 1]))
			{
				input = input.Remove(i, 2);
				hasRemoved = true;

				break;
			}
		}

		if (!hasRemoved)
		{
			endWhile = true;
		}
	}

	return input;
}