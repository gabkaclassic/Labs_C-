using System.Diagnostics;

namespace Lab_1;

public class ArraysProcessor
{
    private static int n;
    private static int m;

    public static void InputArray()
    {
        InputSize();
        ProcessingData();
    }

    private static void ProcessingData()
    {
        var arr1 = new int[n * m];
        var arr2 = new int[n, m];
        var arr3 = new int[n][];
        

        var elements = Console.ReadLine().Split(" ");

        for (var i = 0; i < elements.Length; i++)
            arr1[i] = int.Parse(elements[i]);

        for (var i = 0; i < n; i++)
        for (var j = 0; j < m; j++)
            arr2[i, j] = int.Parse(elements[j + i * m]);

        var numberElements = 0;
        var count = 1;
        while (numberElements < elements.Length && count <= n)
        {
            
            if(elements.Length - numberElements < 2*count + 1)
                arr3[count - 1] = new int[elements.Length - numberElements];
            else
                arr3[count - 1] = new int[count];

            for (var i = 0; i < arr3[count-1].Length; i++)
            {
                if (numberElements >= elements.Length)
                    break;
                arr3[count-1][i] = int.Parse(elements[numberElements++]);
            }

            count++;
        }

        ShowData(arr1, arr2, arr3);
        Process(arr1, arr2, arr3);
        ShowData(arr1, arr2, arr3);
    }

    private static void Process(int[] arr1, int[,] arr2, int[][] arr3)
    {

        var timer = new Stopwatch();
        timer.Start();
        
        for (var i = 0; i < arr1.GetLength(0); i++)
            arr1[i] = (arr1[i]++) + (++arr1[i]);
        
        timer.Stop();
        Console.WriteLine($"Elapsed time for arr1: {timer.Elapsed}");
        Console.WriteLine("----------------------");
        timer.Reset();
        timer.Start();
        
        for (var i = 0; i < arr2.GetLength(0); i++)
        {
            for (var j = 0; j < arr2.GetLength(1); j++)
                arr2[i,j] = (arr2[i,j]++)+(++arr2[i,j]);

        }
        timer.Stop();
        Console.WriteLine($"Elapsed time for arr2: {timer.Elapsed}");
        Console.WriteLine("----------------------");
        timer.Reset();
        timer.Start();
        
        for (var i = 0; i < arr3.GetLength(0); i++)
        {
            for (var j = 0; j < arr3[i].Length; j++)
                arr3[i][j] = (arr3[i][j]++)+(++arr3[i][j]);
        }
        timer.Stop();
        Console.WriteLine($"Elapsed time for arr3: {timer.Elapsed}");
        Console.WriteLine("----------------------");
        

    }

    private static void ShowData(int[] arr1, int[,] arr2, int[][] arr3)
    {
        Console.WriteLine($"[{string.Join(" ", arr1)}]");
        Console.WriteLine("----------------------");
        for (var i = 0; i < arr2.GetLength(0); i++)
        {
            Console.Write("[ ");
            for (var j = 0; j < arr2.GetLength(1); j++)
                 Console.Write($"{arr2[i,j]} ");
            Console.WriteLine("]");
        }
        Console.WriteLine("----------------------");
        for (var i = 0; i < arr3.GetLength(0); i++)
        {
            Console.Write("[ ");
            for (var j = 0; j < arr3[i].Length; j++)
                Console.Write($"{arr3[i][j]} ");
            Console.WriteLine("]");
        }
        Console.WriteLine("----------------------");
    }

    private static void InputSize()
    {
        var sizes = Console.ReadLine()?.Split(" ");
        n = int.Parse(sizes[0]);
        m = int.Parse(sizes[1]);
    }
}