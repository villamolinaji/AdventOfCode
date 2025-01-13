using _15;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var elfAttackPoints = 3;

var result = SimulateBattle(3, elfAttackPoints).score;
Console.WriteLine(result);

// Part 2
elfAttackPoints = 4;

while (true)
{
	var outcome = SimulateBattle(3, elfAttackPoints);
	if (outcome.noElfDied)
	{
		result = outcome.score;
		break;
	}

	elfAttackPoints++;
}

Console.WriteLine(result);


(bool noElfDied, int score) SimulateBattle(int goblinAttackPoints, int elfAttackPoints)
{
	var game = CreateGame(goblinAttackPoints, elfAttackPoints);
	var elfCount = game.Players.Count(player => player.IsElf);

	while (!game.Finished())
	{
		game.Step();
	}

	var noElfDied = game.Players.Count(p => p.IsElf) == elfCount;
	var score = game.Rounds * game.Players.Sum(player => player.HitPoints);

	return (noElfDied, score);
}

Game CreateGame(int goblinAttackPoints, int elfAttackPoints)
{
	var players = new List<Player>();

	var map = new IBlock[lines.Length, lines[0].Length];

	var game = new Game
	{
		Map = map,
		Players = players
	};

	for (var r = 0; r < lines.Length; r++)
	{
		for (var c = 0; c < lines[r].Length; c++)
		{
			switch (lines[r][c])
			{
				case '#':
					map[r, c] = Wall.Instance;

					break;
				case '.':
					map[r, c] = Empty.Instance;

					break;
				case 'G':
				case 'E':
					var player = new Player
					{
						IsElf = lines[r][c] == 'E',
						AttackPoints = lines[r][c] == 'E'
							? elfAttackPoints
							: goblinAttackPoints,
						Pos = (r, c),
						Game = game
					};
					players.Add(player);
					map[r, c] = player;
					break;
			}
		}
	}

	return game;
}
