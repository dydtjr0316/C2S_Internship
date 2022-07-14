// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
Console.WriteLine("Hello, World!");

List<int> temp1 = new List<int>();
List<int> temp2 = new List<int>();
List<int> temp3 = new List<int>();

CalcTime1(1);
CalcTime2(1);
Console.Write("\n");
CalcTime1(100);
CalcTime2(100);
Console.Write("\n");
CalcTime1(10000);
CalcTime2(10000);

public static class Obj
{

}
static void CalcTime1(int count)
{
    Stopwatch sw = new Stopwatch();

    sw.Start();

    for (int i = 0; i < count; i++)
    {
        List<int> list = new List<int>();
        list.Reserve(50);
    }

    sw.Stop();
    Console.WriteLine("Enum-Reserve by over: " + sw.ElapsedTicks);
}

static void CalcTime2(int count)
{
    Stopwatch sw = new Stopwatch();

    sw.Start();

    for (int i = 0; i < count; i++)
    {
        List<int> list = new List<int>();
        list.Capacity = 50;
    }

    sw.Stop();
    Console.WriteLine("Enum-Reserve by lib: " + sw.ElapsedTicks);
}

static void CalcTime3(int count)
{
    Stopwatch sw = new Stopwatch();

    sw.Start();

    for (int i = 0; i < count; i++)
    {
        List<int> list = new List<int>();
        list.Resize(50, new int());
    }

    sw.Stop();
    Console.WriteLine("New-Resize: " + sw.ElapsedTicks);
}



public static class ListTemplate
{
    public static void Reserve<T>(this List<T> list, int size)
    {
        var count = list.Count;

        if (size < count)
        {
            list.RemoveRange(size, count - size);
        }
        else if (size > count)
        {
            if (size > list.Capacity)
            {
                list.Capacity = size;
            }

            list.AddRange(new T[size - count]);
        }
    }
    public static void Resize<T>(this List<T> list, int size, T element )
    {
        int count = list.Count;

        if (size < count)
        {
            list.RemoveRange(size, count - size);
        }
        else if (size > count)
        {
            if (size > list.Capacity)   // Optimization
                list.Capacity = size;

            list.AddRange(Enumerable.Repeat(element, size - count));
        }
    }
}