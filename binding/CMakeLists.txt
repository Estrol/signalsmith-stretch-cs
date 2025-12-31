cmake_minimum_required(VERSION 3.10)
project(SignalsmithStretch-csharp-binding)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

add_subdirectory(./signalsmith-stretch)

add_library(SignalsmithStretch SHARED mod.cpp fft.cpp stft.cpp)
target_link_libraries(SignalsmithStretch PRIVATE signalsmith-stretch)