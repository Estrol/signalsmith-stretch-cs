using System;
using System.Runtime.InteropServices;

namespace Signalsmith
{
    /// <summary>
    /// Dotnet C# binding for Signalsmith Stretch time-stretching and pitch-shifting library.
    /// </summary>
    public class Stretch : IDisposable
    {
        public unsafe void* Handle;

        public Stretch()
        {
            unsafe
            {
                Handle = Native.Create();
            }
        }

        public Stretch(long seed)
        {
            unsafe
            {
                Handle = Native.CreateSeed(seed);
            }
        }

        ~Stretch()
        {
            Release();
        }

        public void Dispose()
        {
            Release();
            GC.SuppressFinalize(this);
        }

        public void Release()
        {
            unsafe
            {
                if (Handle != null)
                {
                    Native.Release(Handle);
                    Handle = null;
                }
            }
        }

        public void PresetDefault(int channels, float sampleRate, bool splitComputation)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.PresetDefault(Handle, channels, sampleRate, splitComputation);
            }
        }

        public void PresetCheaper(int channels, float sampleRate, bool splitComputation)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.PresetCheaper(Handle, channels, sampleRate, splitComputation);
            }
        }

        public void Configure(int channels, int blockSamples, int intervalSamples, bool splitComputation)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.Configure(Handle, channels, blockSamples, intervalSamples, splitComputation);
            }
        }

        public void Reset()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.Reset(Handle);
            }
        }

        public int InputLatency()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.InputLatency(Handle);
            }
        }

        public int OutputLatency()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.OutputLatency(Handle);
            }
        }

        public int BlockSamples()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.BlockSamples(Handle);
            }
        }

        public int IntervalSamples()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.IntervalSamples(Handle);
            }
        }

        public bool SplitComputation()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.SplitComputation(Handle);
            }
        }
        
#if NET7_0_OR_GREATER
        public unsafe void SetFreqMap(delegate* unmanaged<float, float> freqMap)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            Native.SetFreqMap(Handle, freqMap);
        }
#endif
        public void SetFreqMap(IntPtr freqMap)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.SetFreqMap(Handle, freqMap);
            }
        }

        public void SetTransposeSemitones(float semitones, float tonalityLimit)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }
                
                Native.SetTransposeSemitones(Handle, semitones, tonalityLimit);
            }
        }

        public void SetTransposeFactor(float factor, float tonalityLimit)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.SetTransposeFactor(Handle, factor, tonalityLimit);
            }
        }

        public void SetFormantFactor(float multiplier, bool compensatePitch)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.SetFormantFactor(Handle, multiplier, compensatePitch);
            }
        }

        public void SetFormantSemitones(float semitones, bool compensatePitch)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.SetFormantSemitones(Handle, semitones, compensatePitch);
            }
        }

        public void SetFormantBase(float baseFreq)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                Native.SetFormantBase(Handle, baseFreq);
            }
        }

#if NET7_0_OR_GREATER
        public void Seek(Span<float> input, double playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                {
                    Native.Seek(Handle, inputPtr, input.Length, playbackRate);
                }
            }
        }

        public void Seek(Span<float> input, int pcmLength, float playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                {
                    Native.Seek(Handle, inputPtr, pcmLength, playbackRate);
                }
            }
        }
#endif
        public void Seek(float[] input, double playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                {
                    Native.Seek(Handle, inputPtr, input.Length, playbackRate);
                }
            }
        }

        public unsafe void Seek(float* input, int pcmLength, double playbackRate)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            Native.Seek(Handle, input, pcmLength, playbackRate);
        }

#if NET7_0_OR_GREATER
        public void Flush(Span<float> output, double playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* outputPtr = output)
                {
                    Native.Flush(Handle, outputPtr, output.Length, playbackRate);
                }
            }
        }

        public void Flush(Span<float> output, int pcmOutLength, double playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* outputPtr = output)
                {
                    Native.Flush(Handle, outputPtr, pcmOutLength, playbackRate);
                }
            }
        }
#endif
        public void Flush(float[] output, int pcmOutLength, double playbackRate)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* outputPtr = output)
                {
                    Native.Flush(Handle, outputPtr, pcmOutLength, playbackRate);
                }
            }
        }
        public unsafe void Flush(float* output, int pcmOutLength, double playbackRate)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            Native.Flush(Handle, output, pcmOutLength, playbackRate);
        }

        public int SeekLength()
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                return Native.SeekLength(Handle);
            }
        }

#if NET7_0_OR_GREATER
        public void OutputSeek(Span<float> input, int pcmLength)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                {
                    Native.OutputSeek(Handle, inputPtr, int pcmLength);
                }
            }
        }
#endif
        public void OutputSeek(float[] input, int inputLength)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                {
                    Native.OutputSeek(Handle, inputPtr, inputLength);
                }
            }
        }

        public unsafe void OutputSeek(float* input, int inputLength)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            Native.OutputSeek(Handle, input, inputLength);
        }

        public int OutputSeekLength(float playbackRate)
        {
            unsafe
            {
                return Native.OutputSeekLength(Handle, playbackRate);
            }
        }

#if NET7_0_OR_GREATER
        public void Process(Span<float> input, Span<float> output)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                fixed (float* outputPtr = output)
                {
                    Native.Process(Handle, inputPtr, input.Length, outputPtr, output.Length);
                }
            }
        }

        public void Process(Span<float> input, int inPcmLength, Span<float> output, int outPcmLength)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                fixed (float* outputPtr = output)
                {
                    Native.Process(Handle, inputPtr, inPcmLength, outputPtr, outPcmLength);
                }
            }
        }
#endif 
        public void Process(float[] input, float[] output)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                fixed (float* outputPtr = output)
                {
                    Native.Process(Handle, inputPtr, input.Length, outputPtr, output.Length);
                }
            }
        }

        public void Process(float[] input, int inPcmLength, float[] output, int outPcmLength)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                fixed (float* outputPtr = output)
                {
                    Native.Process(Handle, inputPtr, inPcmLength, outputPtr, outPcmLength);
                }
            }
        }

        public unsafe void Process(float* input, int inPcmLength, float* output, int outPcmLength)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }
            
            Native.Process(Handle, input, inPcmLength, output, outPcmLength);
        }

        public bool Exact(float[] input, int inPcmLength, float[] output, int outPcmLength)
        {
            unsafe
            {
                if (Handle == null)
                {
                    throw new ObjectDisposedException("Stretch");
                }

                fixed (float* inputPtr = input)
                fixed (float* outputPtr = output)
                {
                    return Native.Exact(Handle, inputPtr, inPcmLength, outputPtr, outPcmLength);
                }
            }
        }

        public unsafe bool Exact(float* input, int inPcmLength, float* output, int outPcmLength)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            return Native.Exact(Handle, input, inPcmLength, output, outPcmLength);
        }

#if NET7_0_OR_GREATER
        public unsafe bool Exact(Span<float> input, int inPcmLength, Span<float> output, int outPcmLength)
        {
            if (Handle == null)
            {
                throw new ObjectDisposedException("Stretch");
            }

            fixed (float* inputPtr = input)
            fixed (float* outputPtr = output)
            {
                return Native.Exact(Handle, inputPtr, inPcmLength, outputPtr, outPcmLength);
            }
        }
#endif
    }

    internal static partial class Native
    {
        public const string DllName = "SignalsmithStretch";

#if NET7_0_OR_GREATER
        [LibraryImport(DllName, EntryPoint = "Stretch_Create")]
        public static unsafe partial void* Create();
        [LibraryImport(DllName, EntryPoint = "Stretch_CreateSeed")]
        public static unsafe partial void* CreateSeed(long seed);
        [LibraryImport(DllName, EntryPoint = "Stretch_Release")]
        public static unsafe partial void Release(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_PresetDefault")]
        public static unsafe partial void PresetDefault(void* stretch, int channels, float sampleRate, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [LibraryImport(DllName, EntryPoint = "Stretch_PresetCheaper")]
        public static unsafe partial void PresetCheaper(void* stretch, int channels, float sampleRate, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [LibraryImport(DllName, EntryPoint = "Stretch_Configure")]
        public static unsafe partial void Configure(void* stretch, int channels, int blockSamples, int intervalSamples, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [LibraryImport(DllName, EntryPoint = "Stretch_Reset")]
        public static unsafe partial void Reset(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_InputLatency")]
        public static unsafe partial int InputLatency(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_OutputLatency")]
        public static unsafe partial int OutputLatency(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_BlockSamples")]
        public static unsafe partial int BlockSamples(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_IntervalSamples")]
        public static unsafe partial int IntervalSamples(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_SplitComputation")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe partial bool SplitComputation(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetTransposeSemitones")]
        public static unsafe partial void SetTransposeSemitones(void* stretch, float semitones, float tonalityLimit);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetTransposeFactor")]
        public static unsafe partial void SetTransposeFactor(void* stretch, float factor, float tonalityLimit);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetFreqMap")]
        public static unsafe partial void SetFreqMap(void* stretch, delegate* unmanaged<float, float> freqMap);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetFormantFactor")]
        public static unsafe partial void SetFormantFactor(void* stretch, float multiplier, [MarshalAs(UnmanagedType.I1)] bool compensatePitch);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetFormantSemitones")]
        public static unsafe partial void SetFormantSemitones(void* stretch, float semitones, [MarshalAs(UnmanagedType.I1)] bool compensatePitch);
        [LibraryImport(DllName, EntryPoint = "Stretch_SetFormantBase")]
        public static unsafe partial void SetFormantBase(void* stretch, float baseFreq);
        [LibraryImport(DllName, EntryPoint = "Stretch_Seek")]
        public static unsafe partial void Seek(void* stretch, float* input, int pcmLength, double playbackRate);
        [LibraryImport(DllName, EntryPoint = "Stretch_Flush")]
        public static unsafe partial void Flush(void* stretch, float* output, int pcmOutLength, double playbackRate);
        [LibraryImport(DllName, EntryPoint = "Stretch_SeekLength")]
        public static unsafe partial int SeekLength(void* stretch);
        [LibraryImport(DllName, EntryPoint = "Stretch_OutputSeek")]
        public static unsafe partial void OutputSeek(void* stretch, float* input, int inputLength);
        [LibraryImport(DllName, EntryPoint = "Stretch_OutputSeekLength")]
        public static unsafe partial int OutputSeekLength(void* stretch, float playbackRate);
        [LibraryImport(DllName, EntryPoint = "Stretch_Process")]
        public static unsafe partial void Process(void* stretch, float* input, int pcmLength, float* output, int pcmOutLength);
        [LibraryImport(DllName, EntryPoint = "Stretch_Exact")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static unsafe partial bool Exact(void* stretch, float* input, int pcmLength, float* output, int pcmOutLength);
#else
        [DllImport(DllName, EntryPoint = "Stretch_Create")]
        public extern static unsafe void* Create();
        [DllImport(DllName, EntryPoint = "Stretch_CreateSeed")]
        public extern static unsafe void* CreateSeed(long seed);
        [DllImport(DllName, EntryPoint = "Stretch_Release")]
        public extern static unsafe void Release(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_PresetDefault")]
        public extern static unsafe void PresetDefault(void* stretch, int channels, float sampleRate, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [DllImport(DllName, EntryPoint = "Stretch_PresetCheaper")]
        public extern static unsafe void PresetCheaper(void* stretch, int channels, float sampleRate, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [DllImport(DllName, EntryPoint = "Stretch_Configure")]
        public extern static unsafe void Configure(void* stretch, int channels, int blockSamples, int intervalSamples, [MarshalAs(UnmanagedType.I1)] bool splitComputation);
        [DllImport(DllName, EntryPoint = "Stretch_Reset")]
        public extern static unsafe void Reset(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_InputLatency")]
        public extern static unsafe int InputLatency(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_OutputLatency")]
        public extern static unsafe int OutputLatency(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_BlockSamples")]
        public extern static unsafe int BlockSamples(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_IntervalSamples")]
        public extern static unsafe int IntervalSamples(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_SplitComputation")]
        [return: MarshalAs(UnmanagedType.I1)]
        public extern static unsafe bool SplitComputation(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_SetTransposeSemitones")]
        public extern static unsafe void SetTransposeSemitones(void* stretch, float semitones, float tonalityLimit);
        [DllImport(DllName, EntryPoint = "Stretch_SetTransposeFactor")]
        public extern static unsafe void SetTransposeFactor(void* stretch, float factor, float tonalityLimit);
        // freqMap signature: void(float) -> float
        [DllImport(DllName, EntryPoint = "Stretch_SetFreqMap")]
        public extern static unsafe void SetFreqMap(void* stretch, IntPtr freqMap);
        [DllImport(DllName, EntryPoint = "Stretch_SetFormantFactor")]
        public extern static unsafe void SetFormantFactor(void* stretch, float multiplier, [MarshalAs(UnmanagedType.I1)] bool compensatePitch);
        [DllImport(DllName, EntryPoint = "Stretch_SetFormantSemitones")]
        public extern static unsafe void SetFormantSemitones(void* stretch, float semitones, [MarshalAs(UnmanagedType.I1)] bool compensatePitch);
        [DllImport(DllName, EntryPoint = "Stretch_SetFormantBase")]
        public extern static unsafe void SetFormantBase(void* stretch, float baseFreq);
        [DllImport(DllName, EntryPoint = "Stretch_Seek")]
        public extern static unsafe void Seek(void* stretch, float* input, int pcmLength, double playbackRate);
        [DllImport(DllName, EntryPoint = "Stretch_Flush")]
        public extern static unsafe void Flush(void* stretch, float* output, int pcmOutLength, double playbackRate);
        [DllImport(DllName, EntryPoint = "Stretch_SeekLength")]
        public extern static unsafe int SeekLength(void* stretch);
        [DllImport(DllName, EntryPoint = "Stretch_OutputSeek")]
        public extern static unsafe void OutputSeek(void* stretch, float* input, int inputLength);
        [DllImport(DllName, EntryPoint = "Stretch_OutputSeekLength")]
        public extern static unsafe int OutputSeekLength(void* stretch, float playbackRate);
        [DllImport(DllName, EntryPoint = "Stretch_Process")]
        public extern static unsafe void Process(void* stretch, float* input, int pcmLength, float* output, int pcmOutLength);
        [DllImport(DllName, EntryPoint = "Stretch_Exact")]
        [return: MarshalAs(UnmanagedType.I1)]
        public extern static unsafe bool Exact(void* stretch, float* input, int pcmLength, float* output, int pcmOutLength);
#endif
    }   
}