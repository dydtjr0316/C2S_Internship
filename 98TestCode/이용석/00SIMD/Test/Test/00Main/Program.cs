// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Diagnostics;

public class Test
{
    public static void Main(string[] args)
    {
        // --l 4개원소 연산이 안된다는 한계가 있는듯
        // 추가로 공부하기
        // int[] ele4Left = new int[4]{10,20,24,52};
        // int[] ele4Right = new int[4]{20,31,78,65};
        //
        // // Console.WriteLine("----Element 4----");
        // // SIMDArrayAddition(ele4Left, ele4Right);
        // // Console.WriteLine("\n");
        
        // 2. Circle <-> Rect 충돌처리
        //
        //     - 영역을 Rect의 꼭지점 부분과(4), 모서리 부분으로(4) 구분하여 검사
        //
        //     - 모서리 부분
        //
        // a. Rect.left < Circle.x && Circle.x << Rect.right
        //
        // b. Rect.top < Circle.y && Circle.y << Rect.bottom
        //
        // c.  ( a || b ) 조건하 Rect <-> Rect(Circle을 Rect로 변환) 충돌검사
        //
        //     - 꼭지점 부분
        //
        // a. case : Right Top 부분 (Circle.x >= Rect.right && Circle.y <= Rect.top) // 4개 귀퉁이 영역
        //
        // b. Point p(Rect.right, Rect.top) // p1(left top), p2(right top), p3(left bottom), p4(right bottom)
        //
        // c.  각각의 a 조건과 p<->Circle의 충돌검사를 &&연산하고 그 결과를 다시 || 연산하여 결과반환
        
        int[] ele8Left = new int[8]{10,20,24,52,54,87,15,30};
        int[] ele8Right = new int[8]{20,31,78,65,45,32,38,45};
        
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
        for (int j = 0; j < 10000000; ++j)
        {
            for (i = 0; i <= lhs.Length - simdLength; i += simdLength)
            {
                var va = new Vector<int>(lhs, i);
                var vb = new Vector<int>(rhs, i);
                (va * vb).CopyTo(result, i);
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
        for (int j = 0; j < 10000000; ++j)
        {
            for (int i = 0; i < lhs.Length; ++i)
            {
                result[i] = lhs[i] * rhs[i];
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
