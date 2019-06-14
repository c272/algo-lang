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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IalgoListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class algoBaseListener : IalgoListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompileUnit([NotNull] algoParser.CompileUnitContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompileUnit([NotNull] algoParser.CompileUnitContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlock([NotNull] algoParser.BlockContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlock([NotNull] algoParser.BlockContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] algoParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] algoParser.StatementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_define"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_define([NotNull] algoParser.Stat_defineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_define"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_define([NotNull] algoParser.Stat_defineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_setvar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_setvar([NotNull] algoParser.Stat_setvarContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_setvar"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_setvar([NotNull] algoParser.Stat_setvarContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_functionCall([NotNull] algoParser.Stat_functionCallContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_functionCall"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_functionDef([NotNull] algoParser.Stat_functionDefContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_functionDef"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_forLoop([NotNull] algoParser.Stat_forLoopContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_forLoop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_forLoop([NotNull] algoParser.Stat_forLoopContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat_if([NotNull] algoParser.Stat_ifContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.stat_if"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat_if([NotNull] algoParser.Stat_ifContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.literal_params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteral_params([NotNull] algoParser.Literal_paramsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.literal_params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteral_params([NotNull] algoParser.Literal_paramsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.abstract_params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAbstract_params([NotNull] algoParser.Abstract_paramsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.abstract_params"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAbstract_params([NotNull] algoParser.Abstract_paramsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr([NotNull] algoParser.ExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr([NotNull] algoParser.ExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTerm([NotNull] algoParser.TermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTerm([NotNull] algoParser.TermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFactor([NotNull] algoParser.FactorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFactor([NotNull] algoParser.FactorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.sub"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSub([NotNull] algoParser.SubContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.sub"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSub([NotNull] algoParser.SubContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.operator"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOperator([NotNull] algoParser.OperatorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.operator"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOperator([NotNull] algoParser.OperatorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.value"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterValue([NotNull] algoParser.ValueContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.value"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitValue([NotNull] algoParser.ValueContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.array"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArray([NotNull] algoParser.ArrayContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.array"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArray([NotNull] algoParser.ArrayContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="algoParser.array_access"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArray_access([NotNull] algoParser.Array_accessContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="algoParser.array_access"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArray_access([NotNull] algoParser.Array_accessContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace Algo