// See https://aka.ms/new-console-template for more information

using SIMD_Test._00PtoP;
using System.Numerics;
using System.Diagnostics;
Console.WriteLine("Hello, World!");

PtoP ptopObj = new PtoP();


Random randObj = new Random();
int randRange = 100;


for (int i = 0; i < 1; ++i)
{
    Vector2 p1 = new Vector2((float)randObj.NextDouble() * randRange, (float)randObj.NextDouble() * randRange);
    float p1r = (float)randObj.NextDouble() * randRange;
    Vector2 p2 = new Vector2((float)randObj.NextDouble() * randRange, (float)randObj.NextDouble() * randRange);
    float p2r = (float)randObj.NextDouble() * randRange;

    ptopObj.PointToPointSIMD(p1, p1r, p2, p2r);
}

System.Console.WriteLine("SISD time : " + ptopObj.GetSisdTimer() + "ns\n");
System.Console.WriteLine("SIMD time : " + ptopObj.GetSimdTimer() + "ns\n");