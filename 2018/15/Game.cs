namespace _15
{
	class Game
	{
		public required IBlock[,] Map;
		public required List<Player> Players;
		public int Rounds;

		private bool ValidPos((int row, int col) pos)
		{
			return pos.row >= 0 &&
				pos.row < Map.GetLength(0) &&
				pos.col >= 0 &&
				pos.col < Map.GetLength(1);
		}

		public IBlock GetBlock((int row, int col) pos)
		{
			return ValidPos(pos)
				? Map[pos.row, pos.col]
				: Wall.Instance;
		}

		public void Step()
		{
			var finishedBeforeEndOfRound = false;

			foreach (var player in Players.OrderBy(player => player.Pos).ToArray())
			{
				if (player.HitPoints > 0)
				{
					finishedBeforeEndOfRound |= Finished();
					player.Step();
				}
			}

			if (!finishedBeforeEndOfRound)
			{
				Rounds++;
			}
		}

		public bool Finished()
		{
			return Players.Where(p => p.IsElf).All(p => p.HitPoints == 0) ||
				Players.Where(p => !p.IsElf).All(p => p.HitPoints == 0);
		}
	}
}
