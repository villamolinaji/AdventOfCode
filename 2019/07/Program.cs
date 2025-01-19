using _07;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var maxSignal = GetMaxSignal(new[] { 0, 1, 2, 3, 4 });

Console.WriteLine(maxSignal);

// Part 2
maxSignal = GetMaxSignalWithFeedback(new[] { 5, 6, 7, 8, 9 });

Console.WriteLine(maxSignal);


long GetMaxSignal(int[] phaseSettings)
{
	return Permutations(phaseSettings).Max(phaseSequence => RunAmplifiers(phaseSequence));
}

long GetMaxSignalWithFeedback(int[] phaseSettings)
{
	return Permutations(phaseSettings).Max(phaseSequence => RunAmplifiersWithFeedback(phaseSequence));
}

long RunAmplifiers(int[] phaseSequence)
{
	long inputSignal = 0;

	foreach (var phase in phaseSequence)
	{
		var intcode = new IntcodeComputer(program);

		intcode.Input.Enqueue(phase);
		intcode.Input.Enqueue(inputSignal);

		intcode.Run();

		inputSignal = intcode.Output.Dequeue();
	}

	return inputSignal;
}

long RunAmplifiersWithFeedback(int[] phaseSequence)
{
	var amplifiers = phaseSequence.Select(_ => new IntcodeComputer(program)).ToArray();

	for (int i = 0; i < amplifiers.Length; i++)
	{
		amplifiers[i].Input.Enqueue(phaseSequence[i]);
	}

	long inputSignal = 0;
	bool done = false;

	while (!done)
	{
		for (int i = 0; i < amplifiers.Length; i++)
		{
			amplifiers[i].Input.Enqueue(inputSignal);

			amplifiers[i].Run();

			if (amplifiers[i].Output.Count > 0)
			{
				inputSignal = amplifiers[i].Output.Dequeue();
			}

			done = amplifiers[i].IsHalt && i == amplifiers.Length - 1;
		}
	}

	return inputSignal;
}

IEnumerable<int[]> Permutations(int[] sequence)
{
	if (sequence.Length == 1)
	{
		yield return sequence;
	}
	else
	{
		for (int i = 0; i < sequence.Length; i++)
		{
			foreach (var perm in Permutations(sequence.Where((_, index) => index != i).ToArray()))
			{
				yield return new[] { sequence[i] }.Concat(perm).ToArray();
			}
		}
	}
}
