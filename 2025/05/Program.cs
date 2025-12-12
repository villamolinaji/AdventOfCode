string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var ranges = new List<(long start, long end)>();
var ingredients = new List<long>();

for (int i = 0; i < lines.Length; i++)
{
    if (string.IsNullOrWhiteSpace(lines[i]))
    {
        continue;
    }

    if (lines[i].Contains('-'))
    {
        var parts = lines[i].Split('-');
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);
        ranges.Add((start, end));
    }
    else
    {
        ingredients.Add(long.Parse(lines[i]));
    }
}

ranges.Sort((a, b) => a.start.CompareTo(b.start));

long countFresh = 0L;

foreach (var ingredient in ingredients)
{
    bool isFresh = false;

    foreach (var range in ranges)
    {
        if (ingredient >= range.start && ingredient <= range.end)
        {
            isFresh = true;
            break;
        }
    }

    if (isFresh)
    {
        countFresh++;
    }
}

Console.WriteLine($"{countFresh}");

// Part 2
countFresh = 0L;
long current = -1;

foreach (var range in ranges)
{
    long start = range.start;
    long end = range.end;

    if (current >= start)
    {
        start = current + 1;
    }
    if (start <= end)
    {
        countFresh += end - start + 1;
    }

    current = Math.Max(current, end);
}

Console.WriteLine($"{countFresh}");
