

var data =
    (
        from line in File.ReadAllLines("input.txt")
        where !string.IsNullOrWhiteSpace(line)
        select long.Parse(line)
    ).ToArray();

// part 1
// var numbers = data.Select(d => new LinkedListNode<long>(d)).ToArray();

// part 2
var numbers = data.Select(d => new LinkedListNode<long>(d * 811589153)).ToArray();

var linkedList = new LinkedList<long>();
foreach (var number in numbers)
{
    linkedList.AddLast(number);
}

// part 1
//Decrypt(numbers, linkedList);

// part 2
for (int i = 0; i < 10; i++)
{
    Decrypt(numbers, linkedList);
}

long result = GetResult(numbers, linkedList);

Console.WriteLine(result);



void Decrypt(LinkedListNode<long>[] numbers, LinkedList<long> linkedList)
{
    foreach (var number in numbers)
    {
        var positions = number.Value % (numbers.Length - 1);

        if (positions > 0)
        {
            var after = number.Next ?? linkedList.First;
            linkedList.Remove(number);

            while (positions-- > 0)
            {
                after = after!.Next ?? linkedList.First;
            }

            linkedList.AddBefore(after!, number);
        }
        else if (positions < 0)
        {
            var before = number.Previous ?? linkedList.Last;
            linkedList.Remove(number);

            while (positions++ < 0)
            {
                before = before!.Previous ?? linkedList.Last;
            }

            linkedList.AddAfter(before!, number);
        }
    }
}

long GetResult(LinkedListNode<long>[] numbers, LinkedList<long> linkedList)
{
    long result = 0;
    var targetNode = numbers.Where(n => n.Value == 0L).Single();

    for (int i = 0; i < 3; ++i)
    {
        var moveCount = 1000 % linkedList.Count;
        while (moveCount-- > 0)
        {
            targetNode = targetNode!.Next ?? linkedList.First;
        }
        result += targetNode!.Value;
    }

    return result;
}

/*
var numbers = new List<int>();

for (int i = 0; i < lines.Length; i++)
{
    numbers.Add(int.Parse(lines[i]));
}


var auxNumbers = new List<int>();
for (int i = 0; i < lines.Length; i++)
{
    auxNumbers.Add(numbers.ElementAt(i));
}

for (int i = 0; i < numbers.Count; i++)
{
    int number = numbers.ElementAt(i);
    int position = auxNumbers.IndexOf(number);
    int currentPosition = position;

    if (number < 0)
    {
        if ((currentPosition + number) > 0)
            position = (currentPosition + number) % numbers.Count;
        else
            position = numbers.Count + ((currentPosition + number - 1) % numbers.Count);
    }
    else
    {
        if ((currentPosition + number) < numbers.Count)
            position = (currentPosition + number) % numbers.Count;
        else
            position = ((currentPosition + number + 1) % numbers.Count);
    }
    

    if (number != 0)
    {
        auxNumbers = Utils.InsertElement(number, currentPosition, position, auxNumbers);
    }
}

int th1000 = Utils.GetNumber(0, 1000, auxNumbers);
int th2000 = Utils.GetNumber(0, 2000, auxNumbers);
int th3000 = Utils.GetNumber(0, 3000, auxNumbers);

int result = th1000 + th2000 + th3000;

Console.WriteLine(result);

class Utils
{
    public static List<int> InsertElement(int number, int currentPosition, int newPosition, List<int> numbers)
    {
        var auxNumbers = new List<int>();
        for (int i = 0; i < numbers.Count; i++)
        {
            auxNumbers.Add(numbers.ElementAt(i));
        }

        if (newPosition > currentPosition)
        {
            for (int i = currentPosition; i < newPosition; i++)
            {
                auxNumbers[i] = auxNumbers[i + 1];
            }
            auxNumbers[newPosition] = number;
        }
        else if (newPosition < currentPosition)
        {            
            for (int i = currentPosition; i > newPosition; i--)
            {
                auxNumbers[i] = auxNumbers[i - 1];
            }
            auxNumbers[newPosition] = number;
        }

        return auxNumbers;
    }

    public static int GetNumber(int number, int positions, List<int> numbers)
    {
        int currentPosition = numbers.IndexOf(number);
        int newPosition = ((currentPosition + positions) % numbers.Count);

        return numbers[newPosition];
    }
}
*/