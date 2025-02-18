using _21;

var player1Start = 6;
var player2Start = 8;

var player1Space = player1Start;
var player2Space = player2Start;

var player1Score = 0;
var player2Score = 0;

var totalDies = 0;

var die = 1;

var player1DiesList = new List<int>();
var player2DiesList = new List<int>();

while (true)
{
	player1DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}
	player1DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}
	player1DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}

	var sum = player1DiesList.Sum();
	player1DiesList.Clear();

	var nextSpace = (player1Space + sum);
	while (nextSpace > 10)
	{
		nextSpace -= 10;
	}
	player1Space = nextSpace;
	player1Score += nextSpace;
	totalDies += 3;

	if (player1Score >= 1000)
	{
		break;
	}

	player2DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}
	player2DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}
	player2DiesList.Add(die++);
	if (die > 100)
	{
		die = 1;
	}

	sum = player2DiesList.Sum();
	player2DiesList.Clear();

	nextSpace = (player2Space + sum);
	while (nextSpace > 10)
	{
		nextSpace -= 10;
	}
	player2Space = nextSpace;
	player2Score += nextSpace;
	totalDies += 3;

	if (player2Score >= 1000)
	{
		break;
	}
}

var result = player1Score > player2Score
	? player2Score * totalDies
	: player1Score * totalDies;

Console.WriteLine(result);

// Part 2
var cache = new Dictionary<(Player, Player), (long, long)>();

IEnumerable<int> DiceValues() =>
	from i in new[] { 1, 2, 3 }
	from j in new[] { 1, 2, 3 }
	from k in new[] { 1, 2, 3 }
	select i + j + k;

var wins = CountWins((new Player(0, player1Start), new Player(0, player2Start)));

var result2 = Math.Max(wins.activeWins, wins.otherWins);

Console.WriteLine(result2);


(long activeWins, long otherWins) CountWins((Player active, Player other) players)
{
	if (players.other.score >= 21)
	{
		return (0, 1);
	}

	if (!cache.ContainsKey(players))
	{
		var (activeWins, otherWins) = (0L, 0L);

		foreach (var dice in DiceValues())
		{
			var wins = CountWins((players.other, players.active.Move(dice)));

			activeWins += wins.otherWins;
			otherWins += wins.activeWins;
		}

		cache[players] = (activeWins, otherWins);
	}

	return cache[players];
}
