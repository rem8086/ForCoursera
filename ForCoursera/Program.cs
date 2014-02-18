using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForCoursera
{
    class Program
    {
        static void Main(string[] args)
        {
            if ((Convert.ToInt32(args[0]) <= 0) || (Convert.ToInt32(args[1]) <= 0)) throw new Exception("AZAZA");
            PercolationStats ps = new PercolationStats(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
            Console.WriteLine("mean \t= {0}", ps.mean());
            Console.WriteLine("stddev \t= {0}", ps.stddev());
            Console.WriteLine("95% confidence interval \t= {0}, {1}", ps.confidenceLo(), ps.confidenceHi());
            Console.ReadLine();
        }
    }
}
