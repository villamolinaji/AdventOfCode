string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

const int maxPosition = 100;
var currentPosition = 50;
var password = 0;

foreach (var line in lines)
{
    var lineDirection = line[0];

    var lineTimes = int.Parse(line[1..]);

    if (lineDirection == 'L')
    {
        currentPosition = (currentPosition - lineTimes) % maxPosition;
        if (currentPosition < 0)
        {
            currentPosition += maxPosition;
        }
    }
    else
    {
        currentPosition = (currentPosition + lineTimes) % 100;
    }

    if (currentPosition == 0)
    {
        password++;
    }
}

Console.WriteLine(password);

// Part 2
password = 0;
currentPosition = 50;

foreach (var line in lines)
{
    var lineDirection = line[0];

    var lineTimes = int.Parse(line[1..]);

    for (int i = 1; i <= lineTimes; i++)
    {
        if (lineDirection == 'L')
        {
            currentPosition = (currentPosition - 1 + maxPosition) % maxPosition;
        }
        else
        {
            currentPosition = (currentPosition + 1) % 100;
        }

        if (currentPosition == 0)
        {
            password++;
        }
    }
}

Console.WriteLine(password);
