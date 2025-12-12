string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var paths = new Dictionary<string, string[]>();

foreach (var line in lines)
{
    var parts = line.Split(' ');

    var from = parts[0].TrimEnd(':').ToString();
    var to = parts[1..].ToArray();
    paths[from] = to;
}

var pathsCount = PathCount(paths, "you", "out", new Dictionary<string, long>());

Console.WriteLine(pathsCount);

// Part 2
pathsCount =
    PathCount(paths, "svr", "fft", new Dictionary<string, long>()) *
    PathCount(paths, "fft", "dac", new Dictionary<string, long>()) *
    PathCount(paths, "dac", "out", new Dictionary<string, long>()) +
    PathCount(paths, "svr", "dac", new Dictionary<string, long>()) *
    PathCount(paths, "dac", "fft", new Dictionary<string, long>()) *
    PathCount(paths, "fft", "out", new Dictionary<string, long>());

Console.WriteLine(pathsCount);


long PathCount(
    Dictionary<string, string[]> p,
    string from, string to,
    Dictionary<string, long> cache)
{
    if (!cache.ContainsKey(from))
    {
        if (from == to)
        {
            cache[from] = 1;
        }
        else
        {
            var res = 0L;

            foreach (var next in p.GetValueOrDefault(from) ?? [])
            {
                res += PathCount(p, next, to, cache);
            }

            cache[from] = res;
        }
    }

    return cache[from];
}