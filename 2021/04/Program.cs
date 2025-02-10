var lines = File.ReadAllLinesAsync("Input.txt").Result;

var bingoNumbers = lines[0].Split(',').Select(int.Parse).ToList();

var boards = new List<List<int[]>>();
var boardInput = new List<int[]>();

var indexLines = 2;

while (indexLines < lines.Length)
{
	var line = lines[indexLines];
	if (string.IsNullOrEmpty(line))
	{
		boards.Add(boardInput);
		boardInput = new List<int[]>();

		indexLines++;

		continue;
	}

	boardInput.Add(line.Split(' ').Where(l => !string.IsNullOrEmpty(l)).Select(int.Parse).ToArray());

	indexLines++;
}

boards.Add(boardInput);

var score = PlayBingo(false);

Console.WriteLine(score);

// Part 2
score = PlayBingo(true);

Console.WriteLine(score);


int PlayBingo(bool isPart2)
{
	var playedNumbers = new List<int>();

	var winnerBoardsIndex = new HashSet<int>();

	foreach (var number in bingoNumbers)
	{
		playedNumbers.Add(number);

		for (int i = 0; i < boards.Count; i++)
		{
			if (winnerBoardsIndex.Contains(i))
			{
				continue;
			}

			var board = boards[i];

			foreach (var row in board)
			{
				if (row.All(r => playedNumbers.Contains(r)))
				{
					if (!isPart2)
					{
						return CalculateScore(i, number, playedNumbers);
					}
					else
					{
						winnerBoardsIndex.Add(i);

						if (winnerBoardsIndex.Count == boards.Count)
						{
							return CalculateScore(i, number, playedNumbers);
						}
					}
				}
			}

			for (int j = 0; j < board[0].Length; j++)
			{
				var isBingo = true;

				for (int k = 0; k < board.Count; k++)
				{
					if (!playedNumbers.Contains(board[k][j]))
					{
						isBingo = false;
						break;
					}
				}

				if (isBingo)
				{
					if (!isPart2)
					{
						return CalculateScore(i, number, playedNumbers);
					}
					else
					{
						winnerBoardsIndex.Add(i);

						if (winnerBoardsIndex.Count == boards.Count)
						{
							return CalculateScore(i, number, playedNumbers);
						}
					}
				}
			}
		}
	}

	return 0;
}

int CalculateScore(int boardIndex, int bingoNumber, List<int> playedNumbers)
{
	var sumUnmarked = 0;

	var board = boards[boardIndex];

	foreach (var row in board)
	{
		foreach (var number in row)
		{
			if (!playedNumbers.Contains(number))
			{
				sumUnmarked += number;
			}
		}
	}

	return sumUnmarked * bingoNumber;
}