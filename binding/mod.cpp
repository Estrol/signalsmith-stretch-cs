#include <cstring>
#include "./signalsmith-stretch/signalsmith-stretch.h"

#if defined(_WIN32) || defined(__CYGWIN__)
    #define DLL_EXPORT __declspec(dllexport)
#elif defined(__linux__) || defined(__APPLE__)
    #define DLL_EXPORT __attribute__((visibility("default")))
#else
    #define DLL_EXPORT
#endif

struct Stretch {
    int channels;
    float sampleRate;
    signalsmith::stretch::SignalsmithStretch<float>* stretch;
};

struct View
{
    float* data;
    int channel;
    int stride;
    
    View(float* data, int channel, int stride)
        : data(data), channel(channel), stride(stride) {}

    float& operator[](int offset) {
        return data[(offset * stride) + channel];
    }

    const float& operator[](int offset) const {
        return data[(offset * stride) + channel];
    }
};

class InterleavedBuffer {
    float* data;
    int channel;

public:
    InterleavedBuffer(float* data, int channel) : data(data), channel(channel) {}

    View operator[](int c) {
        return View(data, c, channel);
    }
};

extern "C" {
    DLL_EXPORT Stretch* Stretch_Create() {
        Stretch* s = new Stretch();
        s->stretch = new signalsmith::stretch::SignalsmithStretch<float>();
        return s;
    }

    DLL_EXPORT Stretch* Stretch_CreateSeed(long seed) {
        Stretch* s = new Stretch();
        s->stretch = new signalsmith::stretch::SignalsmithStretch<float>(seed);
        return s;
    }

    DLL_EXPORT void Stretch_Release(Stretch* stretch) {
        delete stretch->stretch;
        memset(stretch, 0xFF, sizeof(Stretch));
        delete stretch;
    }

    DLL_EXPORT void Stretch_PresetDefault(Stretch* stretch, int nChannels, float sampleRate, bool splitComputation) {
        stretch->stretch->presetDefault(nChannels, sampleRate, splitComputation);
        stretch->channels = nChannels;
        stretch->sampleRate = sampleRate;
    }

    DLL_EXPORT void Stretch_PresetCheaper(Stretch* stretch, int nChannels, float sampleRate, bool splitComputation) {
        stretch->stretch->presetCheaper(nChannels, sampleRate, splitComputation);
        stretch->channels = nChannels;
        stretch->sampleRate = sampleRate;
    }

    DLL_EXPORT void Stretch_Configure(Stretch* stretch, int nChannels, int blockSamples, int intervalSamples, bool splitComputation) {
        stretch->stretch->configure(nChannels, blockSamples, intervalSamples, splitComputation);
    }

    DLL_EXPORT void Stretch_Reset(Stretch* stretch) {
        stretch->stretch->reset();
    }

    DLL_EXPORT int Stretch_InputLatency(Stretch* stretch) {
        return stretch->stretch->inputLatency();
    }

    DLL_EXPORT int Stretch_OutputLatency(Stretch* stretch) {
        return stretch->stretch->outputLatency();
    }

    DLL_EXPORT int Stretch_BlockSamples(Stretch* stretch) {
        return stretch->stretch->blockSamples();
    }

    DLL_EXPORT int Stretch_IntervalSamples(Stretch* stretch) {
        return stretch->stretch->intervalSamples();
    }

    DLL_EXPORT bool Stretch_SplitComputation(Stretch* stretch) {
        return stretch->stretch->splitComputation();
    }

    DLL_EXPORT void Stretch_SetTransposeSemitones(Stretch* stretch, float semitones, float tonalityLimit) {
        stretch->stretch->setTransposeSemitones(semitones, tonalityLimit);
    }

    DLL_EXPORT void Stretch_SetTransposeFactor(Stretch* stretch, float factor, float tonalityLimit) {
        stretch->stretch->setTransposeFactor(factor, tonalityLimit);
    }

    DLL_EXPORT void Stretch_SetFreqMap(Stretch* stretch, float (*inputToOutput)(float)) {
        stretch->stretch->setFreqMap(inputToOutput);
    }

    DLL_EXPORT void Stretch_SetFormantFactor(Stretch* stretch, float multiplier, bool compensatePitch) {
        stretch->stretch->setFormantFactor(multiplier, compensatePitch);
    }

    DLL_EXPORT void Stretch_SetFormantSemitones(Stretch* stretch, float semitones, bool compensatePitch) {
        stretch->stretch->setFormantSemitones(semitones, compensatePitch);
    }

    DLL_EXPORT void Stretch_SetFormantBase(Stretch* stretch, float baseFreq) {
        stretch->stretch->setFormantBase(baseFreq);
    }

    DLL_EXPORT void Stretch_Seek(Stretch* stretch, float* input, int inputSamples, double playbackRate) {
        InterleavedBuffer inBuffer(input, stretch->channels);
        stretch->stretch->seek(inBuffer, inputSamples, playbackRate);
    }

    DLL_EXPORT void Stretch_Flush(Stretch* stretch, float* output, int pcmOutLength, double playbackRate) {
        InterleavedBuffer outBuffer(output, stretch->channels);
        stretch->stretch->flush(outBuffer, pcmOutLength, playbackRate);
    }

    DLL_EXPORT int Stretch_SeekLength(Stretch* stretch) {
        return stretch->stretch->seekLength();
    }

    DLL_EXPORT void Stretch_OutputSeek(Stretch* stretch, float* input, int inputLength) {
        InterleavedBuffer inBuffer(input, stretch->channels);
        stretch->stretch->outputSeek(inBuffer, inputLength);
    }

    DLL_EXPORT int Stretch_OutputSeekLength(Stretch* stretch, float playbackRate) {
        return stretch->stretch->outputSeekLength(playbackRate);
    }

    DLL_EXPORT void Stretch_Process(Stretch* stretch, float* input, int pcmLength, float* output, int pcmOutLength) {
        InterleavedBuffer inBuffer(input, stretch->channels);
        InterleavedBuffer outBuffer(output, stretch->channels);
        stretch->stretch->process(inBuffer, pcmLength, outBuffer, pcmOutLength);
    }
}