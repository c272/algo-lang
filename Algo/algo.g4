grammar algo;

/*
 * Parser Rules
 */

compileUnit
	: block EOF
	;

block: statement*;

statement: (stat_define
			| stat_functionCall
			| stat_functionDef
			| stat_forLoop ) ENDLINE;

//Defining a variable.
LET_SYM IDENTIFIER EQUALS

/*
 * Lexer Rules
 */

//Integer.
INTEGER: ('-')? [1-9]* [0-9];

//Float.
FLOAT: ('-')? [1-9]* [0-9] '.' [0-9]+;

// Commonly used and reserved symbols in Algo.
LET_SYM: 'let';
FOR_SYM: 'for';
IMPORT_SYM: 'import';
ENDLINE: ';';
EQUALS: '=';

//Ignore comments, not relevant.
COMMENT: '//' .* '\n' -> skip;

//Ignore all whitespace, not relevant.
WS
	:	[ \n\r\t] -> skip;
	;
