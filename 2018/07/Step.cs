namespace _07
{
	public class Step
	{
		required public char StepName { get; set; }

		public List<char> Dependencies { get; set; } = new List<char>();
	}
}
