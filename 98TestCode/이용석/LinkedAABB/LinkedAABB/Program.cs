using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Tree;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector2[] list = new Vector2[] { 
                new Vector2(1,1), new Vector2(2,2),
                new Vector2(-2,-2), new Vector2(-1,-1),
                new Vector2(0,2), new Vector2(1,7),
                new Vector2(2,1), new Vector2(3,2),
                new Vector2(7,2), new Vector2(11,4),
            };

            List<Node> listB = new List<Node>();
            var tree = new Tree();
            for (int i = 0; i < list.Length; i += 2)
            {
                var min = list[i];
                var max = list[i + 1];
                var vol = new Node(new BoundingBox(min, max));
                listB.Add(vol);
                tree.Insert(vol);
                tree.Print();
                Console.WriteLine("\n");
            }

            // Console.WriteLine("----- Remove -----\n");
            // Random random = new Random();
            // for(int i = 0; i < 4; i++)
            // {
            //     int rnd = random.Next() % (listB.Count);
            //     var rm = listB[rnd];
            //     listB.RemoveAt(rnd);
            //     tree.Remove(rm);
            //     tree.Print();
            //     Console.WriteLine("\n");
            // }
            //
            // Node[] rst = tree.Intersect(new BoundingVolume(new Vector3(0, 0, 0), new Vector3(4, 1, 2)));
            // foreach(var r in rst)
            // {
            //     Console.WriteLine(r.ToString());
            // }
        }
    }
}