string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var numberLines = new List<List<int>>();
var operationLines = new List<List<char>>();

foreach (var line in lines)
{
    var numbers = new List<int>();
    var operations = new List<char>();
    var currentNumber = "";
    foreach (var ch in line)
    {
        if (char.IsDigit(ch))
        {
            currentNumber += ch;
        }
        else
        {
            if (currentNumber.Length > 0)
            {
                numbers.Add(int.Parse(currentNumber));
                currentNumber = "";
            }
            if (!char.IsWhiteSpace(ch))
            {
                operations.Add(ch);
            }
        }
    }
    if (currentNumber.Length > 0)
    {
        numbers.Add(int.Parse(currentNumber));
    }

    if (numbers.Count > 0)
    {
        numberLines.Add(numbers);
    }

    if (operations.Count > 0)
    {
        operationLines.Add(operations);
    }
}

long sum = 0;

var columnCount = numberLines[0].Count;
var linesCount = numberLines.Count;

for (var i = 0; i < columnCount; i++)
{
    var columnNumbers = numberLines.Select(x => x[i]).ToList();
    var columnOperations = operationLines.Select(x => x[i]).ToList();
    long columnResult = columnNumbers[0];

    for (var j = 1; j < linesCount; j++)
    {
        var operation = columnOperations[0];
        var nextNumber = columnNumbers[j];
        switch (operation)
        {
            case '+':
                columnResult += nextNumber;
                break;
            case '*':
                columnResult *= nextNumber;
                break;
        }
    }

    sum += columnResult;
}

Console.WriteLine($"{sum}");

// Part 2
var numberLines2 = new string[linesCount];
var lineLenght = lines[0].Length;

for (var i = 0; i < lineLenght; i++)
{
    var isBlankColumn = true;

    for (var j = 0; j < linesCount; j++)
    {
        if (!char.IsWhiteSpace(lines[j][i]))
        {
            isBlankColumn = false;
            break;
        }
    }

    if (isBlankColumn)
    {
        for (var j = 0; j < linesCount; j++)
        {
            numberLines2[j] += ',';
        }

        continue;
    }

    for (var j = 0; j < linesCount; j++)
    {
        numberLines2[j] += lines[j][i];
    }
}

var columnCount2 = numberLines2[0].Split(',').Length;
var numbers2 = new List<string[]>();

foreach (var line in numberLines2)
{
    numbers2.Add(line.Split(','));
}

sum = 0;

for (var i = columnCount - 1; i >= 0; i--)
{
    var columnNumbers = numbers2.Select(x => x[i]).ToList();
    var columnOperations = operationLines.Select(x => x[i]).ToList();

    var maxNumberWidth = columnNumbers.Max(x => x.ToString().Length);

    var numbers = new List<int>();

    for (var j = 0; j < maxNumberWidth; j++)
    {
        var numberString = "";
        for (var k = 0; k < linesCount; k++)
        {
            var number = columnNumbers[k].ToString().PadRight(maxNumberWidth, '0');
            numberString += number[j];
        }

        numbers.Add(int.Parse(numberString));
    }

    var operation = columnOperations[0];
    long result = 0;
    switch (operation)
    {
        case '+':
            result = numbers.Sum();
            break;
        case '*':
            result = numbers.Aggregate(1L, (acc, val) => acc * val);
            break;
    }

    sum += result;
}

Console.WriteLine($"{sum}");