using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForCoursera
{
    class PercolationStats
    {
        double[] results;

        public PercolationStats(int N, int T)    // perform T independent computational experiments on an N-by-N grid
        {
            results = new double[T];
            for (int i = 0; i < T; i++)
            {
                Percolation p = new Percolation(N);
                int count = 0;
                int[] helparray = new int[N * N];
                int helplength = helparray.Length;
                for (int j = 0; j < helparray.Length; j++)
                {
                    helparray[j] = j;
                }
                Random rnd = new Random();
                while (!p.percolates())
                {
                    int randomelement = rnd.Next(helplength);
                    int row = Convert.ToInt32(Math.Floor(helparray[randomelement] / (double)N)) + 1;
                    int column = helparray[randomelement] % N + 1;
                    helparray[randomelement] = helparray[--helplength];
                    p.open(row, column);
                    count++;
                }
                results[i] = (double)count / (N * N);
            }
        }

        public double mean()                     // sample mean of percolation threshold
        {
            double sum = 0;
            for (int i = 0; i < results.Length; i++)
            {
                sum += results[i];
            }
            return sum / results.Length;
        }

        public double stddev()                   // sample standard deviation of percolation threshold
        {
            double mn = mean();
            double sumofsquare = 0;
            for (int i = 0; i < results.Length; i++)
            {
                sumofsquare += Math.Pow(results[i] - mn, 2);
            }
            return Math.Sqrt(sumofsquare / (results.Length - 1));
        }

        public double confidenceLo()             // returns lower bound of the 95% confidence interval
        {
            return mean() - 1.96 * stddev() / Math.Sqrt(results.Length);
        }

        public double confidenceHi()             // returns upper bound of the 95% confidence interval
        {
            return mean() + 1.96 * stddev() / Math.Sqrt(results.Length);
        }

       /* public static void main(String[] args)   // test client, described below
        {
            if ((Convert.ToInt32(args[0]) <= 0)||(Convert.ToInt32(args[1]) <= 0)) throw new Exception("AZAZA");
            PercolationStats ps = new PercolationStats(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
            Console.WriteLine("mean \t= {0}", ps.mean());
            Console.WriteLine("stddev \t= {0}", ps.stddev());
            Console.WriteLine("95% confidence interval \t= {0}, {1}", ps.confidenceLo(), ps.confidenceHi());
        }*/
    }
}
