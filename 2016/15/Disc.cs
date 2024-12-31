namespace _15
{
	public class Disc
	{
		public int Number { get; set; }
		public int Positions { get; set; }
		public int InitialPosition { get; set; }

		public Disc(int number, int positions, int initialPosition)
		{
			Number = number;
			Positions = positions;
			InitialPosition = initialPosition;
		}
	}
}
