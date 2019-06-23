![algologo](logo_small.png)

![deps](https://img.shields.io/badge/dependencies-none-green.svg)      ![license](https://img.shields.io/badge/license-MIT-blue.svg) ![version](https://img.shields.io/badge/version-v0.04-orange.svg) ![support](https://img.shields.io/badge/platform-c%23.net%20%3E%3D%207-lightgrey.svg)

*A programming language which aims to be simple, mathematical and easy to learn.*
## Introduction
Algo is a programming language designed to look similar to mathematical notation that aims to make programming easy through a vast standard library.

The language is currently not ready for general use. If, however, you want to contribute to the library, feel free to fork and make a pull request, which are considered on a case-by-case basis. Please remember to check the pull request templates beforehand.

## Building Algo
To build the interpreter for Algo, you need at least Visual Studio 2015 (Update 3) or higher. Since Algo uses .NET Framework, this is only compileable on Linux through the use of Mono.

Building in "Debug" mode is recommended for all contributions to the update branch, but if you are using it for a personal modification, using "Release" is a better option, for size and compute efficiency.

## Code Examples
**The basic "Hello World" program in Algo:**

    print "Hello World";

**Pythonic imports and preprocessor statements.**

    import somefile;
    
**Variable and function declaration:**

    //Declare some variables and functions.
    let foo = 12.34;
    let bar = 2;
    let baz(x) = {
      return x+2;
    }
    
    //Delete some variables.
    disregard foo;
    disregard bar;
    
**"If", "for" and "while" statements:**
    
    if (a == 1.232) {
      ...
    }
    
    b = [3, 2, 4, 6, "foo"];
    for (i in b) {
      ...
    }
    
    c = 3;
    while (c < 5) {
        print c;
        c += 1;
    }

**Libraries and Objects**
    
    //Define a basic library.
    library SomeLib {
        let b = 3;
    }
    
    //Call from the library.
    print SomeLib.b;
    
    //Define a basic object.
    let someObj = object {
        let x = SomeLib.b;
    };
    
    //Call from the object.
    print someObj.x;
