var lines = File.ReadAllLinesAsync("Input.txt").Result;

var nodesDictionary = new Dictionary<string, Node>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var name = parts[0];
	var weight = int.Parse(parts[1].Trim('(', ')'));

	var childs = new List<string>();
	if (parts.Length > 2)
	{
		childs = parts.Skip(3).Select(x => x.Trim(',')).ToList();
	}

	var node = new Node
	{
		Name = name,
		Weight = weight,
		Childs = childs
	};
	nodesDictionary.Add(name, node);
}

var topNode = nodesDictionary.First(x => !nodesDictionary.Any(y => y.Value.Childs.Contains(x.Key)));

Console.WriteLine(topNode.Key);

// Part 2
int correctWeight = GetCorrectWeight(topNode.Key);
Console.WriteLine(correctWeight);


int GetNodeTotalWeight(string id)
{
	var node = nodesDictionary[id];
	return node.Weight + node.Childs.Select(GetNodeTotalWeight).Sum();
}

int GetCorrectWeight(string id)
{
	var node = nodesDictionary[id];
	var weights = node.Childs.Select(s => new { childid = s, sum = GetNodeTotalWeight(s) }).ToList();
	var firstWeight = weights[0].sum;
	var variances = weights.Skip(1).Where(s => s.sum != firstWeight).ToList();

	if (variances.Count == 0)
	{
		return 0;
	}

	var variantId = variances.Count == 1
		? variances[0].childid
		: weights[0].childid;

	var variance = GetCorrectWeight(variantId);

	if (variance != 0)
	{
		return variance;
	}

	var adjustment = variances.Count == 1
		? weights[0].sum - variances[0].sum
		: variances[0].sum - weights[0].sum;

	return nodesDictionary[variantId].Weight + adjustment;
}

class Node
{
	required public string Name { get; set; }

	public int Weight { get; set; }

	required public List<string> Childs { get; set; }
}