![algologo](logo_small.png) **v0.1.0**

![license](https://img.shields.io/badge/license-MIT-blue.svg) ![issues](https://img.shields.io/github/issues/c272/algo-lang.svg) ![support](https://img.shields.io/badge/platform-c%23.net%20%3E%3D%204%2E7-lightgrey.svg) ![build](https://travis-ci.com/c272/algo-lang.svg?branch=master)

*A programming language which aims to be simple, mathematical and easy to learn.*
## Introduction
Algo is a programming language designed to look similar to mathematical notation that aims to provide a simple and stable set of tools.

The language is not ready for general use. If, however, you want to contribute to the standard library or language itself, feel free to fork and make a pull request, just make sure that your branch passes the unit tests. Please remember to check the pull request templates beforehand.

**Plugins for Popular Text Editors:**
* [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=c272.Algo)
* [Sublime Text 3](https://github.com/c272/algo-lang/tree/master/Syntax%20Highlighting/Sublime%20Text%20Package)


## Building Algo
To build the interpreter for Algo, you need at least Visual Studio 2017 or higher. Since Algo uses .NET Framework, this is only compileable on Linux through the use of Mono. The actual interpreter is platform agnostic, so works on Linux, Mac and Windows.

*Note: The only officially tested versions are elementary OS Loki/Juno (Ubuntu Linux & Forks) and Windows 10 Creator Update 1903+.*

Building in "Debug" mode is recommended for all contributions to the update branch, but if you are using it for a personal modification, using "Release" is a better option, for size and compute efficiency.

## How do I use this?
If you've built the interpreter, or downloaded a release build from the "Releases" tab, first add the directory you extracted `algo.exe` to into PATH, and then you can execute a file by using
```
algo (filename) [--dev]
```
Including the `--dev` flag makes the interpreter print all lexed tokens, and show the parse tree before executing. After execution or errors, it will also dump the variables into console.

Simply running `algo -v` in console will display your current version, including the build number and revision. **Please state your build and revision when reporting bugs in the issues section.**

For more information on language syntax and usage, see the GitHub wiki, [here.](https://github.com/c272/algo-lang/wiki)

## Sample Code
For test samples of Algo code and workflow, see the [examples.](Examples/)
Just to show a small snippet, here's some test code I wrote when originally drafting the language:

    //Import the input library.
    import "io";

    //Define a function to enumerate over the string characters.
    let printCharacters(x) = {

        //Print all the characters individually.
        foreach (i in string.toChars(x)) {
            print i;
        }

        print "Your string is " + len(x) + " characters long.";
    };

    //Make some cool stuff happen.
    print "What's your name?"
    let name = input.get();
    printCharacters(name);
