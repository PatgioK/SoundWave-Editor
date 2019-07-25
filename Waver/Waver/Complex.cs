using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waver
{
    public class Complex
    {
        private double real; // Real component of the complex number
        private double imag; // Imaginary component of the complex number

        /// <summary>
        /// Default constructor for a complex number
        /// </summary>
        public Complex()
        {
            real = 0;
            imag = 0;
        }
        
         /// <summary>
         /// Constructor for a complex number with known values
         /// </summary>
         /// <param name="real"> real part</param>
         /// <param name="imagenary"> imaginary part </param>
        public Complex(double real, double imagenary)
        {
            this.real = real;
            imag = imagenary;
        }
        
        /*
         * Method is a getter for the magnitude of the complex number.
         * Magnitude is calculated uing Pythagoras
         *  
         * return Magnitude of the complex number
         */
         /// <summary>
         /// Function that calculates magnitude of the complex number pythag
         /// </summary>
         /// <return> a double</return>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(real * real + imag * imag);
            }
        }

       
         /// <summary>
         /// sets the real part of a complex number
         /// </summary>
         /// <param name="real"></param>
        public void setReal(double real)
        {
            this.real = real;
        }
        
         /// <summary>
         /// Gets the real part of a complex number
         /// </summary>
         /// <returns>a double of the real</returns>
        public double getReal()
        {
            return real;
        }
        
         /// <summary>
         /// Sets the imaginary part of a complex number
         /// </summary>
         /// <param name="imaginary"></param>
        public void setImaginary(double imaginary)
        {
            imag = imaginary;
        }
        
         /// <summary>
         /// Gets the imaginary part of a complex number
         /// </summary>
         /// <returns>a double of the imaginary number</returns>
        public double getImaginary()
        {
            return imag;
        }
    }
}
