cmake_minimum_required(VERSION 3.0)

project(smooth_resample_noisy_data_mls)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Find PCL, with the option to include certain components: io, common, etc.
find_package(PCL 1.10 REQUIRED)

include_directories(
    ${PCL_INCLUDE_DIRS}
    ${PROJECT_SOURCE_DIR}
    ${Boost_INCLUDE_DIR}
)

link_directories(${PCL_LIBRARY_DIRS})
add_definitions(${PCL_DEFINITIONS})

add_executable (resampling_mls resampling_mls.cpp)
target_link_libraries (resampling_mls ${PCL_LIBRARIES})

