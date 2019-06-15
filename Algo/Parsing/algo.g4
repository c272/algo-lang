grammar algo;

/*
 * Parser Rules
 */

compileUnit
	: block EOF
	;

block: statement*;

//Statements which require endlines, and those that don't.
statement: (  stat_define
			| stat_functionCall
			| stat_print
			| stat_setvar
		   )  ENDLINE
		   
			|  ( stat_forLoop
			   | stat_functionDef
			   | stat_if);

//Types of statement.
stat_define: LET_SYM IDENTIFIER EQUALS expr;
stat_setvar: IDENTIFIER EQUALS expr;
stat_functionCall: IDENTIFIER LBRACKET literal_params? RBRACKET;
stat_functionDef: LET_SYM IDENTIFIER LBRACKET abstract_params? RBRACKET EQUALS LBRACE statement* RBRACE;
stat_forLoop: FOR_SYM LBRACKET IDENTIFIER IN_SYM IDENTIFIER RBRACKET LBRACE statement* RBRACE;
stat_if: IF_SYM LBRACKET expr RBRACKET LBRACE statement* RBRACE;
stat_print: PRINT_SYM expr;

//Checks, parameter types.
literal_params: (expr COMMA)* expr;
abstract_params: (IDENTIFIER COMMA)* IDENTIFIER;

//A check, along with possible check operators.
check: expr check_operator expr
	   | expr;

check_operator: BIN_OR | BIN_AND | GRTR_THAN | LESS_THAN | GRTR_THAN_ET | LESS_THAN_ET | BIN_EQUALS;

//An expression, divided into layers of precedence.
expr: expr ADD_OP term
	| expr TAKE_OP term
	| term;

term: term MUL_OP factor
	| term DIV_OP factor
	| factor;

factor: factor POW_OP sub
	  | sub;

sub: value | LBRACKET expr RBRACKET;

//Mathematical operators.
operator: MUL_OP | DIV_OP | TAKE_OP | ADD_OP | POW_OP;

//A single literal value.
value: stat_functionCall | IDENTIFIER | INTEGER | FLOAT | BOOLEAN | STRING | RATIONAL | array | array_access;

//An array.
array: '[' ((value ',')* value)? ']';
array_access: IDENTIFIER '[' literal_params ']';

/*
 * Lexer Rules
 */

//Integer.
INTEGER: ('-')? [1-9]* [0-9];

//Float.
FLOAT: ('-')? [1-9]* [0-9] '.' [0-9]+;

//Boolean.
BOOLEAN: 'true' | 'false';

//String.
STRING: '"' (~[\"])* '"';

//Rational.
RATIONAL: INTEGER '/' INTEGER;

// Commonly used and reserved symbols in Algo.
// RESERVED WORDS
LET_SYM: 'let';
FOR_SYM: 'for';
IN_SYM: 'in';
IF_SYM: 'if';
IMPORT_SYM: 'import';
RETURN_SYM: 'return';
PRINT_SYM: 'print';

//NON-MATHEMATICAL SYMBOLS
ENDLINE: ';';
EQUALS: '=';
COMMA: ',';
LBRACE: '{';
RBRACE: '}';
LSQBR: '[';
RSQBR: ']';

//MATHEMATICAL SYMBOLS
LBRACKET: '(';
RBRACKET: ')';
ADD_OP: '+';
TAKE_OP: '-';
MUL_OP: '*';
DIV_OP: '/';
POW_OP: '^';

//CHECK OPERATORS
BIN_OR: '|';
BIN_AND: '&';
BIN_EQUALS: '==';
GRTR_THAN: '>';
LESS_THAN: '<';
GRTR_THAN_ET: '>=';
LESS_THAN_ET: '<=';


//Identifier.
IDENTIFIER: [A-Za-z_] [0-9A-Za-z_]*;

//Ignore comments, not relevant.
COMMENT: '//' .*? '\n' -> skip;

//Ignore all whitespace, not relevant.
WS:	[ \n\r\t] -> skip;

//Unknown symbol.
UNKNOWN_SYMBOL: .;