namespace example;

using System.Runtime.InteropServices;
using Signalsmith;
using Miniaudio;
using System.Runtime.CompilerServices;

public static class Program
{
    static Stretch Stretch;
    static unsafe ma_decoder* Decoder = null;
    static float[] stretchBuffer = new float[4096];

    public static void Main(string[] args)
    {
        Console.WriteLine("Signalsmith Stretch C# Binding Example");

        unsafe
        {
            ma_device* device = (ma_device*)NativeMemory.Alloc((nuint)sizeof(ma_device));

            ma_device_config config = ma.device_config_init(ma_device_type.ma_device_type_playback);
            config.playback.format = ma_format.ma_format_f32;
            config.playback.channels = 2;
            config.sampleRate = 44100;
            config.dataCallback = &MACallback;
            config.pUserData = null;

            ma_result result = ma.device_init(null, &config, device);
            if (result != ma_result.MA_SUCCESS)
            {
                Console.WriteLine("Failed to initialize playback device.");
                return;
            }

            ma.device_start(device);

            ma_decoder* decoder = (ma_decoder*)NativeMemory.Alloc((nuint)sizeof(ma_decoder));
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "audio.mp3";
            
            fixed (byte* pFilePath = System.Text.Encoding.UTF8.GetBytes(filePath))
            {
                result = ma.decoder_init_file((sbyte*)pFilePath, null, decoder);
            }

            if (result != ma_result.MA_SUCCESS)
            {
                Console.WriteLine("Failed to initialize decoder.");
                return;
            }

            Stretch = new Stretch();
            Stretch.PresetDefault(2, 44100.0f, true);

            Decoder = decoder;

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            ma.device_uninit(device);
            NativeMemory.Free(device);

            ma.decoder_uninit(decoder);
            NativeMemory.Free(decoder);

            Stretch.Release();
        }
    }
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static unsafe void MACallback(ma_device* device, void* output, void* input, uint frameCount)
    {
        if (Decoder == null || Stretch.Handle == null)
        {
            return;
        }

        Span<float> outputBuffer = new(output, (int)(frameCount * device->playback.channels));

        float rate = 1.5f; // Stretch factor
        uint frameCountToRead = (uint)(frameCount * rate);

        ulong framesRead;
        fixed (float* pOutputBuffer = stretchBuffer)
        {
            ma_result result = ma.decoder_read_pcm_frames(Decoder, pOutputBuffer, frameCountToRead, &framesRead);
            if (result != ma_result.MA_SUCCESS)
            {
                if (result == ma_result.MA_AT_END)
                {
                    ma.device_stop(device);
                }
                else
                {
                    throw new Exception("Failed to read PCM frames from decoder.");
                }
            }
        }

        Stretch.Process(stretchBuffer, (int)frameCountToRead, outputBuffer, (int)frameCount);
    }
}