using System.Numerics;

record Rectangle(long top, long left, long bottom, long right);

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

        var points = new List<(long x, long y)>();
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            long x = long.Parse(parts[0]);
            long y = long.Parse(parts[1]);

            points.Add((x, y));
        }

        var pointsCount = points.Count;
        var areas = new List<(long area, int i, int j)>();
        for (int i = 0; i < pointsCount; i++)
        {
            var (x1, y1) = points[i];

            for (int j = 0; j < pointsCount; j++)
            {
                var (x2, y2) = points[j];

                var maxX = Math.Max(x1, x2);
                var minX = Math.Min(x1, x2);
                var maxY = Math.Max(y1, y2);
                var minY = Math.Min(y1, y2);

                long area = (maxX + 1 - minX) * (maxY + 1 - minY);

                if (i > j)
                {
                    areas.Add((area, i, j));
                }
            }
        }

        areas.Sort((a, b) => a.area.CompareTo(b.area));

        var longestArea = areas[^1].area;

        Console.WriteLine($"{longestArea}");

        // Part 2
        var segments = Boundary(points.Select(p => new Complex(p.x, p.y)).ToArray()).ToArray();

        var result2 = (
             from r in RectanglesOrderedByArea(points.Select(p => new Complex(p.x, p.y)).ToArray())
             where segments.All(s => !AabbCollision(r, s))
             select Area(r)
         ).First();

        Console.WriteLine(result2);
    }

    static IEnumerable<Rectangle> Boundary(Complex[] corners) =>
           from pair in corners.Zip(corners.Prepend(corners.Last()))
           select RectangleFromPoints(pair.First, pair.Second);

    static Rectangle RectangleFromPoints(Complex p1, Complex p2)
    {
        var top = Math.Min(p1.Imaginary, p2.Imaginary);
        var bottom = Math.Max(p1.Imaginary, p2.Imaginary);
        var left = Math.Min(p1.Real, p2.Real);
        var right = Math.Max(p1.Real, p2.Real);

        return new Rectangle((long)top, (long)left, (long)bottom, (long)right);
    }

    static IEnumerable<Rectangle> RectanglesOrderedByArea(Complex[] points) =>
        from p1 in points
        from p2 in points
        let r = RectangleFromPoints(p1, p2)
        orderby Area(r) descending
        select r;

    static long Area(Rectangle r) => (r.bottom - r.top + 1) * (r.right - r.left + 1);

    static bool AabbCollision(Rectangle a, Rectangle b)
    {
        var aIsToTheLeft = a.right <= b.left;
        var aIsToTheRight = a.left >= b.right;
        var aIsAbove = a.bottom <= b.top;
        var aIsBelow = a.top >= b.bottom;

        return !(aIsToTheRight || aIsToTheLeft || aIsAbove || aIsBelow);
    }
}