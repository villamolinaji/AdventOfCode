namespace _25
{
	public class State
	{
		required public string Id { get; set; }

		public int Write0 { get; set; }

		public int Write1 { get; set; }

		public int Move0 { get; set; }

		public int Move1 { get; set; }

		required public string NextState0 { get; set; }

		required public string NextState1 { get; set; }
	}
}
