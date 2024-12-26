/*
using System.Text.RegularExpressions;
var inputs = File.ReadAllLines(@"C:\Users\perali\OneDrive - Version 1\Desktop\input.txt");
Dir root = new Dir("ROOT");
Dir currentDir = root;
currentDir.Dirs.Add(new Dir("/") { Parent = currentDir });
foreach (var input in inputs)
{
    var m = Regex.Match(input, @"cd \.\.");
    if (m.Success)
    {
        currentDir = currentDir.Parent;
        continue;
    }
    m = Regex.Match(input, @"\$ cd (.+)");
    if (m.Success)
    {
        var n = m.Groups[1].ToString();
        var a = currentDir.Dirs.SingleOrDefault(d => d.Name == n);
        if (a == null)
        {
            a = new Dir(n) { Parent = currentDir };
            currentDir.Dirs.Add(a);
        }
        currentDir = a;
        continue;
    }
    m = Regex.Match(input, @"(\d+) (.+)");
    if (m.Success)
    {
        var s = Convert.ToInt32(m.Groups[1].ToString());
        var n = m.Groups[2].ToString();
        currentDir.Files.Add(new Fil(n) { Size = s });
        continue;
    }
    m = Regex.Match(input, @"dir (.+)");
    if (m.Success)
    {
        var n = m.Groups[1].ToString();
        currentDir.Dirs.Add(new Dir(n) { Parent = currentDir });
        continue;
    }
    Console.WriteLine($"UPS {input}");
}
Console.WriteLine(root.Size);
Console.WriteLine(Counter.i);
static class Counter
{
    public static int x = 9199225;
    public static int i = int.MaxValue;
}
class Fil
{
    public string Name { get; set; }
    public Fil(string name)
    {
        Name = name;
    }
    public int Size { get; set; }
}
class Dir
{
    public string Name { get; set; }
    public int Size
    {
        get
        {
            var a = Dirs.Sum(d => d.Size) + Files.Sum(d => d.Size);
            if (a > Counter.x)
            {
                if (a < Counter.i) Counter.i = a;
            }
            return a;
        }
    }
    public Dir(string name)
    {
        Name = name;
    }
    public Dir Parent { get; set; }
    public List<Fil> Files { get; set; } = new List<Fil>();
    public List<Dir> Dirs { get; set; } = new List<Dir>();
}
*/