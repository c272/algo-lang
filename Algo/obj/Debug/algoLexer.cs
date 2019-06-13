﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Z:\Algo\Algo\algo.g4 by ANTLR 4.6.6

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
		INTEGER=1, FLOAT=2, BOOLEAN=3, STRING=4, LET_SYM=5, FOR_SYM=6, IN_SYM=7, 
		IF_SYM=8, IMPORT_SYM=9, ENDLINE=10, EQUALS=11, COMMA=12, LBRACKET=13, 
		RBRACKET=14, ADD_OP=15, TAKE_OP=16, MUL_OP=17, DIV_OP=18, POW_OP=19, BIN_OR=20, 
		BIN_AND=21, LBRACE=22, RBRACE=23, IDENTIFIER=24, COMMENT=25, WS=26, UNKNOWN_SYMBOL=27;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INTEGER", "FLOAT", "BOOLEAN", "STRING", "LET_SYM", "FOR_SYM", "IN_SYM", 
		"IF_SYM", "IMPORT_SYM", "ENDLINE", "EQUALS", "COMMA", "LBRACKET", "RBRACKET", 
		"ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "BIN_OR", "BIN_AND", 
		"LBRACE", "RBRACE", "IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
	};


	public algoLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, "'let'", "'for'", "'in'", "'if'", "'import'", 
		"';'", "'='", "','", "'('", "')'", "'+'", "'-'", "'*'", "'/'", "'^'", 
		"'||'", "'&&'", "'{'", "'}'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INTEGER", "FLOAT", "BOOLEAN", "STRING", "LET_SYM", "FOR_SYM", "IN_SYM", 
		"IF_SYM", "IMPORT_SYM", "ENDLINE", "EQUALS", "COMMA", "LBRACKET", "RBRACKET", 
		"ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "BIN_OR", "BIN_AND", 
		"LBRACE", "RBRACE", "IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x1D\xB5\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x3\x2\x5\x2;\n\x2\x3\x2\a\x2>\n\x2\f\x2\xE"+
		"\x2\x41\v\x2\x3\x2\x3\x2\x3\x3\x5\x3\x46\n\x3\x3\x3\a\x3I\n\x3\f\x3\xE"+
		"\x3L\v\x3\x3\x3\x3\x3\x3\x3\x6\x3Q\n\x3\r\x3\xE\x3R\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x5\x4^\n\x4\x3\x5\x3\x5\a\x5\x62"+
		"\n\x5\f\x5\xE\x5\x65\v\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3"+
		"\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3"+
		"\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3"+
		"\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3\x14\x3\x14\x3\x15\x3"+
		"\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3"+
		"\x19\a\x19\x9E\n\x19\f\x19\xE\x19\xA1\v\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A"+
		"\a\x1A\xA7\n\x1A\f\x1A\xE\x1A\xAA\v\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3"+
		"\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\xA8\x2\x2\x1D\x3\x2\x3\x5\x2"+
		"\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17"+
		"\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14"+
		"\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2"+
		"\x1C\x37\x2\x1D\x3\x2\b\x3\x2\x33;\x3\x2\x32;\x3\x2$$\x5\x2\x43\\\x61"+
		"\x61\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\"\"\xBD\x2\x3"+
		"\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v"+
		"\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2"+
		"\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2"+
		"\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2"+
		"\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2"+
		"\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2"+
		"\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x3:\x3\x2\x2\x2"+
		"\x5\x45\x3\x2\x2\x2\a]\x3\x2\x2\x2\t_\x3\x2\x2\x2\vh\x3\x2\x2\x2\rl\x3"+
		"\x2\x2\x2\xFp\x3\x2\x2\x2\x11s\x3\x2\x2\x2\x13v\x3\x2\x2\x2\x15}\x3\x2"+
		"\x2\x2\x17\x7F\x3\x2\x2\x2\x19\x81\x3\x2\x2\x2\x1B\x83\x3\x2\x2\x2\x1D"+
		"\x85\x3\x2\x2\x2\x1F\x87\x3\x2\x2\x2!\x89\x3\x2\x2\x2#\x8B\x3\x2\x2\x2"+
		"%\x8D\x3\x2\x2\x2\'\x8F\x3\x2\x2\x2)\x91\x3\x2\x2\x2+\x94\x3\x2\x2\x2"+
		"-\x97\x3\x2\x2\x2/\x99\x3\x2\x2\x2\x31\x9B\x3\x2\x2\x2\x33\xA2\x3\x2\x2"+
		"\x2\x35\xAF\x3\x2\x2\x2\x37\xB3\x3\x2\x2\x2\x39;\a/\x2\x2:\x39\x3\x2\x2"+
		"\x2:;\x3\x2\x2\x2;?\x3\x2\x2\x2<>\t\x2\x2\x2=<\x3\x2\x2\x2>\x41\x3\x2"+
		"\x2\x2?=\x3\x2\x2\x2?@\x3\x2\x2\x2@\x42\x3\x2\x2\x2\x41?\x3\x2\x2\x2\x42"+
		"\x43\t\x3\x2\x2\x43\x4\x3\x2\x2\x2\x44\x46\a/\x2\x2\x45\x44\x3\x2\x2\x2"+
		"\x45\x46\x3\x2\x2\x2\x46J\x3\x2\x2\x2GI\t\x2\x2\x2HG\x3\x2\x2\x2IL\x3"+
		"\x2\x2\x2JH\x3\x2\x2\x2JK\x3\x2\x2\x2KM\x3\x2\x2\x2LJ\x3\x2\x2\x2MN\t"+
		"\x3\x2\x2NP\a\x30\x2\x2OQ\t\x3\x2\x2PO\x3\x2\x2\x2QR\x3\x2\x2\x2RP\x3"+
		"\x2\x2\x2RS\x3\x2\x2\x2S\x6\x3\x2\x2\x2TU\av\x2\x2UV\at\x2\x2VW\aw\x2"+
		"\x2W^\ag\x2\x2XY\ah\x2\x2YZ\a\x63\x2\x2Z[\an\x2\x2[\\\au\x2\x2\\^\ag\x2"+
		"\x2]T\x3\x2\x2\x2]X\x3\x2\x2\x2^\b\x3\x2\x2\x2_\x63\a$\x2\x2`\x62\n\x4"+
		"\x2\x2\x61`\x3\x2\x2\x2\x62\x65\x3\x2\x2\x2\x63\x61\x3\x2\x2\x2\x63\x64"+
		"\x3\x2\x2\x2\x64\x66\x3\x2\x2\x2\x65\x63\x3\x2\x2\x2\x66g\a$\x2\x2g\n"+
		"\x3\x2\x2\x2hi\an\x2\x2ij\ag\x2\x2jk\av\x2\x2k\f\x3\x2\x2\x2lm\ah\x2\x2"+
		"mn\aq\x2\x2no\at\x2\x2o\xE\x3\x2\x2\x2pq\ak\x2\x2qr\ap\x2\x2r\x10\x3\x2"+
		"\x2\x2st\ak\x2\x2tu\ah\x2\x2u\x12\x3\x2\x2\x2vw\ak\x2\x2wx\ao\x2\x2xy"+
		"\ar\x2\x2yz\aq\x2\x2z{\at\x2\x2{|\av\x2\x2|\x14\x3\x2\x2\x2}~\a=\x2\x2"+
		"~\x16\x3\x2\x2\x2\x7F\x80\a?\x2\x2\x80\x18\x3\x2\x2\x2\x81\x82\a.\x2\x2"+
		"\x82\x1A\x3\x2\x2\x2\x83\x84\a*\x2\x2\x84\x1C\x3\x2\x2\x2\x85\x86\a+\x2"+
		"\x2\x86\x1E\x3\x2\x2\x2\x87\x88\a-\x2\x2\x88 \x3\x2\x2\x2\x89\x8A\a/\x2"+
		"\x2\x8A\"\x3\x2\x2\x2\x8B\x8C\a,\x2\x2\x8C$\x3\x2\x2\x2\x8D\x8E\a\x31"+
		"\x2\x2\x8E&\x3\x2\x2\x2\x8F\x90\a`\x2\x2\x90(\x3\x2\x2\x2\x91\x92\a~\x2"+
		"\x2\x92\x93\a~\x2\x2\x93*\x3\x2\x2\x2\x94\x95\a(\x2\x2\x95\x96\a(\x2\x2"+
		"\x96,\x3\x2\x2\x2\x97\x98\a}\x2\x2\x98.\x3\x2\x2\x2\x99\x9A\a\x7F\x2\x2"+
		"\x9A\x30\x3\x2\x2\x2\x9B\x9F\t\x5\x2\x2\x9C\x9E\t\x6\x2\x2\x9D\x9C\x3"+
		"\x2\x2\x2\x9E\xA1\x3\x2\x2\x2\x9F\x9D\x3\x2\x2\x2\x9F\xA0\x3\x2\x2\x2"+
		"\xA0\x32\x3\x2\x2\x2\xA1\x9F\x3\x2\x2\x2\xA2\xA3\a\x31\x2\x2\xA3\xA4\a"+
		"\x31\x2\x2\xA4\xA8\x3\x2\x2\x2\xA5\xA7\v\x2\x2\x2\xA6\xA5\x3\x2\x2\x2"+
		"\xA7\xAA\x3\x2\x2\x2\xA8\xA9\x3\x2\x2\x2\xA8\xA6\x3\x2\x2\x2\xA9\xAB\x3"+
		"\x2\x2\x2\xAA\xA8\x3\x2\x2\x2\xAB\xAC\a\f\x2\x2\xAC\xAD\x3\x2\x2\x2\xAD"+
		"\xAE\b\x1A\x2\x2\xAE\x34\x3\x2\x2\x2\xAF\xB0\t\a\x2\x2\xB0\xB1\x3\x2\x2"+
		"\x2\xB1\xB2\b\x1B\x2\x2\xB2\x36\x3\x2\x2\x2\xB3\xB4\v\x2\x2\x2\xB4\x38"+
		"\x3\x2\x2\x2\f\x2:?\x45JR]\x63\x9F\xA8\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Algo
