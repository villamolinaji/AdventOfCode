//var input = File.ReadAllText(@"C:\Users\perali\OneDrive - Version 1\Desktop\input.txt");
/*
var input = File.ReadAllText("input.txt");
var pares = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
List<Par> paress = new List<Par> { Par.Parse("[[2]]", new Dictionary<string, Par>()), Par.Parse("[[6]]", new Dictionary<string, Par>()) };
int c = 0;
for (int i = 0; i < pares.Length; i++)
{
    var par = pares[i];
    var p = par.Split("\n", StringSplitOptions.RemoveEmptyEntries);
    Par p1 = Par.Parse(p[0].Trim(), new Dictionary<string, Par>());
    Par p2 = Par.Parse(p[1].Trim(), new Dictionary<string, Par>());
    paress.Add(p1);
    paress.Add(p2);
    var o = Par.Ordered(p1, p2);
    if (o == -1) c += (i + 1);
}
Console.WriteLine(c);
paress.Sort(new Comp());
var res = paress.Select((p, i) => new { P = p.ToString(), I = i + 1 }).Where(p => p.P == "[[2]]" || p.P == "[[6]]").Aggregate(1, (a, b) => a * b.I);
Console.WriteLine(res);
class Comp : IComparer<Par>
{
    public int Compare(Par? x, Par? y)
    {
        return Par.Ordered(x, y);
    }
}
class Par
{
    public int? Int { get; set; }
    public List<Par> Lis { get; set; }
    public override string ToString()
    {
        return $"{(Int.HasValue ? Int : "[" + string.Join(",", Lis) + "]")}";
    }
    public static int Ordered(Par a, Par b)
    {
        if (a.Int.HasValue && b.Int.HasValue) return a.Int.Value < b.Int.Value ? -1 : (a.Int.Value > b.Int.Value ? 1 : 0);
        if (a.Lis != null && b.Lis != null)
        {
            for (int i = 0; i < Math.Max(a.Lis.Count, b.Lis.Count); i++)
            {
                if (i >= a.Lis.Count) return -1;
                if (i >= b.Lis.Count) return 1;
                var o = Ordered(a.Lis[i], b.Lis[i]);
                if (o == 0) continue;
                else return o;
            }
            return 0;
        }
        if (a.Lis != null && b.Int != null) return Ordered(a, new Par { Lis = new List<Par> { b } });
        if (b.Lis != null && a.Int != null) return Ordered(new Par { Lis = new List<Par> { a } }, b);
        return 1;
    }
    public static Par Parse(string str, Dictionary<string, Par> dic)
    {
        Par currentPar = new Par();
        if (str.Count() == 2)
        {
            return new Par { Lis = new List<Par>() };
        }
        if (str.Count(s => s == '[') == 1)
        {
            return new Par { Lis = str.Substring(1, str.Length - 2).Split(',').Select(s => s.StartsWith("P") ? dic[s] : new Par { Int = Convert.ToInt32(s) }).ToList() };
        }
        Queue<Par> queue = new Queue<Par>();
        int lastIndex = 0;
        for (int i = 0; i < str.Length; i++)
        {
            var c = str[i];
            if (c == '[')
            {
                lastIndex = i;
            }
            else if (c == ']')
            {
                Par p = Parse(str.Substring(lastIndex, i - lastIndex + 1), dic);
                var id = $"P{dic.Count}";
                dic[id] = p;
                str = $"{str.Substring(0, lastIndex)}{id}{str.Substring(i + 1)}";
                break;
            }
        }
        return Parse(str, dic);
    }
}*/