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
robocopy "Algo\bin\Debug" "Builds" Algo.exe AlgoSDK.dll Antlr4.Runtime.dll MathNet.Numerics.dll Newtonsoft.Json.dll System.Runtime.InteropServices.RuntimeInformation.dll ILRepack.dll
robocopy "Algo\Standard Library\Algo Scripts" "Builds\std" *.*
echo Build finished.
pause
