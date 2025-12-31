#include <cstring>
#include "signalsmith-linear/stft.h"

#if defined(_WIN32) || defined(__CYGWIN__)
    #define DLL_EXPORT __declspec(dllexport)
#elif defined(__linux__) || defined(__APPLE__)
    #define DLL_EXPORT __attribute__((visibility("default")))
#else
    #define DLL_EXPORT
#endif

// Abstract base class for STFT to allow for different template instantiations

enum STFTWindowShape { ignore, acg, kaiser };

class BaseSTFT {
public:
    float complex[2];

    virtual ~BaseSTFT() = default;

    virtual void configure(int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry) = 0;
    virtual size_t blockSamples() const = 0;
    virtual size_t fftSamples() const = 0;
    virtual size_t defaultInterval() const = 0;
    virtual size_t bands() const = 0;
    virtual size_t analysisLatency() const = 0;
    virtual size_t synthesisLatency() const = 0;
    virtual size_t latency() const = 0;
    virtual void reset() = 0;

    virtual float binToFreq(float b) const = 0;
    virtual float freqToBin(float f) const = 0;

    virtual void writeInput(size_t channel, size_t offset, size_t length, const float* inputArray) = 0;
    virtual void moveInput(size_t samples, bool clearMovedRegion) = 0;

    virtual size_t samplesSinceAnalysis() const = 0;
    virtual void finishOutput(float strength = 0.0f, size_t offset = 0) = 0;
    virtual void readOutput(size_t channel, size_t offset, size_t length, float* outputArray) = 0;
    virtual void addOutput(size_t channel, size_t offset, size_t length, const float* outputArray) = 0;
    virtual void replaceOutput(size_t channel, size_t offset, size_t length, const float* outputArray) = 0;
    virtual void moveOutput(size_t samples) = 0;
    virtual size_t samplesSinceSynthesis() const = 0;
    virtual std::complex<float>* spectrum(size_t channel) = 0;
    virtual float* analysisWindow() = 0;
    virtual void analysisOffset(size_t offset) = 0;
    virtual float* synthesisWindow() = 0;
    virtual void synthesisOffset(size_t offset) = 0;

    virtual void setInterval(size_t defaultInterval, STFTWindowShape windowShape, float asymmetry) = 0;
    virtual void analyse(size_t sampleInPast = 0) = 0;
    virtual size_t analyseSteps() const = 0;
    virtual void analyseStep(size_t step, size_t sampleInPast = 0) = 0;
    virtual void synthesise() = 0;
    virtual size_t synthesiseSteps() const = 0;
    virtual void synthesiseStep(size_t step) = 0;
};

class STFT : public BaseSTFT {
    signalsmith::linear::DynamicSTFT<float, false> stft;

    std::vector<std::complex<float>> inputBuffer;
    std::vector<std::complex<float>> outputBuffer;

public:
    ~STFT() override = default;

    void configure(int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry) override {
        stft.configure(inChannels, outChannels, blockSamples, extraInputHistory, intervalSamples, asymmetry);
    }

    size_t blockSamples() const override {
        return stft.blockSamples();
    }

    size_t fftSamples() const override {
        return stft.fftSamples();
    }

    size_t defaultInterval() const override {
        return stft.defaultInterval();
    }

    size_t bands() const override {
        return stft.bands();
    }

    size_t analysisLatency() const override {
        return stft.analysisLatency();
    }

    size_t synthesisLatency() const override {
        return stft.synthesisLatency();
    }

    size_t latency() const override {
        return stft.latency();
    }

    void reset() override {
        stft.reset();
    }

    float binToFreq(float b) const override {
        return stft.binToFreq(b);
    }

    float freqToBin(float f) const override {
        return stft.freqToBin(f);
    }

    void writeInput(size_t channel, size_t offset, size_t length, const float* inputArray) override {
        stft.writeInput(channel, offset, length, inputArray);
    }

    void readOutput(size_t channel, size_t offset, size_t length, float* outputArray) override {
        stft.readOutput(channel, offset, length, outputArray);
    }

    void moveInput(size_t samples, bool clearMovedRegion) override {
        stft.moveInput(samples, clearMovedRegion);
    }

    size_t samplesSinceAnalysis() const override {
        return stft.samplesSinceAnalysis();
    }

    void finishOutput(float strength = 0.0f, size_t offset = 0) override {
        stft.finishOutput(strength, offset);
    }

    void addOutput(size_t channel, size_t offset, size_t length, const float* outputArray) override {
        stft.addOutput(channel, offset, length, outputArray);
    }

    void replaceOutput(size_t channel, size_t offset, size_t length, const float* outputArray) override {
        stft.replaceOutput(channel, offset, length, outputArray);
    }

    void moveOutput(size_t samples) override {
        stft.moveOutput(samples);
    }

    size_t samplesSinceSynthesis() const override {
        return stft.samplesSinceSynthesis();
    }

    std::complex<float>* spectrum(size_t channel) override {
        return stft.spectrum(channel);
    }

    float* analysisWindow() override {
        return stft.analysisWindow();
    }

    void analysisOffset(size_t offset) override {
        stft.analysisOffset(offset);
    }

    float* synthesisWindow() override {
        return stft.synthesisWindow();
    }

    void synthesisOffset(size_t offset) override {
        stft.synthesisOffset(offset);
    }

    void setInterval(size_t defaultInterval, STFTWindowShape windowShape, float asymmetry) override {
        signalsmith::linear::DynamicSTFT<float, false>::WindowShape shape = signalsmith::linear::DynamicSTFT<float, false>::WindowShape::ignore;
        if (windowShape == acg) {
            shape = signalsmith::linear::DynamicSTFT<float, false>::acg;
        } else if (windowShape == kaiser) {
            shape = signalsmith::linear::DynamicSTFT<float, false>::kaiser;
        }
        stft.setInterval(defaultInterval, shape, asymmetry);
    }

    void analyse(size_t sampleInPast = 0) override {
        stft.analyse(sampleInPast);
    }

    size_t analyseSteps() const override {
        return stft.analyseSteps();
    }

    void analyseStep(size_t step, size_t sampleInPast = 0) override {
        stft.analyseStep(step, sampleInPast);
    }

    void synthesise() override {
        stft.synthesise();
    }

    size_t synthesiseSteps() const override {
        return stft.synthesiseSteps();
    }

    void synthesiseStep(size_t step) override {
        stft.synthesiseStep(step);
    }
};

class SFTF_SplitComputation : public BaseSTFT {
    signalsmith::linear::DynamicSTFT<float, true> stft;

    std::vector<std::complex<float>> inputBuffer;
    std::vector<std::complex<float>> outputBuffer;

public:
    ~SFTF_SplitComputation() override = default;

    void configure(int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry) override {
        stft.configure(inChannels, outChannels, blockSamples, extraInputHistory, intervalSamples, asymmetry);
    }

    size_t blockSamples() const override {
        return stft.blockSamples();
    }

    size_t fftSamples() const override {
        return stft.fftSamples();
    }

    size_t defaultInterval() const override {
        return stft.defaultInterval();
    }

    size_t bands() const override {
        return stft.bands();
    }

    size_t analysisLatency() const override {
        return stft.analysisLatency();
    }

    size_t synthesisLatency() const override {
        return stft.synthesisLatency();
    }

    size_t latency() const override {
        return stft.latency();
    }

    void reset() override {
        stft.reset();
    }

    float binToFreq(float b) const override {
        return stft.binToFreq(b);
    }

    float freqToBin(float f) const override {
        return stft.freqToBin(f);
    }

    void writeInput(size_t channel, size_t offset, size_t length, const float* inputArray) override {
        stft.writeInput(channel, offset, length, inputArray);
    }

    void readOutput(size_t channel, size_t offset, size_t length, float* outputArray) override {
        stft.readOutput(channel, offset, length, outputArray);
    }

    void moveInput(size_t samples, bool clearMovedRegion) override {
        stft.moveInput(samples, clearMovedRegion);
    }

    size_t samplesSinceAnalysis() const override {
        return stft.samplesSinceAnalysis();
    }

    void finishOutput(float strength = 0.0f, size_t offset = 0) override {
        stft.finishOutput(strength, offset);
    }

    void addOutput(size_t channel, size_t offset, size_t length, const float* outputArray) override {
        stft.addOutput(channel, offset, length, outputArray);
    }

    void replaceOutput(size_t channel, size_t offset, size_t length, const float* outputArray) override {
        stft.replaceOutput(channel, offset, length, outputArray);
    }

    void moveOutput(size_t samples) override {
        stft.moveOutput(samples);
    }

    size_t samplesSinceSynthesis() const override {
        return stft.samplesSinceSynthesis();
    }

    std::complex<float>* spectrum(size_t channel) override {
        return stft.spectrum(channel);
    }

    float* analysisWindow() override {
        return stft.analysisWindow();
    }

    void analysisOffset(size_t offset) override {
        stft.analysisOffset(offset);
    }

    float* synthesisWindow() override {
        return stft.synthesisWindow();
    }

    void synthesisOffset(size_t offset) override {
        stft.synthesisOffset(offset);
    }

    void setInterval(size_t defaultInterval, STFTWindowShape windowShape, float asymmetry) override {
        signalsmith::linear::DynamicSTFT<float, true>::WindowShape shape = signalsmith::linear::DynamicSTFT<float, true>::WindowShape::ignore;
        if (windowShape == acg) {
            shape = signalsmith::linear::DynamicSTFT<float, true>::acg;
        } else if (windowShape == kaiser) {
            shape = signalsmith::linear::DynamicSTFT<float, true>::kaiser;
        }
        stft.setInterval(defaultInterval, shape, asymmetry);
    }

    void analyse(size_t sampleInPast = 0) override {
        stft.analyse(sampleInPast);
    }

    size_t analyseSteps() const override {
        return stft.analyseSteps();
    }

    void analyseStep(size_t step, size_t sampleInPast = 0) override {
        stft.analyseStep(step, sampleInPast);
    }

    void synthesise() override {
        stft.synthesise();
    }

    size_t synthesiseSteps() const override {
        return stft.synthesiseSteps();
    }

    void synthesiseStep(size_t step) override {
        stft.synthesiseStep(step);
    }
};

extern "C" {
    DLL_EXPORT BaseSTFT* STFT_Create(bool splitComputation) {
        if (splitComputation) {
            return new SFTF_SplitComputation();
        } else {
            return new STFT();
        }
    }

    DLL_EXPORT void STFT_Delete(BaseSTFT* stft) {
        stft->reset();
        delete stft;
    }

    DLL_EXPORT void STFT_Configure(BaseSTFT* stftBase, int inChannels, int outChannels, int blockSamples, int extraInputHistory, int intervalSamples, float asymmetry) {
        stftBase->configure(inChannels, outChannels, blockSamples, extraInputHistory, intervalSamples, asymmetry);
    }

    DLL_EXPORT size_t STFT_BlockSamples(BaseSTFT* stftBase) {
        return stftBase->blockSamples();
    }

    DLL_EXPORT size_t STFT_FFTSamples(BaseSTFT* stftBase) {
        return stftBase->fftSamples();
    }

    DLL_EXPORT size_t STFT_Bands(BaseSTFT* stftBase) {
        return stftBase->bands();
    }

    DLL_EXPORT void STFT_Reset(BaseSTFT* stftBase) {
        stftBase->reset();
    }

    DLL_EXPORT void STFT_WriteInput(BaseSTFT* stftBase, size_t channel, size_t offset, size_t length, const float* inputArray) {
        stftBase->writeInput(channel, offset, length, inputArray);
    }

    DLL_EXPORT void STFT_ReadOutput(BaseSTFT* stftBase, size_t channel, size_t offset, size_t length, float* outputArray) {
        stftBase->readOutput(channel, offset, length, outputArray);
    }

    DLL_EXPORT void STFT_MoveInput(BaseSTFT* stftBase, size_t samples, bool clearMovedRegion) {
        stftBase->moveInput(samples, clearMovedRegion);
    }

    DLL_EXPORT void STFT_SetInterval(BaseSTFT* stftBase, size_t defaultInterval, STFTWindowShape windowShape, float asymmetry) {
        stftBase->setInterval(defaultInterval, windowShape, asymmetry);
    }

    DLL_EXPORT void STFT_Analyse(BaseSTFT* stftBase, size_t sampleInPast) {
        stftBase->analyse(sampleInPast);
    }

    DLL_EXPORT void STFT_Synthesise(BaseSTFT* stftBase) {
        stftBase->synthesise();
    }

    DLL_EXPORT float* STFT_Spectrum(BaseSTFT* stftBase, size_t channel) {
        std::complex<float>* spec = stftBase->spectrum(channel);
        stftBase->complex[0] = spec->real();
        stftBase->complex[1] = spec->imag();

        return stftBase->complex;
    }

    DLL_EXPORT float* STFT_AnalysisWindow(BaseSTFT* stftBase) {
        return stftBase->analysisWindow();
    }

    DLL_EXPORT float* STFT_SynthesisWindow(BaseSTFT* stftBase) {
        return stftBase->synthesisWindow();
    }

    DLL_EXPORT size_t STFT_AnalysisLatency(BaseSTFT* stftBase) {
        return stftBase->analysisLatency();
    }

    DLL_EXPORT size_t STFT_SynthesisLatency(BaseSTFT* stftBase) {
        return stftBase->synthesisLatency();
    }

    DLL_EXPORT size_t STFT_Latency(BaseSTFT* stftBase) {
        return stftBase->latency();
    }

    DLL_EXPORT float STFT_BinToFreq(BaseSTFT* stftBase, float b) {
        return stftBase->binToFreq(b);
    }

    DLL_EXPORT float STFT_FreqToBin(BaseSTFT* stftBase, float f) {
        return stftBase->freqToBin(f);
    }

    DLL_EXPORT size_t STFT_AnalyseSteps(BaseSTFT* stftBase) {
        return stftBase->analyseSteps();
    }

    DLL_EXPORT size_t STFT_SynthesiseSteps(BaseSTFT* stftBase) {
        return stftBase->synthesiseSteps();
    }

    DLL_EXPORT void STFT_AnalyseStep(BaseSTFT* stftBase, size_t step, size_t sampleInPast) {
        stftBase->analyseStep(step, sampleInPast);
    }

    DLL_EXPORT void STFT_SynthesiseStep(BaseSTFT* stftBase, size_t step) {
        stftBase->synthesiseStep(step);
    }

    DLL_EXPORT size_t STFT_SamplesSinceAnalysis(BaseSTFT* stftBase) {
        return stftBase->samplesSinceAnalysis();
    }

    DLL_EXPORT size_t STFT_SamplesSinceSynthesis(BaseSTFT* stftBase) {
        return stftBase->samplesSinceSynthesis();
    }

    DLL_EXPORT void STFT_FinishOutput(BaseSTFT* stftBase, float strength, size_t offset) {
        stftBase->finishOutput(strength, offset);
    }

    DLL_EXPORT void STFT_AddOutput(BaseSTFT* stftBase, size_t channel, size_t offset, size_t length, const float* outputArray) {
        stftBase->addOutput(channel, offset, length, outputArray);
    }

    DLL_EXPORT void STFT_ReplaceOutput(BaseSTFT* stftBase, size_t channel, size_t offset, size_t length, const float* outputArray) {
        stftBase->replaceOutput(channel, offset, length, outputArray);
    }

    DLL_EXPORT void STFT_MoveOutput(BaseSTFT* stftBase, size_t samples) {
        stftBase->moveOutput(samples);
    }

    DLL_EXPORT void STFT_AnalysisOffset(BaseSTFT* stftBase, size_t offset) {
        stftBase->analysisOffset(offset);
    }

    DLL_EXPORT void STFT_SynthesisOffset(BaseSTFT* stftBase, size_t offset) {
        stftBase->synthesisOffset(offset);
    }
}