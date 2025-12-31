#include <cstring>
#include "signalsmith-linear/fft.h"

#if defined(_WIN32) || defined(__CYGWIN__)
    #define DLL_EXPORT __declspec(dllexport)
#elif defined(__linux__) || defined(__APPLE__)
    #define DLL_EXPORT __attribute__((visibility("default")))
#else
    #define DLL_EXPORT
#endif

struct FFT {
    signalsmith::linear::FFT<float> fft;

    std::vector<std::complex<float>> inputBuffer;
    std::vector<std::complex<float>> outputBuffer;
};

extern "C" {
    DLL_EXPORT FFT* FFT_Create(size_t size) {
        FFT* fft = new FFT();
        return fft;
    }

    DLL_EXPORT void FFT_Delete(FFT* fft) {
        delete fft;
    }

    DLL_EXPORT void FFT_Resize(FFT* fft, size_t size) {
        fft->fft.resize(size);
    }

    DLL_EXPORT size_t FFT_Size(FFT* fft) {
        return fft->fft.size();
    }

    DLL_EXPORT size_t FFT_Steps(FFT* fft) {
        return fft->fft.steps();
    }

    DLL_EXPORT void FFT_Proc(FFT* fft, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        size_t size = fft->fft.size();
        fft->inputBuffer.resize(size);
        fft->outputBuffer.resize(size);

        for (size_t i = 0; i < size; ++i) {
            fft->inputBuffer[i] = std::complex<float>(inputReal[i], inputImag ? inputImag[i] : 0.0f);
        }

        fft->fft.fft(fft->inputBuffer.data(), fft->outputBuffer.data());
        for (size_t i = 0; i < size; ++i) {
            outputReal[i] = fft->outputBuffer[i].real();
            outputImag[i] = fft->outputBuffer[i].imag();
        }
    }

    DLL_EXPORT void FFT_ProcStep(FFT* fft, size_t step, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        size_t size = fft->fft.size();
        fft->inputBuffer.resize(size);
        fft->outputBuffer.resize(size);

        for (size_t i = 0; i < size; ++i) {
            fft->inputBuffer[i] = std::complex<float>(inputReal[i], inputImag ? inputImag[i] : 0.0f);
        }

        fft->fft.fft(step, fft->inputBuffer.data(), fft->outputBuffer.data());
        for (size_t i = 0; i < size; ++i) {
            outputReal[i] = fft->outputBuffer[i].real();
            outputImag[i] = fft->outputBuffer[i].imag();
        }
    }

    DLL_EXPORT void FFT_ProcSplit(FFT* fft, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        fft->fft.fft(inputReal, inputImag, outputReal, outputImag);
    }

    DLL_EXPORT void FFT_ProcSplitStep(FFT* fft, size_t step, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        fft->fft.fft(step, inputReal, inputImag, outputReal, outputImag);
    }

    DLL_EXPORT void FFT_InverseProc(FFT* fft, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        size_t size = fft->fft.size();
        fft->inputBuffer.resize(size);
        fft->outputBuffer.resize(size);

        for (size_t i = 0; i < size; ++i) {
            fft->inputBuffer[i] = std::complex<float>(inputReal[i], inputImag ? inputImag[i] : 0.0f);
        }
        fft->fft.ifft(fft->inputBuffer.data(), fft->outputBuffer.data());
        for (size_t i = 0; i < size; ++i) {
            outputReal[i] = fft->outputBuffer[i].real();
            outputImag[i] = fft->outputBuffer[i].imag();
        }
    }

    DLL_EXPORT void FFT_InverseProcStep(FFT* fft, size_t step, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        size_t size = fft->fft.size();
        fft->inputBuffer.resize(size);
        fft->outputBuffer.resize(size);

        for (size_t i = 0; i < size; ++i) {
            fft->inputBuffer[i] = std::complex<float>(inputReal[i], inputImag ? inputImag[i] : 0.0f);
        }

        fft->fft.ifft(step, fft->inputBuffer.data(), fft->outputBuffer.data());
        for (size_t i = 0; i < size; ++i) {
            outputReal[i] = fft->outputBuffer[i].real();
            outputImag[i] = fft->outputBuffer[i].imag();
        }
    }

    DLL_EXPORT void FFT_InverseProcSplit(FFT* fft, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        fft->fft.ifft(inputReal, inputImag, outputReal, outputImag);
    }

    DLL_EXPORT void FFT_InverseProcSplitStep(FFT* fft, size_t step, const float* inputReal, const float* inputImag, float* outputReal, float* outputImag) {
        fft->fft.ifft(step, inputReal, inputImag, outputReal, outputImag);
    }
}