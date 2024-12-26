var currentPassword = "vzbxkghb";

var nextPassword = CalculateNextPassword(currentPassword);

Console.WriteLine(nextPassword);

// Part 2
nextPassword = CalculateNextPassword(nextPassword);

Console.WriteLine(nextPassword);


string CalculateNextPassword(string currentPassword)
{
	var queue = new Queue<string>();
	queue.Enqueue(GetNextPassword(currentPassword));

	while (queue.Count > 0)
	{
		var newPassword = queue.Dequeue();

		if (IsValidPassword(newPassword))
		{
			return newPassword;
		}

		queue.Enqueue(GetNextPassword(newPassword));
	}

	return string.Empty;
}

string GetNextPassword(string currentPassword)
{
	var password = currentPassword.ToCharArray();
	var index = password.Length - 1;

	while (index >= 0)
	{
		password[index]++;

		if (password[index] > 'z')
		{
			password[index] = 'a';
			index--;
		}
		else
		{
			break;
		}
	}

	return new string(password);
}

bool IsValidPassword(string password)
{
	if (password.Contains("i") || password.Contains("o") || password.Contains("l"))
	{
		return false;
	}

	var hasStraight = false;
	for (var i = 0; i < password.Length - 2; i++)
	{
		if (password[i] == password[i + 1] - 1 && password[i] == password[i + 2] - 2)
		{
			hasStraight = true;
			break;
		}
	}

	if (!hasStraight)
	{
		return false;
	}

	var pairCount = 0;
	int index = 0;
	while (index < password.Length - 1)
	{
		if (password[index] == password[index + 1])
		{
			pairCount++;
			index++;
		}

		index++;
	}

	return pairCount >= 2;
}