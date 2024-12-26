/*
 * var input = File.ReadAllLines(@"C:\Users\perali\OneDrive - Version 1\Desktop\input.txt");
var matrix = input.Select(x => x.ToArray()).ToArray();
int xS = 0, yS = 0, xE = 0, yE = 0;
List<(int, int)> steps = new List<(int, int)>();
for (int i = 0; i < input.Length; i++)
{
    var z = input[i];
    for (int j = 0; j < z.Length; j++)
    {
        if (z[j] == 'S')
        {
            xS = j;
            yS = i;
        }
        if (z[j] == 'E')
        {
            xE = j;
            yE = i;
        }
    }
}
matrix[yS][xS] = 'a';
matrix[yE][xE] = 'z';
var res1 = MinDis(xS, yS);
Console.WriteLine($"Parte 1: {res1}");
List<int> distancias = new List<int>();
for (int i = 0; i < input.Length; i++)
{
    var z = input[i];
    for (int j = 0; j < z.Length; j++)
    {
        if (z[j] == 'a')
        {
            distancias.Add(MinDis(j, i));
        }
    }
}
var res2 = distancias.Min();
Console.WriteLine($"Parte 2: {res2}");
int MinDis(int xStart, int yStart)
{
    Queue<((int, int), int)> pendientes = new Queue<((int, int), int)>();
    HashSet<(int, int)> visitados = new HashSet<(int, int)>();
    pendientes.Enqueue(((xStart, yStart), 0));
    while (true)
    {
        if (pendientes.Count == 0) return int.MaxValue;
        var t = pendientes.Dequeue();
        var pos = t.Item1;
        var dis = t.Item2;
        int x = pos.Item1;
        int y = pos.Item2;
        if (x == xE && y == yE) return dis;
        if (visitados.Contains(pos)) continue;
        visitados.Add(pos);
        var c = matrix[y][x];
        if (x > 0 && Comp(matrix[y][x - 1], c))
        {
            pendientes.Enqueue(((x - 1, y), dis + 1));
        }
        if (x < matrix[y].Length - 1 && Comp(matrix[y][x + 1], c))
        {
            pendientes.Enqueue(((x + 1, y), dis + 1));
        }
        if (y > 0 && Comp(matrix[y - 1][x], c))
        {
            pendientes.Enqueue(((x, y - 1), dis + 1));
        }
        if (y < matrix.Length - 1 && Comp(matrix[y + 1][x], c))
        {
            pendientes.Enqueue(((x, y + 1), dis + 1));
        }
    }
}
bool Comp(char a, char b)
{
    return (a - b) <= 1;
}
*/