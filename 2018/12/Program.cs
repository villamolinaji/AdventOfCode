using System.Text;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var initialState = lines[0].Substring(15);

var notes = new Dictionary<string, char>();

foreach (var line in lines.Where(l => l.Contains("=>")))
{

	var parts = line.Split("=>");
	var key = parts[0].Trim();
	var value = parts[1].Trim()[0];

	notes[key] = value;
}

long generations = 20;

var result = GetTotalPots(generations);

Console.WriteLine(result);

// Part 2
generations = 50000000000;

result = GetTotalPots(generations);

Console.WriteLine(result);


long GetTotalPots(long generations)
{
	var state = new Dictionary<int, char>();
	for (int i = 0; i < initialState.Length; i++)
	{
		state[i] = initialState[i];
	}

	long lastSum = 0;
	long generationShift = 0;

	for (long generation = 0; generation < generations; generation++)
	{
		var newState = new Dictionary<int, char>();
		int min = state.Keys.Min();
		int max = state.Keys.Max();

		for (int i = min - 2; i <= max + 2; i++)
		{
			var segment = new StringBuilder();
			for (int j = -2; j <= 2; j++)
			{
				segment.Append(state.ContainsKey(i + j)
					? state[i + j]
					: '.');
			}

			newState[i] = notes.ContainsKey(segment.ToString())
				? notes[segment.ToString()]
				: '.';
		}

		state = newState;

		state = state
			.Where(kv => kv.Value == '#')
			.ToDictionary(kv => kv.Key, kv => kv.Value);

		long currentSum = state.Keys.Sum();

		if (generation > 100 &&
			currentSum - lastSum == generationShift)
		{
			long remainingGenerations = generations - generation - 1;
			long finalSum = currentSum + remainingGenerations * generationShift;

			return finalSum;
		}

		generationShift = currentSum - lastSum;
		lastSum = currentSum;
	}

	return state.Keys.Sum();
}