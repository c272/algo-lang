@echo OFF
setlocal enabledelayedexpansion

echo MAKE SURE MSBUILD IS ADDED TO PATH!
echo Building...
msbuild.exe
echo Creating directories...
mkdir Builds
cd Builds
mkdir std
mkdir packages
cd ..
echo Copying build dependencies...
robocopy "Algo\bin\Debug" "Builds" Algo.exe
robocopy "Algo\Standard Library\Algo Scripts" "Builds\std" *.*
echo Build finished.
pause
