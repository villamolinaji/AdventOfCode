namespace _10
{
	public class Node
	{
		required public string id { get; set; }

		required public List<int> values
		{
			get;
			set;
		}

		required public string outLow { get; set; }

		required public string outHigh { get; set; }
	}
}
