using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomomorphicEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            int nBase = 7;
            int xr = 9;
            Polynom p1 = new Polynom(257, nBase);
            Polynom p2 = new Polynom(18, nBase);
            Polynom sx = new Polynom(new int[]{-2,1});


            Polynom c1 = p1.Composition(sx);
            Polynom c2 = p2.Composition(sx);
            Console.WriteLine("p1: " + p1);
            Console.WriteLine("p2: " + p2);
            Console.WriteLine("s(x): " + sx);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("c1: " + c1);
            Console.WriteLine("c2: " + c2);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("p1 + p2: " + (p1 + p2));
            Console.WriteLine("(p1 + p2)(nBase): " + (p1 + p2).Value(nBase));
            Console.WriteLine("c1 + c2: " + (c1 + c2));
            Console.WriteLine("(c1 + c2)(xr): " + (c1 + c2).Value(xr));
            Console.WriteLine("-------------------------------");
            Console.WriteLine("p1 * p2: " + (p1 * p2));
            Console.WriteLine("(p1 * p2)(nBase): " + (p1 * p2).Value(nBase));
            Console.WriteLine("c1 * c2: " + (c1 * c2));
            Console.WriteLine("(c1 * c2)(xr): " + (c1 * c2).Value(xr));

            Console.ReadKey();
        }
    }
}
