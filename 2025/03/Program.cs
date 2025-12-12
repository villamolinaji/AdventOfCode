string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var joltages = new List<int>();

foreach (var line in lines)
{
    var joltage = GetJoltage(line);
    joltages.Add(joltage);
}

var sum = joltages.Sum();

Console.WriteLine($"{sum}");

// Part 2
var joltages2 = new List<long>();
var dp = new Dictionary<(int, long), long>();
foreach (var line in lines)
{
    dp.Clear();
    var joltage2 = GetJoltage2(line, 0, 0);
    joltages2.Add(joltage2);
}

var sum2 = joltages2.Sum();

Console.WriteLine($"{sum2}");


int GetJoltage(string line)
{
    var firstDigit = 0;
    var secondDigit = 0;

    for (var i = 0; i < line.Length; i++)
    {
        var digit = char.GetNumericValue(line[i]);

        if (digit > firstDigit && i < line.Length - 1)
        {
            firstDigit = (int)digit;
            secondDigit = 0;
        }
        else if (digit > secondDigit)
        {
            secondDigit = (int)digit;
        }
    }

    var stringDigit = firstDigit.ToString() + secondDigit.ToString();

    return int.Parse(stringDigit);
}

long GetJoltage2(string line, int i, long used)
{
    if (i == line.Length && used == 12)
    {
        return 0;
    }

    if (i == line.Length)
    {
        return -1000000000000000000L;
    }

    var key = (i, used);
    if (dp.ContainsKey(key))
    {
        return dp[key];
    }

    long ans = GetJoltage2(line, i + 1, used);

    if (used < 12)
    {
        long value = (long)Math.Pow(10, 11 - used) * (line[i] - '0');
        ans = Math.Max(ans, value + GetJoltage2(line, i + 1, used + 1));
    }

    dp[key] = ans;

    return ans;
}