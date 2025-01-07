namespace _04
{
	public class Guard
	{
		public int Id { get; set; }

		required public Dictionary<DateTime, List<int>> SleepMinutes { get; set; }

		public (int minute, int times) MinuteMostSlept { get; set; }
	}
}
