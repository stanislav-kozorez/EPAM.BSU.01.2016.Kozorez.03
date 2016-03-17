using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynome p = new Polynome(0, 2, 0, 5, -5, 0, -53.6, -0.53);
            Console.WriteLine(p.ToString());
            Console.WriteLine("Power = {0}",p.Power);
            Console.WriteLine("index[0] = {0}",p.Indexes[0]);
            p.Indexes[0] = 10;
            Console.WriteLine("index[0] = {0}", p.Indexes[0]);
            Console.ReadKey();
        }
    }
}
