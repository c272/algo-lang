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
            | stat_enumDef
			| stat_functionCall
			| stat_print
			| stat_setvar
			| stat_setvar_op
			| stat_setvar_postfix
			| stat_return
			| stat_deletevar
			| stat_import
			| stat_loadFuncExt
            | stat_list_add
            | stat_list_remove
		   )  ENDLINE
		   
			|  ( stat_forLoop
			   | stat_functionDef
			   | stat_if
			   | stat_library
			   | stat_whileLoop
			   );

//Types of statement.
stat_define: LET_SYM IDENTIFIER EQUALS expr;
stat_setvar: (IDENTIFIER | obj_access) EQUALS expr rounding_expr?;
stat_setvar_op: (IDENTIFIER | obj_access) selfmod_op expr;
stat_setvar_postfix: (IDENTIFIER | obj_access) postfix_op;
stat_deletevar: DISREGARD_SYM (IDENTIFIER | obj_access | MUL_OP);
stat_enumDef: LET_SYM IDENTIFIER EQUALS ENUM_SYM LBRACE abstract_params? RBRACE;
stat_functionCall: (IDENTIFIER | obj_access) LBRACKET literal_params? RBRACKET;
stat_functionDef: LET_SYM IDENTIFIER LBRACKET abstract_params? RBRACKET EQUALS LBRACE statement* RBRACE;
stat_loadFuncExt: EXTERNAL_SYM IDENTIFIER STREAMING_SYM obj_access;
stat_return: RETURN_SYM expr;
stat_forLoop: FOR_SYM LBRACKET IDENTIFIER ((IN_SYM expr) | UP_SYM TO_SYM expr) RBRACKET LBRACE statement* RBRACE;
stat_whileLoop: WHILE_SYM LBRACKET check RBRACKET LBRACE statement* RBRACE;
stat_if: IF_SYM LBRACKET check RBRACKET LBRACE statement* RBRACE stat_elif* stat_else?;
stat_print: PRINT_SYM expr rounding_expr?;
stat_library: LIB_SYM IDENTIFIER LBRACE statement* RBRACE;
stat_import: IMPORT_SYM expr (AS_SYM IDENTIFIER)?;
stat_list_add: ADD_SYM expr TO_SYM (IDENTIFIER | obj_access) (AT_SYM expr)?;
stat_list_remove: REMOVE_SYM expr FROM_SYM? (IDENTIFIER | obj_access);

//Elif and else statements, not directly available, only by proxy.
stat_elif: ELSE_SYM IF_SYM LBRACKET check RBRACKET LBRACE statement* RBRACE;
stat_else: ELSE_SYM LBRACE statement* RBRACE;

//Checks, parameter types.
literal_params: (expr COMMA)* expr;
abstract_params: (IDENTIFIER COMMA)* IDENTIFIER;

//A check, along with possible check operators.
check:   check BIN_OR checkfrag
	   | check BIN_AND checkfrag
	   | LBRACKET check RBRACKET
	   | INVERT_SYM check
	   | checkfrag;

checkfrag:   expr GRTR_THAN expr
		   | expr LESS_THAN expr
		   | expr GRTR_THAN_ET expr
		   | expr LESS_THAN_ET expr
		   | expr BIN_EQUALS expr
	       | expr BIN_NET expr
		   | expr;

//An expression, divided into layers of precedence.
expr: expr ADD_OP term
	| expr TAKE_OP term
	| term;

//A rounding fractal.
rounding_expr: TO_SYM expr SIG_FIG_SYM;

term: term MUL_OP factor
	| term DIV_OP factor
	| factor;

factor: factor POW_OP sub
	  | sub;

sub: value | LBRACKET expr RBRACKET;

//Mathematical operators.
operator: MUL_OP | DIV_OP | TAKE_OP | ADD_OP | POW_OP;
//Self modifying operators.
selfmod_op: ADDFROM_OP | TAKEFROM_OP | MULFROM_OP | DIVFROM_OP;
//Postfix operators.
postfix_op: ADD_PFOP | TAKE_PFOP;

//A single literal value.
value: stat_functionCall | obj_access | IDENTIFIER | INTEGER | FLOAT | BOOLEAN | STRING | RATIONAL | NULL | array | array_access | object;

//Accessing a library or object.
obj_access: (IDENTIFIER POINT)+ IDENTIFIER;

//An array.
array: '[' ((value ',')* value)? ']';
//Accessing an array, through a stored, function returned or literal array.
array_access: (IDENTIFIER | obj_access | stat_functionCall | array) '[' literal_params ']';

//A single Algo object represented in text.
object: OBJ_SYM LBRACE obj_child_definitions? RBRACE;
obj_child_definitions: (obj_vardefine | obj_funcdefine)+;
obj_vardefine: LET_SYM IDENTIFIER EQUALS expr ENDLINE;
obj_funcdefine: LET_SYM IDENTIFIER LBRACKET abstract_params? RBRACKET EQUALS LBRACE statement* RBRACE;

/*
 * Lexer Rules
 */

//Integer.
INTEGER: ('-')? [1-9] [0-9]* | '0';

//Float.
FLOAT: ('-')? [1-9]* [0-9] '.' [0-9]+;

//Boolean.
BOOLEAN: 'true' | 'false';

//String.
STRING: '"' (~["] | '\\"')* '"';

//Rational.
RATIONAL: INTEGER [ ]* DIV_OP [ ]* INTEGER; 

//Null.
NULL: 'null';

// Commonly used and reserved symbols in Algo.
// RESERVED WORDS
LET_SYM: 'let';
FOR_SYM: 'for';
ADD_SYM: 'add';
AT_SYM: 'at';
REMOVE_SYM: 'remove';
FROM_SYM: 'from';
WHILE_SYM: 'while';
IN_SYM: 'in';
IF_SYM: 'if';
UP_SYM: 'up';
TO_SYM: 'to';
AS_SYM: 'as';
ENUM_SYM: 'enum';
LIB_SYM: 'library';
SIG_FIG_SYM: 'sf';
OBJ_SYM: 'object';
ELSE_SYM: 'else';
IMPORT_SYM: 'import';
RETURN_SYM: 'return';
PRINT_SYM: 'print';
DISREGARD_SYM: 'disregard';
EXTERNAL_SYM: 'external';

//NON-MATHEMATICAL SYMBOLS
ENDLINE: ';';
EQUALS: '=';
COMMA: ',';
LBRACE: '{';
RBRACE: '}';
LSQBR: '[';
RSQBR: ']';
INVERT_SYM: '!';
STREAMING_SYM: '<-';

//MATHEMATICAL SYMBOLS
LBRACKET: '(';
RBRACKET: ')';
ADD_OP: '+';
TAKE_OP: '-';
MUL_OP: '*';
DIV_OP: '/';
POW_OP: '^';
POINT: '.';
ADDFROM_OP: '+=';
TAKEFROM_OP: '-=';
DIVFROM_OP: '/=';
MULFROM_OP: '*=';
ADD_PFOP: '++';
TAKE_PFOP: '--';

//CHECK OPERATORS
BIN_OR: '|';
BIN_AND: '&';
BIN_NET: '!=';
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