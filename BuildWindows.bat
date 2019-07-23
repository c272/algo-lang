@echo OFF
set /p runBuild=Make sure MSBuild is added to path before running this program. Is MSBuild added to path? (Y/N)

if "%runBuild%"=="Y" (
    echo Building...
    MSBuild.exe
    echo Creating directories...
    mkdir Builds
    cd Builds
    mkdir std
    mkdir packages
    cd ..
    echo Copying build dependencies...
    robocopy "Algo\bin\Debug\Algo.exe" "Builds\"
    robocopy "Algo\Standard Library\Algo Scripts\" "Builds\std\" *.*
    echo Build finished.
)
