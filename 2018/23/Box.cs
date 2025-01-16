namespace _23
{
	public class Box : IComparable<Box>
	{
		public int X { get; }

		public int Y { get; }

		public int Z { get; }

		public int Length { get; }

		public List<Nanobot> Nanobots { get; }

		public int Count => Nanobots.Count;

		public Box(int x, int y, int z, int length, List<Nanobot> nanobots)
		{
			X = x;
			Y = y;
			Z = z;
			Length = length;

			Nanobots = nanobots
				.Where(b =>
					Math.Abs(b.X - X) +
					Math.Abs(b.Y - Y) +
					Math.Abs(b.Z - Z) <= b.Radius + Length)
				.ToList();
		}

		public int CompareTo(Box? other) => -Count.CompareTo(other?.Count ?? 0);
	}
}
