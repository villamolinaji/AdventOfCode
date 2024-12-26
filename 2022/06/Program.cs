string[] lines = File.ReadAllLines("input.txt");

int result = 0;

/*char c1 = ' ';
char c2 = ' ';
char c3 = ' ';
char c4 = ' ';*/

string input = lines[0];
int cont = 0;
char[] array = new char[14];

foreach(var s in input)
{
    result++;
    if (!array.Contains(s))
    {
        array[cont] = s;
        cont++;
        if (cont == 14)
        {
            break;
        }
    }
    else
    {
        for (int i = 0; i < cont; i++)
        {
            if (array[i] == s)
            {
                for (int j = 0; j < cont - i - 1; j++)
                {
                    array[j] = array[i + j + 1];
                }
                cont = cont - (i + 1);
                array[cont] = s;                
                cont++;
                for (int j = cont; j < 14; j++)
                {
                    array[j] = ' ';
                }
                break;
            }
        }
    }

    /*if (cont == 0)
    {
        c1 = s;
        cont++;
    }
    else if (cont == 1)
    {
        if (c1 == s)
        {
            cont = 0;
        }
        else
        {
            c2 = s;
            cont++;
        }
    }
    else if (cont == 2)
    {
        if (c1 == s)            
        {
            cont = 2;
            c1 = c2;
            c2 = s;
        }
        else if (c2 == s)
        {
            cont = 1;
            c1 = s;            
        }
        else
        {
            c3 = s;
            cont++;
        }
    }
    else if (cont == 3)
    {
        if (c1 == s)
        {
            cont = 3;
            c1 = c2;
            c2 = c3;
            c3 = s;
        }
        else if (c2 == s)
        {
            cont = 2;
            c1 = c3;
            c2 = s;
        }
        else if (c3 == s)
        {
            cont = 1;
            c1 = s;            
        }
        else
        {
            break;
        }
    }*/
}

Console.WriteLine(result.ToString());