﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Z:\VSProjects\algo\algo-lang\Algo\Parsing\algo.g4 by ANTLR 4.6.6

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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="algoParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IalgoVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompileUnit([NotNull] algoParser.CompileUnitContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] algoParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] algoParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_define"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_define([NotNull] algoParser.Stat_defineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_setvar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_setvar([NotNull] algoParser.Stat_setvarContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_setvar_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_setvar_op([NotNull] algoParser.Stat_setvar_opContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_setvar_postfix"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_setvar_postfix([NotNull] algoParser.Stat_setvar_postfixContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_deletevar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_deletevar([NotNull] algoParser.Stat_deletevarContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_enumDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_enumDef([NotNull] algoParser.Stat_enumDefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_asyncFunctionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_asyncFunctionCall([NotNull] algoParser.Stat_asyncFunctionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.functionCall_particle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall_particle([NotNull] algoParser.FunctionCall_particleContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_loadFuncExt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_loadFuncExt([NotNull] algoParser.Stat_loadFuncExtContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_return([NotNull] algoParser.Stat_returnContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_forLoop([NotNull] algoParser.Stat_forLoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_whileLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_whileLoop([NotNull] algoParser.Stat_whileLoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_if([NotNull] algoParser.Stat_ifContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_print([NotNull] algoParser.Stat_printContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_library"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_library([NotNull] algoParser.Stat_libraryContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_import"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_import([NotNull] algoParser.Stat_importContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_list_add"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_list_add([NotNull] algoParser.Stat_list_addContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_list_remove"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_list_remove([NotNull] algoParser.Stat_list_removeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_try_catch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_try_catch([NotNull] algoParser.Stat_try_catchContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_throw"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_throw([NotNull] algoParser.Stat_throwContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_break"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_break([NotNull] algoParser.Stat_breakContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_continue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_continue([NotNull] algoParser.Stat_continueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_elif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_elif([NotNull] algoParser.Stat_elifContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_else"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_else([NotNull] algoParser.Stat_elseContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.literal_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral_params([NotNull] algoParser.Literal_paramsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.abstract_params"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAbstract_params([NotNull] algoParser.Abstract_paramsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.check"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCheck([NotNull] algoParser.CheckContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.checkfrag"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCheckfrag([NotNull] algoParser.CheckfragContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] algoParser.ExprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.rounding_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRounding_expr([NotNull] algoParser.Rounding_exprContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] algoParser.TermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactor([NotNull] algoParser.FactorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.sub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSub([NotNull] algoParser.SubContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperator([NotNull] algoParser.OperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.selfmod_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSelfmod_op([NotNull] algoParser.Selfmod_opContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.postfix_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPostfix_op([NotNull] algoParser.Postfix_opContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] algoParser.ValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.particle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParticle([NotNull] algoParser.ParticleContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray([NotNull] algoParser.ArrayContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.array_access_particle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray_access_particle([NotNull] algoParser.Array_access_particleContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.object"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObject([NotNull] algoParser.ObjectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.obj_child_definitions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObj_child_definitions([NotNull] algoParser.Obj_child_definitionsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.obj_vardefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObj_vardefine([NotNull] algoParser.Obj_vardefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.obj_funcdefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObj_funcdefine([NotNull] algoParser.Obj_funcdefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.obj_externdefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObj_externdefine([NotNull] algoParser.Obj_externdefineContext context);
}
} // namespace Algo
