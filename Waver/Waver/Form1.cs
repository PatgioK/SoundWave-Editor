using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace Waver
{
    public unsafe partial class Form1 : Form
    {
        // Data containers
        public WaveReader wave = null;
        public double[] rite;
        public double[] lft;
        public Complex[] com1;
        public Complex[] com2 = null;
        
        //recorder variables
        public static int isRecording = 2;  //set to 1 if current wav is recorded, 0 if changed to new imported wav
        public static int isCompress = 2;
        public int dist, offset;
        public delegate void graph(Complex[] A, Complex[] A2);
        public delegate void inverse(double[] s);
        public static graph timeGraph;
        public inverse freqGraph;
        
        public Form1()
        {
            InitializeComponent();
            chart1.MouseWheel += new MouseEventHandler(ch1_MouseWheel);
            chart1.MouseClick += new MouseEventHandler(ch1_MouseClick);
            chart2.MouseClick += new MouseEventHandler(ch2_MouseClick);
            timeGraph = new graph(graphDFT);
            freqGraph = new inverse(graphInverse);
        }
        
        /// <summary>
        /// Opens dialog box to select wav file to open and calls relevant functions
        /// to plot time / frequency graphs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Wav Files|*.wav";
            openFile.Title = "Select a File";
            isRecording = 0;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                lft = null;
                rite = null;
                wave = null;

                wave = new WaveReader();
                wave.openWav(openFile.FileName, out lft, out rite);
                this.Invoke(freqGraph, new object[] { lft });

                if (lft.Length < 500)
                {
                    offset = 0;
                    dist = lft.Length;
                }
                else
                {
                    offset = lft.Length / 2 - 250;
                    dist = 500;
                }
                windowing();
            }
        }

       /// <summary>
       /// Opens the dll recorder, resets data containers and wave reader
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void openRecorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lft = null;
            rite = null;
            wave = null;
            wave = new WaveReader();
            Recorder.start();
        }

        /// <summary>
        /// Takes selection from chart1 and applies windowing before DFT
        /// Passes data to relevant function to graph new frequency chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Click(object sender, EventArgs e)
        {
            int start = (int)chart1.ChartAreas[0].CursorX.SelectionStart;
            int end = (int)chart1.ChartAreas[0].CursorX.SelectionEnd;

            if ((start >= 0) && (start <= lft.Length))
            {
                if ((end >= 0) && (end <= lft.Length))
                {
                    if (start < end)
                    {
                        dist = end - start;
                        offset = start;
                    }
                    else
                    {
                        dist = start - end;
                        offset = end;
                    }
                    windowing();
                }
                else
                {
                    MessageBox.Show("Make a selection!");
                }
            }
            else
            {
                MessageBox.Show("Make a selection!");
            }
        }

        /// <summary>
        /// Applies selected filter to selection in frequency graph.
        /// calls relevant functions to plot new time domain graph.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filter_Click(object sender, EventArgs e)
        {
            int select = (int)chart2.ChartAreas[0].CursorX.SelectionStart;
            Thread filter = null;

            if ((select >= 0) && (select <= com1.Length))
            {
                if (highPass.Checked)
                {
                    filter = new Thread(() => highfilter(select));
                }
                if (lowPass.Checked)
                {
                    filter = new Thread(() => lowfilter(select));
                }

                if (filter != null)
                {
                    //clear existing data on charts
                    chart1.Series["series1"].Points.Clear();
                    chart2.Series["series2"].Points.Clear();

                    filter.Start();
                }
            }
            else
            {
                MessageBox.Show("Error: Please Make a Selection first.");
            }
        }

        /// <summary>
        /// Saves a new wav file. Opens dialog box.
        /// Calls relevant functions to set wav headers before writing file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Wav Files|*.wav";
            saveFile.Title = "Save a wav File";
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                if (isRecording == 1)
                {
                    wave.recorderWav();
                }
                else
                {
                    wave.rebuildWav(lft, rite);
                }
                wave.writeWav(saveFile.FileName);
            }

        }

        /// <summary>
        /// Cuts (copies data to clipboard then deletes) a selection from chart 1 time domain graph.
        /// Updates time graph with new wav.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = (int)chart1.ChartAreas[0].CursorX.SelectionStart;
            int end = (int)chart1.ChartAreas[0].CursorX.SelectionEnd;
            int range, offset;

            if (start < end)
            {
                range = end - start;
                offset = start;
            }
            else
            {
                range = start - end;
                offset = end;
            }

            double[] selection = new double[range];
            double[] tmp = new double[lft.Length - range];
            double[] selection2 = null;
            double[] tmp2 = null;

            if (rite != null)
            {
                selection2 = new double[range];
                tmp2 = new double[rite.Length - range];
            }

            for (int i = 0; i < range; i++)
            {
                selection[i] = lft[i + offset];
                if (rite != null)
                {
                    selection2[i] = rite[i + offset];
                }
            }

            if (rite != null)
            {
                double[] temp = new double[selection.Length + selection2.Length];
                Array.Copy(selection, temp, selection.Length);
                Array.Copy(selection2, 0, temp, selection.Length, selection2.Length);
                Clipboard.SetData("Copy", temp);

            }
            else
            {
                Clipboard.SetData("Copy", selection);
            }

            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            Array.Copy(lft, 0, tmp, 0, start);
            Array.Copy(lft, end, tmp, start, tmp.Length - start);
            lft = tmp;

            if (rite != null)
            {
                Array.Copy(rite, 0, tmp2, 0, start);
                Array.Copy(rite, end, tmp2, start, tmp2.Length - start);
                rite = tmp2;
            }
            this.Invoke(freqGraph, new object[] { lft });
        }

        /// <summary>
        /// Copies a selection from the time domain graph onto clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int start = (int)chart1.ChartAreas[0].CursorX.SelectionStart;
            int end = (int)chart1.ChartAreas[0].CursorX.SelectionEnd;
            int range, offset;

            if (start < end)
            {
                range = end - start;
                offset = start;
            }
            else
            {
                range = start - end;
                offset = end;
            }

            double[] selection = new double[range];
            double[] selection2 = null;
            if (rite != null)
            {
                selection2 = new double[range];
            }

            for (int i = 0; i < range; i++)
            {
                selection[i] = lft[i + offset];
                if (rite != null)
                {
                    selection2[i] = rite[i + offset];
                }
            }

            if (rite != null)
            {
                double[] tmp = new double[selection.Length + selection2.Length];
                //Marshal.Copy(winData, convData, 0, (int)size);
                Array.Copy(selection, tmp, selection.Length);
                Array.Copy(selection2, 0, tmp, selection.Length, selection2.Length);
                Clipboard.SetData("Copy", tmp);
            }
            else
            {
                Clipboard.SetData("Copy", selection);
            }
        }

        /// <summary>
        /// Pastes data from clipboard onto time domain graph at mouse pointer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] tmp = (double[])Clipboard.GetData("Copy");
            double[] paste = null;
            double[] paste2 = null;

            //Marshal.Copy(winData, convData, 0, (int)size);
            int point = (int)chart1.ChartAreas[0].CursorX.SelectionStart;

            if (rite != null)
            {
                paste = new double[tmp.Length / 2];
                paste2 = new double[tmp.Length / 2];
                Array.Copy(tmp, paste, tmp.Length / 2);
                Array.Copy(tmp, tmp.Length / 2, paste2, 0, tmp.Length / 2);
            }
            else
            {
                paste = tmp;
            }

            double[] newWave = new double[paste.Length + lft.Length];
            double[] newWave2 = null;

            if (rite != null)
            {
                newWave2 = new double[paste2.Length + rite.Length];
            }

            Array.Copy(lft, 0, newWave, 0, point);
            Array.Copy(paste, 0, newWave, point, paste.Length);
            Array.Copy(lft, point, newWave, point + paste.Length, lft.Length - point);
            lft = newWave;

            if (rite != null)
            {
                Array.Copy(rite, 0, newWave2, 0, point);
                Array.Copy(paste2, 0, newWave2, point, paste2.Length);
                Array.Copy(rite, point, newWave2, point + paste2.Length, rite.Length - point);
                rite = newWave2;
            }

            this.Invoke(freqGraph, new object[] { lft });
            
        }

        /// <summary>
        /// Graphs the wav from the recorder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphRecorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRecording = 1;
            //IntPtr winData = (IntPtr)Recorder.getPlayBuffer();
            byte** winData = Recorder.getPlayBuffer();
            uint size = Recorder.getDataLength();
            byte[] byteArray = new byte[size];

            //Marshal.Copy(winData, convData, 0, (int)size);
            for (uint i = 0; i < size; ++i)
            {
                byteArray[i] = (*winData)[i];
            }

            lft = null;
            int lsize =  Convert.ToInt32(size);
            lft = new double[lsize];
            for(uint i = 0; i < size; i++)
            {
                //left[i] = BitConverter.ToDouble(byteArray, 0);
                lft[i] = (double)(byteArray[i] - 128);
            }
            windowing();
            this.Invoke(freqGraph, new object[] { lft });
        }

        /// <summary>
        /// Calculates data from complex and graphs points onto frequency graph
        /// </summary>
        /// <param name="A"></param>
        /// <param name="A2"></param>
        public void graphDFT(Complex[] A, Complex[] A2)
        {
            int n = A.Length;

            chart2.Series["series2"].Points.Clear();

            for (int i = 1; i < n; i++)
            {
                chart2.Series["series2"].Points.Add(A[i].Magnitude);
                
            }

            chart2.Series["series2"].ChartType = SeriesChartType.Column;
            chart2.Series["series2"].Color = Color.Blue;

            if (A2 != null)
            {
                chart2.Series["series2"].ChartType = SeriesChartType.Column;
                chart2.Series["series2"].Color = Color.Blue;
            }
        }

        /// <summary>
        /// Window function that takes selection and applies selected windows to it.
        /// </summary>
        public void windowing()
        {
            double[] winSample, winSample2 = null;
            Thread dft = null;
            com1 = new Complex[dist];
            winSample = new double[dist];
            Array.Copy(lft, offset, winSample, 0, dist);
            int numThreads;
            
            if (radioButton1.Checked)
            {
                numThreads = 1;
            }
            if(radioButton2.Checked)
            {
                numThreads = 2;
            }
            if(radioButton3.Checked)
            {
                numThreads = 4;
            }

            if (rite != null)
            {
                com2 = new Complex[dist];
                winSample2 = new double[dist];
                Array.Copy(rite, offset, winSample2, 0, dist);
            }

            if (TriangleBut.Checked)
            {
                Windowing.triWindow(ref winSample, dist);
                dft = new Thread(() => Fourier.DFT(winSample, winSample2, ref com1, ref com2, this));
            }

            if (HammingBut.Checked)
            {
                Windowing.hamWindow(ref winSample, dist);
                dft = new Thread(() => Fourier.DFT(winSample, winSample2, ref com1, ref com2, this));
            }
            dft = new Thread(() => Fourier.DFT(winSample, winSample2, ref com1, ref com2, this));

            if (dft != null)
            {
                dft.Start();
            }
        }

        /// <summary>
        /// Applies low pass filter to selection in freq chart.
        /// Updates time domain chart after filtering.
        /// </summary>
        /// <param name="select">X cursor position in chart, represents bucket</param>
        private void lowfilter(int select)
        {
            Filter.lowPass(ref com1, ref com2, ref lft, ref rite, select, this);
            this.Invoke(freqGraph, new object[] { lft });
            windowing();
        }

        /// <summary>
        /// Applies high pass filter to selection in freq chart.
        /// </summary>
        /// <param name="select">X cursor position in chart, represents bucket</param>
        private void highfilter(int select)
        {
            Filter.highPass(ref com1, ref com2, ref lft, ref rite, select, this);
            this.Invoke(freqGraph, new object[] { lft });
            windowing();
        }


        /// <summary>
        /// Takes data and adds it to chart 1 to graph.
        /// </summary>
        /// <param name="s"></param>
        public void graphInverse(double[] s)
        {

            chart1.Series["series1"].Points.Clear();

            for (int i = 1; i < s.Length; i++)
            {
                chart1.Series["series1"].Points.Add(s[i]);
            }

            chart1.Series["series1"].ChartType = SeriesChartType.FastLine;
            chart1.Series["series1"].Color = Color.Blue;
        }

        /// <summary>
        /// Selects an area in chart 1, time domain graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ch1_MouseClick(object sender, MouseEventArgs e)
        {
            chart1.ChartAreas[0].CursorX.SelectionStart = chart1.ChartAreas[0].CursorX.SelectionStart;
            chart1.ChartAreas[0].CursorX.SelectionEnd = chart1.ChartAreas[0].CursorX.SelectionEnd;

            chart1.UpdateCursor();
        }

        /// <summary>
        /// Zoom function for chart 1, time domain graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ch1_MouseWheel(object sender, MouseEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            try
            {
                if (e.Delta < 0)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;

                    double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;

                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch { }
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
        }

        /// <summary>
        /// Runs DFT on 1, 2, and 4 threads and compares run times.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //thread benchmarking
            Thread dft = null;
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            dft = new Thread(() => Fourier.DFT(lft, rite, ref com1, ref com2, this, 1));
            sw1.Stop();


            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            dft = new Thread(() => Fourier.DFT(lft, rite, ref com1, ref com2, this, 2));
            sw2.Stop();

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            dft = new Thread(() => Fourier.DFT(lft, rite, ref com1, ref com2, this, 4));
            sw3.Stop();

            MessageBox.Show("1 thread time: " + sw1.Elapsed + ". 2 thread time: " + sw2.Elapsed + ". 4 thread time: " + sw3.Elapsed);

            //thread benchmarking
        }

        private void saveToCompressedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isCompress = 1;
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Wav Files|*.wav";
            saveFile.Title = "Save a wav File";
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                if (isRecording == 1)
                {
                    wave.recorderWav();
                }
                else
                {
                    wave.rebuildWav(lft, rite);
                }
                wave.writeWav(saveFile.FileName);
            }
            isCompress = 0;
        }

        private void openCompressedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                isCompress = 1;
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Wav Files|*.wav";
                openFile.Title = "Select a File";
                isRecording = 0;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    lft = null;
                    rite = null;
                    wave = null;

                    wave = new WaveReader();
                    wave.openWav(openFile.FileName, out lft, out rite);
                    this.Invoke(freqGraph, new object[] { lft });

                    if (lft.Length < 500)
                    {
                        offset = 0;
                        dist = lft.Length;
                    }
                    else
                    {
                        offset = lft.Length / 2 - 250;
                        dist = 500;
                    }
                    windowing();
                }
            }
            isCompress = 0;
        }

        /// <summary>
        /// Selection area for chart 2, frequency graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ch2_MouseClick(object sender, MouseEventArgs e)
        {
            int bucket = (int)chart2.ChartAreas[0].CursorX.SelectionStart;
            int start, end;

            if (bucket < lft.Length / 2)
            {
                start = bucket;
                end = com1.Length - bucket;
            }
            else if (bucket > lft.Length)
            {
                end = bucket;
                start = com1.Length / 2 - bucket;
            }
            else
            {
                start = end = bucket;
            }

            chart2.ChartAreas[0].CursorX.SelectionStart = start;
            chart2.ChartAreas[0].CursorX.SelectionEnd = end;
            chart2.UpdateCursor();
        }

    }
}
