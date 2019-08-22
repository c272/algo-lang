# Change Log

Changes to the Algo Language Support Extension are documented here.

## 1.0.0

- Initial release, added contextual grammar support.

## 1.0.3

- Updated grammar to include the new "try" and "catch" keywords.

## 1.0.4

- Added more detailed TextMate contexts instead of grouping keywords into one rule.
- Operators are now detected as operators, and not invalid symbols.
- Logical checks are now detected by the grammar.
- Import statements now show an automatically generated path.

## 1.0.5
- Fixed a scoping bug affecting keyword.operator.word patterns.

## 1.0.6
- Fixed a precedence bug involving for and foreach keywords.

## 1.0.7
- The "to" keyword is now added as an operator keyword. (#1)

## 1.0.8
- Added the new "throw" functionality (v0.0.4).