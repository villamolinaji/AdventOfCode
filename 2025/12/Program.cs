using System.Text.RegularExpressions;

record Region(int w, int h, int[] counts);

class Program
{

    static void Main()
    {
        var lines = File.ReadAllTextAsync("Input.txt").Result;

        var blocks = lines.Split("\r\n\r\n");

        var shapes = (
            from block in blocks[..^1]
            let area = block.Count(ch => ch == '#')
            select area
        ).ToArray();

        var regions = (
            from line in blocks.Last().Split("\n")
            let nums = Regex.Matches(line, @"\d+").Select(m => int.Parse(m.Value)).ToArray()
            let w = nums[0]
            let h = nums[1]
            let counts = nums[2..]
            select new Region(w, h, counts)
        ).ToArray();

        var regionsCanFit = 0;

        foreach (var region in regions)
        {
            var areaNeeded = Enumerable.Zip(region.counts, shapes).Sum(p => p.First * p.Second);

            if (areaNeeded <= region.w * region.h)
            {
                regionsCanFit++;
            }
        }

        Console.WriteLine(regionsCanFit);
    }
}