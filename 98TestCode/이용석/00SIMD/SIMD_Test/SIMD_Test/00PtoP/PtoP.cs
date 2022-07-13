
using System.Numerics;
using System.Diagnostics;
namespace SIMD_Test._00PtoP { }

public class PtoP
{
    #region 멤버 변수

    private Stopwatch timer = new Stopwatch();

    private long simdTimer = 0;
    private long sisdTimer = 0;

    #endregion

    #region GET/SET

    public long GetSisdTimer()
    {
        return sisdTimer;
    }

    public long GetSimdTimer()
    {
        return simdTimer;
    }
    
    #endregion

    #region 연산 Func
    
    public int[] SIMDArrayAddition(int[] lhs, int[] rhs)
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
    
    public int[] SISDArrayAddition(int[] lhs, int[] rhs)
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

    public float[] StoreDataToArray(Vector2 p, float r)
    {
        float[] result = new float[8];

        result[0] = p.X;
        result[1] = p.Y;
        result[2] = r;

        for (int i = 3; i < 7; ++i)
            result[i] = 0;
        
        return result;
    }
    // p1r, p2r은 p1, p2의 반지름(r)을 표기한 것
    public void PointToPointSIMD(Vector2 p1,float p1r, Vector2 p2, float p2r)
    {
        var simdLength = Vector<float>.Count;
        //var result = new int[lhs.Length];

        #region SIMD

        timer.Start();

        for (int z = 0; z < 1000000; ++z)
        {

            float[] p1Data = StoreDataToArray(p1, p1r);
            float[] p2Data = StoreDataToArray(p2, p2r);
            float[] result = new float[p1Data.Length];



            for (int i = 0; i <= p1Data.Length - simdLength; i += simdLength)
            {
                var va = new Vector<float>(p1Data, i);
                var vb = new Vector<float>(p2Data, i);
                (va * vb).CopyTo(result, i);
                // result에서 값다시 빼내야함
            }
        } 

        timer.Stop();
        simdTimer += (timer.ElapsedTicks);
        #endregion

        #region Timer 초기화

        timer.Reset();
        
        #endregion

        #region SISD
        
        timer.Start();
        
        // 두 원의 center 사이의 거리

        for (int z = 0; z < 1000000; ++z)
        {
            float dist = (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);

            // 두 원의 반지름 합
            float sumR = p1r + p2r;
        }

        timer.Stop();
        sisdTimer += (timer.ElapsedTicks);
        

        #endregion

    }

    #endregion

    #region 공통 Func
    public void PrintArray(int[] arr)
    {
        int i = 0;
        for (; i < arr.Length; ++i) {
            Console.WriteLine(arr[i]);
            Console.WriteLine(" - ");
        }
        Console.WriteLine("\n");
    }

    #endregion
    

    
    
}