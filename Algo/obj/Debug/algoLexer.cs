﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Z:\Algo\Algo\Parsing\algo.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Algo {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class algoLexer : Lexer {
	public const int
		INTEGER=1, FLOAT=2, BOOLEAN=3, STRING=4, RATIONAL=5, LET_SYM=6, FOR_SYM=7, 
		IN_SYM=8, IF_SYM=9, TO_SYM=10, LIB_SYM=11, SIG_FIG_SYM=12, OBJ_SYM=13, 
		ELSE_SYM=14, IMPORT_SYM=15, RETURN_SYM=16, PRINT_SYM=17, DISREGARD_SYM=18, 
		ENDLINE=19, EQUALS=20, COMMA=21, LBRACE=22, RBRACE=23, LSQBR=24, RSQBR=25, 
		LBRACKET=26, RBRACKET=27, ADD_OP=28, TAKE_OP=29, MUL_OP=30, DIV_OP=31, 
		POW_OP=32, POINT=33, BIN_OR=34, BIN_AND=35, BIN_EQUALS=36, GRTR_THAN=37, 
		LESS_THAN=38, GRTR_THAN_ET=39, LESS_THAN_ET=40, IDENTIFIER=41, COMMENT=42, 
		WS=43, UNKNOWN_SYMBOL=44;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "LET_SYM", "FOR_SYM", 
		"IN_SYM", "IF_SYM", "TO_SYM", "LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", "ELSE_SYM", 
		"IMPORT_SYM", "RETURN_SYM", "PRINT_SYM", "DISREGARD_SYM", "ENDLINE", "EQUALS", 
		"COMMA", "LBRACE", "RBRACE", "LSQBR", "RSQBR", "LBRACKET", "RBRACKET", 
		"ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "POINT", "BIN_OR", 
		"BIN_AND", "BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", "LESS_THAN_ET", 
		"IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
	};


	public algoLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, "'let'", "'for'", "'in'", "'if'", 
		"'to'", "'library'", "'sf'", "'object'", "'else'", "'import'", "'return'", 
		"'print'", "'disregard'", "';'", "'='", "','", "'{'", "'}'", "'['", "']'", 
		"'('", "')'", "'+'", "'-'", "'*'", "'/'", "'^'", "'.'", "'|'", "'&'", 
		"'=='", "'>'", "'<'", "'>='", "'<='"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "LET_SYM", 
		"FOR_SYM", "IN_SYM", "IF_SYM", "TO_SYM", "LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", 
		"ELSE_SYM", "IMPORT_SYM", "RETURN_SYM", "PRINT_SYM", "DISREGARD_SYM", 
		"ENDLINE", "EQUALS", "COMMA", "LBRACE", "RBRACE", "LSQBR", "RSQBR", "LBRACKET", 
		"RBRACKET", "ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "POINT", 
		"BIN_OR", "BIN_AND", "BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", 
		"LESS_THAN_ET", "IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "algo.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2.\x11C\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x3\x2\x5\x2]\n\x2\x3\x2\x3\x2\a\x2\x61\n\x2\f\x2"+
		"\xE\x2\x64\v\x2\x3\x3\x5\x3g\n\x3\x3\x3\a\x3j\n\x3\f\x3\xE\x3m\v\x3\x3"+
		"\x3\x3\x3\x3\x3\x6\x3r\n\x3\r\x3\xE\x3s\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x5\x4\x7F\n\x4\x3\x5\x3\x5\a\x5\x83\n\x5\f\x5"+
		"\xE\x5\x86\v\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3"+
		"\a\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3"+
		"\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3"+
		"\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3"+
		"\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3"+
		"\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3$\x3$\x3%\x3%\x3%\x3&"+
		"\x3&\x3\'\x3\'\x3(\x3(\x3(\x3)\x3)\x3)\x3*\x3*\a*\x105\n*\f*\xE*\x108"+
		"\v*\x3+\x3+\x3+\x3+\a+\x10E\n+\f+\xE+\x111\v+\x3+\x3+\x3+\x3+\x3,\x3,"+
		"\x3,\x3,\x3-\x3-\x3\x10F\x2\x2.\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2"+
		"\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2"+
		"\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2"+
		"\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2"+
		"\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O"+
		"\x2)Q\x2*S\x2+U\x2,W\x2-Y\x2.\x3\x2\b\x3\x2\x33;\x3\x2\x32;\x3\x2$$\x5"+
		"\x2\x43\\\x61\x61\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\""+
		"\"\x124\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2"+
		"\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2"+
		"\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19"+
		"\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2"+
		"\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)"+
		"\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3"+
		"\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2"+
		"\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41"+
		"\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2"+
		"I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2"+
		"\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2"+
		"\x3\\\x3\x2\x2\x2\x5\x66\x3\x2\x2\x2\a~\x3\x2\x2\x2\t\x80\x3\x2\x2\x2"+
		"\v\x89\x3\x2\x2\x2\r\x8D\x3\x2\x2\x2\xF\x91\x3\x2\x2\x2\x11\x95\x3\x2"+
		"\x2\x2\x13\x98\x3\x2\x2\x2\x15\x9B\x3\x2\x2\x2\x17\x9E\x3\x2\x2\x2\x19"+
		"\xA6\x3\x2\x2\x2\x1B\xA9\x3\x2\x2\x2\x1D\xB0\x3\x2\x2\x2\x1F\xB5\x3\x2"+
		"\x2\x2!\xBC\x3\x2\x2\x2#\xC3\x3\x2\x2\x2%\xC9\x3\x2\x2\x2\'\xD3\x3\x2"+
		"\x2\x2)\xD5\x3\x2\x2\x2+\xD7\x3\x2\x2\x2-\xD9\x3\x2\x2\x2/\xDB\x3\x2\x2"+
		"\x2\x31\xDD\x3\x2\x2\x2\x33\xDF\x3\x2\x2\x2\x35\xE1\x3\x2\x2\x2\x37\xE3"+
		"\x3\x2\x2\x2\x39\xE5\x3\x2\x2\x2;\xE7\x3\x2\x2\x2=\xE9\x3\x2\x2\x2?\xEB"+
		"\x3\x2\x2\x2\x41\xED\x3\x2\x2\x2\x43\xEF\x3\x2\x2\x2\x45\xF1\x3\x2\x2"+
		"\x2G\xF3\x3\x2\x2\x2I\xF5\x3\x2\x2\x2K\xF8\x3\x2\x2\x2M\xFA\x3\x2\x2\x2"+
		"O\xFC\x3\x2\x2\x2Q\xFF\x3\x2\x2\x2S\x102\x3\x2\x2\x2U\x109\x3\x2\x2\x2"+
		"W\x116\x3\x2\x2\x2Y\x11A\x3\x2\x2\x2[]\a/\x2\x2\\[\x3\x2\x2\x2\\]\x3\x2"+
		"\x2\x2]^\x3\x2\x2\x2^\x62\t\x2\x2\x2_\x61\t\x3\x2\x2`_\x3\x2\x2\x2\x61"+
		"\x64\x3\x2\x2\x2\x62`\x3\x2\x2\x2\x62\x63\x3\x2\x2\x2\x63\x4\x3\x2\x2"+
		"\x2\x64\x62\x3\x2\x2\x2\x65g\a/\x2\x2\x66\x65\x3\x2\x2\x2\x66g\x3\x2\x2"+
		"\x2gk\x3\x2\x2\x2hj\t\x2\x2\x2ih\x3\x2\x2\x2jm\x3\x2\x2\x2ki\x3\x2\x2"+
		"\x2kl\x3\x2\x2\x2ln\x3\x2\x2\x2mk\x3\x2\x2\x2no\t\x3\x2\x2oq\a\x30\x2"+
		"\x2pr\t\x3\x2\x2qp\x3\x2\x2\x2rs\x3\x2\x2\x2sq\x3\x2\x2\x2st\x3\x2\x2"+
		"\x2t\x6\x3\x2\x2\x2uv\av\x2\x2vw\at\x2\x2wx\aw\x2\x2x\x7F\ag\x2\x2yz\a"+
		"h\x2\x2z{\a\x63\x2\x2{|\an\x2\x2|}\au\x2\x2}\x7F\ag\x2\x2~u\x3\x2\x2\x2"+
		"~y\x3\x2\x2\x2\x7F\b\x3\x2\x2\x2\x80\x84\a$\x2\x2\x81\x83\n\x4\x2\x2\x82"+
		"\x81\x3\x2\x2\x2\x83\x86\x3\x2\x2\x2\x84\x82\x3\x2\x2\x2\x84\x85\x3\x2"+
		"\x2\x2\x85\x87\x3\x2\x2\x2\x86\x84\x3\x2\x2\x2\x87\x88\a$\x2\x2\x88\n"+
		"\x3\x2\x2\x2\x89\x8A\x5\x3\x2\x2\x8A\x8B\a\x31\x2\x2\x8B\x8C\x5\x3\x2"+
		"\x2\x8C\f\x3\x2\x2\x2\x8D\x8E\an\x2\x2\x8E\x8F\ag\x2\x2\x8F\x90\av\x2"+
		"\x2\x90\xE\x3\x2\x2\x2\x91\x92\ah\x2\x2\x92\x93\aq\x2\x2\x93\x94\at\x2"+
		"\x2\x94\x10\x3\x2\x2\x2\x95\x96\ak\x2\x2\x96\x97\ap\x2\x2\x97\x12\x3\x2"+
		"\x2\x2\x98\x99\ak\x2\x2\x99\x9A\ah\x2\x2\x9A\x14\x3\x2\x2\x2\x9B\x9C\a"+
		"v\x2\x2\x9C\x9D\aq\x2\x2\x9D\x16\x3\x2\x2\x2\x9E\x9F\an\x2\x2\x9F\xA0"+
		"\ak\x2\x2\xA0\xA1\a\x64\x2\x2\xA1\xA2\at\x2\x2\xA2\xA3\a\x63\x2\x2\xA3"+
		"\xA4\at\x2\x2\xA4\xA5\a{\x2\x2\xA5\x18\x3\x2\x2\x2\xA6\xA7\au\x2\x2\xA7"+
		"\xA8\ah\x2\x2\xA8\x1A\x3\x2\x2\x2\xA9\xAA\aq\x2\x2\xAA\xAB\a\x64\x2\x2"+
		"\xAB\xAC\al\x2\x2\xAC\xAD\ag\x2\x2\xAD\xAE\a\x65\x2\x2\xAE\xAF\av\x2\x2"+
		"\xAF\x1C\x3\x2\x2\x2\xB0\xB1\ag\x2\x2\xB1\xB2\an\x2\x2\xB2\xB3\au\x2\x2"+
		"\xB3\xB4\ag\x2\x2\xB4\x1E\x3\x2\x2\x2\xB5\xB6\ak\x2\x2\xB6\xB7\ao\x2\x2"+
		"\xB7\xB8\ar\x2\x2\xB8\xB9\aq\x2\x2\xB9\xBA\at\x2\x2\xBA\xBB\av\x2\x2\xBB"+
		" \x3\x2\x2\x2\xBC\xBD\at\x2\x2\xBD\xBE\ag\x2\x2\xBE\xBF\av\x2\x2\xBF\xC0"+
		"\aw\x2\x2\xC0\xC1\at\x2\x2\xC1\xC2\ap\x2\x2\xC2\"\x3\x2\x2\x2\xC3\xC4"+
		"\ar\x2\x2\xC4\xC5\at\x2\x2\xC5\xC6\ak\x2\x2\xC6\xC7\ap\x2\x2\xC7\xC8\a"+
		"v\x2\x2\xC8$\x3\x2\x2\x2\xC9\xCA\a\x66\x2\x2\xCA\xCB\ak\x2\x2\xCB\xCC"+
		"\au\x2\x2\xCC\xCD\at\x2\x2\xCD\xCE\ag\x2\x2\xCE\xCF\ai\x2\x2\xCF\xD0\a"+
		"\x63\x2\x2\xD0\xD1\at\x2\x2\xD1\xD2\a\x66\x2\x2\xD2&\x3\x2\x2\x2\xD3\xD4"+
		"\a=\x2\x2\xD4(\x3\x2\x2\x2\xD5\xD6\a?\x2\x2\xD6*\x3\x2\x2\x2\xD7\xD8\a"+
		".\x2\x2\xD8,\x3\x2\x2\x2\xD9\xDA\a}\x2\x2\xDA.\x3\x2\x2\x2\xDB\xDC\a\x7F"+
		"\x2\x2\xDC\x30\x3\x2\x2\x2\xDD\xDE\a]\x2\x2\xDE\x32\x3\x2\x2\x2\xDF\xE0"+
		"\a_\x2\x2\xE0\x34\x3\x2\x2\x2\xE1\xE2\a*\x2\x2\xE2\x36\x3\x2\x2\x2\xE3"+
		"\xE4\a+\x2\x2\xE4\x38\x3\x2\x2\x2\xE5\xE6\a-\x2\x2\xE6:\x3\x2\x2\x2\xE7"+
		"\xE8\a/\x2\x2\xE8<\x3\x2\x2\x2\xE9\xEA\a,\x2\x2\xEA>\x3\x2\x2\x2\xEB\xEC"+
		"\a\x31\x2\x2\xEC@\x3\x2\x2\x2\xED\xEE\a`\x2\x2\xEE\x42\x3\x2\x2\x2\xEF"+
		"\xF0\a\x30\x2\x2\xF0\x44\x3\x2\x2\x2\xF1\xF2\a~\x2\x2\xF2\x46\x3\x2\x2"+
		"\x2\xF3\xF4\a(\x2\x2\xF4H\x3\x2\x2\x2\xF5\xF6\a?\x2\x2\xF6\xF7\a?\x2\x2"+
		"\xF7J\x3\x2\x2\x2\xF8\xF9\a@\x2\x2\xF9L\x3\x2\x2\x2\xFA\xFB\a>\x2\x2\xFB"+
		"N\x3\x2\x2\x2\xFC\xFD\a@\x2\x2\xFD\xFE\a?\x2\x2\xFEP\x3\x2\x2\x2\xFF\x100"+
		"\a>\x2\x2\x100\x101\a?\x2\x2\x101R\x3\x2\x2\x2\x102\x106\t\x5\x2\x2\x103"+
		"\x105\t\x6\x2\x2\x104\x103\x3\x2\x2\x2\x105\x108\x3\x2\x2\x2\x106\x104"+
		"\x3\x2\x2\x2\x106\x107\x3\x2\x2\x2\x107T\x3\x2\x2\x2\x108\x106\x3\x2\x2"+
		"\x2\x109\x10A\a\x31\x2\x2\x10A\x10B\a\x31\x2\x2\x10B\x10F\x3\x2\x2\x2"+
		"\x10C\x10E\v\x2\x2\x2\x10D\x10C\x3\x2\x2\x2\x10E\x111\x3\x2\x2\x2\x10F"+
		"\x110\x3\x2\x2\x2\x10F\x10D\x3\x2\x2\x2\x110\x112\x3\x2\x2\x2\x111\x10F"+
		"\x3\x2\x2\x2\x112\x113\a\f\x2\x2\x113\x114\x3\x2\x2\x2\x114\x115\b+\x2"+
		"\x2\x115V\x3\x2\x2\x2\x116\x117\t\a\x2\x2\x117\x118\x3\x2\x2\x2\x118\x119"+
		"\b,\x2\x2\x119X\x3\x2\x2\x2\x11A\x11B\v\x2\x2\x2\x11BZ\x3\x2\x2\x2\f\x2"+
		"\\\x62\x66ks~\x84\x106\x10F\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Algo
