#!/bin/bash

# Build the entire solution.
msbuild 

# Create the native bundle for this machine.
######################################################
# MAKE SURE YOUR MACHINE CONFIG IS AT THIS LOCATION! #
######################################################

mkbundle -o Builds/algo --simple Algo/bin/Debug/Algo.exe --machine-config /etc/mono/4.5/machine.config --no-config --nodeps Algo/bin/Debug/*.dll

# Copy over the standard library, create necessary directories.
mkdir Builds
cd Builds
mkdir std
mkdir packages
cd ..
cp Algo/Standard\ Library/Algo\ Scripts/*  Builds/std
echo BUILD COMPLETE!
