/*
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
var inputs = File.ReadAllLines(@"C:\Users\perali\OneDrive - Version 1\Desktop\input.txt");
int cycle = 0;
int x = 1;
//int[] cycles = new int[] { 20, 60, 100, 140, 180, 220 };
int i = 0;
int sum = 0;
StringBuilder sb = new StringBuilder();
foreach (var input in inputs)
{
    var m = Regex.Match(input, "noop");
    if (m.Success)
    {
        cycle++;
        Console.WriteLine($"x={x}, pos={(cycle % 40) - 1}");
        var pos = (cycle % 40) - 1;
        if (pos == x || pos == x - 1 || pos == x + 1)
        {
            sb.Append("#");
        }
        else
        {
            sb.Append(".");
        }
        //if (i < cycles.Length && cycle > cycles[i])
        //{
        //    sum += x * cycles[i++];
        //}
    }
    m = Regex.Match(input, @"addx (-?\d+)");
    if (m.Success)
    {
        cycle += 1;
        Console.WriteLine($"x={x}, pos={(cycle % 40) - 1}");
        var pos = (cycle % 40) - 1;
        if (pos == x || pos == x - 1 || pos == x + 1)
        {
            sb.Append("#");
        }
        else
        {
            sb.Append(".");
        }
        cycle += 1;
        Console.WriteLine($"x={x}, pos={(cycle % 40) - 1}");
        pos = (cycle % 40) - 1;
        if (pos == x || pos == x - 1 || pos == x + 1)
        {
            sb.Append("#");
        }
        else
        {
            sb.Append(".");
        }
        //if (i < cycles.Length && cycle >= cycles[i])
        //{
        //    sum += x * cycles[i++];
        //}
        x += Convert.ToInt32(m.Groups[1].Value);
    }
}
var xxx = sb.ToString();
Console.WriteLine(xxx.Substring(0, 40));
Console.WriteLine(xxx.Substring(39, 40));
Console.WriteLine(xxx.Substring(79, 40));
Console.WriteLine(xxx.Substring(119, 40));
Console.WriteLine(xxx.Substring(159, 40));
Console.WriteLine(xxx.Substring(199, 40));
*/