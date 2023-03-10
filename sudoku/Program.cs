using sudoku.Model;
using sudoku.Models;
using System.Globalization;

public class Program
{
    public static bool checkMatrix(int[,]  numbers)
    {
        int rows = numbers.GetLength(0);
        int cols = numbers.GetLength(1);
        if(rows != 9 || cols != 9 )
        {
            return false;
        }
        for(int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (numbers[i,j] < 1 || numbers[i,j] > 9)
                {
                    Console.WriteLine(i + "," + j);
                    return false;
                }
            }
        }
        return true;
    }

    public static bool Contains(List<Element> elements, Element element)
    {
        foreach(Element e in elements)
        {
            if(e.Value == element.Value) return true;
        }
        return false;
    }
    public static bool ContainsExact(List<Element> elements, Element element)
    {
        foreach (Element e in elements)
        {
            if (e.Value == element.Value && e.Row == element.Row && e.Column == element.Column) return true;
        }
        return false;
    }

    public static List<Element> findIncorrectElements(List<Element> allElements, List<Element>incorrectElements , Element element)
    {
        List<Element> elements = new();

        foreach(Element e in allElements)
        {
            if(e.Value== element.Value)
            {
                if (!ContainsExact(incorrectElements, e))
                {
                    elements.Add(e);
                }
            }            
        }
        return elements;
    }

    public static int checkRowCol(int[,] numbers, int row, int col, SetUniqueInvalidIndexes set)
    {
        bool flag = true;
        List<Element> allElements = new List<Element>();

        //check Row
        for(int i = 0; i < 9; i++)
        {
            Element e = new(numbers[row, i], row, i);
            if(!Contains(allElements, e))
            {
                allElements.Add(e);
            }
            else
            {
                flag = false;
                List<Element> temp = new();
                temp = findIncorrectElements(allElements, set.elements, e);
                set.elements.AddRange(temp);
                if(!ContainsExact(set.elements, e))
                {
                    set.elements.Add(e);
                }
            }
        }
        allElements.Clear();
        //check Column
        for (int i = 0; i < 9; i++)
        {
            Element e = new(numbers[i, col], i, col);
            if (!Contains(allElements, e))
            {
                allElements.Add(e);
            }
            else
            {
                flag = false;
                List<Element> temp = new();
                temp = findIncorrectElements(allElements, set.elements, e);
                set.elements.AddRange(temp);
                if (!ContainsExact(set.elements, e))
                {
                    set.elements.Add(e);
                }
            }
        }
        if (flag)
        {
            return 1;
        }
        return 0;
    }

    public static int checkSquare(int[,] numbers, int row, int col, SetUniqueInvalidIndexes set)
    {
        int maxRow = row + 3;
        int maxCol = col + 3;
        bool flag = true;
        List<Element> allElements = new List<Element>();
        for(int i = row; i < maxRow; i++)
        {
            for (int j = col; j < maxCol; j++)
            {
                Element e = new(numbers[i,j], i, j);
                if (!Contains(allElements, e))
                {
                    allElements.Add(e);
                }
                else
                {
                    flag = false;
                    List<Element> temp = new();
                    temp = findIncorrectElements(allElements, set.elements, e);
                    set.elements.AddRange(temp);
                    if (!ContainsExact(set.elements, e))
                    {
                        set.elements.Add(e);
                    }
                }
            }
        }
        if (flag)
        {
            return 1;
        }
        return 0;
    }
    public static void Main()
    {
        int sum = 0;
        SetUniqueInvalidIndexes set = new SetUniqueInvalidIndexes();
        int[,] a =
        {
            { 2, 8, 3, 7, 9, 5, 4, 1, 6 },
            { 6, 9, 1, 8, 4, 2, 5, 3, 7 },
            { 4, 7, 5, 6, 3, 1, 2, 9, 8 },
            { 7, 5, 6, 9, 8, 4, 3, 2, 1 },
            { 1, 3, 9, 5, 2, 6, 7, 8, 4 },
            { 8, 2, 4, 1, 7, 3, 6, 5, 9 },
            { 9, 4, 2, 3, 6, 8, 1, 7, 5 },
            { 5, 6, 7, 2, 1, 9, 8, 4, 3 },
            { 3, 1, 8, 4, 5, 7, 9, 6, 2 }
        };
        if(!checkMatrix(a))
        {
            Console.WriteLine("Invalid input");
        }
        else
        {
            //check rows and columns
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sum += checkRowCol(a, i, j, set);
                }
            }
            //check squares
            for(int i = 0; i < 9; i += 3)
            {
                for( int j = 0; j < 9; j += 3)
                {
                    sum += checkSquare(a, i, j, set);
                }
            }
            if(sum == 90)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("NO");
                foreach (Element e in set.elements)
                {
                    Console.WriteLine(e.Row + " " + e.Column);
                }
            }              
        }
        Console.ReadLine();
    }
}

