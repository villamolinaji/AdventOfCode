namespace _15
{
	class Player : IBlock
	{
		public (int row, int col) Pos;
		public bool IsElf;
		public int AttackPoints = 3;
		public int HitPoints = 200;
		public required Game Game;

		private readonly (int row, int col)[] directions = new[] { (-1, 0), (0, -1), (0, 1), (1, 0) };

		public bool Step()
		{
			if (HitPoints <= 0)
			{
				return false;
			}

			if (Attack())
			{
				return true;
			}

			if (Move())
			{
				Attack();

				return true;
			}

			return false;
		}

		private bool Move()
		{
			var targets = FindTargets();
			if (!targets.Any())
			{
				return false;
			}

			var opponent = targets
				.OrderBy(t => t.target)
				.First();

			var nextPos = targets
				.Where(t => t.player == opponent.player)
				.Select(t => t.firstStep)
				.OrderBy(p => p).First();

			(Game.Map[nextPos.row, nextPos.col], Game.Map[Pos.row, Pos.col]) = (Game.Map[Pos.row, Pos.col], Game.Map[nextPos.row, nextPos.col]);

			Pos = nextPos;

			return true;
		}

		private IEnumerable<(Player player, (int row, int col) firstStep, (int row, int col) target)> FindTargets()
		{
			var minDist = int.MaxValue;
			foreach (var (player, firstStep, target, dist) in BlocksNextToOpponentsByDistance())
			{
				if (dist > minDist)
				{
					break;
				}

				minDist = dist;

				yield return (player, firstStep, target);
			}
		}

		private IEnumerable<(Player player, (int row, int col) firstStep, (int row, int col) target, int dist)> BlocksNextToOpponentsByDistance()
		{
			var seen = new HashSet<(int row, int col)> { Pos };
			var queue = new Queue<((int row, int col) pos, (int row, int col) origDir, int dist)>();

			foreach (var (drow, dcol) in directions)
			{
				var posT = (Pos.row + drow, Pos.col + dcol);

				queue.Enqueue((posT, posT, 1));
			}

			while (queue.Any())
			{
				var (pos, firstStep, dist) = queue.Dequeue();

				if (Game.GetBlock(pos) is Empty)
				{
					foreach (var (drow, dcol) in directions)
					{
						var posT = (pos.row + drow, pos.col + dcol);

						if (!seen.Contains(posT))
						{
							seen.Add(posT);

							queue.Enqueue((posT, firstStep, dist + 1));

							if (Game.GetBlock(posT) is Player player && player.IsElf != IsElf)
							{
								yield return (player, firstStep, pos, dist);
							}
						}
					}
				}
			}
		}

		private bool Attack()
		{
			var opponents = new List<Player>();

			foreach (var (drow, dcol) in directions)
			{
				var posT = (Pos.row + drow, Pos.col + dcol);

				if (Game.GetBlock(posT) is Player opponent &&
					opponent.IsElf != IsElf)
				{
					opponents.Add(opponent);
				}
			}

			if (!opponents.Any())
			{
				return false;
			}

			var minHitPoints = opponents.Min(o => o.HitPoints);

			var target = opponents.First(o => o.HitPoints == minHitPoints);

			target.HitPoints -= AttackPoints;

			if (target.HitPoints <= 0)
			{
				Game.Players.Remove(target);
				Game.Map[target.Pos.row, target.Pos.col] = Empty.Instance;
			}

			return true;
		}
	}
}
