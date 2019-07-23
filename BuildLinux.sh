#!/bin/bash

# Build the entire of the solution..
msbuild 

# Create the native bundle for this machine.
######################################################
# MAKE SURE YOUR MACHINE CONFIG IS AT THIS LOCATION! #
######################################################

mkbundle -o algo --simple Algo/bin/Debug/Algo.exe --machine-config /etc/mono/4.5/machine.config --no-config --nodeps Algo/bin/Debug/*.dll
