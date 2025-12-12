string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var rows = lines.Length;
var cols = lines[0].Length;

var rollsAccessed = 0;

var map = lines.Select(line => line.ToCharArray()).ToArray();

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        if (map[row][col] == '@')
        {
            if (CanBeAccessed(row, col))
            {
                rollsAccessed++;
            }
        }
    }
}

Console.WriteLine($"{rollsAccessed}");

// Part 2
var rollsRemoved = 0;

while (true)
{
    var toRemove = new List<(int row, int col)>();

    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            if (map[row][col] == '@')
            {
                if (CanBeAccessed(row, col))
                {
                    toRemove.Add((row, col));
                }
            }
        }
    }

    if (toRemove.Count == 0)
    {
        break;
    }

    rollsRemoved += toRemove.Count;

    foreach (var (row, col) in toRemove)
    {
        map[row][col] = 'x';
    }
}

Console.WriteLine($"{rollsRemoved}");


bool CanBeAccessed(int row, int col)
{
    var directions = new (int dRow, int dCol)[]
    {
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1)
    };

    var countPapers = 0;

    foreach (var direction in directions)
    {
        var newRow = row + direction.dRow;
        var newCol = col + direction.dCol;
        if (newRow >= 0 && newRow < rows &&
            newCol >= 0 && newCol < cols)
        {
            if (map[newRow][newCol] == '@')
            {
                countPapers++;
            }
        }
    }

    return countPapers < 4;
}