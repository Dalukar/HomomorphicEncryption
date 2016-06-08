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
            //тестовые значения
            int a = 257;
            int b = 18;
            int nRoots = 1;

            int nBase;
            int xr;
            Polynom sx;

            // если есть входные аргументы, юзаем их
            if(args.Length !=0)
            {
                a = Convert.ToInt32(args[0]);
                b = Convert.ToInt32(args[1]);
                nRoots = Convert.ToInt32(args[2]);
            }
            else
            {
                Console.WriteLine("no arguments passed, using test values...");
            }

            // генерим полином и закрытый ключ
            int sqrt = Convert.ToInt32(Math.Floor(Math.Pow(a < b ? a : b, (double)1 / nRoots)));
            Random rnd = new Random();
            sqrt = rnd.Next(2, sqrt + 1);
            nBase = Convert.ToInt32(Math.Pow(sqrt, nRoots));
            int n = rnd.Next(-10, 10); // хз какой диапазон брать
            xr = sqrt - n;
            sx = (new Polynom(new double[] { n, 1 })) ^ nRoots;

            // преобразумем числа в полиномы
            Polynom p1 = new Polynom(a, nBase);
            Polynom p2 = new Polynom(b, nBase);

            // композиция с S(x)
            Polynom c1 = p1.Composition(sx);
            Polynom c2 = p2.Composition(sx);

            // результат деления
            Polynom[] divP = (p1 / p2);
            Polynom[] divC = (c1 / c2);

            // вывод в консоль
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("p1: " + p1);
            Console.WriteLine("p2: " + p2);
            Console.WriteLine("s(x): " + sx);
            Console.WriteLine("nBase: " + nBase);
            Console.WriteLine("xr: " + xr);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("c1: " + c1);
            Console.WriteLine("c2: " + c2);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("p1 + p2: " + (p1 + p2));
            Console.WriteLine("(p1 + p2)(nBase): " + (p1 + p2).Value(nBase));
            Console.WriteLine("c1 + c2: " + (c1 + c2));
            Console.WriteLine("(c1 + c2)(xr): " + (c1 + c2).Value(xr));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("p1 * p2: " + (p1 * p2));
            Console.WriteLine("(p1 * p2)(nBase): " + (p1 * p2).Value(nBase));
            Console.WriteLine("c1 * c2: " + (c1 * c2));
            Console.WriteLine("(c1 * c2)(xr): " + (c1 * c2).Value(xr));
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("p1 / p2: " + divP[0]);
            Console.WriteLine("remainder:\t" + divP[1]);
            Console.WriteLine("(p1 / p2)(nBase): " +Math.Round( divP[0].Value(nBase),2));
            Console.WriteLine("remainder:\t" + Math.Round(divP[1].Value(nBase), 2) + "/" + b);
            Console.WriteLine("c1 / c2: " + divC[0]);
            Console.WriteLine("remainder:\t" + divC[1]);
            Console.WriteLine("(c1 / c2)(xr): " + Math.Round(divC[0].Value(xr),2));
            Console.WriteLine("remainder:\t" + Math.Round(divC[1].Value(xr),2) + "/" + b);
            Console.WriteLine("----------------------------------------------");

            Console.ReadKey();
        }
    }
}
