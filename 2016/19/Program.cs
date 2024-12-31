int howManyElves = 3014603;

Console.WriteLine(GetWinner());

Console.WriteLine(GetWinner2());


int GetWinner()
{
	LinkedList<int> elves = new LinkedList<int>();

	for (int i = 1; i <= howManyElves; i++)
	{
		elves.AddLast(i);
	}

	var current = elves.First;

	while (elves.Count > 1)
	{
		var toStealFrom = current!.Next ?? elves.First;

		elves.Remove(toStealFrom!);

		current = current.Next ?? elves.First;
	}

	return elves.First!.Value;
}

int GetWinner2()
{
	LinkedList<int> elves = new LinkedList<int>();

	for (int i = 1; i <= howManyElves; i++)
	{
		elves.AddLast(i);
	}

	var current = elves.First;
	var across = elves.First;
	int halfCount = howManyElves / 2;

	for (int i = 0; i < halfCount; i++)
	{
		across = across!.Next ?? elves.First;
	}

	while (elves.Count > 1)
	{
		var toRemove = across;
		across = across!.Next ?? elves.First;

		elves.Remove(toRemove!);

		if (elves.Count % 2 == 0)
		{
			across = across!.Next ?? elves.First;
		}

		current = current!.Next ?? elves.First;
	}

	return elves.First!.Value;
}
