Things Completed since demo:
- graphical filtering
- high pass and low pass filter
- threaded DFT, benchmark button for comparing 1, 2, and 4 threads
- Forward / Inverse DFT
- Convolution of filter
- 2 windowing functions
- cut copy paste
- microphone in

Things in progress:
- Compression //is done! but some writing file error
- Playback
- complete user thread amount selection
- stereo-ize everything

Known Issues:
- If selection is too big in timedomain, and you filter it, there is some part of the array that
has a different amplitude in the timedomain.

-Compression methods are done and work with tested arrays, having trouble writing 
in wav files though
