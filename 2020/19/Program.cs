using System.Text.RegularExpressions;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rules = new Dictionary<string, string>();
var messages = new List<string>();

var isRule = true;
foreach (var line in lines)
{
	if (line == "")
	{
		isRule = false;
		continue;
	}

	if (isRule)
	{
		var parts = line.Split(": ");
		rules[parts[0]] = parts[1];
	}
	else
	{
		messages.Add(line);
	}
}

var validMessages = 0;

var processed = new Dictionary<string, string>();

var regex = new Regex("^" + CreateRegex("0") + "$");

validMessages = messages.Count(message => regex.IsMatch(message));

Console.WriteLine(validMessages);

// Part 2
regex = new Regex($@"^({CreateRegex("42")})+(?<open>{CreateRegex("42")})+(?<close-open>{CreateRegex("31")})+(?(open)(?!))$");

validMessages = messages.Count(message => regex.IsMatch(message));

Console.WriteLine(validMessages);



string CreateRegex(string regex)
{
	if (processed.TryGetValue(regex, out var s))
	{
		return s;
	}

	var orig = rules[regex];
	if (orig.StartsWith('\"'))
	{
		return processed[regex] = orig.Replace("\"", "");
	}

	if (!orig.Contains('|'))
	{
		return processed[regex] = string.Join("", orig.Split().Select(CreateRegex));
	}

	return processed[regex] = "(" + string.Join("", orig.Split().Select(x => x == "|" ? x : CreateRegex(x))) + ")";
}