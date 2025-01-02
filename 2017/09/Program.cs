var input = File.ReadAllTextAsync("Input.txt").Result;

var score = 0;
var totalScore = 0;
var scoreStack = new Stack<int>();
bool isGarbage = false;

int garbageCount = 0;

for (int i = 0; i < input.Length; i++)
{
	if (input[i] == '!')
	{
		i++;
		continue;
	}

	if (isGarbage)
	{
		if (input[i] == '>')
		{
			isGarbage = false;
		}
		else
		{
			garbageCount++;
		}

		continue;
	}

	if (!isGarbage)
	{
		if (input[i] == '<')
		{
			isGarbage = true;
			continue;
		}

		if (input[i] == '{')
		{
			score++;
			scoreStack.Push(score);
		}
		if (input[i] == '}')
		{
			totalScore += scoreStack.Pop();
			score--;
		}
	}
}

Console.WriteLine(totalScore);

Console.WriteLine(garbageCount);
