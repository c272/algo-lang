//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\Files\Programming\GitHub\algo\Algo\Parsing\algo.g4 by ANTLR 4.6.6

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
		INTEGER=1, FLOAT=2, BOOLEAN=3, STRING=4, RATIONAL=5, NULL=6, LET_SYM=7, 
		FOR_SYM=8, WHILE_SYM=9, IN_SYM=10, IF_SYM=11, UP_SYM=12, TO_SYM=13, AS_SYM=14, 
		LIB_SYM=15, SIG_FIG_SYM=16, OBJ_SYM=17, ELSE_SYM=18, IMPORT_SYM=19, RETURN_SYM=20, 
		PRINT_SYM=21, DISREGARD_SYM=22, EXTERNAL_SYM=23, ENDLINE=24, EQUALS=25, 
		COMMA=26, LBRACE=27, RBRACE=28, LSQBR=29, RSQBR=30, INVERT_SYM=31, LBRACKET=32, 
		RBRACKET=33, ADD_OP=34, TAKE_OP=35, MUL_OP=36, DIV_OP=37, POW_OP=38, POINT=39, 
		ADDFROM_OP=40, TAKEFROM_OP=41, DIVFROM_OP=42, MULFROM_OP=43, ADD_PFOP=44, 
		TAKE_PFOP=45, BIN_OR=46, BIN_AND=47, BIN_NET=48, BIN_EQUALS=49, GRTR_THAN=50, 
		LESS_THAN=51, GRTR_THAN_ET=52, LESS_THAN_ET=53, IDENTIFIER=54, COMMENT=55, 
		WS=56, UNKNOWN_SYMBOL=57;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "NULL", "LET_SYM", 
		"FOR_SYM", "WHILE_SYM", "IN_SYM", "IF_SYM", "UP_SYM", "TO_SYM", "AS_SYM", 
		"LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", "ELSE_SYM", "IMPORT_SYM", "RETURN_SYM", 
		"PRINT_SYM", "DISREGARD_SYM", "EXTERNAL_SYM", "ENDLINE", "EQUALS", "COMMA", 
		"LBRACE", "RBRACE", "LSQBR", "RSQBR", "INVERT_SYM", "LBRACKET", "RBRACKET", 
		"ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "POINT", "ADDFROM_OP", 
		"TAKEFROM_OP", "DIVFROM_OP", "MULFROM_OP", "ADD_PFOP", "TAKE_PFOP", "BIN_OR", 
		"BIN_AND", "BIN_NET", "BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", 
		"LESS_THAN_ET", "IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
	};


	public algoLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, "'null'", "'let'", "'for'", "'while'", 
		"'in'", "'if'", "'up'", "'to'", "'as'", "'library'", "'sf'", "'object'", 
		"'else'", "'import'", "'return'", "'print'", "'disregard'", "'external'", 
		"';'", "'='", "','", "'{'", "'}'", "'['", "']'", "'!'", "'('", "')'", 
		"'+'", "'-'", "'*'", "'/'", "'^'", "'.'", "'+='", "'-='", "'/='", "'*='", 
		"'++'", "'--'", "'|'", "'&'", "'!='", "'=='", "'>'", "'<'", "'>='", "'<='"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "NULL", "LET_SYM", 
		"FOR_SYM", "WHILE_SYM", "IN_SYM", "IF_SYM", "UP_SYM", "TO_SYM", "AS_SYM", 
		"LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", "ELSE_SYM", "IMPORT_SYM", "RETURN_SYM", 
		"PRINT_SYM", "DISREGARD_SYM", "EXTERNAL_SYM", "ENDLINE", "EQUALS", "COMMA", 
		"LBRACE", "RBRACE", "LSQBR", "RSQBR", "INVERT_SYM", "LBRACKET", "RBRACKET", 
		"ADD_OP", "TAKE_OP", "MUL_OP", "DIV_OP", "POW_OP", "POINT", "ADDFROM_OP", 
		"TAKEFROM_OP", "DIVFROM_OP", "MULFROM_OP", "ADD_PFOP", "TAKE_PFOP", "BIN_OR", 
		"BIN_AND", "BIN_NET", "BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", 
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2;\x176\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31\x4\x32"+
		"\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37"+
		"\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x3\x2\x5\x2w\n\x2\x3\x2\x3\x2\a\x2{"+
		"\n\x2\f\x2\xE\x2~\v\x2\x3\x2\x5\x2\x81\n\x2\x3\x3\x5\x3\x84\n\x3\x3\x3"+
		"\a\x3\x87\n\x3\f\x3\xE\x3\x8A\v\x3\x3\x3\x3\x3\x3\x3\x6\x3\x8F\n\x3\r"+
		"\x3\xE\x3\x90\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x5"+
		"\x4\x9C\n\x4\x3\x5\x3\x5\a\x5\xA0\n\x5\f\x5\xE\x5\xA3\v\x5\x3\x5\x3\x5"+
		"\x3\x6\x3\x6\a\x6\xA9\n\x6\f\x6\xE\x6\xAC\v\x6\x3\x6\x3\x6\a\x6\xB0\n"+
		"\x6\f\x6\xE\x6\xB3\v\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b"+
		"\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v"+
		"\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF"+
		"\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11"+
		"\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17"+
		"\x3\x17\x3\x17\x3\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18"+
		"\x3\x18\x3\x18\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C"+
		"\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3\"\x3\"\x3"+
		"#\x3#\x3$\x3$\x3%\x3%\x3&\x3&\x3\'\x3\'\x3(\x3(\x3)\x3)\x3)\x3*\x3*\x3"+
		"*\x3+\x3+\x3+\x3,\x3,\x3,\x3-\x3-\x3-\x3.\x3.\x3.\x3/\x3/\x3\x30\x3\x30"+
		"\x3\x31\x3\x31\x3\x31\x3\x32\x3\x32\x3\x32\x3\x33\x3\x33\x3\x34\x3\x34"+
		"\x3\x35\x3\x35\x3\x35\x3\x36\x3\x36\x3\x36\x3\x37\x3\x37\a\x37\x15F\n"+
		"\x37\f\x37\xE\x37\x162\v\x37\x3\x38\x3\x38\x3\x38\x3\x38\a\x38\x168\n"+
		"\x38\f\x38\xE\x38\x16B\v\x38\x3\x38\x3\x38\x3\x38\x3\x38\x3\x39\x3\x39"+
		"\x3\x39\x3\x39\x3:\x3:\x3\x169\x2\x2;\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6"+
		"\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B"+
		"\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16"+
		"+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39"+
		"\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2"+
		"(O\x2)Q\x2*S\x2+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2"+
		"\x33\x65\x2\x34g\x2\x35i\x2\x36k\x2\x37m\x2\x38o\x2\x39q\x2:s\x2;\x3\x2"+
		"\t\x3\x2\x33;\x3\x2\x32;\x3\x2$$\x3\x2\"\"\x5\x2\x43\\\x61\x61\x63|\x6"+
		"\x2\x32;\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\"\"\x181\x2\x3\x3\x2\x2\x2"+
		"\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2"+
		"\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2"+
		"\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3"+
		"\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3"+
		"\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2"+
		"\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2"+
		"\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2"+
		"\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2"+
		"\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2"+
		"\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2"+
		"U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2"+
		"\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3"+
		"\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2"+
		"\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2s\x3\x2\x2\x2\x3\x80\x3\x2\x2\x2"+
		"\x5\x83\x3\x2\x2\x2\a\x9B\x3\x2\x2\x2\t\x9D\x3\x2\x2\x2\v\xA6\x3\x2\x2"+
		"\x2\r\xB6\x3\x2\x2\x2\xF\xBB\x3\x2\x2\x2\x11\xBF\x3\x2\x2\x2\x13\xC3\x3"+
		"\x2\x2\x2\x15\xC9\x3\x2\x2\x2\x17\xCC\x3\x2\x2\x2\x19\xCF\x3\x2\x2\x2"+
		"\x1B\xD2\x3\x2\x2\x2\x1D\xD5\x3\x2\x2\x2\x1F\xD8\x3\x2\x2\x2!\xE0\x3\x2"+
		"\x2\x2#\xE3\x3\x2\x2\x2%\xEA\x3\x2\x2\x2\'\xEF\x3\x2\x2\x2)\xF6\x3\x2"+
		"\x2\x2+\xFD\x3\x2\x2\x2-\x103\x3\x2\x2\x2/\x10D\x3\x2\x2\x2\x31\x116\x3"+
		"\x2\x2\x2\x33\x118\x3\x2\x2\x2\x35\x11A\x3\x2\x2\x2\x37\x11C\x3\x2\x2"+
		"\x2\x39\x11E\x3\x2\x2\x2;\x120\x3\x2\x2\x2=\x122\x3\x2\x2\x2?\x124\x3"+
		"\x2\x2\x2\x41\x126\x3\x2\x2\x2\x43\x128\x3\x2\x2\x2\x45\x12A\x3\x2\x2"+
		"\x2G\x12C\x3\x2\x2\x2I\x12E\x3\x2\x2\x2K\x130\x3\x2\x2\x2M\x132\x3\x2"+
		"\x2\x2O\x134\x3\x2\x2\x2Q\x136\x3\x2\x2\x2S\x139\x3\x2\x2\x2U\x13C\x3"+
		"\x2\x2\x2W\x13F\x3\x2\x2\x2Y\x142\x3\x2\x2\x2[\x145\x3\x2\x2\x2]\x148"+
		"\x3\x2\x2\x2_\x14A\x3\x2\x2\x2\x61\x14C\x3\x2\x2\x2\x63\x14F\x3\x2\x2"+
		"\x2\x65\x152\x3\x2\x2\x2g\x154\x3\x2\x2\x2i\x156\x3\x2\x2\x2k\x159\x3"+
		"\x2\x2\x2m\x15C\x3\x2\x2\x2o\x163\x3\x2\x2\x2q\x170\x3\x2\x2\x2s\x174"+
		"\x3\x2\x2\x2uw\a/\x2\x2vu\x3\x2\x2\x2vw\x3\x2\x2\x2wx\x3\x2\x2\x2x|\t"+
		"\x2\x2\x2y{\t\x3\x2\x2zy\x3\x2\x2\x2{~\x3\x2\x2\x2|z\x3\x2\x2\x2|}\x3"+
		"\x2\x2\x2}\x81\x3\x2\x2\x2~|\x3\x2\x2\x2\x7F\x81\a\x32\x2\x2\x80v\x3\x2"+
		"\x2\x2\x80\x7F\x3\x2\x2\x2\x81\x4\x3\x2\x2\x2\x82\x84\a/\x2\x2\x83\x82"+
		"\x3\x2\x2\x2\x83\x84\x3\x2\x2\x2\x84\x88\x3\x2\x2\x2\x85\x87\t\x2\x2\x2"+
		"\x86\x85\x3\x2\x2\x2\x87\x8A\x3\x2\x2\x2\x88\x86\x3\x2\x2\x2\x88\x89\x3"+
		"\x2\x2\x2\x89\x8B\x3\x2\x2\x2\x8A\x88\x3\x2\x2\x2\x8B\x8C\t\x3\x2\x2\x8C"+
		"\x8E\a\x30\x2\x2\x8D\x8F\t\x3\x2\x2\x8E\x8D\x3\x2\x2\x2\x8F\x90\x3\x2"+
		"\x2\x2\x90\x8E\x3\x2\x2\x2\x90\x91\x3\x2\x2\x2\x91\x6\x3\x2\x2\x2\x92"+
		"\x93\av\x2\x2\x93\x94\at\x2\x2\x94\x95\aw\x2\x2\x95\x9C\ag\x2\x2\x96\x97"+
		"\ah\x2\x2\x97\x98\a\x63\x2\x2\x98\x99\an\x2\x2\x99\x9A\au\x2\x2\x9A\x9C"+
		"\ag\x2\x2\x9B\x92\x3\x2\x2\x2\x9B\x96\x3\x2\x2\x2\x9C\b\x3\x2\x2\x2\x9D"+
		"\xA1\a$\x2\x2\x9E\xA0\n\x4\x2\x2\x9F\x9E\x3\x2\x2\x2\xA0\xA3\x3\x2\x2"+
		"\x2\xA1\x9F\x3\x2\x2\x2\xA1\xA2\x3\x2\x2\x2\xA2\xA4\x3\x2\x2\x2\xA3\xA1"+
		"\x3\x2\x2\x2\xA4\xA5\a$\x2\x2\xA5\n\x3\x2\x2\x2\xA6\xAA\x5\x3\x2\x2\xA7"+
		"\xA9\t\x5\x2\x2\xA8\xA7\x3\x2\x2\x2\xA9\xAC\x3\x2\x2\x2\xAA\xA8\x3\x2"+
		"\x2\x2\xAA\xAB\x3\x2\x2\x2\xAB\xAD\x3\x2\x2\x2\xAC\xAA\x3\x2\x2\x2\xAD"+
		"\xB1\x5K&\x2\xAE\xB0\t\x5\x2\x2\xAF\xAE\x3\x2\x2\x2\xB0\xB3\x3\x2\x2\x2"+
		"\xB1\xAF\x3\x2\x2\x2\xB1\xB2\x3\x2\x2\x2\xB2\xB4\x3\x2\x2\x2\xB3\xB1\x3"+
		"\x2\x2\x2\xB4\xB5\x5\x3\x2\x2\xB5\f\x3\x2\x2\x2\xB6\xB7\ap\x2\x2\xB7\xB8"+
		"\aw\x2\x2\xB8\xB9\an\x2\x2\xB9\xBA\an\x2\x2\xBA\xE\x3\x2\x2\x2\xBB\xBC"+
		"\an\x2\x2\xBC\xBD\ag\x2\x2\xBD\xBE\av\x2\x2\xBE\x10\x3\x2\x2\x2\xBF\xC0"+
		"\ah\x2\x2\xC0\xC1\aq\x2\x2\xC1\xC2\at\x2\x2\xC2\x12\x3\x2\x2\x2\xC3\xC4"+
		"\ay\x2\x2\xC4\xC5\aj\x2\x2\xC5\xC6\ak\x2\x2\xC6\xC7\an\x2\x2\xC7\xC8\a"+
		"g\x2\x2\xC8\x14\x3\x2\x2\x2\xC9\xCA\ak\x2\x2\xCA\xCB\ap\x2\x2\xCB\x16"+
		"\x3\x2\x2\x2\xCC\xCD\ak\x2\x2\xCD\xCE\ah\x2\x2\xCE\x18\x3\x2\x2\x2\xCF"+
		"\xD0\aw\x2\x2\xD0\xD1\ar\x2\x2\xD1\x1A\x3\x2\x2\x2\xD2\xD3\av\x2\x2\xD3"+
		"\xD4\aq\x2\x2\xD4\x1C\x3\x2\x2\x2\xD5\xD6\a\x63\x2\x2\xD6\xD7\au\x2\x2"+
		"\xD7\x1E\x3\x2\x2\x2\xD8\xD9\an\x2\x2\xD9\xDA\ak\x2\x2\xDA\xDB\a\x64\x2"+
		"\x2\xDB\xDC\at\x2\x2\xDC\xDD\a\x63\x2\x2\xDD\xDE\at\x2\x2\xDE\xDF\a{\x2"+
		"\x2\xDF \x3\x2\x2\x2\xE0\xE1\au\x2\x2\xE1\xE2\ah\x2\x2\xE2\"\x3\x2\x2"+
		"\x2\xE3\xE4\aq\x2\x2\xE4\xE5\a\x64\x2\x2\xE5\xE6\al\x2\x2\xE6\xE7\ag\x2"+
		"\x2\xE7\xE8\a\x65\x2\x2\xE8\xE9\av\x2\x2\xE9$\x3\x2\x2\x2\xEA\xEB\ag\x2"+
		"\x2\xEB\xEC\an\x2\x2\xEC\xED\au\x2\x2\xED\xEE\ag\x2\x2\xEE&\x3\x2\x2\x2"+
		"\xEF\xF0\ak\x2\x2\xF0\xF1\ao\x2\x2\xF1\xF2\ar\x2\x2\xF2\xF3\aq\x2\x2\xF3"+
		"\xF4\at\x2\x2\xF4\xF5\av\x2\x2\xF5(\x3\x2\x2\x2\xF6\xF7\at\x2\x2\xF7\xF8"+
		"\ag\x2\x2\xF8\xF9\av\x2\x2\xF9\xFA\aw\x2\x2\xFA\xFB\at\x2\x2\xFB\xFC\a"+
		"p\x2\x2\xFC*\x3\x2\x2\x2\xFD\xFE\ar\x2\x2\xFE\xFF\at\x2\x2\xFF\x100\a"+
		"k\x2\x2\x100\x101\ap\x2\x2\x101\x102\av\x2\x2\x102,\x3\x2\x2\x2\x103\x104"+
		"\a\x66\x2\x2\x104\x105\ak\x2\x2\x105\x106\au\x2\x2\x106\x107\at\x2\x2"+
		"\x107\x108\ag\x2\x2\x108\x109\ai\x2\x2\x109\x10A\a\x63\x2\x2\x10A\x10B"+
		"\at\x2\x2\x10B\x10C\a\x66\x2\x2\x10C.\x3\x2\x2\x2\x10D\x10E\ag\x2\x2\x10E"+
		"\x10F\az\x2\x2\x10F\x110\av\x2\x2\x110\x111\ag\x2\x2\x111\x112\at\x2\x2"+
		"\x112\x113\ap\x2\x2\x113\x114\a\x63\x2\x2\x114\x115\an\x2\x2\x115\x30"+
		"\x3\x2\x2\x2\x116\x117\a=\x2\x2\x117\x32\x3\x2\x2\x2\x118\x119\a?\x2\x2"+
		"\x119\x34\x3\x2\x2\x2\x11A\x11B\a.\x2\x2\x11B\x36\x3\x2\x2\x2\x11C\x11D"+
		"\a}\x2\x2\x11D\x38\x3\x2\x2\x2\x11E\x11F\a\x7F\x2\x2\x11F:\x3\x2\x2\x2"+
		"\x120\x121\a]\x2\x2\x121<\x3\x2\x2\x2\x122\x123\a_\x2\x2\x123>\x3\x2\x2"+
		"\x2\x124\x125\a#\x2\x2\x125@\x3\x2\x2\x2\x126\x127\a*\x2\x2\x127\x42\x3"+
		"\x2\x2\x2\x128\x129\a+\x2\x2\x129\x44\x3\x2\x2\x2\x12A\x12B\a-\x2\x2\x12B"+
		"\x46\x3\x2\x2\x2\x12C\x12D\a/\x2\x2\x12DH\x3\x2\x2\x2\x12E\x12F\a,\x2"+
		"\x2\x12FJ\x3\x2\x2\x2\x130\x131\a\x31\x2\x2\x131L\x3\x2\x2\x2\x132\x133"+
		"\a`\x2\x2\x133N\x3\x2\x2\x2\x134\x135\a\x30\x2\x2\x135P\x3\x2\x2\x2\x136"+
		"\x137\a-\x2\x2\x137\x138\a?\x2\x2\x138R\x3\x2\x2\x2\x139\x13A\a/\x2\x2"+
		"\x13A\x13B\a?\x2\x2\x13BT\x3\x2\x2\x2\x13C\x13D\a\x31\x2\x2\x13D\x13E"+
		"\a?\x2\x2\x13EV\x3\x2\x2\x2\x13F\x140\a,\x2\x2\x140\x141\a?\x2\x2\x141"+
		"X\x3\x2\x2\x2\x142\x143\a-\x2\x2\x143\x144\a-\x2\x2\x144Z\x3\x2\x2\x2"+
		"\x145\x146\a/\x2\x2\x146\x147\a/\x2\x2\x147\\\x3\x2\x2\x2\x148\x149\a"+
		"~\x2\x2\x149^\x3\x2\x2\x2\x14A\x14B\a(\x2\x2\x14B`\x3\x2\x2\x2\x14C\x14D"+
		"\a#\x2\x2\x14D\x14E\a?\x2\x2\x14E\x62\x3\x2\x2\x2\x14F\x150\a?\x2\x2\x150"+
		"\x151\a?\x2\x2\x151\x64\x3\x2\x2\x2\x152\x153\a@\x2\x2\x153\x66\x3\x2"+
		"\x2\x2\x154\x155\a>\x2\x2\x155h\x3\x2\x2\x2\x156\x157\a@\x2\x2\x157\x158"+
		"\a?\x2\x2\x158j\x3\x2\x2\x2\x159\x15A\a>\x2\x2\x15A\x15B\a?\x2\x2\x15B"+
		"l\x3\x2\x2\x2\x15C\x160\t\x6\x2\x2\x15D\x15F\t\a\x2\x2\x15E\x15D\x3\x2"+
		"\x2\x2\x15F\x162\x3\x2\x2\x2\x160\x15E\x3\x2\x2\x2\x160\x161\x3\x2\x2"+
		"\x2\x161n\x3\x2\x2\x2\x162\x160\x3\x2\x2\x2\x163\x164\a\x31\x2\x2\x164"+
		"\x165\a\x31\x2\x2\x165\x169\x3\x2\x2\x2\x166\x168\v\x2\x2\x2\x167\x166"+
		"\x3\x2\x2\x2\x168\x16B\x3\x2\x2\x2\x169\x16A\x3\x2\x2\x2\x169\x167\x3"+
		"\x2\x2\x2\x16A\x16C\x3\x2\x2\x2\x16B\x169\x3\x2\x2\x2\x16C\x16D\a\f\x2"+
		"\x2\x16D\x16E\x3\x2\x2\x2\x16E\x16F\b\x38\x2\x2\x16Fp\x3\x2\x2\x2\x170"+
		"\x171\t\b\x2\x2\x171\x172\x3\x2\x2\x2\x172\x173\b\x39\x2\x2\x173r\x3\x2"+
		"\x2\x2\x174\x175\v\x2\x2\x2\x175t\x3\x2\x2\x2\xF\x2v|\x80\x83\x88\x90"+
		"\x9B\xA1\xAA\xB1\x160\x169\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Algo
