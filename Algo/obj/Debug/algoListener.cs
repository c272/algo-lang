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
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="algoParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IalgoListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] algoParser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] algoParser.CompileUnitContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] algoParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] algoParser.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] algoParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] algoParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_define"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_define([NotNull] algoParser.Stat_defineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_define"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_define([NotNull] algoParser.Stat_defineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_setvar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_setvar([NotNull] algoParser.Stat_setvarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_setvar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_setvar([NotNull] algoParser.Stat_setvarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_setvar_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_setvar_op([NotNull] algoParser.Stat_setvar_opContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_setvar_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_setvar_op([NotNull] algoParser.Stat_setvar_opContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_deletevar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_deletevar([NotNull] algoParser.Stat_deletevarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_deletevar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_deletevar([NotNull] algoParser.Stat_deletevarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_functionCall([NotNull] algoParser.Stat_functionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_functionDef([NotNull] algoParser.Stat_functionDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_return([NotNull] algoParser.Stat_returnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_return([NotNull] algoParser.Stat_returnContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_forLoop([NotNull] algoParser.Stat_forLoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_forLoop([NotNull] algoParser.Stat_forLoopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_if([NotNull] algoParser.Stat_ifContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_if([NotNull] algoParser.Stat_ifContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_print([NotNull] algoParser.Stat_printContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_print([NotNull] algoParser.Stat_printContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_library"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_library([NotNull] algoParser.Stat_libraryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_library"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_library([NotNull] algoParser.Stat_libraryContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_elif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_elif([NotNull] algoParser.Stat_elifContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_elif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_elif([NotNull] algoParser.Stat_elifContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat_else([NotNull] algoParser.Stat_elseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat_else([NotNull] algoParser.Stat_elseContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.literal_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral_params([NotNull] algoParser.Literal_paramsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.literal_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral_params([NotNull] algoParser.Literal_paramsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.abstract_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAbstract_params([NotNull] algoParser.Abstract_paramsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.abstract_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAbstract_params([NotNull] algoParser.Abstract_paramsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.check"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCheck([NotNull] algoParser.CheckContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.check"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCheck([NotNull] algoParser.CheckContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.check_operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCheck_operator([NotNull] algoParser.Check_operatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.check_operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCheck_operator([NotNull] algoParser.Check_operatorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] algoParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] algoParser.ExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.rounding_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRounding_expr([NotNull] algoParser.Rounding_exprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.rounding_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRounding_expr([NotNull] algoParser.Rounding_exprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] algoParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] algoParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] algoParser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] algoParser.FactorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.sub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSub([NotNull] algoParser.SubContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.sub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSub([NotNull] algoParser.SubContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperator([NotNull] algoParser.OperatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperator([NotNull] algoParser.OperatorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.selfmod_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelfmod_op([NotNull] algoParser.Selfmod_opContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.selfmod_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelfmod_op([NotNull] algoParser.Selfmod_opContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterValue([NotNull] algoParser.ValueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitValue([NotNull] algoParser.ValueContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.obj_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObj_access([NotNull] algoParser.Obj_accessContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.obj_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObj_access([NotNull] algoParser.Obj_accessContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArray([NotNull] algoParser.ArrayContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArray([NotNull] algoParser.ArrayContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.array_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArray_access([NotNull] algoParser.Array_accessContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.array_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArray_access([NotNull] algoParser.Array_accessContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.object"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObject([NotNull] algoParser.ObjectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.object"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObject([NotNull] algoParser.ObjectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.obj_child_definitions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObj_child_definitions([NotNull] algoParser.Obj_child_definitionsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.obj_child_definitions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObj_child_definitions([NotNull] algoParser.Obj_child_definitionsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.obj_vardefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObj_vardefine([NotNull] algoParser.Obj_vardefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.obj_vardefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObj_vardefine([NotNull] algoParser.Obj_vardefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.obj_funcdefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObj_funcdefine([NotNull] algoParser.Obj_funcdefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.obj_funcdefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObj_funcdefine([NotNull] algoParser.Obj_funcdefineContext context);
}
} // namespace Algo
