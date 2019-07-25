using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waver
{
    class Windowing
    {
        /// <summary>
        /// Applies the triangle filter to a selected number of samples
        /// </summary>
        /// <param name="samples"> Array of samples </param>
        /// <param name="num"> Number of samples in the array </param>
        public static void triWindow(ref double[] samples, double num)
        {
            double[] weight = new double[(int)num];

            for (int k = 0; k < num; k++)
            {
                weight[k] = (2 / num) * (2 / num - Math.Abs(k - (num - 1) / 2));
            }
            int j = 0;
            for (int i = 0; i < samples.Length;)
            {
                samples[i++] *= weight[j++];
                if (j == num) { j = 0; }
            }
        }
        
       /// <summary>
       /// Applies the hamming window to a selection
       /// </summary>
       /// <param name="samples"> Array of samples </param>
       /// <param name="num"> number of samples </param>
        public static void hamWindow(ref double[] samples, double num)
        {
            double[] weight = new double[(int)num];
            for (int k = 0; k < num; k++)
            {
                weight[k] = 0.538836 - 0.46164 * Math.Cos(2 * Math.PI * k / (num - 1));
            }
            int t = 0;
            for (int i = 0; i < samples.Length;)
            {
                samples[i++] *= weight[t++];
                if (t == num) { t = 0; }
            }
        }
    }
}
