record Equation(int[] buttonIndices, int sum);

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

        var indicators = new List<string>();
        var buttons = new List<string[]>();
        var joltages = new List<int[]>();

        foreach (var line in lines)
        {
            var indicator = line.Substring(1, line.IndexOf(']') - 1);

            var buttonParts = line.Substring(line.IndexOf(']') + 2,
                line.IndexOf('{') - line.IndexOf(']') - 3);
            buttonParts = buttonParts
                .Replace("(", "")
                .Replace(")", "");
            var buttonArray = buttonParts.Split(' ');

            var joltageValues = line
                .Substring(line.IndexOf('{') + 1, line.IndexOf('}') - line.IndexOf('{') - 1)
                .Split(',');

            indicators.Add(indicator);
            buttons.Add(buttonArray);
            joltages.Add(joltageValues.Select(v => int.Parse(v.Trim())).ToArray());
        }

        var count = indicators.Count;
        var total = 0L;

        for (var i = 0; i < count; i++)
        {
            int exp = TagToNum(indicators[i]);

            var buttonValues = buttons[i];

            var tools = new List<int>();

            foreach (var buttonValue in buttonValues)
            {
                int num = 0;

                foreach (var numtok in buttonValue.Split(','))
                {
                    num += 1 << int.Parse(numtok);
                }

                tools.Add(num);
            }

            total += BfsOnTools(exp, tools);
        }

        Console.WriteLine(total);

        // Part 2
        total = 0L;

        for (var i = 0; i < count; i++)
        {
            var s = Solve2(joltages[i], buttons[i], indicators[i]);
            total += s;
        }

        Console.WriteLine(total);
    }


    static int TagToNum(string tag)
    {
        int num = 0;
        for (int i = tag.Length - 1; i >= 0; i--)
        {
            num *= 2;

            if (tag[i] == '#')
            {
                num += 1;
            }
        }

        return num;
    }

    static int BfsOnTools(int exp, List<int> tools)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<(int value, int count)>();
        queue.Enqueue((0, 0));
        visited.Add(0);

        while (queue.Count > 0)
        {
            var (v, c) = queue.Dequeue();

            if (v == exp)
            {
                return c;
            }

            foreach (var tool in tools)
            {
                int next = v ^ tool;

                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue((next, c + 1));
                }
            }
        }

        return 0;
    }

    static int Solve2(int[] joltages, string[] buttons, string target)
    {
        var equations = new List<Equation>();
        for (int i = 0; i < joltages.Length; i++)
        {
            var jolt = joltages[i];
            var buttonIndices = new List<int>();
            for (var buttonIndex = 0; buttonIndex < buttons.Length; buttonIndex++)
            {
                var buttonValue = buttons[buttonIndex];
                int num = 0;

                foreach (var numtok in buttonValue.Split(','))
                {
                    num += 1 << int.Parse(numtok);
                }

                if ((num & 1 << i) != 0)
                {
                    buttonIndices.Add(buttonIndex);
                }
            }
            equations.Add(new Equation(buttonIndices.ToArray(), jolt));
        }

        return SolveEquations(equations);
    }

    static int SolveEquations(List<Equation> equations)
    {
        if (equations.Count == 0)
        {
            return 0;
        }

        var res = int.MaxValue / 2;
        var eq = equations.MinBy(eq => eq.buttonIndices.Length);
        var q = Choose(eq!.sum, eq.buttonIndices, new int[20]).ToArray();

        foreach (var xs in q)
        {

            var substitutedEquations =
                equations.Select(eqT => Substitute(eqT, eq.buttonIndices, xs)).ToArray();

            if (substitutedEquations.Any(eq => eq.sum < 0) ||
                substitutedEquations.Any(eq => eq.sum > 0 && !eq.buttonIndices.Any()))
            {
                continue;
            }

            var remainingEquations = substitutedEquations.Where(eq => eq.sum != 0 || eq.buttonIndices.Any()).ToArray();

            var cur = xs.Sum() + SolveEquations(remainingEquations.ToList());
            if (cur < res)
            {
                res = cur;
            }
        }

        return res;
    }

    static Equation Substitute(Equation eq, int[] indices, int[] values)
    {
        var sum = eq.sum;
        var remainingIndices = eq.buttonIndices.ToList();

        for (int i = 0; i < indices.Length; i++)
        {
            var index = indices[i];
            var value = values[index];

            if (remainingIndices.Contains(index))
            {
                remainingIndices.Remove(index);
                sum -= value;
            }
        }

        return new Equation(remainingIndices.ToArray(), sum);
    }

    static IEnumerable<int[]> Choose(int s, int[] indices, int[] acc)
    {
        if (indices.Length == 1)
        {
            acc = acc.ToArray();
            acc[indices[0]] = s;
            yield return acc;
            yield break;
        }

        for (int i = 0; i <= s; i++)
        {
            foreach (var v in Choose(s - i, indices[1..].ToArray(), acc))
            {
                var vT = v.ToArray();
                vT[indices[0]] = i;
                yield return vT;
            }
        }
    }
}