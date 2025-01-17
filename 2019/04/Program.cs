var input = "240298-784956";

var from = int.Parse(input.Split('-')[0]);
var to = int.Parse(input.Split('-')[1]);

var countPasswords = 0;
var countPasswords2 = 0;

for (int i = from; i <= to; i++)
{
	if (isValidPassword(i.ToString()))
	{
		countPasswords++;
	}
	if (isValidPassword2(i.ToString()))
	{
		countPasswords2++;
	}
}

Console.WriteLine(countPasswords);

Console.WriteLine(countPasswords2);


bool isValidPassword(string password)
{
	var hasDouble = false;
	var hasDecreasing = false;

	for (int i = 0; i < password.Length - 1; i++)
	{
		if (password[i] == password[i + 1])
		{
			hasDouble = true;
		}

		if (password[i] > password[i + 1])
		{
			hasDecreasing = true;
		}
	}

	return hasDouble && !hasDecreasing;
}

bool isValidPassword2(string password)
{
	var hasDouble = false;
	var hasDecreasing = false;

	for (int i = 0; i < password.Length - 1; i++)
	{
		if (password[i] == password[i + 1] &&
			((i == 0 && password[i] != password[i + 2]) || (i == password.Length - 2 || password[i] != password[i + 2]) && password[i] != password[i - 1]))
		{
			hasDouble = true;
		}

		if (password[i] > password[i + 1])
		{
			hasDecreasing = true;
		}
	}
	return hasDouble && !hasDecreasing;
}