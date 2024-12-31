namespace _10
{
	public class Node
	{
		required public string Id { get; set; }

		required public List<int> Values
		{
			get;
			set;
		}

		required public string OutLow { get; set; }

		required public string OutHigh { get; set; }
	}
}
