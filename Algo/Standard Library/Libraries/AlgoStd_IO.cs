﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Antlr4.Runtime;

namespace Algo.StandardLibrary
{
    /// <summary>
    /// The Algo standard library for IO. (Input grabbing, outputting to non-terminal devices)
    /// </summary>
    public class AlgoStd_IO : IFunctionPlugin
    {
        public string Name { get; set; } = "std_io";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //input.get();
            new AlgoPluginFunction()
            {
                Name = "input_get",
                Function = GetConsoleInput,
                ParameterCount = 0
            },

            //input.fromFile();
            new AlgoPluginFunction()
            {
                Name = "input_fromFile",
                Function = GetInputFromFile,
                ParameterCount = 1
            },

            //file.exists();
            new AlgoPluginFunction()
            {
                Name = "file_exists",
                Function = DoesFileExist,
                ParameterCount = 1
            },

            //input.args();
            new AlgoPluginFunction()
            {
                Name = "input_args",
                Function = GetConsoleArgs,
                ParameterCount = 0
            },

            //output.toFile();
            new AlgoPluginFunction()
            {
                Name = "output_toFile",
                Function = OutputToFile,
                ParameterCount = 2
            },

            //output.changeConsoleColour();
            new AlgoPluginFunction()
            {
                Name = "output_changeConsoleColour",
                Function = ChangeConsoleColour,
                ParameterCount = 1
            },

            //file.create();
            new AlgoPluginFunction()
            {
                Name = "output_createFile",
                Function = CreateFile,
                ParameterCount = 1
            },

            //file.delete();
            new AlgoPluginFunction()
            {
                Name = "file_delete",
                Function = DeleteFile,
                ParameterCount = 1
            },

            //file.createDir();
            new AlgoPluginFunction()
            {
                Name = "dir_create",
                Function = CreateDir,
                ParameterCount = 1
            },

            //file.deleteDir();
            new AlgoPluginFunction()
            {
                Name = "dir_delete",
                Function = DeleteDir,
                ParameterCount = 1
            }
        };

        //Deletes a directory.
        private static AlgoValue DeleteDir(ParserRuleContext context, AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name of the directory to delete must be a String, not " + args[0].Type.ToString() + ".");
                return null;
            }

            //Attempt to create.
            try
            {
                Directory.Delete((string)args[0].Value);
            }
            catch(Exception e)
            {
                Error.Fatal(context, "Failed to delete directory, '" + e.Message + "'.");
            }

            return null;
        }

        //Creates a directory.
        private static AlgoValue CreateDir(ParserRuleContext context, AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name of the directory to create must be a String, not " + args[0].Type.ToString() + ".");
                return null;
            }

            //Attempt to create.
            try
            {
                Directory.CreateDirectory((string)args[0].Value);
            }
            catch (Exception e)
            {
                Error.Fatal(context, "Failed to create directory, '" + e.Message + "'.");
            }

            return null;
        }

        //Delete a file.
        private static AlgoValue DeleteFile(ParserRuleContext context, AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name of the file to delete must be a String, not " + args[0].Type.ToString() + ".");
                return null;
            }

            //Attempt to create.
            try
            {
                File.Delete((string)args[0].Value);
            }
            catch (Exception e)
            {
                Error.Fatal(context, "Failed to delete file, '" + e.Message + "'.");
            }

            return null;
        }

        //Get the current console arguments as an Algo list.
        public static AlgoValue GetConsoleArgs(ParserRuleContext context, AlgoValue[] args)
        {
            return new AlgoValue()
            {
                Type = AlgoValueType.List,
                Value = algoVisitor.ConsoleArguments
            };
        }

        //Getting input directly from the terminal.
        public static AlgoValue GetConsoleInput(ParserRuleContext context, params AlgoValue[] args)
        {
            //Return the string result of the input.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = Console.ReadLine()
            };
        }

        //Get input from file.
        //args[0] = file path
        public static AlgoValue GetInputFromFile(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check if the parameter is actually a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "File name to read from must be a string.");
                return null;
            }

            //Attempt to read from file.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = File.ReadAllText((string)args[0].Value)
            };
        }

        //Writing output to file.
        //args[0] = file path
        //args[1] = text to write
        public static AlgoValue OutputToFile(ParserRuleContext context, params AlgoValue[] args)
        {
            //CHeck if both parameters are strings.
            if (args[0].Type != AlgoValueType.String || args[1].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Invalid file name or text to output (both must be strings).");
                return null;
            }

            //Attempt to write to file.
            File.WriteAllText((string)args[0].Value, (string)args[1].Value);
            return AlgoValue.Null;
        }

        //Change the colour of the console.
        public static AlgoValue ChangeConsoleColour(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the first argument.
            if (args[0].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Invalid colour given (colour must be an enum value).");
                return null;
            }

            //It's an integer, check it's not too large.
            if ((BigInteger)args[0].Value > 14)
            {
                //Too large.
                Error.Fatal(context, "Invalid colour given (enum value is too large)");
                return null;
            }

            //Cast to integer.
            Console.ForegroundColor = (ConsoleColor)int.Parse(((BigInteger)args[0].Value).ToString());
            return AlgoValue.Null;
        }

        //Check if a file exists.
        public static AlgoValue DoesFileExist(ParserRuleContext context, params AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Argument for file must be a string.");
                return null;
            }

            //Check whether file exists.
            if (File.Exists((string)args[0].Value))
            {
                return AlgoValue.True;
            }

            return AlgoValue.False;
        }

        //Create a file at a given path.
        public static AlgoValue CreateFile(ParserRuleContext context, params AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Name of file to create must be a string.");
                return null;
            }

            //Attempt to create a file.
            try
            {
                File.WriteAllText((string)args[0].Value, "");
                return AlgoValue.True;
            }
            catch(Exception e)
            {
                Error.Warning(context, "Failed to create file '" + (string)args[0].Value + "' (" + e.Message + ").");
                return AlgoValue.False;
            }
        }
    }
}
