var numOfPlayers = 413;
var lastMarble = 71082;

Console.WriteLine(PlayMarble(numOfPlayers, lastMarble));

// Part 2
lastMarble = lastMarble * 100;

Console.WriteLine(PlayMarble(numOfPlayers, lastMarble));


long PlayMarble(int numOfPlayers, long lastMarble)
{
	var scores = new long[numOfPlayers];
	var circle = new LinkedList<int>();
	var current = circle.AddFirst(0);

	for (var i = 1; i <= lastMarble; i++)
	{
		if (i % 23 == 0)
		{
			var player = i % numOfPlayers;

			scores[player] += i;

			for (var j = 0; j < 7; j++)
			{
				current = current!.Previous ?? circle.Last;
			}

			scores[player] += current!.Value;

			var next = current.Next ?? circle.First;

			circle.Remove(current);

			current = next;
		}
		else
		{
			current = circle.AddAfter(current!.Next ?? circle!.First!, i);
		}
	}

	return scores.Max();
}