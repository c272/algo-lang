//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Larry\Files\Programming\c272\algo-lang\Algo\Parsing\algo.g4 by ANTLR 4.6.6

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
		INTEGER=1, FLOAT=2, BOOLEAN=3, STRING=4, RATIONAL=5, HEX=6, NULL=7, LET_SYM=8, 
		FOR_SYM=9, FOREACH_SYM=10, ADD_SYM=11, BREAK_SYM=12, CONTINUE_SYM=13, 
		AT_SYM=14, REMOVE_SYM=15, FROM_SYM=16, WHILE_SYM=17, IN_SYM=18, IF_SYM=19, 
		UP_SYM=20, TO_SYM=21, AS_SYM=22, ENUM_SYM=23, LIB_SYM=24, SIG_FIG_SYM=25, 
		OBJ_SYM=26, ELSE_SYM=27, IMPORT_SYM=28, RETURN_SYM=29, PRINT_SYM=30, DISREGARD_SYM=31, 
		EXTERNAL_SYM=32, TRY_SYM=33, CATCH_SYM=34, THROW_SYM=35, FIRE_SYM=36, 
		ENDLINE=37, EQUALS=38, COMMA=39, LBRACE=40, RBRACE=41, LSQBR=42, RSQBR=43, 
		INVERT_SYM=44, STREAMING_SYM=45, STREAMING_SYM_RIGHT=46, LBRACKET=47, 
		RBRACKET=48, ADD_OP=49, TAKE_OP=50, MUL_OP=51, DIV_OP=52, POW_OP=53, MOD_OP=54, 
		POINT=55, ADDFROM_OP=56, TAKEFROM_OP=57, DIVFROM_OP=58, MULFROM_OP=59, 
		ADD_PFOP=60, TAKE_PFOP=61, BIN_OR=62, BIN_AND=63, BIN_NET=64, BIN_EQUALS=65, 
		GRTR_THAN=66, LESS_THAN=67, GRTR_THAN_ET=68, LESS_THAN_ET=69, IDENTIFIER=70, 
		COMMENT=71, WS=72, UNKNOWN_SYMBOL=73;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "HEX", "NULL", "LET_SYM", 
		"FOR_SYM", "FOREACH_SYM", "ADD_SYM", "BREAK_SYM", "CONTINUE_SYM", "AT_SYM", 
		"REMOVE_SYM", "FROM_SYM", "WHILE_SYM", "IN_SYM", "IF_SYM", "UP_SYM", "TO_SYM", 
		"AS_SYM", "ENUM_SYM", "LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", "ELSE_SYM", 
		"IMPORT_SYM", "RETURN_SYM", "PRINT_SYM", "DISREGARD_SYM", "EXTERNAL_SYM", 
		"TRY_SYM", "CATCH_SYM", "THROW_SYM", "FIRE_SYM", "ENDLINE", "EQUALS", 
		"COMMA", "LBRACE", "RBRACE", "LSQBR", "RSQBR", "INVERT_SYM", "STREAMING_SYM", 
		"STREAMING_SYM_RIGHT", "LBRACKET", "RBRACKET", "ADD_OP", "TAKE_OP", "MUL_OP", 
		"DIV_OP", "POW_OP", "MOD_OP", "POINT", "ADDFROM_OP", "TAKEFROM_OP", "DIVFROM_OP", 
		"MULFROM_OP", "ADD_PFOP", "TAKE_PFOP", "BIN_OR", "BIN_AND", "BIN_NET", 
		"BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", "LESS_THAN_ET", 
		"IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
	};


	public algoLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, "'null'", "'let'", "'for'", 
		"'foreach'", "'add'", "'break'", "'continue'", "'at'", "'remove'", "'from'", 
		"'while'", "'in'", "'if'", "'up'", "'to'", "'as'", "'enum'", "'library'", 
		"'sf'", "'object'", "'else'", "'import'", "'return'", "'print '", "'disregard'", 
		"'external'", "'try'", "'catch'", "'throw'", "'fire'", "';'", "'='", "','", 
		"'{'", "'}'", "'['", "']'", "'!'", "'<-'", "'->'", "'('", "')'", "'+'", 
		"'-'", "'*'", "'/'", "'^'", "'%'", "'.'", "'+='", "'-='", "'/='", "'*='", 
		"'++'", "'--'", "'|'", "'&'", "'!='", "'=='", "'>'", "'<'", "'>='", "'<='"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INTEGER", "FLOAT", "BOOLEAN", "STRING", "RATIONAL", "HEX", "NULL", 
		"LET_SYM", "FOR_SYM", "FOREACH_SYM", "ADD_SYM", "BREAK_SYM", "CONTINUE_SYM", 
		"AT_SYM", "REMOVE_SYM", "FROM_SYM", "WHILE_SYM", "IN_SYM", "IF_SYM", "UP_SYM", 
		"TO_SYM", "AS_SYM", "ENUM_SYM", "LIB_SYM", "SIG_FIG_SYM", "OBJ_SYM", "ELSE_SYM", 
		"IMPORT_SYM", "RETURN_SYM", "PRINT_SYM", "DISREGARD_SYM", "EXTERNAL_SYM", 
		"TRY_SYM", "CATCH_SYM", "THROW_SYM", "FIRE_SYM", "ENDLINE", "EQUALS", 
		"COMMA", "LBRACE", "RBRACE", "LSQBR", "RSQBR", "INVERT_SYM", "STREAMING_SYM", 
		"STREAMING_SYM_RIGHT", "LBRACKET", "RBRACKET", "ADD_OP", "TAKE_OP", "MUL_OP", 
		"DIV_OP", "POW_OP", "MOD_OP", "POINT", "ADDFROM_OP", "TAKEFROM_OP", "DIVFROM_OP", 
		"MULFROM_OP", "ADD_PFOP", "TAKE_PFOP", "BIN_OR", "BIN_AND", "BIN_NET", 
		"BIN_EQUALS", "GRTR_THAN", "LESS_THAN", "GRTR_THAN_ET", "LESS_THAN_ET", 
		"IDENTIFIER", "COMMENT", "WS", "UNKNOWN_SYMBOL"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2K\x1ED\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31\x4\x32"+
		"\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37"+
		"\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x4;\t;\x4<\t<\x4=\t=\x4>\t>\x4?\t?\x4"+
		"@\t@\x4\x41\t\x41\x4\x42\t\x42\x4\x43\t\x43\x4\x44\t\x44\x4\x45\t\x45"+
		"\x4\x46\t\x46\x4G\tG\x4H\tH\x4I\tI\x4J\tJ\x3\x2\x5\x2\x97\n\x2\x3\x2\x3"+
		"\x2\a\x2\x9B\n\x2\f\x2\xE\x2\x9E\v\x2\x3\x2\x5\x2\xA1\n\x2\x3\x3\x5\x3"+
		"\xA4\n\x3\x3\x3\a\x3\xA7\n\x3\f\x3\xE\x3\xAA\v\x3\x3\x3\x3\x3\x3\x3\x6"+
		"\x3\xAF\n\x3\r\x3\xE\x3\xB0\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x5\x4\xBC\n\x4\x3\x5\x3\x5\x3\x5\x3\x5\a\x5\xC2\n\x5\f\x5"+
		"\xE\x5\xC5\v\x5\x3\x5\x3\x5\x3\x6\x3\x6\a\x6\xCB\n\x6\f\x6\xE\x6\xCE\v"+
		"\x6\x3\x6\x3\x6\a\x6\xD2\n\x6\f\x6\xE\x6\xD5\v\x6\x3\x6\x3\x6\x3\a\x3"+
		"\a\x3\a\x3\a\x6\a\xDD\n\a\r\a\xE\a\xDE\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3"+
		"\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x14\x3"+
		"\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3"+
		"\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19\x3"+
		"\x19\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3"+
		"\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3"+
		"\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3"+
		" \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3!\x3!\x3!\x3!\x3!\x3!\x3!\x3!"+
		"\x3!\x3\"\x3\"\x3\"\x3\"\x3#\x3#\x3#\x3#\x3#\x3#\x3$\x3$\x3$\x3$\x3$\x3"+
		"$\x3%\x3%\x3%\x3%\x3%\x3&\x3&\x3\'\x3\'\x3(\x3(\x3)\x3)\x3*\x3*\x3+\x3"+
		"+\x3,\x3,\x3-\x3-\x3.\x3.\x3.\x3/\x3/\x3/\x3\x30\x3\x30\x3\x31\x3\x31"+
		"\x3\x32\x3\x32\x3\x33\x3\x33\x3\x34\x3\x34\x3\x35\x3\x35\x3\x36\x3\x36"+
		"\x3\x37\x3\x37\x3\x38\x3\x38\x3\x39\x3\x39\x3\x39\x3:\x3:\x3:\x3;\x3;"+
		"\x3;\x3<\x3<\x3<\x3=\x3=\x3=\x3>\x3>\x3>\x3?\x3?\x3@\x3@\x3\x41\x3\x41"+
		"\x3\x41\x3\x42\x3\x42\x3\x42\x3\x43\x3\x43\x3\x44\x3\x44\x3\x45\x3\x45"+
		"\x3\x45\x3\x46\x3\x46\x3\x46\x3G\x3G\aG\x1D6\nG\fG\xEG\x1D9\vG\x3H\x3"+
		"H\x3H\x3H\aH\x1DF\nH\fH\xEH\x1E2\vH\x3H\x3H\x3H\x3H\x3I\x3I\x3I\x3I\x3"+
		"J\x3J\x3\x1E0\x2\x2K\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF"+
		"\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10"+
		"\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/"+
		"\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F"+
		"=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2"+
		"+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33\x65\x2\x34"+
		"g\x2\x35i\x2\x36k\x2\x37m\x2\x38o\x2\x39q\x2:s\x2;u\x2<w\x2=y\x2>{\x2"+
		"?}\x2@\x7F\x2\x41\x81\x2\x42\x83\x2\x43\x85\x2\x44\x87\x2\x45\x89\x2\x46"+
		"\x8B\x2G\x8D\x2H\x8F\x2I\x91\x2J\x93\x2K\x3\x2\n\x3\x2\x33;\x3\x2\x32"+
		";\x3\x2$$\x3\x2\"\"\x5\x2\x32;\x43H\x63h\x5\x2\x43\\\x61\x61\x63|\x6\x2"+
		"\x32;\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\"\"\x1FA\x2\x3\x3\x2\x2\x2\x2"+
		"\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2"+
		"\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2"+
		"\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2"+
		"\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2"+
		"\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2"+
		"\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2"+
		"\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2"+
		"\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2"+
		"\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2"+
		"\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3"+
		"\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2"+
		"\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3\x2"+
		"\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2\x2"+
		"\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2s\x3\x2\x2\x2\x2u\x3\x2\x2\x2\x2w\x3"+
		"\x2\x2\x2\x2y\x3\x2\x2\x2\x2{\x3\x2\x2\x2\x2}\x3\x2\x2\x2\x2\x7F\x3\x2"+
		"\x2\x2\x2\x81\x3\x2\x2\x2\x2\x83\x3\x2\x2\x2\x2\x85\x3\x2\x2\x2\x2\x87"+
		"\x3\x2\x2\x2\x2\x89\x3\x2\x2\x2\x2\x8B\x3\x2\x2\x2\x2\x8D\x3\x2\x2\x2"+
		"\x2\x8F\x3\x2\x2\x2\x2\x91\x3\x2\x2\x2\x2\x93\x3\x2\x2\x2\x3\xA0\x3\x2"+
		"\x2\x2\x5\xA3\x3\x2\x2\x2\a\xBB\x3\x2\x2\x2\t\xBD\x3\x2\x2\x2\v\xC8\x3"+
		"\x2\x2\x2\r\xD8\x3\x2\x2\x2\xF\xE0\x3\x2\x2\x2\x11\xE5\x3\x2\x2\x2\x13"+
		"\xE9\x3\x2\x2\x2\x15\xED\x3\x2\x2\x2\x17\xF5\x3\x2\x2\x2\x19\xF9\x3\x2"+
		"\x2\x2\x1B\xFF\x3\x2\x2\x2\x1D\x108\x3\x2\x2\x2\x1F\x10B\x3\x2\x2\x2!"+
		"\x112\x3\x2\x2\x2#\x117\x3\x2\x2\x2%\x11D\x3\x2\x2\x2\'\x120\x3\x2\x2"+
		"\x2)\x123\x3\x2\x2\x2+\x126\x3\x2\x2\x2-\x129\x3\x2\x2\x2/\x12C\x3\x2"+
		"\x2\x2\x31\x131\x3\x2\x2\x2\x33\x139\x3\x2\x2\x2\x35\x13C\x3\x2\x2\x2"+
		"\x37\x143\x3\x2\x2\x2\x39\x148\x3\x2\x2\x2;\x14F\x3\x2\x2\x2=\x156\x3"+
		"\x2\x2\x2?\x15D\x3\x2\x2\x2\x41\x167\x3\x2\x2\x2\x43\x170\x3\x2\x2\x2"+
		"\x45\x174\x3\x2\x2\x2G\x17A\x3\x2\x2\x2I\x180\x3\x2\x2\x2K\x185\x3\x2"+
		"\x2\x2M\x187\x3\x2\x2\x2O\x189\x3\x2\x2\x2Q\x18B\x3\x2\x2\x2S\x18D\x3"+
		"\x2\x2\x2U\x18F\x3\x2\x2\x2W\x191\x3\x2\x2\x2Y\x193\x3\x2\x2\x2[\x195"+
		"\x3\x2\x2\x2]\x198\x3\x2\x2\x2_\x19B\x3\x2\x2\x2\x61\x19D\x3\x2\x2\x2"+
		"\x63\x19F\x3\x2\x2\x2\x65\x1A1\x3\x2\x2\x2g\x1A3\x3\x2\x2\x2i\x1A5\x3"+
		"\x2\x2\x2k\x1A7\x3\x2\x2\x2m\x1A9\x3\x2\x2\x2o\x1AB\x3\x2\x2\x2q\x1AD"+
		"\x3\x2\x2\x2s\x1B0\x3\x2\x2\x2u\x1B3\x3\x2\x2\x2w\x1B6\x3\x2\x2\x2y\x1B9"+
		"\x3\x2\x2\x2{\x1BC\x3\x2\x2\x2}\x1BF\x3\x2\x2\x2\x7F\x1C1\x3\x2\x2\x2"+
		"\x81\x1C3\x3\x2\x2\x2\x83\x1C6\x3\x2\x2\x2\x85\x1C9\x3\x2\x2\x2\x87\x1CB"+
		"\x3\x2\x2\x2\x89\x1CD\x3\x2\x2\x2\x8B\x1D0\x3\x2\x2\x2\x8D\x1D3\x3\x2"+
		"\x2\x2\x8F\x1DA\x3\x2\x2\x2\x91\x1E7\x3\x2\x2\x2\x93\x1EB\x3\x2\x2\x2"+
		"\x95\x97\a/\x2\x2\x96\x95\x3\x2\x2\x2\x96\x97\x3\x2\x2\x2\x97\x98\x3\x2"+
		"\x2\x2\x98\x9C\t\x2\x2\x2\x99\x9B\t\x3\x2\x2\x9A\x99\x3\x2\x2\x2\x9B\x9E"+
		"\x3\x2\x2\x2\x9C\x9A\x3\x2\x2\x2\x9C\x9D\x3\x2\x2\x2\x9D\xA1\x3\x2\x2"+
		"\x2\x9E\x9C\x3\x2\x2\x2\x9F\xA1\a\x32\x2\x2\xA0\x96\x3\x2\x2\x2\xA0\x9F"+
		"\x3\x2\x2\x2\xA1\x4\x3\x2\x2\x2\xA2\xA4\a/\x2\x2\xA3\xA2\x3\x2\x2\x2\xA3"+
		"\xA4\x3\x2\x2\x2\xA4\xA8\x3\x2\x2\x2\xA5\xA7\t\x2\x2\x2\xA6\xA5\x3\x2"+
		"\x2\x2\xA7\xAA\x3\x2\x2\x2\xA8\xA6\x3\x2\x2\x2\xA8\xA9\x3\x2\x2\x2\xA9"+
		"\xAB\x3\x2\x2\x2\xAA\xA8\x3\x2\x2\x2\xAB\xAC\t\x3\x2\x2\xAC\xAE\a\x30"+
		"\x2\x2\xAD\xAF\t\x3\x2\x2\xAE\xAD\x3\x2\x2\x2\xAF\xB0\x3\x2\x2\x2\xB0"+
		"\xAE\x3\x2\x2\x2\xB0\xB1\x3\x2\x2\x2\xB1\x6\x3\x2\x2\x2\xB2\xB3\av\x2"+
		"\x2\xB3\xB4\at\x2\x2\xB4\xB5\aw\x2\x2\xB5\xBC\ag\x2\x2\xB6\xB7\ah\x2\x2"+
		"\xB7\xB8\a\x63\x2\x2\xB8\xB9\an\x2\x2\xB9\xBA\au\x2\x2\xBA\xBC\ag\x2\x2"+
		"\xBB\xB2\x3\x2\x2\x2\xBB\xB6\x3\x2\x2\x2\xBC\b\x3\x2\x2\x2\xBD\xC3\a$"+
		"\x2\x2\xBE\xC2\n\x4\x2\x2\xBF\xC0\a^\x2\x2\xC0\xC2\a$\x2\x2\xC1\xBE\x3"+
		"\x2\x2\x2\xC1\xBF\x3\x2\x2\x2\xC2\xC5\x3\x2\x2\x2\xC3\xC1\x3\x2\x2\x2"+
		"\xC3\xC4\x3\x2\x2\x2\xC4\xC6\x3\x2\x2\x2\xC5\xC3\x3\x2\x2\x2\xC6\xC7\a"+
		"$\x2\x2\xC7\n\x3\x2\x2\x2\xC8\xCC\x5\x3\x2\x2\xC9\xCB\t\x5\x2\x2\xCA\xC9"+
		"\x3\x2\x2\x2\xCB\xCE\x3\x2\x2\x2\xCC\xCA\x3\x2\x2\x2\xCC\xCD\x3\x2\x2"+
		"\x2\xCD\xCF\x3\x2\x2\x2\xCE\xCC\x3\x2\x2\x2\xCF\xD3\x5i\x35\x2\xD0\xD2"+
		"\t\x5\x2\x2\xD1\xD0\x3\x2\x2\x2\xD2\xD5\x3\x2\x2\x2\xD3\xD1\x3\x2\x2\x2"+
		"\xD3\xD4\x3\x2\x2\x2\xD4\xD6\x3\x2\x2\x2\xD5\xD3\x3\x2\x2\x2\xD6\xD7\x5"+
		"\x3\x2\x2\xD7\f\x3\x2\x2\x2\xD8\xD9\a\x32\x2\x2\xD9\xDA\az\x2\x2\xDA\xDC"+
		"\x3\x2\x2\x2\xDB\xDD\t\x6\x2\x2\xDC\xDB\x3\x2\x2\x2\xDD\xDE\x3\x2\x2\x2"+
		"\xDE\xDC\x3\x2\x2\x2\xDE\xDF\x3\x2\x2\x2\xDF\xE\x3\x2\x2\x2\xE0\xE1\a"+
		"p\x2\x2\xE1\xE2\aw\x2\x2\xE2\xE3\an\x2\x2\xE3\xE4\an\x2\x2\xE4\x10\x3"+
		"\x2\x2\x2\xE5\xE6\an\x2\x2\xE6\xE7\ag\x2\x2\xE7\xE8\av\x2\x2\xE8\x12\x3"+
		"\x2\x2\x2\xE9\xEA\ah\x2\x2\xEA\xEB\aq\x2\x2\xEB\xEC\at\x2\x2\xEC\x14\x3"+
		"\x2\x2\x2\xED\xEE\ah\x2\x2\xEE\xEF\aq\x2\x2\xEF\xF0\at\x2\x2\xF0\xF1\a"+
		"g\x2\x2\xF1\xF2\a\x63\x2\x2\xF2\xF3\a\x65\x2\x2\xF3\xF4\aj\x2\x2\xF4\x16"+
		"\x3\x2\x2\x2\xF5\xF6\a\x63\x2\x2\xF6\xF7\a\x66\x2\x2\xF7\xF8\a\x66\x2"+
		"\x2\xF8\x18\x3\x2\x2\x2\xF9\xFA\a\x64\x2\x2\xFA\xFB\at\x2\x2\xFB\xFC\a"+
		"g\x2\x2\xFC\xFD\a\x63\x2\x2\xFD\xFE\am\x2\x2\xFE\x1A\x3\x2\x2\x2\xFF\x100"+
		"\a\x65\x2\x2\x100\x101\aq\x2\x2\x101\x102\ap\x2\x2\x102\x103\av\x2\x2"+
		"\x103\x104\ak\x2\x2\x104\x105\ap\x2\x2\x105\x106\aw\x2\x2\x106\x107\a"+
		"g\x2\x2\x107\x1C\x3\x2\x2\x2\x108\x109\a\x63\x2\x2\x109\x10A\av\x2\x2"+
		"\x10A\x1E\x3\x2\x2\x2\x10B\x10C\at\x2\x2\x10C\x10D\ag\x2\x2\x10D\x10E"+
		"\ao\x2\x2\x10E\x10F\aq\x2\x2\x10F\x110\ax\x2\x2\x110\x111\ag\x2\x2\x111"+
		" \x3\x2\x2\x2\x112\x113\ah\x2\x2\x113\x114\at\x2\x2\x114\x115\aq\x2\x2"+
		"\x115\x116\ao\x2\x2\x116\"\x3\x2\x2\x2\x117\x118\ay\x2\x2\x118\x119\a"+
		"j\x2\x2\x119\x11A\ak\x2\x2\x11A\x11B\an\x2\x2\x11B\x11C\ag\x2\x2\x11C"+
		"$\x3\x2\x2\x2\x11D\x11E\ak\x2\x2\x11E\x11F\ap\x2\x2\x11F&\x3\x2\x2\x2"+
		"\x120\x121\ak\x2\x2\x121\x122\ah\x2\x2\x122(\x3\x2\x2\x2\x123\x124\aw"+
		"\x2\x2\x124\x125\ar\x2\x2\x125*\x3\x2\x2\x2\x126\x127\av\x2\x2\x127\x128"+
		"\aq\x2\x2\x128,\x3\x2\x2\x2\x129\x12A\a\x63\x2\x2\x12A\x12B\au\x2\x2\x12B"+
		".\x3\x2\x2\x2\x12C\x12D\ag\x2\x2\x12D\x12E\ap\x2\x2\x12E\x12F\aw\x2\x2"+
		"\x12F\x130\ao\x2\x2\x130\x30\x3\x2\x2\x2\x131\x132\an\x2\x2\x132\x133"+
		"\ak\x2\x2\x133\x134\a\x64\x2\x2\x134\x135\at\x2\x2\x135\x136\a\x63\x2"+
		"\x2\x136\x137\at\x2\x2\x137\x138\a{\x2\x2\x138\x32\x3\x2\x2\x2\x139\x13A"+
		"\au\x2\x2\x13A\x13B\ah\x2\x2\x13B\x34\x3\x2\x2\x2\x13C\x13D\aq\x2\x2\x13D"+
		"\x13E\a\x64\x2\x2\x13E\x13F\al\x2\x2\x13F\x140\ag\x2\x2\x140\x141\a\x65"+
		"\x2\x2\x141\x142\av\x2\x2\x142\x36\x3\x2\x2\x2\x143\x144\ag\x2\x2\x144"+
		"\x145\an\x2\x2\x145\x146\au\x2\x2\x146\x147\ag\x2\x2\x147\x38\x3\x2\x2"+
		"\x2\x148\x149\ak\x2\x2\x149\x14A\ao\x2\x2\x14A\x14B\ar\x2\x2\x14B\x14C"+
		"\aq\x2\x2\x14C\x14D\at\x2\x2\x14D\x14E\av\x2\x2\x14E:\x3\x2\x2\x2\x14F"+
		"\x150\at\x2\x2\x150\x151\ag\x2\x2\x151\x152\av\x2\x2\x152\x153\aw\x2\x2"+
		"\x153\x154\at\x2\x2\x154\x155\ap\x2\x2\x155<\x3\x2\x2\x2\x156\x157\ar"+
		"\x2\x2\x157\x158\at\x2\x2\x158\x159\ak\x2\x2\x159\x15A\ap\x2\x2\x15A\x15B"+
		"\av\x2\x2\x15B\x15C\a\"\x2\x2\x15C>\x3\x2\x2\x2\x15D\x15E\a\x66\x2\x2"+
		"\x15E\x15F\ak\x2\x2\x15F\x160\au\x2\x2\x160\x161\at\x2\x2\x161\x162\a"+
		"g\x2\x2\x162\x163\ai\x2\x2\x163\x164\a\x63\x2\x2\x164\x165\at\x2\x2\x165"+
		"\x166\a\x66\x2\x2\x166@\x3\x2\x2\x2\x167\x168\ag\x2\x2\x168\x169\az\x2"+
		"\x2\x169\x16A\av\x2\x2\x16A\x16B\ag\x2\x2\x16B\x16C\at\x2\x2\x16C\x16D"+
		"\ap\x2\x2\x16D\x16E\a\x63\x2\x2\x16E\x16F\an\x2\x2\x16F\x42\x3\x2\x2\x2"+
		"\x170\x171\av\x2\x2\x171\x172\at\x2\x2\x172\x173\a{\x2\x2\x173\x44\x3"+
		"\x2\x2\x2\x174\x175\a\x65\x2\x2\x175\x176\a\x63\x2\x2\x176\x177\av\x2"+
		"\x2\x177\x178\a\x65\x2\x2\x178\x179\aj\x2\x2\x179\x46\x3\x2\x2\x2\x17A"+
		"\x17B\av\x2\x2\x17B\x17C\aj\x2\x2\x17C\x17D\at\x2\x2\x17D\x17E\aq\x2\x2"+
		"\x17E\x17F\ay\x2\x2\x17FH\x3\x2\x2\x2\x180\x181\ah\x2\x2\x181\x182\ak"+
		"\x2\x2\x182\x183\at\x2\x2\x183\x184\ag\x2\x2\x184J\x3\x2\x2\x2\x185\x186"+
		"\a=\x2\x2\x186L\x3\x2\x2\x2\x187\x188\a?\x2\x2\x188N\x3\x2\x2\x2\x189"+
		"\x18A\a.\x2\x2\x18AP\x3\x2\x2\x2\x18B\x18C\a}\x2\x2\x18CR\x3\x2\x2\x2"+
		"\x18D\x18E\a\x7F\x2\x2\x18ET\x3\x2\x2\x2\x18F\x190\a]\x2\x2\x190V\x3\x2"+
		"\x2\x2\x191\x192\a_\x2\x2\x192X\x3\x2\x2\x2\x193\x194\a#\x2\x2\x194Z\x3"+
		"\x2\x2\x2\x195\x196\a>\x2\x2\x196\x197\a/\x2\x2\x197\\\x3\x2\x2\x2\x198"+
		"\x199\a/\x2\x2\x199\x19A\a@\x2\x2\x19A^\x3\x2\x2\x2\x19B\x19C\a*\x2\x2"+
		"\x19C`\x3\x2\x2\x2\x19D\x19E\a+\x2\x2\x19E\x62\x3\x2\x2\x2\x19F\x1A0\a"+
		"-\x2\x2\x1A0\x64\x3\x2\x2\x2\x1A1\x1A2\a/\x2\x2\x1A2\x66\x3\x2\x2\x2\x1A3"+
		"\x1A4\a,\x2\x2\x1A4h\x3\x2\x2\x2\x1A5\x1A6\a\x31\x2\x2\x1A6j\x3\x2\x2"+
		"\x2\x1A7\x1A8\a`\x2\x2\x1A8l\x3\x2\x2\x2\x1A9\x1AA\a\'\x2\x2\x1AAn\x3"+
		"\x2\x2\x2\x1AB\x1AC\a\x30\x2\x2\x1ACp\x3\x2\x2\x2\x1AD\x1AE\a-\x2\x2\x1AE"+
		"\x1AF\a?\x2\x2\x1AFr\x3\x2\x2\x2\x1B0\x1B1\a/\x2\x2\x1B1\x1B2\a?\x2\x2"+
		"\x1B2t\x3\x2\x2\x2\x1B3\x1B4\a\x31\x2\x2\x1B4\x1B5\a?\x2\x2\x1B5v\x3\x2"+
		"\x2\x2\x1B6\x1B7\a,\x2\x2\x1B7\x1B8\a?\x2\x2\x1B8x\x3\x2\x2\x2\x1B9\x1BA"+
		"\a-\x2\x2\x1BA\x1BB\a-\x2\x2\x1BBz\x3\x2\x2\x2\x1BC\x1BD\a/\x2\x2\x1BD"+
		"\x1BE\a/\x2\x2\x1BE|\x3\x2\x2\x2\x1BF\x1C0\a~\x2\x2\x1C0~\x3\x2\x2\x2"+
		"\x1C1\x1C2\a(\x2\x2\x1C2\x80\x3\x2\x2\x2\x1C3\x1C4\a#\x2\x2\x1C4\x1C5"+
		"\a?\x2\x2\x1C5\x82\x3\x2\x2\x2\x1C6\x1C7\a?\x2\x2\x1C7\x1C8\a?\x2\x2\x1C8"+
		"\x84\x3\x2\x2\x2\x1C9\x1CA\a@\x2\x2\x1CA\x86\x3\x2\x2\x2\x1CB\x1CC\a>"+
		"\x2\x2\x1CC\x88\x3\x2\x2\x2\x1CD\x1CE\a@\x2\x2\x1CE\x1CF\a?\x2\x2\x1CF"+
		"\x8A\x3\x2\x2\x2\x1D0\x1D1\a>\x2\x2\x1D1\x1D2\a?\x2\x2\x1D2\x8C\x3\x2"+
		"\x2\x2\x1D3\x1D7\t\a\x2\x2\x1D4\x1D6\t\b\x2\x2\x1D5\x1D4\x3\x2\x2\x2\x1D6"+
		"\x1D9\x3\x2\x2\x2\x1D7\x1D5\x3\x2\x2\x2\x1D7\x1D8\x3\x2\x2\x2\x1D8\x8E"+
		"\x3\x2\x2\x2\x1D9\x1D7\x3\x2\x2\x2\x1DA\x1DB\a\x31\x2\x2\x1DB\x1DC\a\x31"+
		"\x2\x2\x1DC\x1E0\x3\x2\x2\x2\x1DD\x1DF\v\x2\x2\x2\x1DE\x1DD\x3\x2\x2\x2"+
		"\x1DF\x1E2\x3\x2\x2\x2\x1E0\x1E1\x3\x2\x2\x2\x1E0\x1DE\x3\x2\x2\x2\x1E1"+
		"\x1E3\x3\x2\x2\x2\x1E2\x1E0\x3\x2\x2\x2\x1E3\x1E4\a\f\x2\x2\x1E4\x1E5"+
		"\x3\x2\x2\x2\x1E5\x1E6\bH\x2\x2\x1E6\x90\x3\x2\x2\x2\x1E7\x1E8\t\t\x2"+
		"\x2\x1E8\x1E9\x3\x2\x2\x2\x1E9\x1EA\bI\x2\x2\x1EA\x92\x3\x2\x2\x2\x1EB"+
		"\x1EC\v\x2\x2\x2\x1EC\x94\x3\x2\x2\x2\x11\x2\x96\x9C\xA0\xA3\xA8\xB0\xBB"+
		"\xC1\xC3\xCC\xD3\xDE\x1D7\x1E0\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Algo
