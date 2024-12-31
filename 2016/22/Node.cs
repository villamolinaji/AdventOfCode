namespace _22
{
	public class Node
	{
		public int X { get; set; }

		public int Y { get; set; }

		public int Size { get; set; }

		public int Used { get; set; }

		public int UsePercent { get; set; }

		public bool Goal { get; set; }

		public int Avail => Size - Used;

	}
}
