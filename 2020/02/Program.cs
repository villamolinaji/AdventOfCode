var lines = File.ReadAllLinesAsync("Input.txt").Result;

var policies = new List<(int min, int max, char letter, string password)>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var minMax = parts[0].Split('-');
	var min = int.Parse(minMax[0]);
	var max = int.Parse(minMax[1]);
	var letter = parts[1][0];
	var password = parts[2];

	policies.Add((min, max, letter, password));
}

var validPasswords = 0;

foreach (var policy in policies)
{
	var count = policy.password.Count(c => c == policy.letter);

	if (count >= policy.min &&
		count <= policy.max)
	{
		validPasswords++;
	}
}

Console.WriteLine(validPasswords);

// Part 2
validPasswords = 0;
foreach (var policy in policies)
{
	var position1 = policy.password[policy.min - 1];
	var position2 = policy.password[policy.max - 1];

	if ((position1 == policy.letter && position2 != policy.letter) ||
		(position1 != policy.letter && position2 == policy.letter))
	{
		validPasswords++;
	}
}

Console.WriteLine(validPasswords);