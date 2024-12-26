/*
using System.Text.RegularExpressions;
var inputs = File.ReadAllLines(@"C:\Users\perali\OneDrive - Version 1\Desktop\input.txt");
var a = new (int, int)[10];
var pos = new HashSet<string>();
pos.Add($"{a[0].Item1} {a[0].Item2}");
foreach (var input in inputs)
{
    var m = Regex.Match(input, @"([UDRL]) (\d+)");
    var dir = m.Groups[1].Value.ToString();
    var n = Convert.ToInt32(m.Groups[2].Value);
    for (int i = 0; i < n; i++)
    {
        switch (dir)
        {
            case "U":
                a[0].Item2++;
                break;
            case "D":
                a[0].Item2--;
                break;
            case "L":
                a[0].Item1--;
                break;
            case "R":
                a[0].Item1++;
                break;
        }
        for (int y = 0; y < a.Length - 1; y++)
        {
            if (Math.Abs(a[y].Item1 - a[y + 1].Item1) <= 1 && Math.Abs(a[y].Item2 - a[y + 1].Item2) <= 1) continue;
            a[y + 1].Item1 += a[y].Item1 - a[y + 1].Item1 >= 0 ? Math.Min(1, a[y].Item1 - a[y + 1].Item1) : Math.Max(-1, a[y].Item1 - a[y + 1].Item1);
            a[y + 1].Item2 += a[y].Item2 - a[y + 1].Item2 >= 0 ? Math.Min(1, a[y].Item2 - a[y + 1].Item2) : Math.Max(-1, a[y].Item2 - a[y + 1].Item2);
            if (y == a.Length - 2)
            {
                pos.Add($"{a[y + 1].Item1} {a[y + 1].Item2}");
            }
        }
    }
}
Console.WriteLine(pos.Count);
*/