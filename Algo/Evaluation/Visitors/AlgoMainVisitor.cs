using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using MathNet.Numerics;

namespace Algo
{
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //Scopes collection for this instance.
        public static AlgoScopeCollection Scopes = new AlgoScopeCollection();

        //Plugins collection for this instance.
        public static AlgoFunctionPlugins Plugins = new AlgoFunctionPlugins();

        //The provided console arguments for this instance.
        public static List<AlgoValue> ConsoleArguments = new List<AlgoValue>();

        //When the "Statement" node is visited.
        public override object VisitCompileUnit(algoParser.CompileUnitContext context)
        {
            //Visit the block.
            VisitBlock(context.block());
            return null;
        }

        //Visit each statement in turn.
        public override object VisitBlock([NotNull] algoParser.BlockContext context)
        {
            //Enumerate over all statements.
            foreach (var statement in context.statement())
            {
                VisitStatement(statement);
            }

            return null;
        }

        //When evaluating a statement, switch for type.
        public override object VisitStatement([NotNull] algoParser.StatementContext context)
        {
            //What type is it?
            if (context.stat_define() != null)
            {
                //Define statement.
                VisitStat_define(context.stat_define());
            }
            else if (context.stat_setvar() != null)
            {
                //Set a variable after its definition.
                VisitStat_setvar(context.stat_setvar());
            }
            else if (context.stat_setvar_op() != null)
            {
                //Set a variable using a self modifying operator.
                VisitStat_setvar_op(context.stat_setvar_op());
            }
            else if (context.stat_setvar_postfix() != null)
            {
                //Set a variable using a postfix operator.
                VisitStat_setvar_postfix(context.stat_setvar_postfix());
            }
            else if (context.stat_deletevar() != null)
            {
                //Delete a variable from scope.
                VisitStat_deletevar(context.stat_deletevar());
            }
            else if (context.stat_if() != null)
            {
                //An "if" statement.
                return VisitStat_if(context.stat_if());
            }
            else if (context.stat_forLoop() != null)
            {
                //A for loop.
                return VisitStat_forLoop(context.stat_forLoop());
            }
            else if (context.stat_whileLoop() != null)
            {
                //A while loop.
                return VisitStat_whileLoop(context.stat_whileLoop());
            }
            else if (context.stat_enumDef() != null)
            {
                //Definition of an enum.
                VisitStat_enumDef(context.stat_enumDef());
            }
            else if (context.stat_functionCall() != null)
            {
                //A function call.
                //This should NOT return, it's a top level single function call.
                VisitStat_functionCall(context.stat_functionCall());
            }
            else if (context.stat_asyncFunctionCall() != null)
            {
                //Asynchronous function call.
                //Doesn't return a value, it fires onto another thread.
                VisitStat_asyncFunctionCall(context.stat_asyncFunctionCall());
            }
            else if (context.stat_functionDef() != null)
            {
                //A function definition.
                VisitStat_functionDef(context.stat_functionDef());
            }
            else if (context.stat_return() != null)
            {
                //Returning a value from a function call.
                return VisitStat_return(context.stat_return());
            }
            else if (context.stat_print() != null)
            {
                //A print statement.
                VisitStat_print(context.stat_print());
            }
            else if (context.stat_library() != null)
            {
                //Defining a library.
                VisitStat_library(context.stat_library());
            }
            else if (context.stat_import() != null)
            {
                //Importing an Algo script.
                VisitStat_import(context.stat_import());
            }
            else if (context.stat_loadFuncExt() != null)
            {
                //Load an external or internal plugin function.
                VisitStat_loadFuncExt(context.stat_loadFuncExt());
            }
            else if (context.stat_list_add() != null)
            {
                //Add a value to a list.
                VisitStat_list_add(context.stat_list_add());
            }
            else if (context.stat_list_remove() != null)
            {
                //Remove a value from a list.
                VisitStat_list_remove(context.stat_list_remove());
            }
            else if (context.stat_break() != null)
            {
                //Break statement, return a break value.
                return AlgoValue.Break;
            }
            else if (context.stat_continue() != null)
            {
                //Continue statement, return a continue value.
                return AlgoValue.Continue;
            }
            else if (context.stat_try_catch() != null)
            {
                //Try/catch block.
                return VisitStat_try_catch(context.stat_try_catch());
            }
            else if (context.stat_throw() != null)
            {
                //Throw an error.
                VisitStat_throw(context.stat_throw());
            }
            else
            {
                Error.Fatal(context, "Syntax error, unrecognized statement.");
            }

            return null;
        }
    }
}
