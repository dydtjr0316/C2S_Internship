// See https://aka.ms/new-console-template for more information

using AABB_Ver2;

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

public class MainProgram
{
    public static void Main(string[] args)
    {
        var nSweeps = 100000; // The number of Monte Carlo sweeps.
        var sampleInterval = 100; // The number of sweeps per sample.
        var nSmall = 1000; // The number of small particles.
        var nLarge = 100; // The number of large particles.
        var diameterSmall = 1.0f; // The diameter of the small particles.
        var diameterLarge = 10.0f; // The diameter of the large particles.
        var density = 0.1f; // The system density.
        var maxDisp = 0.1f; // Maximum trial displacement (in units of diameter).

// Total particles.
        var nParticles = nSmall + nLarge;

// Number of samples.
        var nSamples = nSweeps / sampleInterval;

// Particle radii.
        var radiusSmall = 0.5 * diameterSmall;
        var radiusLarge = 0.5 * diameterLarge;

// Output formatting flag.

        var format = Math.Floor(Math.Log10(nSamples));

        List<bool> periodicity = new List<bool>(2) { false, false };

// Work out base length of simulation box.
        var baseLength =
            Math.Pow((Math.PI * (nSmall * diameterSmall + nLarge * diameterLarge)) / (4.0f * density),
                1.0f / 2.0f);

        List<float> boxSize = new List<float>(2) { (float)baseLength, (float)baseLength };

        Tree tree = new Tree(2, 0.1f, ref periodicity, ref boxSize, 16);

        List<float> pos = new List<float>(2) { 1.0f, 1.0f };


        tree.InsertParticle(0, ref pos, 2.0f);
    }
}
