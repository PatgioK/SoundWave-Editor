using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Waver
{
    unsafe class Recorder
    {
        /// <summary>
        /// Opens the recorder
        /// </summary>
        /// <returns>boolean if successful</returns>
        [DllImport("3770A3.dll", CharSet = CharSet.Auto)]
        public static extern Boolean start();

        /// <summary>
        /// Returns a byte pointer to memory of save buffer location
        /// </summary>
        /// <returns>byte pointer</returns>
        [DllImport("3770A3.dll", CharSet = CharSet.Auto)]
        public static extern byte** getSaveBuffer();
        
        /// <summary>
        /// Returns a byte pointer to memory of play buffer location
        /// </summary>
        /// <returns>byte pointer</returns>
        [DllImport("3770A3.dll", CharSet = CharSet.Auto)]
        public static extern byte** getPlayBuffer();

        /// <summary>
        /// Returns the data length of the wav file
        /// </summary>
        /// <returns>uint data length</returns>
        [DllImport("3770A3.dll", CharSet = CharSet.Auto)]
        public static extern uint getDataLength();

        /// <summary>
        /// Sets data length of a recorded wav file
        /// </summary>
        /// <param name="len"> length of file in a uint</param>
        /// <returns>uint</returns>
        [DllImport("3770A3.dll", CharSet = CharSet.Auto)]
        public static extern uint setDataLength(uint len);
    }
}
