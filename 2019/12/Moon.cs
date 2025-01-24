namespace _12
{
	public class Moon
	{
		public int PosX { get; set; }

		public int PosY { get; set; }

		public int PosZ { get; set; }

		public int VelX { get; set; } = 0;

		public int VelY { get; set; } = 0;

		public int VelZ { get; set; } = 0;

		public HashSet<(int x, int y, int z)> PosVisited { get; set; } = new HashSet<(int x, int y, int z)>();

		public int StepsRepeat { get; set; } = 0;
	}
}
