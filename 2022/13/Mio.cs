/*using System.Net.Sockets;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;

string firstLine = "";
string secondLine = "";

int index = 0;
var correctIndexs = new List<int>();

foreach (var line in lines)
{
    if (!line.StartsWith("["))
    {
        firstLine = "";
        secondLine = "";

        continue;
    }

    if (string.IsNullOrEmpty(firstLine))
    {
        firstLine = line;
    }
    else if (string.IsNullOrEmpty(secondLine))
    {
        secondLine = line;
    }

    if (!string.IsNullOrEmpty(firstLine) && !string.IsNullOrEmpty(secondLine))
    {
        List<List<int>> leftPackages = Utils.GetPackages(firstLine);
        List<List<int>> rightPackages = Utils.GetPackages(secondLine);

        index++;
        if (Utils.CheckCorrectIndex(leftPackages, rightPackages))
        {
            correctIndexs.Add(index);
        }
    }
}

result = correctIndexs.Sum();
Console.WriteLine(result);


class Utils
{
    public static List<List<int>> GetPackages(string line)
    {
        int brackets = 0;
        var lineAux = line;
        List<int> openBracktes = new List<int>();
        List<int> closeBracktes = new List<int>();
        List<List<int>> result = new List<List<int>>();

        //var s = lineAux.First();

        int cont = 0;
        foreach (var s in line)
        {
            if (s == '[')
            {
                brackets++;
                openBracktes.Add(cont);
            }
            else if (s == ']')
            {

                closeBracktes.Add(cont);

                int openPos = 0;
                for (int i = 0; i < brackets; i++)
                {
                    if (i > 0)
                        openPos = lineAux.IndexOf('[', openPos + 1);
                    else
                        openPos = lineAux.IndexOf('[', openPos);
                }
                brackets--;

                //var s2 = lineAux.Substring(openBracktes.Last() + 1, (closeBracktes.Last() - openBracktes.Last()) - 1);
                //lineAux = lineAux.Substring(0, openBracktes.Last()) + lineAux.Substring(closeBracktes.Last() + 1, lineAux.Length - (closeBracktes.Last() + 1));
                var s2 = lineAux.Substring(openPos + 1, (lineAux.IndexOf(']') - openPos) - 1);
                lineAux = lineAux.Substring(0, openPos) + lineAux.Substring(lineAux.IndexOf(']') + 1, lineAux.Length - (lineAux.IndexOf(']') + 1));

                openBracktes.RemoveAt(openBracktes.Count - 1);
                closeBracktes.RemoveAt(closeBracktes.Count - 1);

                if (s2 != ",")
                {
                    var numbers = new List<int>();
                    foreach (var number in s2.Split(","))
                    {
                        int outNumber = 0;
                        if (int.TryParse(number, out outNumber))
                        {
                            numbers.Add(outNumber);
                        }
                    }
                    result.Add(numbers);
                }
            }
            cont++;
        }

        return result;
    }

    public static bool CheckCorrectIndex(List<List<int>> left, List<List<int>> right)
    {
        bool result = true;

        while (left.Count > 0 && result)
        {
            if (right.Count == 0)
            {
                result = false;
                break;
            }

            var firstLeft = left.First();
            var firstRight = right.First();

            left.RemoveAt(0);
            right.RemoveAt(0);

            //int min = Math.Min(firstLeft.Count, firstRight.Count);
            

            for (int i = 0; i < firstLeft.Count; i++)
            {
                if (firstRight.Count <= i)
                {
                    result = false;
                    break;
                }
                if (firstRight[i] < firstLeft[i])
                {
                    result = false;
                    break;
                }
                else if (firstRight[i] > firstLeft[i])
                {
                    break;
                }
            }
        }

        return result;
    }
}*/