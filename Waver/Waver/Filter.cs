using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waver
{
    public class Filter
    {

        /// <summary>
        /// Applies a High pass filter. Takes freq from a selection from chart2 frequency graph
        /// </summary>
        /// <param name="com1"> Array of complex numbers</param>
        /// <param name="com2"> Array of complex numbers if 2 channels</param>
        /// <param name="samples"> Array of samples</param>
        /// <param name="samples2"> Array of samples if 2 channels</param>
        /// <param name="bucket"> Frequency selected from freq graph </param>
        /// <param name="form"> Form1</param>
        /// <returns></returns>
        public static bool highPass(ref Complex[] com1, ref Complex[] com2, ref double[] samples, ref double[] samples2, int bucket, Form1 form)
        {
            int nyq = com1.Length;
            int start;
            int end;
            double[] filter = new double[0];
            double[] filter2 = new double[0];
            com1 = new Complex[com1.Length];

            for (int i = 0; i < com1.Length; i++)
            {
                com1[i] = new Complex(1, -1);
                if (samples2 != null)
                {
                    com2[i] = new Complex(1, -1);
                }
            }

            if (bucket == 0)
            {
                return false;
            }

            if (bucket < nyq)
            {
                start = bucket;
                end = com1.Length - bucket;
            }
            else if (bucket > nyq)
            {
                start = bucket - nyq;
                end = bucket;
            }
            else
            {
                com1[nyq].setReal(0);
                com1[nyq].setImaginary(0);
                Fourier.Inverse(com1, ref filter, form);

                Convolution(ref samples, filter);
                return true;
            }

            for (int i = 1; i < start; i++)
            {
                com1[i].setReal(0);
                com1[i].setImaginary(0);
            }

            for (int i = end; i < com1.Length; i++)
            {
                com1[i].setReal(0);
                com1[i].setImaginary(0);
            }
            Fourier.Inverse(com1, ref filter, form);
            Convolution(ref samples, filter);
            
            return true;
        }

        /// <summary>
        /// Applies a low pass filter. Takes freq from a selection from chart2 frequency graph
        /// </summary>
        /// <param name="com1"> Array of complex numbers</param>
        /// <param name="com2"> Array of complex numbers if 2 channels</param>
        /// <param name="samples"> Array of samples</param>
        /// <param name="samples2"> Array of samples if 2 channels</param>
        /// <param name="bucket"> Frequency selected from freq graph </param>
        /// <param name="form"> Form1</param>
        /// <returns></returns>
        public static bool lowPass(ref Complex[] com1, ref Complex[] com2, ref double[] samples, ref double[] samples2, int bucket, Form1 form)
        {
            int nyq = com1.Length / 2;
            int start;
            int end;
            double[] filter = new double[0];
            double[] filter2 = new double[0];
            com1 = new Complex[com1.Length];

            for (int i = 0; i < com1.Length; i++)
            {
                com1[i] = new Complex();
            }

            if (bucket == 0)
            {
                return false;
            }

            if (bucket < nyq)
            {
                start = bucket;
                end = com1.Length - bucket;
            }
            else if (bucket > nyq)
            {
                start = bucket - nyq;
                end = bucket;
            }
            else
            {
                com1[nyq].setReal(1);
                com1[nyq].setImaginary(-1);
                Fourier.Inverse(com1, ref filter, form);
                Convolution(ref samples, filter);

                return true;
            }

            for (int i = 0; i < start; i++)
            {
                com1[i].setReal(1);
                com1[i].setImaginary(-1);
                
            }
            for (int i = end; i < com1.Length; i++)
            {
                com1[i].setReal(1);
                com1[i].setImaginary(-1);
            }
            Fourier.Inverse(com1, ref filter, form);
            Convolution(ref samples, filter);
            
            return true;
        }

        /// <summary>
        /// Convolutes the filter passsed in to the original wav
        /// </summary>
        /// <param name="samples"> Array of samples </param>
        /// <param name="filter"> Array of Filter </param>
        private static void Convolution(ref double[] samples, double[] filter)
        {
            double[] fSamples = new double[samples.Length + filter.Length], temp = new double[samples.Length];
            System.Array.Copy(samples, fSamples, samples.Length);
            //Marshal.Copy(sample, fsamples, 0, (int)size);

            for (int i = 0; i < samples.Length; i++)
            {
                for(int f = 0; f < filter.Length; f++)
                {
                    temp[i] += (fSamples[i + f] * filter[f]);
                }
            }
            //Array.Resize(ref temp, samples.Length);
            samples = temp;
        }
    }
}
