var lines = File.ReadAllLinesAsync("Input.txt").Result;

var registers = new Dictionary<string, int>();

var highestValue = 0;

foreach (var line in lines)
{
	var parts = line.Split(' ');

	var register = parts[0];
	var operation = parts[1];
	var value = int.Parse(parts[2]);
	var conditionRegister = parts[4];
	var conditionOperator = parts[5];
	var conditionValue = int.Parse(parts[6]);

	if (!registers.ContainsKey(register))
	{
		registers[register] = 0;
	}

	if (!registers.ContainsKey(conditionRegister))
	{
		registers[conditionRegister] = 0;
	}

	bool condition = false;

	switch (conditionOperator)
	{
		case "==":
			condition = registers[conditionRegister] == conditionValue;
			break;
		case "!=":
			condition = registers[conditionRegister] != conditionValue;
			break;
		case ">":
			condition = registers[conditionRegister] > conditionValue;
			break;
		case "<":
			condition = registers[conditionRegister] < conditionValue;
			break;
		case ">=":
			condition = registers[conditionRegister] >= conditionValue;
			break;
		case "<=":
			condition = registers[conditionRegister] <= conditionValue;
			break;
	}

	if (condition)
	{
		switch (operation)
		{
			case "inc":
				registers[register] += value;
				break;
			case "dec":
				registers[register] -= value;
				break;
		}
	}

	highestValue = Math.Max(highestValue, registers.Values.Max());
}

Console.WriteLine(registers.Values.Max());
Console.WriteLine(highestValue);