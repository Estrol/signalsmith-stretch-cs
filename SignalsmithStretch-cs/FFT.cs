using System;
using System.Runtime.InteropServices;

namespace Signalsmith
{
    /// <summary>
    /// Dotnet C# binding for Signalsmith FFT
    /// </summary>
    public class FFT
    {
        public unsafe void* Handle;

        public FFT(int size)
        {
            unsafe { Handle = Native.FFT_Create(size); }
        }

        public unsafe void Resize(int size)
        {
            Native.FFT_Resize(Handle, (UIntPtr)size);
        }

        public static int Size()
        {
            return (int)Native.FFT_Size();
        }

        public static int Steps()
        {
            return (int)Native.FFT_Steps();
        }

        public void Process(float[] inputReal, float[] inputImag, float[] outputReal, float[] outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_Proc(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void ProcessStep(int step, float[] inputReal, float[] inputImag, float[] outputReal, float[] outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_ProcStep(Handle, (UIntPtr)step, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void ProcessSplit(float[] inputReal, float[] inputImag, float[] outputReal, float[] outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_ProcSplit(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void ProcessSplitStep(int step, float[] inputReal, float[] inputImag, float[] outputReal, float[] outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_ProcSplitStep(Handle, (UIntPtr)step, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void InverseProcess(float[] inputReal, float[] inputImag, float[] outputReal, float[] outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_InverseProc(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

#if NET7_0_OR_GREATER
        public void Process(Span<float> inputReal, Span<float> inputImag, Span<float> outputReal, Span<float> outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_Proc(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void InverseProcess(Span<float> inputReal, Span<float> inputImag, Span<float> outputReal, Span<float> outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_InverseProc(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void InverseProcessStep(int step, Span<float> inputReal, Span<float> inputImag, Span<float> outputReal, Span<float> outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_InverseProcStep(Handle, (UIntPtr)step, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void InverseProcessSplit(Span<float> inputReal, Span<float> inputImag, Span<float> outputReal, Span<float> outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_InverseProcSplit(Handle, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }

        public void InverseProcessSplitStep(int step, Span<float> inputReal, Span<float> inputImag, Span<float> outputReal, Span<float> outputImag)
        {
            unsafe
            {
                fixed (float* inRealPtr = inputReal)
                fixed (float* inImagPtr = inputImag)
                fixed (float* outRealPtr = outputReal)
                fixed (float* outImagPtr = outputImag)
                {
                    Native.FFT_InverseProcSplitStep(Handle, (UIntPtr)step, inRealPtr, inImagPtr, outRealPtr, outImagPtr);
                }
            }
        }
#endif
    }

    internal static partial class Native
    {
#if NET7_0_OR_GREATER
        [LibraryImport(DllName, EntryPoint = "FFT_Create")]
        public static unsafe partial void* FFT_Create(int size);

        [LibraryImport(DllName, EntryPoint = "FFT_Delete")]
        public static unsafe partial void FFT_Delete(void* fft);

        [LibraryImport(DllName, EntryPoint = "FFT_Resize")]
        public static unsafe partial void FFT_Resize(void* fft, UIntPtr size);

        [LibraryImport(DllName, EntryPoint = "FFT_Size")]
        public static partial UIntPtr FFT_Size();

        [LibraryImport(DllName, EntryPoint = "FFT_Steps")]
        public static partial UIntPtr FFT_Steps();

        [LibraryImport(DllName, EntryPoint = "FFT_Proc")]
        public static unsafe partial void FFT_Proc(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);
        
        [LibraryImport(DllName, EntryPoint = "FFT_ProcStep")]
        public static unsafe partial void FFT_ProcStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_ProcSplit")]
        public static unsafe partial void FFT_ProcSplit(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_ProcSplitStep")]
        public static unsafe partial void FFT_ProcSplitStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_InverseProc")]
        public static unsafe partial void FFT_InverseProc(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_InverseProcStep")]
        public static unsafe partial void FFT_InverseProcStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_InverseProcSplit")]
        public static unsafe partial void FFT_InverseProcSplit(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [LibraryImport(DllName, EntryPoint = "FFT_InverseProcSplitStep")]
        public static unsafe partial void FFT_InverseProcSplitStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);
#else
        [DllImport(DllName, EntryPoint = "FFT_Create")]
        public static extern unsafe void* FFT_Create(int size);

        [DllImport(DllName, EntryPoint = "FFT_Delete")]
        public static extern unsafe void FFT_Delete(void* fft);

        [DllImport(DllName, EntryPoint = "FFT_Resize")]
        public static extern unsafe void FFT_Resize(void* fft, UIntPtr size);

        [DllImport(DllName, EntryPoint = "FFT_Size")]
        public static extern UIntPtr FFT_Size();

        [DllImport(DllName, EntryPoint = "FFT_Steps")]
        public static extern UIntPtr FFT_Steps();

        [DllImport(DllName, EntryPoint = "FFT_Proc")]
        public static extern unsafe void FFT_Proc(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_ProcStep")]
        public static extern unsafe void FFT_ProcStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_ProcSplit")]
        public static extern unsafe void FFT_ProcSplit(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_ProcSplitStep")]
        public static extern unsafe void FFT_ProcSplitStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_InverseProc")]
        public static extern unsafe void FFT_InverseProc(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_InverseProcStep")]
        public static extern unsafe void FFT_InverseProcStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_InverseProcSplit")]
        public static extern unsafe void FFT_InverseProcSplit(void* fft, float* inputReal, float* inputImag, float* outputReal, float* outputImag);

        [DllImport(DllName, EntryPoint = "FFT_InverseProcSplitStep")]
        public static extern unsafe void FFT_InverseProcSplitStep(void* fft, UIntPtr step, float* inputReal, float* inputImag, float* outputReal, float* outputImag);
#endif
    }
}