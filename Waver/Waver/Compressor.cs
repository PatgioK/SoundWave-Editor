using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waver
{
    class Compressor
    {
        public static int key = 255;


        /// <summary>
        /// Function runs Modified Run Length Encoding on a byte array.
        /// Converts to int and returns the int array in differential encoding style
        /// </summary>
        /// <param name="buffer"> byte array to be compressed</param>
        /// <returns>an int array in differential encoded MRLE format</returns>
        public static int[] compress(byte[] buffer)
        {
            int[] ints = new int[buffer.Length / 4];
            for(int i = 0; i < ints.Length; ++i)
            {
                ints[i] = BitConverter.ToInt32(buffer, i * 4);
            }
            int[] diffs = new int[ints.Length];

            diffs[0] = ints[0];

            for(int i = 1; i < ints.Length; ++i)
            {
                diffs[i] = ints[i] - ints[i - 1];
            }

            int[] MRLE = new int[diffs.Length];
            int length = 0;
            int val = 0;
            int newindex = 0;
            for(int i = 0; i < diffs.Length - 1; ++i)
            {
                val = diffs[i];
                length = 1;
                if (val == diffs[++i])
                {
                    while(val == diffs[i + length])
                    {
                        i++;
                        length++;
                    }
                }
                if(length > 1 || val == key)
                {
                    MRLE[newindex] = key;
                    newindex++;
                    MRLE[newindex] = length;
                    newindex++;
                    MRLE[newindex] = diffs[i];
                }
                else
                {
                    MRLE[i] = diffs[i];
                }

            }
            Array.Resize(ref MRLE, newindex);
            return MRLE;
        }

        /// <summary>
        /// Decompresses a byte array, Converts it to an Int array.
        /// reverse differential encoding
        /// </summary>
        /// <param name="buffer"> byte array to be decompressed</param>
        /// <returns>int array decompressed</returns>
        public static int[] decompress(byte[] buffer)
        {
            int[] ints = new int[buffer.Length * 4];
            for (int i = 0; i < ints.Length; ++i)
            {
                ints[i] = BitConverter.ToInt32(buffer, i * 4);
            }
            int[] origArr = new int[7483648];
            int index = 0;
            int newindex = 0;
            while(index < ints.Length)
            {
                if(ints[index] == key)
                {
                    int val = buffer[index + 2];
                    for(int i = 1; i < buffer[index++]; ++i)
                    {
                        origArr[index + i] = val;
                        newindex++;
                    }
                }
                else
                {
                    origArr[newindex] = buffer[index];
                    index++;
                    newindex++;
                }
            }
            Array.Resize(ref origArr, newindex);
            return origArr;
        }
    }
}
