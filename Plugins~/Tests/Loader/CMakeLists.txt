cmake_minimum_required(VERSION 3.15)

file(GLOB sources *.cpp *.h )
set(SRC_ROOT   "${CMAKE_CURRENT_SOURCE_DIR}/../../Src")

add_executable(LoaderTest  ${sources})
target_include_directories(LoaderTest PRIVATE 
    ${SRC_ROOT}
    ${GTEST_INCLUDE_DIRS}    
)
               
target_link_libraries(LoaderTest Loader 
    ${GTEST_BOTH_LIBRARIES}
    ${COCOA_LIBRARY}
)  
  

add_test(NAME LoaderTest COMMAND LoaderTest)


# Copy test data
set(num_test_images 100)
target_compile_definitions(LoaderTest PRIVATE NUM_TEST_IMAGES=${num_test_images})
foreach(loop_var RANGE ${num_test_images})     
    configure_file(
        ${CMAKE_CURRENT_SOURCE_DIR}/../Data/TestImage.png
        ${CMAKE_CURRENT_BINARY_DIR}/TestImage_${loop_var}.png COPYONLY
    )    
endforeach(loop_var) 

if(APPLE)
    # When run in XCode, the working directory is not in the root, but instead under the configuration folder
    # In the future, we should probably configure XCODE_SCHEME_WORKING_DIRECTORY, but I think it is still too new to be used now (CMake 3.17.3).
    # https://cmake.org/cmake/help/v3.17/prop_tgt/XCODE_SCHEME_WORKING_DIRECTORY.html#prop_tgt:XCODE_SCHEME_WORKING_DIRECTORY
    foreach(loop_var RANGE ${num_test_images})     
        add_custom_command(TARGET LoaderTest POST_BUILD
            COMMAND ${CMAKE_COMMAND} -E copy ${CMAKE_CURRENT_SOURCE_DIR}/../Data/TestImage.png ${CMAKE_CURRENT_BINARY_DIR}/$<CONFIGURATION>/TestImage_${loop_var}.png
        )
    endforeach(loop_var) 
    
endif()    


