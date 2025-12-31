using System;
using System.Runtime.InteropServices;

namespace Signalsmith
{
    /// <summary>
    /// Dotnet C# binding for Signalsmith Dynamic STFT
    /// </summary>
    public class STFT
    {
        public unsafe void* Handle;

        public STFT() : this(false)
        {
        }

        public STFT(bool splitComputation)
        {
            unsafe {
                Handle = Native.STFT_Create(splitComputation);
            }
        }

        public void Release()
        {
            unsafe {
                Native.STFT_Delete(Handle);
                Handle = null;
            }
        }

        public void Configure(int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry)
        {
            unsafe {
                Native.STFT_Configure(Handle, inChannels, outChannels, blockSamples, extraInputHistory, intervalSamples, asymmetry);
            }
        }

        public UIntPtr BlockSamples()
        {
            unsafe {
                return Native.STFT_BlockSamples(Handle);
            }
        }

        public UIntPtr FFTSamples()
        {
            unsafe {
                return Native.STFT_FFTSamples(Handle);
            }
        }

        public UIntPtr Bands()
        {
            unsafe {
                return Native.STFT_Bands(Handle);
            }
        }

        public void Reset()
        {
            unsafe {
                Native.STFT_Reset(Handle);
            }
        }

        public void WriteInput(UIntPtr channel, UIntPtr offset, UIntPtr length, float[] inputArray)
        {
            unsafe {
                fixed (float* inputPtr = inputArray)
                {
                    Native.STFT_WriteInput(Handle, channel, offset, length, inputPtr);
                }
            }
        }

#if NET7_0_OR_GREATER
        public void WriteInput(UIntPtr channel, UIntPtr offset, UIntPtr length, Span<float> inputSpan)
        {
            unsafe {
                fixed (float* inputPtr = inputSpan)
                {
                    Native.STFT_WriteInput(Handle, channel, offset, length, inputPtr);
                }
            }
        }
#endif

        public unsafe void WriteInput(UIntPtr channel, UIntPtr offset, UIntPtr length, float* inputPtr)
        {
            Native.STFT_WriteInput(Handle, channel, offset, length, inputPtr);
        }

        public void ReadOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float[] outputArray)
        {
            unsafe {
                fixed (float* outputPtr = outputArray)
                {
                    Native.STFT_ReadOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }

#if NET7_0_OR_GREATER
        public void ReadOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, Span<float> outputSpan)
        {
            unsafe {
                fixed (float* outputPtr = outputSpan)
                {
                    Native.STFT_ReadOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }
#endif

        public unsafe void ReadOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputPtr)
        {
            Native.STFT_ReadOutput(Handle, channel, offset, length, outputPtr);
        }

        public void MoveInput(UIntPtr samples, bool clearMovedRegion)
        {
            unsafe {
                Native.STFT_MoveInput(Handle, samples, clearMovedRegion);
            }
        }

        public void SetInterval(UIntPtr defaultInterval, int windowShape, float asymmetry)
        {
            unsafe {
                Native.STFT_SetInterval(Handle, defaultInterval, windowShape, asymmetry);
            }
        }

        public void Analyse(UIntPtr sampleInPast)
        {
            unsafe {
                Native.STFT_Analyse(Handle, sampleInPast);
            }
        }

        public void Synthesise()
        {
            unsafe {
                Native.STFT_Synthesise(Handle);
            }
        }

        public float[] Spectrum(UIntPtr channel)
        {
            unsafe {
                float* specPtr = Native.STFT_Spectrum(Handle, channel);
                return new float[] { specPtr[0], specPtr[1] };
            }
        }

#if NET7_0_OR_GREATER
        public void GetSpectrum(UIntPtr channel, Span<float> spectrumSpan)
        {
            unsafe {
                float* specPtr = Native.STFT_Spectrum(Handle, channel);
                spectrumSpan[0] = specPtr[0];
                spectrumSpan[1] = specPtr[1];
            }
        }
#endif

        public float[] AnalysisWindow()
        {
            unsafe {
                float* windowPtr = Native.STFT_AnalysisWindow(Handle);
                int size = (int)BlockSamples();
                float[] window = new float[size];
                for (int i = 0; i < size; i++)
                {
                    window[i] = windowPtr[i];
                }
                return window;
            }
        }

        public float[] SynthesisWindow()
        {
            unsafe {
                float* windowPtr = Native.STFT_SynthesisWindow(Handle);
                int size = (int)BlockSamples();
                float[] window = new float[size];
                for (int i = 0; i < size; i++)
                {
                    window[i] = windowPtr[i];
                }
                return window;
            }
        }

#if NET7_0_OR_GREATER
        public bool GetAnalysisWindow(Span<float> windowSpan)
        {
            unsafe {
                float* windowPtr = Native.STFT_AnalysisWindow(Handle);
                int size = (int)BlockSamples();
                if (windowSpan.Length < size)
                    return false;
                for (int i = 0; i < size; i++)
                {
                    windowSpan[i] = windowPtr[i];
                }
                return true;
            }
        }

        public bool GetSynthesisWindow(Span<float> windowSpan)
        {
            unsafe {
                float* windowPtr = Native.STFT_SynthesisWindow(Handle);
                int size = (int)BlockSamples();
                if (windowSpan.Length < size)
                    return false;
                for (int i = 0; i < size; i++)
                {
                    windowSpan[i] = windowPtr[i];
                }
                return true;
            }
        }
#endif

        public UIntPtr AnalysisLatency()
        {
            unsafe {
                return Native.STFT_AnalysisLatency(Handle);
            }
        }

        public UIntPtr SynthesisLatency()
        {
            unsafe {
                return Native.STFT_SynthesisLatency(Handle);
            }
        }

        public UIntPtr Latency()
        {
            unsafe {
                return Native.STFT_Latency(Handle);
            }
        }

        public float BinToFreq(float b)
        {
            unsafe {
                return Native.STFT_BinToFreq(Handle, b);
            }
        }

        public float FreqToBin(float f)
        {
            unsafe {
                return Native.STFT_FreqToBin(Handle, f);
            }
        }

        public UIntPtr AnalyseSteps()
        {
            unsafe {
                return Native.STFT_AnalyseSteps(Handle);
            }
        }

        public UIntPtr SynthesiseSteps()
        {
            unsafe {
                return Native.STFT_SynthesiseSteps(Handle);
            }
        }

        public void AnalyseStep(UIntPtr step, UIntPtr sampleInPast)
        {
            unsafe {
                Native.STFT_AnalyseStep(Handle, step, sampleInPast);
            }
        }

        public void SynthesiseStep(UIntPtr step)
        {
            unsafe {
                Native.STFT_SynthesiseStep(Handle, step);
            }
        }

        public UIntPtr SamplesSinceAnalysis()
        {
            unsafe {
                return Native.STFT_SamplesSinceAnalysis(Handle);
            }
        }

        public UIntPtr SamplesSinceSynthesis()
        {
            unsafe {
                return Native.STFT_SamplesSinceSynthesis(Handle);
            }
        }

        public void FinishOutput(float strength, UIntPtr offset)
        {
            unsafe {
                Native.STFT_FinishOutput(Handle, strength, offset);
            }
        }

        public void AddOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float[] outputArray)
        {
            unsafe {
                fixed (float* outputPtr = outputArray)
                {
                    Native.STFT_AddOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }
        
#if NET7_0_OR_GREATER
        public void AddOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, Span<float> outputSpan)
        {
            unsafe {
                fixed (float* outputPtr = outputSpan)
                {
                    Native.STFT_AddOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }
#endif

        public unsafe void AddOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputPtr)
        {
            Native.STFT_AddOutput(Handle, channel, offset, length, outputPtr);
        }

        public void ReplaceOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float[] outputArray)
        {
            unsafe {
                fixed (float* outputPtr = outputArray)
                {
                    Native.STFT_ReplaceOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }

#if NET7_0_OR_GREATER
        public void ReplaceOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, Span<float> outputSpan)
        {
            unsafe {
                fixed (float* outputPtr = outputSpan)
                {
                    Native.STFT_ReplaceOutput(Handle, channel, offset, length, outputPtr);
                }
            }
        }
#endif

        public unsafe void ReplaceOutput(UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputPtr)
        {
            Native.STFT_ReplaceOutput(Handle, channel, offset, length, outputPtr);
        }

        public void MoveOutput(UIntPtr samples)
        {
            unsafe {
                Native.STFT_MoveOutput(Handle, samples);
            }
        }

        public void AnalysisOffset(UIntPtr offset)
        {
            unsafe {
                Native.STFT_AnalysisOffset(Handle, offset);
            }
        }

        public void SynthesisOffset(UIntPtr offset)
        {
            unsafe {
                Native.STFT_SynthesisOffset(Handle, offset);
            }
        }
    }

    internal static partial class Native
    {
#if NET7_0_OR_GREATER
        [LibraryImport(DllName, EntryPoint = "STFT_Create")]
        public static unsafe partial void* STFT_Create([MarshalAs(UnmanagedType.I1)] bool splitComputation);

        [LibraryImport(DllName, EntryPoint = "STFT_Delete")]
        public static unsafe partial void STFT_Delete(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_Configure")]
        public static unsafe partial void STFT_Configure(void* stft, int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry);

        [LibraryImport(DllName, EntryPoint = "STFT_BlockSamples")]
        public static unsafe partial UIntPtr STFT_BlockSamples(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_FFTSamples")]
        public static unsafe partial UIntPtr STFT_FFTSamples(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_Bands")]
        public static unsafe partial UIntPtr STFT_Bands(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_Reset")]
        public static unsafe partial void STFT_Reset(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_WriteInput")]
        public static unsafe partial void STFT_WriteInput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* inputArray);

        [LibraryImport(DllName, EntryPoint = "STFT_ReadOutput")]
        public static unsafe partial void STFT_ReadOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [LibraryImport(DllName, EntryPoint = "STFT_MoveInput")]
        public static unsafe partial void STFT_MoveInput(void* stft, UIntPtr samples, [MarshalAs(UnmanagedType.I1)] bool clearMovedRegion);

        [LibraryImport(DllName, EntryPoint = "STFT_SetInterval")]
        public static unsafe partial void STFT_SetInterval(void* stft, UIntPtr defaultInterval, int windowShape, float asymmetry);

        [LibraryImport(DllName, EntryPoint = "STFT_Analyse")]
        public static unsafe partial void STFT_Analyse(void* stft, UIntPtr sampleInPast);

        [LibraryImport(DllName, EntryPoint = "STFT_Synthesise")]
        public static unsafe partial void STFT_Synthesise(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_Spectrum")]
        public static unsafe partial float* STFT_Spectrum(void* stft, UIntPtr channel);

        [LibraryImport(DllName, EntryPoint = "STFT_AnalysisWindow")]
        public static unsafe partial float* STFT_AnalysisWindow(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_SynthesisWindow")]
        public static unsafe partial float* STFT_SynthesisWindow(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_AnalysisLatency")]
        public static unsafe partial UIntPtr STFT_AnalysisLatency(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_SynthesisLatency")]
        public static unsafe partial UIntPtr STFT_SynthesisLatency(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_Latency")]
        public static unsafe partial UIntPtr STFT_Latency(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_BinToFreq")]
        public static unsafe partial float STFT_BinToFreq(void* stft, float b);

        [LibraryImport(DllName, EntryPoint = "STFT_FreqToBin")]
        public static unsafe partial float STFT_FreqToBin(void* stft, float f);

        [LibraryImport(DllName, EntryPoint = "STFT_AnalyseSteps")]
        public static unsafe partial UIntPtr STFT_AnalyseSteps(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_SynthesiseSteps")]
        public static unsafe partial UIntPtr STFT_SynthesiseSteps(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_AnalyseStep")]
        public static unsafe partial void STFT_AnalyseStep(void* stft, UIntPtr step, UIntPtr sampleInPast);

        [LibraryImport(DllName, EntryPoint = "STFT_SynthesiseStep")]
        public static unsafe partial void STFT_SynthesiseStep(void* stft, UIntPtr step);

        [LibraryImport(DllName, EntryPoint = "STFT_SamplesSinceAnalysis")]
        public static unsafe partial UIntPtr STFT_SamplesSinceAnalysis(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_SamplesSinceSynthesis")]
        public static unsafe partial UIntPtr STFT_SamplesSinceSynthesis(void* stft);

        [LibraryImport(DllName, EntryPoint = "STFT_FinishOutput")]
        public static unsafe partial void STFT_FinishOutput(void* stft, float strength, UIntPtr offset);

        [LibraryImport(DllName, EntryPoint = "STFT_AddOutput")]
        public static unsafe partial void STFT_AddOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [LibraryImport(DllName, EntryPoint = "STFT_ReplaceOutput")]
        public static unsafe partial void STFT_ReplaceOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [LibraryImport(DllName, EntryPoint = "STFT_MoveOutput")]
        public static unsafe partial void STFT_MoveOutput(void* stft, UIntPtr samples);

        [LibraryImport(DllName, EntryPoint = "STFT_AnalysisOffset")]
        public static unsafe partial void STFT_AnalysisOffset(void* stft, UIntPtr offset);

        [LibraryImport(DllName, EntryPoint = "STFT_SynthesisOffset")]
        public static unsafe partial void STFT_SynthesisOffset(void* stft, UIntPtr offset);
#else
        [DllImport(DllName, EntryPoint = "STFT_Create")]
        public static extern unsafe void* STFT_Create(bool splitComputation);

        [DllImport(DllName, EntryPoint = "STFT_Delete")]
        public static extern unsafe void STFT_Delete(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_Configure")]
        public static extern unsafe void STFT_Configure(void* stft, int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry);

        [DllImport(DllName, EntryPoint = "STFT_BlockSamples")]
        public static extern unsafe UIntPtr STFT_BlockSamples(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_FFTSamples")]
        public static extern unsafe UIntPtr STFT_FFTSamples(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_Bands")]
        public static extern unsafe UIntPtr STFT_Bands(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_Reset")]
        public static extern unsafe void STFT_Reset(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_WriteInput")]
        public static extern unsafe void STFT_WriteInput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* inputArray);

        [DllImport(DllName, EntryPoint = "STFT_ReadOutput")]
        public static extern unsafe void STFT_ReadOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [DllImport(DllName, EntryPoint = "STFT_MoveInput")]
        public static extern unsafe void STFT_MoveInput(void* stft, UIntPtr samples, bool clearMovedRegion);

        [DllImport(DllName, EntryPoint = "STFT_SetInterval")]
        public static extern unsafe void STFT_SetInterval(void* stft, UIntPtr defaultInterval, int windowShape, float asymmetry);

        [DllImport(DllName, EntryPoint = "STFT_Analyse")]
        public static extern unsafe void STFT_Analyse(void* stft, UIntPtr sampleInPast);

        [DllImport(DllName, EntryPoint = "STFT_Synthesise")]
        public static extern unsafe void STFT_Synthesise(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_Spectrum")]
        public static extern unsafe float* STFT_Spectrum(void* stft, UIntPtr channel);

        [DllImport(DllName, EntryPoint = "STFT_AnalysisWindow")]
        public static extern unsafe float* STFT_AnalysisWindow(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_SynthesisWindow")]
        public static extern unsafe float* STFT_SynthesisWindow(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_AnalysisLatency")]
        public static extern unsafe UIntPtr STFT_AnalysisLatency(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_SynthesisLatency")]
        public static extern unsafe UIntPtr STFT_SynthesisLatency(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_Latency")]
        public static extern unsafe UIntPtr STFT_Latency(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_BinToFreq")]
        public static extern unsafe float STFT_BinToFreq(void* stft, float b);

        [DllImport(DllName, EntryPoint = "STFT_FreqToBin")]
        public static extern unsafe float STFT_FreqToBin(void* stft, float f);

        [DllImport(DllName, EntryPoint = "STFT_AnalyseSteps")]
        public static extern unsafe UIntPtr STFT_AnalyseSteps(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_SynthesiseSteps")]
        public static extern unsafe UIntPtr STFT_SynthesiseSteps(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_AnalyseStep")]
        public static extern unsafe void STFT_AnalyseStep(void* stft, UIntPtr step, UIntPtr sampleInPast);

        [DllImport(DllName, EntryPoint = "STFT_SynthesiseStep")]
        public static extern unsafe void STFT_SynthesiseStep(void* stft, UIntPtr step);

        [DllImport(DllName, EntryPoint = "STFT_SamplesSinceAnalysis")]
        public static extern unsafe UIntPtr STFT_SamplesSinceAnalysis(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_SamplesSinceSynthesis")]
        public static extern unsafe UIntPtr STFT_SamplesSinceSynthesis(void* stft);

        [DllImport(DllName, EntryPoint = "STFT_FinishOutput")]
        public static extern unsafe void STFT_FinishOutput(void* stft, float strength, UIntPtr offset);

        [DllImport(DllName, EntryPoint = "STFT_AddOutput")]
        public static extern unsafe void STFT_AddOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [DllImport(DllName, EntryPoint = "STFT_ReplaceOutput")]
        public static extern unsafe void STFT_ReplaceOutput(void* stft, UIntPtr channel, UIntPtr offset, UIntPtr length, float* outputArray);

        [DllImport(DllName, EntryPoint = "STFT_MoveOutput")]
        public static extern unsafe void STFT_MoveOutput(void* stft, UIntPtr samples);

        [DllImport(DllName, EntryPoint = "STFT_AnalysisOffset")]
        public static extern unsafe void STFT_AnalysisOffset(void* stft, UIntPtr offset);

        [DllImport(DllName, EntryPoint = "STFT_SynthesisOffset")]
        public static extern unsafe void STFT_SynthesisOffset(void* stft, UIntPtr offset);
#endif
    }
}