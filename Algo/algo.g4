grammar algo;

/*
 * Parser Rules
 */

compileUnit
	: block EOF
	;

block: statement*;

//Statements which require endlines, and those that don't.
statement: (stat_define
			| stat_functionCall
		   ) ENDLINE

			| (stat_forLoop
			   | stat_functionDef);

//Types of statement.
stat_define: LET_SYM IDENTIFIER EQUALS expr;
stat_setvar: IDENTIFIER EQUALS expr;
stat_functionCall: IDENTIFIER LBRACKET literal_params? RBRACKET;
stat_functionDef: LET_SYM IDENTIFIER LBRACKET abstract_params? RBRACKET EQUALS LBRACE statement* RBRACE;
stat_forLoop: FOR_SYM LBRACKET IDENTIFIER IN_SYM IDENTIFIER RBRACKET LBRACE statement* RBRACE;
stat_if: IF_SYM LBRACKET expr RBRACKET LBRACE statement* RBRACE;

//Checks, parameter types.
literal_params: (expr COMMA)* expr;
abstract_params: (IDENTIFIER COMMA)* IDENTIFIER;

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
value: stat_functionCall | INTEGER | FLOAT | BOOLEAN | STRING;

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

// Commonly used and reserved symbols in Algo.
LET_SYM: 'let';
FOR_SYM: 'for';
IN_SYM: 'in';
IF_SYM: 'if';
IMPORT_SYM: 'import';
ENDLINE: ';';
EQUALS: '=';
COMMA: ',';
LBRACKET: '(';
RBRACKET: ')';
ADD_OP: '+';
TAKE_OP: '-';
MUL_OP: '*';
DIV_OP: '/';
POW_OP: '^';
BIN_OR: '||';
BIN_AND: '&&';
LBRACE: '{';
RBRACE: '}';

//Identifier.
IDENTIFIER: [A-Za-z_] [0-9A-Za-z_]*;

//Ignore comments, not relevant.
COMMENT: '//' .*? '\n' -> skip;

//Ignore all whitespace, not relevant.
WS:	[ \n\r\t] -> skip;

//Unknown symbol.
UNKNOWN_SYMBOL: .;