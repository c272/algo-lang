![algologo](logo_small.png)

![license](https://img.shields.io/badge/license-MIT-blue.svg) ![issues](https://img.shields.io/github/issues/c272/algo-lang.svg) ![support](https://img.shields.io/badge/platform-c%23.net%20%3E%3D%207-lightgrey.svg) ![build](https://travis-ci.com/c272/algo-lang.svg?branch=master)

*A programming language which aims to be simple, mathematical and easy to learn.*
## Introduction
Algo is a programming language designed to look similar to mathematical notation that aims to make programming easy through a vast standard library.

The language is currently not ready for general use. If, however, you want to contribute to the standard library or language itself, feel free to fork and make a pull request, which are considered on a case-by-case basis. Please remember to check the pull request templates beforehand.

**Plugins for Popular Text Editors:**
* [Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=c272.Algo)
* Atom (*Coming v0.0.4*)
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
Also, as an example of what the language looks like, here's some sample code I've typed up.

*Warning: This may be out of date when there are major updates and revisions to the language, and the README hasn't been modified.*

    //Import the input library and string library.
    import "io";

    //Define a function to enumerate over the string characters.
    let printCharacters(x) = {

        //Print all the characters individually.
        let charList = string.getChars(x);
        foreach (i in charList) {
            print i;
        }

        print "Your string is " + len(x) + " characters long.";
    };

    //Make some cool stuff happen.
    print "What's your name?"
    let name = input.get();
    printCharacters(name);
