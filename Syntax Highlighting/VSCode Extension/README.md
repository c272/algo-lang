# Algo Language Support Extension

This plugin adds syntax highlighting for the Algo programming language, by c272.
https://github.com/c272/algo-lang/

## Features

Adds contextual highlighting for all faucets of the Algo language, including comments, strings and escape characters, and function folding.

## Known Issues

There are currently no known issues with this extension, so if you find some, feel free to report the issue here:
https://github.com/c272/algo-lang/issues/

## Release Notes

### 1.0.5
Fixed a small scoping bug pertaining to operator keywords.

### 1.0.4
Added more detailed TextMate contexts instead of grouping keywords into one rule. Operators are now also properly detected as operators, and not invalid symbols, along with logical checks. Import statements now show an automatically generated path.

### 1.0.3
Updated the grammar to include the new "try" and "catch" keywords, and some more contextual actions.

### 1.0.0

Initial release of the extension, added contextual grammar support for Algo 0.0.4.