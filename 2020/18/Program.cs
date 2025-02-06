var lines = File.ReadAllLinesAsync("Input.txt").Result;

var result = 0L;

foreach (var line in lines)
{
	var expression = line.Replace(" ", "");

	result += Evaluate(expression, false);
}

Console.WriteLine(result);

// Part 2
result = 0L;

foreach (var line in lines)
{
	var expression = line.Replace(" ", "");

	result += Evaluate(expression, true);
}

Console.WriteLine(result);


long Evaluate(string expression, bool isPart2)
{
	var operationStack = new Stack<char>();
	var valueStack = new Stack<long>();

	operationStack.Push('(');

	foreach (var ch in expression)
	{
		switch (ch)
		{
			case '*':
				EvaluatelUntil("(", operationStack, valueStack);

				operationStack.Push('*');

				break;
			case '+':
				EvaluatelUntil(!isPart2 ? "(" : "(*", operationStack, valueStack);

				operationStack.Push('+');

				break;
			case '(':
				operationStack.Push('(');

				break;
			case ')':
				EvaluatelUntil("(", operationStack, valueStack);

				operationStack.Pop();

				break;
			default:
				valueStack.Push(long.Parse(ch.ToString()));

				break;
		}
	}

	EvaluatelUntil("(", operationStack, valueStack);

	return valueStack.Single();
}

void EvaluatelUntil(string endCharacters, Stack<char> operationStack, Stack<long> valueStack)
{
	while (!endCharacters.Contains(operationStack.Peek()))
	{
		if (operationStack.Pop() == '+')
		{
			valueStack.Push(valueStack.Pop() + valueStack.Pop());
		}
		else
		{
			valueStack.Push(valueStack.Pop() * valueStack.Pop());
		}
	}
}