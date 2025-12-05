namespace Signalsmith;
using System.Runtime.InteropServices;

/// <summary>
/// Dotnet C# binding for Signalsmith Stretch time-stretching and pitch-shifting library.
/// </summary>
public struct Stretch
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

    public void Release()
    {
        unsafe
        {
            Native.Release(Handle);
            Handle = null;
        }
    }

    public readonly void PresetDefault(int channels, float sampleRate, bool splitComputation)
    {
        unsafe
        {
            Native.PresetDefault(Handle, channels, sampleRate, splitComputation);
        }
    }

    public readonly void PresetCheaper(int channels, float sampleRate, bool splitComputation)
    {
        unsafe
        {
            Native.PresetCheaper(Handle, channels, sampleRate, splitComputation);
        }
    }

    public readonly void Configure(int channels, int blockSamples, int intervalSamples, bool splitComputation)
    {
        unsafe
        {
            Native.Configure(Handle, channels, blockSamples, intervalSamples, splitComputation);
        }
    }

    public readonly void Reset()
    {
        unsafe
        {
            Native.Reset(Handle);
        }
    }

    public readonly int InputLatency()
    {
        unsafe
        {
            return Native.InputLatency(Handle);
        }
    }

    public readonly int OutputLatency()
    {
        unsafe
        {
            return Native.OutputLatency(Handle);
        }
    }

    public readonly int BlockSamples()
    {
        unsafe
        {
            return Native.BlockSamples(Handle);
        }
    }

    public readonly int IntervalSamples()
    {
        unsafe
        {
            return Native.IntervalSamples(Handle);
        }
    }

    public readonly bool SplitComputation()
    {
        unsafe
        {
            return Native.SplitComputation(Handle);
        }
    }
    
    public readonly unsafe void SetFreqMap(delegate* unmanaged<float, float> freqMap)
    {
        Native.SetFreqMap(Handle, freqMap);
    }

    public readonly void SetTransposeSemitones(float semitones, float tonalityLimit)
    {
        unsafe
        {
            Native.SetTransposeSemitones(Handle, semitones, tonalityLimit);
        }
    }

    public readonly void SetTransposeFactor(float factor, float tonalityLimit)
    {
        unsafe
        {
            Native.SetTransposeFactor(Handle, factor, tonalityLimit);
        }
    }

    public readonly void SetFormantFactor(float multiplier, bool compensatePitch)
    {
        unsafe
        {
            Native.SetFormantFactor(Handle, multiplier, compensatePitch);
        }
    }

    public readonly void SetFormantSemitones(float semitones, bool compensatePitch)
    {
        unsafe
        {
            Native.SetFormantSemitones(Handle, semitones, compensatePitch);
        }
    }

    public readonly void SetFormantBase(float baseFreq)
    {
        unsafe
        {
            Native.SetFormantBase(Handle, baseFreq);
        }
    }

    public readonly void Seek(Span<float> input, double playbackRate)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            {
                Native.Seek(Handle, inputPtr, input.Length, playbackRate);
            }
        }
    }

    public readonly void Seek(float[] input, double playbackRate)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            {
                Native.Seek(Handle, inputPtr, input.Length, playbackRate);
            }
        }
    }

    public readonly void Seek(Span<float> input, int pcmLength, float playbackRate)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            {
                Native.Seek(Handle, inputPtr, pcmLength, playbackRate);
            }
        }
    }

    public readonly int SeekLength()
    {
        unsafe
        {
            return Native.SeekLength(Handle);
        }
    }

    public readonly void OutputSeek(Span<float> input)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            {
                Native.OutputSeek(Handle, inputPtr, input.Length);
            }
        }
    }

    public readonly int OutputSeekLength(float playbackRate)
    {
        unsafe
        {
            return Native.OutputSeekLength(Handle, playbackRate);
        }
    }

    public readonly void Process(Span<float> input, Span<float> output)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            fixed (float* outputPtr = output)
            {
                Native.Process(Handle, inputPtr, input.Length, outputPtr, output.Length);
            }
        }
    }

    public readonly void Process(Span<float> input, int inPcmLength, Span<float> output, int outPcmLength)
    {
        unsafe
        {
            fixed (float* inputPtr = input)
            fixed (float* outputPtr = output)
            {
                Native.Process(Handle, inputPtr, inPcmLength, outputPtr, outPcmLength);
            }
        }
    }

    public readonly unsafe void Process(float* input, int inPcmLength, float* output, int outPcmLength)
    {
        Native.Process(Handle, input, inPcmLength, output, outPcmLength);
    }
}

static partial class Native
{
    public const string DllName = "SignalsmithStretch";

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

    [LibraryImport(DllName, EntryPoint = "Stretch_SeekLength")]
    public static unsafe partial int SeekLength(void* stretch);

    [LibraryImport(DllName, EntryPoint = "Stretch_OutputSeek")]
    public static unsafe partial void OutputSeek(void* stretch, float* input, int inputLength);

    [LibraryImport(DllName, EntryPoint = "Stretch_OutputSeekLength")]
    public static unsafe partial int OutputSeekLength(void* stretch, float playbackRate);

    [LibraryImport(DllName, EntryPoint = "Stretch_Process")]
    public static unsafe partial void Process(void* stretch, float* input, int pcmLength, float* output, int pcmOutLength);
}