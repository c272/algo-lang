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
	/// Visit a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_forLoop([NotNull] algoParser.Stat_forLoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStat_if([NotNull] algoParser.Stat_ifContext context);

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
	/// Visit a parse tree produced by <see cref="algoParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] algoParser.ExprContext context);

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
	/// Visit a parse tree produced by <see cref="algoParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] algoParser.ValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.array"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray([NotNull] algoParser.ArrayContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="algoParser.array_access"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArray_access([NotNull] algoParser.Array_accessContext context);
}
} // namespace Algo