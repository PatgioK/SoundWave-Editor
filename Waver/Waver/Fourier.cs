using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Waver
{
    class Fourier
    {
        static Complex[] thrArrayData;
        /// <summary>
        /// Handles threaded DFTs, based on number of threads passed in
        /// calls relevant functions to graph processed data.
        /// </summary>
        /// <param name="left">left channel array in double</param>
        /// <param name="right">right channel array in double</param>
        /// <param name="com1">left channel array in complex</param>
        /// <param name="com2">right channel array in complex</param>
        /// <param name="f">form 1</param>
        public static void DFT(double[] left, double[] right, ref Complex[] com1, ref Complex[] com2, Form1 f, int threadNum)
        {
            int size = left.Length;
            thrArrayData = new Complex[size];
            Thread[] thrArray = new Thread[threadNum];

            for (int i = 0; i < size; i++)
            {
                thrArrayData[i] = new Complex();
            }

            if(threadNum == 1)
            {
                thrArray[0] = new Thread(() => { DFTthread(left, size, 0, threadNum); });
                thrArray[0].Start();
            }

            if(threadNum == 2)
            {
                thrArray[0] = new Thread(() => { DFTthread(left, size, 0, threadNum); });
                thrArray[0].Start();
                thrArray[1] = new Thread(() => { DFTthread(left, size, 1, threadNum); });
                thrArray[1].Start();
            }

            if(threadNum == 4)
            {
                thrArray[0] = new Thread(() => { DFTthread(left, size, 0, threadNum); });
                thrArray[0].Start();
                thrArray[1] = new Thread(() => { DFTthread(left, size, 1, threadNum); });
                thrArray[1].Start();
                thrArray[2] = new Thread(() => { DFTthread(left, size, 2, threadNum); });
                thrArray[2].Start();
                thrArray[3] = new Thread(() => { DFTthread(left, size, 3, threadNum); });
                thrArray[3].Start();
            }
            
            com1 = thrArrayData;
            MessageBox.Show("DFT num of threads: " + threadNum);
            f.Invoke(Form1.timeGraph, new object[] { com1, com2 });
        }

        /// <summary>
        /// Actual function that runs DFT. Based on number of threads wanted, splits up
        /// sample array and processes part of the array.
        /// </summary>
        /// <param name="left"> array of samples to be processed</param>
        /// <param name="size"> size of the sample array  </param>
        /// <param name="threadNum"> number of current thread running this function </param>
        /// <param name="maxThreads"> max threads to run this funciton </param>
        private static void DFTthread(double[] left, int size, int threadNum, int maxThreads)
        {
            int thNum = threadNum;
            double temp;
            Complex cmplx;
            double real; //real
            double imag; //imaginary

            int beginning = ((size / maxThreads) * (thNum - 1)), endPt = ((size / maxThreads) * (thNum));
            if (beginning < 0)
            {
                beginning = 0;
            }
            if (thNum == maxThreads - 1)
            {
                endPt = size;
            }

            for (int f = beginning; f < endPt; f++)
            {
                real = 0;
                imag = 0;
                for (int t = 0; t < size - 1; t++)
                {
                    real += left[t] * Math.Cos(2 * Math.PI * t * f / size);
                    imag -= left[t] * Math.Sin(2 * Math.PI * t * f / size);
                }
                cmplx = new Complex(real, imag);
                thrArrayData[f] = cmplx;
            }
        }

        /// <summary>
        /// DFT function that runs on two threads
        /// </summary>
        /// <param name="left"> array of samples to be processed</param>
        /// <param name="right"> array of samples to be processed, if 2 channels</param>
        /// <param name="com1"> Array of complex </param>
        /// <param name="com2">Array of complex if 2 channels</param>
        /// <param name="f"> Form 1</param>
        public static void DFT(double[] left, double[] right, ref Complex[] com1, ref Complex[] com2, Form1 f)
        {
            int N = left.Length;
            int N2 = 0;
            Complex[] a = new Complex[N], a2 = null;

            if (right != null)
            {
                N2 = right.Length;
                a2 = new Complex[N2];
            }

            for (int i = 0; i < N; i++)
            {
                a[i] = new Complex();

                if (right != null)
                {
                    a2[i] = new Complex();
                }
            }

            Thread frontdft1 = new Thread(() => beginDFT(left, ref a, N));
            Thread frontdft2 = new Thread(() => endDFT(left, ref a, N));

            frontdft1.Start();
            frontdft2.Start();
            
            frontdft1.Join();
            frontdft2.Join();

            com1 = a;

            if (a2 != null)
            {
                com2 = a2;
            }
            f.Invoke(Form1.timeGraph, new object[] { com1, com2 });
        }
        
         /// <summary>
         /// Processes first half of sample array, threading
         /// </summary>
         /// <param name="left"> Sample array</param>
         /// <param name="com1"> Complex Array</param>
         /// <param name="size"> Size of arrays</param>
        private static void beginDFT(double[] left, ref Complex[] com1, int size)
        {
            for (int f = 0; f < size / 2; f++)
            {
                double real = 0;
                double imag = 0;

                for (int t = 0; t < size; t++)
                {
                    real += left[t] * Math.Cos(2 * Math.PI * t * f / (double)size);
                    imag += -left[t] * Math.Sin(2 * Math.PI * t * f / (double)size);
                }

                com1[f].setReal(real);
                com1[f].setImaginary(imag);
            }
        }

        /// <summary>
        /// Processes second half of sample array, threading
        /// </summary>
        /// <param name="left"> Sample array</param>
        /// <param name="com1"> Complex Array</param>
        /// <param name="size"> Size of arrays</param>
        private static void endDFT(double[] left, ref Complex[] com1, int size)
        {
            for (int f = size / 2; f < size; f++)
            {
                double real = 0;
                double imag = 0;

                for (int t = 0; t < size; t++)
                {
                    real += left[t] * Math.Cos(2 * Math.PI * t * f / (double)size);
                    imag += -left[t] * Math.Sin(2 * Math.PI * t * f / (double)size);
                }
                com1[f].setReal(real);
                com1[f].setImaginary(imag);
            }
        }
        
         /// <summary>
         /// Function calculates inverse fourier and calls relevant funcs to graph it.
         /// </summary>
         /// <param name="com1"> Array of complex</param>
         /// <param name="left"> array of the samples</param>
         /// <param name="form"> form 1</param>
        public static void Inverse(Complex[] com1, ref double[] left, Form1 form)
        {
            int N = com1.Length;
            left = new double[N];

            for (int t = 0; t < N; t++)
            {
                for (int f = 0; f < N; f++)
                {
                    left[t] += (((com1[f].getReal() * Math.Cos(2 * Math.PI * t * f / (double)N))
                        - (com1[f].getImaginary() * Math.Sin(2 * Math.PI * t * f / (double)N))));
                }
                left[t] = left[t] / (double)N;
            }
            form.Invoke(form.freqGraph, new object[] { left });
        }
    }
}
