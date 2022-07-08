// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Diagnostics;

public class Test
{
    public static void Main(string[] args)
    {
        int[] ele4Left = new int[4]{10,20,24,52};
        int[] ele4Right = new int[4]{20,31,78,65};
        
        int[] ele8Left = new int[8]{10,20,24,52,54,87,15,30};
        int[] ele8Right = new int[8]{20,31,78,65,45,32,38,45};
        
        // Console.WriteLine("----Element 4----");
        // SIMDArrayAddition(ele4Left, ele4Right);
        // Console.WriteLine("\n");
        
        Console.WriteLine("----Element 8----");
        SIMDArrayAddition(ele8Left, ele8Right);

        SISDArrayAddition(ele8Left, ele8Right);
        Console.WriteLine("\n");
        
    }

    public static int[] SIMDArrayAddition(int[] lhs, int[] rhs)
    {

        Stopwatch timer = new Stopwatch();


        var simdLength = Vector<int>.Count;
        var result = new int[lhs.Length];
        var i = 0;
        timer.Start();
        for (int j = 0; j < 100000; ++j)
        {
            for (i = 0; i <= lhs.Length - simdLength; i += simdLength)
            {

                var va = new Vector<int>(lhs, i);
                var vb = new Vector<int>(rhs, i);
                (va + vb).CopyTo(result, i);
            }
        }

        timer.Stop();
        System.Console.WriteLine("SIMD time : " + timer.ElapsedMilliseconds * 1000000 + "ns\n");
        PrintArray(result);

        return result;
    }


    public static int[] SISDArrayAddition(int[] lhs, int[] rhs)
    {
        Stopwatch timer = new Stopwatch();
        
        
        var result = new int[lhs.Length];

        timer.Start();
        for (int j = 0; j < 100000; ++j)
        {
            for (int i = 0; i < lhs.Length; ++i)
            {
                result[i] = lhs[i] + rhs[i];
            }
        }
        timer.Stop();

        
        System.Console.WriteLine("SISD time : " + timer.ElapsedMilliseconds*1000000 + "ns\n");
        PrintArray(result);
        return result;
    }

    public static void PrintArray(int[] arr)
    {
        int i = 0;
        for (; i < arr.Length; ++i) {
            Console.WriteLine(arr[i]);
            Console.WriteLine(" - ");
        }
        Console.WriteLine("\n");
    }
}
