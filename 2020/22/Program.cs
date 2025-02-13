var lines = File.ReadAllLinesAsync("Input.txt").Result;

var player1 = new Queue<int>();
var player2 = new Queue<int>();

ReadInput();

while (player1.Count > 0 && player2.Count > 0)
{
	var card1 = player1.Dequeue();
	var card2 = player2.Dequeue();

	if (card1 > card2)
	{
		player1.Enqueue(card1);
		player1.Enqueue(card2);
	}
	else
	{
		player2.Enqueue(card2);
		player2.Enqueue(card1);
	}
}

var score = GetScore();

Console.WriteLine(score);

// Part 2
ReadInput();

Game2(player1, player2);

score = GetScore();

Console.WriteLine(score);


void ReadInput()
{
	bool isPlayer1 = true;

	foreach (var line in lines)
	{
		if (line == "Player 1:")
		{
			isPlayer1 = true;
		}
		else if (line == "Player 2:")
		{
			isPlayer1 = false;
		}
		else if (!string.IsNullOrWhiteSpace(line))
		{
			if (isPlayer1)
			{
				player1.Enqueue(int.Parse(line));
			}
			else
			{
				player2.Enqueue(int.Parse(line));
			}
		}
	}
}

bool Game2(Queue<int> player1, Queue<int> player2)
{
	var visited = new HashSet<string>();

	while (player1.Any() && player2.Any())
	{
		var hash = string.Join(",", player1) + ";" + string.Join(",", player2);

		if (visited.Contains(hash))
		{
			return true;
		}
		visited.Add(hash);

		var (card1, card2) = (player1.Dequeue(), player2.Dequeue());

		bool player1Wins;

		if (player1.Count >= card1 && player2.Count >= card2)
		{
			player1Wins = Game2(new Queue<int>(player1.Take(card1)), new Queue<int>(player2.Take(card2)));
		}
		else
		{
			player1Wins = card1 > card2;
		}

		if (player1Wins)
		{
			player1.Enqueue(card1);
			player1.Enqueue(card2);
		}
		else
		{
			player2.Enqueue(card2);
			player2.Enqueue(card1);
		}
	}

	return player1.Any();
}

int GetScore()
{
	var score = 0;
	var winner = player1.Count > 0
		? player1
		: player2;

	int i = winner.Count;
	while (winner.Count > 0)
	{
		score += i * winner.Dequeue();
		i--;
	}

	return score;
}