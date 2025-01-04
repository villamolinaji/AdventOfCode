namespace _20
{
	public class Particle
	{
		public int Index { get; set; }

		public (int x, int y, int z) Position { get; set; }

		public (int x, int y, int z) Velocity { get; set; }

		public (int x, int y, int z) Acceleration { get; set; }

		public bool IsDestroyed { get; set; }
	}
}
