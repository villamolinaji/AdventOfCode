namespace _21
{
	record Player(int score, int pos)
	{
		public Player Move(int steps)
		{
			var newPos = (this.pos - 1 + steps) % 10 + 1;

			return new Player(this.score + newPos, newPos);
		}
	}
}
