string line = File.ReadAllText("Input.txt");

var ranges = line.Split(',');

var invalidIds = new List<long>();

foreach (var range in ranges)
{
    var ids = range.Split('-');
    var firstId = long.Parse(ids[0]);
    var secondId = long.Parse(ids[1]);

    invalidIds.AddRange(GetInvalidIds(firstId, secondId, false));
}

var sum = invalidIds.Sum();

Console.WriteLine($"{sum}");

sum = 0;
invalidIds.Clear();

foreach (var range in ranges)
{
    var ids = range.Split('-');
    var firstId = long.Parse(ids[0]);
    var secondId = long.Parse(ids[1]);

    invalidIds.AddRange(GetInvalidIds(firstId, secondId, true));
}

sum = invalidIds.Sum();

Console.WriteLine($"{sum}");


List<long> GetInvalidIds(long firstId, long secondId, bool isPart2)
{
    var invalidIds = new List<long>();

    var minId = Math.Min(firstId, secondId);
    var maxId = Math.Max(firstId, secondId);

    for (var i = minId; i <= maxId; i++)
    {
        var isInvalid = isPart2 ? IsInvalid2(i) : IsInvalid(i);

        if (isInvalid)
        {
            invalidIds.Add(i);
        }
    }


    return invalidIds;
}

bool IsInvalid(long id)
{
    var idString = id.ToString();

    if (idString.Length % 2 != 0)
    {
        return false;
    }

    var idStringA = idString.Substring(0, idString.Length / 2);
    var idStringB = idString.Substring(idString.Length / 2, idString.Length / 2);

    if (idStringA == idStringB)
    {
        return true;
    }

    return false;
}

bool IsInvalid2(long id)
{
    var idString = id.ToString();

    for (int i = 0; i < idString.Length / 2; i++)
    {
        var sequence = idString.Substring(0, i + 1);

        var times = idString.Length / sequence.Length;

        var sequenceBuilt = string.Empty;

        for (int j = 0; j < times; j++)
        {
            sequenceBuilt += sequence;
        }

        if (sequenceBuilt == idString)
        {
            return true;
        }
    }

    return false;
}