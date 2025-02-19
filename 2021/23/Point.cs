namespace _23
{
	public class Point
	{
		public int Row { get; }

		public int Col { get; }

		public Point(int row, int col)
		{
			Row = row;
			Col = col;
		}

		public Point Down => new Point(Row + 1, Col);

		public Point Up => new Point(Row - 1, Col);

		public Point Left => new Point(Row, Col - 1);

		public Point Right => new Point(Row, Col + 1);
	}
}
