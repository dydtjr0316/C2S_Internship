// See https://aka.ms/new-console-template for more information

#define FIX_DATA_TEST_VER
#undef FIX_DATA_TEST_VER

using AABB_Ver2;
public static class ListTemplate
{
    public static void Resize<T>(this List<T> list, int size)
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
    // public static void Resize<T>(this List<T> list, int size )
    // {
    //     int count = list.Count;
    //
    //     if (size < count)
    //     {
    //         list.RemoveRange(size, count - size);
    //     }
    //     else if (size > count)
    //     {
    //         if (size > list.Capacity)   // Optimization
    //             list.Capacity = size;
    //         
    //         list.AddRange(Enumerable.Repeat(element, size - count));
    //     }
    // }
}

public class MainProgram
{
    public static void Main(string[] args)
    {
        Random rand = new Random();
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

        Tree tree = new Tree(2, 0.1f, ref periodicity, ref boxSize, 100);

        List<float> pos;
        float r = (float)rand.NextDouble();

        // pos = new List<float>(10)
        // {
        //     (float)rand.NextDouble()*10.0f, (float)rand.NextDouble()*10.0f,
        //     // (float)rand.NextDouble()*10.0f, (float)rand.NextDouble()*10.0f,
        //     // (float)rand.NextDouble()*10.0f, (float)rand.NextDouble()*10.0f,
        //     // (float)rand.NextDouble()*10.0f, (float)rand.NextDouble()*10.0f,
        //     // (float)rand.NextDouble()*10.0f, (float)rand.NextDouble()*10.0f,
        // };
        float randMulValue = 100.0f;
        float randRMulValue = 3.0f;// fix value로 사용중
        
        
#if FIX_DATA_TEST_VER
        pos = new List<float>(2) { 88.33573f,5.368899f };
        pos[0] = (float)rand.NextDouble() * 100.0f;
        pos[1] = (float)rand.NextDouble() * 100.0f;
        Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
        tree.InsertParticle(0, ref pos, randRMulValue);

        pos = new List<float>(2) { 42.113247f,13.384803f };
        Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
        tree.InsertParticle(1, ref pos, randRMulValue);

        pos = new List<float>(2) {37.538536f,49.318657f };
        Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
        tree.InsertParticle(2, ref pos, randRMulValue);

        pos = new List<float>(2) { 20.5316f,35.824306f };
        Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
        tree.InsertParticle(3, ref pos, randRMulValue);

        pos = new List<float>(2) { 71.68918f,7.4194775f };
        Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
        tree.InsertParticle(4, ref pos, randRMulValue);
        
#else // FIX_DATA_TEST_VER
        for (int i = 0; i < 10; ++i)
        {
            pos = new List<float>(2) { (float)rand.NextDouble() * 100.0f, (float)rand.NextDouble() * 100.0f };
            Console.Write(pos[0] + "---" + pos[1] + "))) r = " + randRMulValue + "\n");
            tree.InsertParticle(i, ref pos, randRMulValue);

        }


#endif // FIX_DATA_TEST_VER
        tree.printTree();
        
        Console.Write("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\n\n\n");
        int removeParticleID;
#if FIX_DATA_TEST_VER
        removeParticleID = 4;
#else // !FIX_DATA_TEST_VER
        removeParticleID = 20;        
#endif  // FIX_DATA_TEST_VER
        
        // tree.Query(4);
        // tree.RemoveParticle(removeParticleID);
        // tree.printTree();
    }
}
