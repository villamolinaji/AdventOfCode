string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var countSplits = 0;

var startPosition = lines[0].IndexOf('S');

var rows = lines.Length;
var cols = lines[0].Length;
var queue = new Queue<(int, int)>();
var visited = new HashSet<(int, int)>();
queue.Enqueue((0, startPosition));

while (queue.Count > 0)
{
    var (r, c) = queue.Dequeue();
    if (!visited.Add((r, c)))
    {
        continue;
    }

    if (r + 1 == rows)
    {
        continue;
    }

    if (lines[r + 1][c] == '^')
    {
        queue.Enqueue((r + 1, c - 1));
        queue.Enqueue((r + 1, c + 1));
        countSplits++;
    }
    else
    {
        queue.Enqueue((r + 1, c));
    }
}

Console.WriteLine($"{countSplits}");

// Part 2
Dictionary<(int, int), long> cache = new();

var timelines = GetTimelines(0, startPosition);

Console.WriteLine($"{timelines}");


long GetTimelines(int r, int c)
{
    if (cache.TryGetValue((r, c), out var val))
    {
        return val;
    }

    if (r + 1 == rows)
    {
        return cache[(r, c)] = 1;
    }

    long result;
    if (lines[r + 1][c] == '^')
    {
        result = GetTimelines(r + 1, c - 1) + GetTimelines(r + 1, c + 1);
    }
    else
    {
        result = GetTimelines(r + 1, c);
    }

    cache[(r, c)] = result;

    return result;
}