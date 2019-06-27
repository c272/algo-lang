![algologo](logo_small.png)

![deps](https://img.shields.io/badge/dependencies-none-green.svg)      ![license](https://img.shields.io/badge/license-MIT-blue.svg) ![version](https://img.shields.io/badge/version-v0.04-orange.svg) ![support](https://img.shields.io/badge/platform-c%23.net%20%3E%3D%207-lightgrey.svg)

*A programming language which aims to be simple, mathematical and easy to learn.*
## Introduction
Algo is a programming language designed to look similar to mathematical notation that aims to make programming easy through a vast standard library.

The language is currently not ready for general use. If, however, you want to contribute to the library, feel free to fork and make a pull request, which are considered on a case-by-case basis. Please remember to check the pull request templates beforehand.

## Building Algo
To build the interpreter for Algo, you need at least Visual Studio 2015 (Update 3) or higher. Since Algo uses .NET Framework, this is only compileable on Linux through the use of Mono.

Building in "Debug" mode is recommended for all contributions to the update branch, but if you are using it for a personal modification, using "Release" is a better option, for size and compute efficiency.

## Code Example

    //Import the input library and string library.
    import "std/input";
    import "std/core";
    import "std/string"

    //Define a function to enumerate over the string characters.
    let printCharacters(x) = {

        //Print all the characters individually.
        let charList = string.getChars(x);
        for (i in charList) {
            print charList[i];
        }

        print "Your string is " + len(x) + " characters long.";
    };

    //Make some cool stuff happen.
    print "What's your name?"
    let name = input.get();
    printCharacters(name);
