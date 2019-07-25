using System;
using System.IO;
using System.Text;

namespace Waver
{
    public unsafe class WaveReader
    {
        public byte[] byteArray;
        public int chunkID;
        public int chunkSize;
        public int waveFormat;
        public int fmtID;
        public int fmtSize;
        public int fmtCode;
        public int channels;
        public int sampleRate;
        public int bitRate;
        public int fmtBlockAlign;
        public int bitDepth;
        public int fmtExtraSize;
        public int dataID;
        public int dataSize;
        
        /// <summary>
        /// Converts 2 bytes in an array into a double
        /// </summary>
        /// <param name="firstByte"> first byte</param>
        /// <param name="secondByte"> second byte </param>
        /// <returns></returns>
        static double bytesToDouble(byte firstByte, byte secondByte)
        {
            short x = BitConverter.ToInt16(new byte[2] { firstByte, secondByte }, 0);
            return x;
        }
        
         /// <summary>
         /// Converts an int to an array of bytes
         /// </summary>
         /// <param name="i"> integer to convert</param>
         /// <returns>a byte array of the int</returns>
        public byte[] intToByteArr(int i)
        {
            byte[] x = BitConverter.GetBytes(i);
            return x;
        }

        /// <summary>
        /// Converts a double to an array of 2 bytes
        /// </summary>
        /// <param name="d"> double value goign to be converted</param>
        /// <returns> Array of 2 bytes </returns>
        static byte[] doubleToBytes(double d)
        {
            short x = (short)(d);
            return BitConverter.GetBytes(x);
        }

        /// <summary>
        /// Setup wav header for a recorded wav
        /// </summary>
        public void recorderWav()
        {
            byte** winData = Recorder.getPlayBuffer();
            uint size = Recorder.getDataLength();
            int lsize = Convert.ToInt32(size);
            
            chunkID = 1179011410;
            waveFormat = 1163280727;
            channels = 1;
            fmtCode = 1;
            sampleRate = 22050;
            bitRate = 22050;
            fmtBlockAlign = 1;
            bitDepth = 8;
            dataID = 1635017060;
            dataSize = (int)Recorder.getDataLength();
            
            byteArray = new byte[size];
            for (uint i = 0; i < size; ++i)
            {
                byteArray[i] = (*winData)[i];
            }
            
            chunkSize = byteArray.Length + 44 - 8;
        }
        
         /// <summary>
         /// Opens a file and reads its wav header and data.
         /// </summary>
         /// <param name="filename"> file name</param>
         /// <param name="left"> output to form 1 for left side of samples</param>
         /// <param name="right">output to form 1 for right side of samples if stereo </param>
        public void openWav(string filename, out double[] left, out double[] right)
        {
            int samples, pos = 0;
            BinaryReader reader;
            if (Form1.isCompress == 1)
            {
                byte[] stillcomp = File.ReadAllBytes(filename);
                int[] comps = Compressor.decompress(stillcomp);
                byte[] uncomp = new byte[comps.Length];
                for(int i = 0; i < comps.Length; i += 4)
                {
                    byte[] temp = intToByteArr(comps[i]);
                    uncomp[i] = temp[0];
                    uncomp[i + 1] = temp[1];
                    uncomp[i + 2] = temp[2];
                    uncomp[i + 3] = temp[3];
                }
                Stream S = new MemoryStream(uncomp);
                reader = new BinaryReader(S);
            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.Open);
                reader = new BinaryReader(fs);
            }

            chunkID = reader.ReadInt32();
            chunkSize = reader.ReadInt32();
            waveFormat = reader.ReadInt32();
            fmtID = reader.ReadInt32();
            fmtSize = reader.ReadInt32();
            fmtCode = reader.ReadInt16();
            channels = reader.ReadInt16();
            sampleRate = reader.ReadInt32();
            bitRate = reader.ReadInt32();
            fmtBlockAlign = reader.ReadInt16();
            bitDepth = reader.ReadInt16();

            if (fmtSize == 18)
            {
                fmtExtraSize = reader.ReadInt16(); //extra values
                reader.ReadBytes(fmtExtraSize);
            }

            dataID = reader.ReadInt32();
            dataSize = reader.ReadInt32();
            byteArray = reader.ReadBytes(dataSize);

            // Check bitDepth
            if (bitDepth == 16)
            {
                if (channels == 2) samples = dataSize / 4;
                else samples = dataSize / 2;
            }
            else
            {
                if (channels == 2) samples = dataSize / 2;
                else samples = dataSize;
            }
            
            left = new double[samples];
            if (channels == 2) right = new double[samples]; //null if mono, not null for right channel
            else right = null;

            if (bitDepth == 16)
            {
                int i = 0;
                while (pos < dataSize)
                {
                    left[i] = bytesToDouble(byteArray[pos], byteArray[pos + 1]);
                    pos += 2;
                    if (channels == 2)
                    {
                        right[i] = bytesToDouble(byteArray[pos], byteArray[pos + 1]);
                        pos += 2;
                    }
                    i++;
                }
            }
            else if (bitDepth == 8)
            {
                int i = 0;
                while (pos < dataSize)
                {
                    left[i] = Convert.ToDouble(byteArray[pos]);
                    pos++;
                    if (channels == 2)
                    {
                        right[i] = Convert.ToDouble(byteArray[pos]);
                        pos++;
                    }
                    i++;
                }
            }
        }
        
         /// <summary>
         /// Recalculates wav header and data before writing wav files
         /// </summary>
         /// <param name="left"> array ofleft side samples of audio</param>
         /// <param name="right"> array of right side samples</param>
        public void rebuildWav(double[] left, double[] right)
        {
            byte[] newWav;

            if (bitDepth == 16)
            {
                if (channels == 1)
                {
                    newWav = new byte[left.Length * 2];
                    for (int i = 0; i < left.Length; i++)
                    {
                        Buffer.BlockCopy(doubleToBytes(left[i]), 0, newWav, i * 2, 2);
                    }
                }
                else
                {
                    newWav = new byte[left.Length * 4];
                    int i = 0;
                    int pos = 0;
                    while (pos < newWav.Length)
                    {
                        Buffer.BlockCopy(doubleToBytes(left[i]), 0, newWav, pos, 2);
                        pos += 2;
                        if (channels == 2)
                        {
                            Buffer.BlockCopy(doubleToBytes(right[i]), 0, newWav, pos, 2);
                            pos += 2;
                        }
                        i++;
                    }
                }
            }
            else if (bitDepth == 8)
            {
                if (channels == 1)
                {
                    newWav = new byte[left.Length];
                    for (int i = 0; i < left.Length; i++)
                    {
                        Buffer.BlockCopy(doubleToBytes(left[i]), 0, newWav, i, 1);
                    }
                }
                else
                {
                    newWav = new byte[left.Length * 4];
                    int i = 0;
                    int pos = 0;
                    while (pos < newWav.Length)
                    {
                        Buffer.BlockCopy(doubleToBytes(left[i]), 0, newWav, pos, 1);
                        pos++;
                        if (channels == 2)
                        {
                            Buffer.BlockCopy(doubleToBytes(right[i]), 0, newWav, pos, 1);
                            pos++;
                        }
                        i++;
                    }
                }
            }
            else
            {
                return;
            }
            byteArray = newWav;
            dataSize = newWav.Length;
            chunkSize = newWav.Length + 44 - 8;
        }
        
         /// <summary>
         /// Function to write wav header and data to specified filename
         /// </summary>
         /// <param name="fileName"> file name to write wav file to.</param>
        public void writeWav(string fileName)
        {
            if (byteArray != null)
            {
                byte[] file = new byte[chunkSize + 8];

                Buffer.BlockCopy(intToByteArr(chunkID), 0, file, 0, 4);
                Buffer.BlockCopy(intToByteArr(chunkSize), 0, file, 4, 4);
                Buffer.BlockCopy(intToByteArr(waveFormat), 0, file, 8, 4);
                Buffer.BlockCopy(intToByteArr(fmtID), 0, file, 12, 4);
                Buffer.BlockCopy(intToByteArr(fmtSize), 0, file, 16, 4);
                Buffer.BlockCopy(intToByteArr(fmtCode), 0, file, 20, 2);
                Buffer.BlockCopy(intToByteArr(channels), 0, file, 22, 2);
                Buffer.BlockCopy(intToByteArr(sampleRate), 0, file, 24, 4);
                Buffer.BlockCopy(intToByteArr(bitRate), 0, file, 28, 4);
                Buffer.BlockCopy(intToByteArr(fmtBlockAlign), 0, file, 32, 2);
                Buffer.BlockCopy(intToByteArr(bitDepth), 0, file, 34, 2);
                Buffer.BlockCopy(intToByteArr(dataID), 0, file, 36, 4);

                Buffer.BlockCopy(intToByteArr(dataSize), 0, file, 40, 4);

                Buffer.BlockCopy(byteArray, 0, file, 44, dataSize);

                if(Form1.isCompress == 1)
                {
                    int[] temp = Compressor.compress(file);

                    byte[] temp2 = new byte[temp.Length * 4];

                    for (int i = 0; i < temp.Length; i += 4)
                    {
                        byte[] temp3 = intToByteArr(temp[i]);
                        temp2[i] = temp3[0];
                        temp2[i + 1] = temp3[1];
                        temp2[i + 2] = temp3[2];
                        temp2[i + 3] = temp3[3];
                    }
                    File.WriteAllBytes(fileName, temp2);

                    //Marshal.Copy(winData, convData, 0, (int)size);
                }
                else
                {
                    File.WriteAllBytes(fileName, file);
                }
            }
        }
    }
}
