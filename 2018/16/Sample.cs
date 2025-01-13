namespace _16
{
	public class Sample
	{
		public int[] Before { get; set; }
		public int[] Instruction { get; set; }
		public int[] After { get; set; }

		public Sample(int[] before, int[] instruction, int[] after)
		{
			Before = before;
			Instruction = instruction;
			After = after;
		}
	}
}
