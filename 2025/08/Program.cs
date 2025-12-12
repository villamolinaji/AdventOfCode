string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var points = new List<(long x, long y, long z)>();
foreach (var line in lines)
{
    var parts = line.Split(',');
    long x = long.Parse(parts[0]);
    long y = long.Parse(parts[1]);
    long z = long.Parse(parts[2]);

    points.Add((x, y, z));
}

int pointsCount = points.Count;

var distances = new List<(long dist, int i, int j)>();
for (int i = 0; i < pointsCount; i++)
{
    var (x1, y1, z1) = points[i];

    for (int j = 0; j < pointsCount; j++)
    {
        var (x2, y2, z2) = points[j];
        long dx = x1 - x2;
        long dy = y1 - y2;
        long dz = z1 - z2;
        long distance = dx * dx + dy * dy + dz * dz;

        if (i > j)
        {
            distances.Add((distance, i, j));
        }
    }
}

distances.Sort((a, b) => a.dist.CompareTo(b.dist));

int[] unionFind = new int[pointsCount];
for (int i = 0; i < pointsCount; i++)
{
    unionFind[i] = i;
}

int connections = 0;

for (int t = 0; t < distances.Count; t++)
{
    var (dist, i, j) = distances[t];

    if (t == 1000)
    {
        var circuits = new Dictionary<int, int>();
        for (int x = 0; x < pointsCount; x++)
        {
            int root = Find(x);

            if (!circuits.ContainsKey(root))
            {
                circuits[root] = 0;
            }

            circuits[root]++;
        }

        var sizes = circuits.Values.OrderBy(v => v).ToList();

        long part1 = (long)sizes[^1] * sizes[^2] * sizes[^3];

        Console.WriteLine("Part 1");
        Console.WriteLine(part1);
    }

    if (Find(i) != Find(j))
    {
        connections++;

        if (connections == pointsCount - 1)
        {
            long part2 = points[i].x * points[j].x;

            Console.WriteLine("Part 2");
            Console.WriteLine(part2);
        }

        Mix(i, j);
    }
}


int Find(int x)
{
    if (unionFind[x] == x)
    {
        return x;
    }

    unionFind[x] = Find(unionFind[x]);

    return unionFind[x];
}

void Mix(int x, int y)
{
    unionFind[Find(x)] = Find(y);
}