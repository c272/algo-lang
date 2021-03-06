﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;

namespace Algo
{
    //A class for managing function plugins in Algo.
    public class AlgoFunctionPlugins
    {
        //The list of loaded plugins.
        public List<IFunctionPlugin> Plugins = new List<IFunctionPlugin>();

        //On construct, add all the standard libraries as loaded function plugins.
        //Also, load in all plugins in the "/packages/" directory (and all subdirectories).
        public AlgoFunctionPlugins()
        {
            //Add standard libraries.
            //string.*
            Plugins.Add(new StandardLibrary.AlgoStd_String());
            //*.* (core)
            Plugins.Add(new StandardLibrary.AlgoStd_Core());
            //input.* and output.* (io)
            Plugins.Add(new StandardLibrary.AlgoStd_IO());
            //web.* 
            Plugins.Add(new StandardLibrary.AlgoStd_Web());
            //json.* 
            Plugins.Add(new StandardLibrary.AlgoStd_JSON());
            //*.* (reflection)
            Plugins.Add(new StandardLibrary.AlgoStd_Reflection());
            //random.* (maths)
            Plugins.Add(new StandardLibrary.AlgoStd_Maths());

            //Are there any plugin files in the /packages/ directory?
            string[] files = null;
            string[] dirs = null;
            try
            {
                files = Directory.GetFiles(CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory));
                dirs = Directory.GetDirectories(CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory));
            }
            catch
            {
                //Failed to load, this is likely a compiled ALEC executable. Don't bother doing any more.
                return;
            }

            List<string> dllFiles = new List<string>();
            foreach (var file in files)
            {
                if (file.Contains(".dll"))
                {
                    dllFiles.Add(file);
                }
            }

            //Also search one directory deeper.
            foreach (var dir in dirs)
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                FileInfo[] dirFiles = d.GetFiles();
                foreach (var file in dirFiles)
                {
                    if (file.Extension == ".dll")
                    {
                        dllFiles.Add(file.FullName);
                    }
                }
            }

            //Attempt to load all the DLL files.
            foreach (var file in dllFiles)
            {
                LoadPlugin(file);
            }
        }

        //Load a plugin from an assembly path.
        public void LoadPlugin(string filePath)
        {
            //Check if the assembly exists.
            if (!File.Exists(filePath))
            {
                Error.WarningNoContext("Could not load plugin with path '" + filePath + "', path does not exist.");
                return;
            }

            //Is it a DLL file?
            FileInfo fi = new FileInfo(filePath);
            if (fi.Extension != ".dll")
            {
                Error.WarningNoContext("Could not load plugin with path '" + filePath + "', file is not an assembly.");
                return;
            }

            //It is, load the DLL file into memory using reflection.
            Assembly loadedAssembly = null;
            try
            {
                loadedAssembly = Assembly.LoadFrom(filePath);
            }
            catch (Exception e)
            {
                Error.WarningNoContext("Could not load plugin with path '" + filePath + "', error loading assembly.\nError:\n" + e.Message);
                return;
            }

            //Skim through all the classes in the assembly and find those that inherit from IFunctionPlugin.
            var funcInterface = typeof(IFunctionPlugin);
            var types = loadedAssembly.GetTypes()
                                      .Where(p => funcInterface.IsAssignableFrom(p));


            //Instantiate a copy of each of these types.
            var pluginsToAdd = new List<IFunctionPlugin>();
            try
            {
                foreach (var type in types)
                {
                    pluginsToAdd.Add((IFunctionPlugin)Activator.CreateInstance(type));
                }
            }
            catch
            {
                Error.WarningNoContext("Plugin with file path '" + filePath + "' contains a broken or outdated plugin class, and cannot be loaded. Contact the plugin developer about this issue.");
                return;
            }

            //Add the plugins to the main list.
            Plugins.AddRange(pluginsToAdd);
        }

        //Checks if a plugin with the given name exists.
        public bool PluginExists(string name)
        {
            return (Plugins.FindIndex(x => x.Name == name) != -1);
        }

        //Return a plugin with the given name.
        public IFunctionPlugin GetPlugin(string name)
        {
            if (!PluginExists(name))
            {
                Error.FatalNoContext("A plugin with the name '" + name + "' does not exist, so could not return.");
                return null;
            }

            return Plugins.Find(x => x.Name == name);
        }

        //Get an AlgoValue of type EmulatedFunction by supplying an external function load context.
        public AlgoValue GetEmulatedFuncValue(algoParser.Stat_loadFuncExtContext context)
        {
            //Attempt to get the text (as array) of the class and function from obj_access.
            if (context.particle().Length != 1)
            {
                Error.Fatal(context, "Import function is invalid, must be in the form \"Class.Function\".");
                return null;
            }

            string className = context.IDENTIFIER()[1].GetText();
            string funcName = context.particle()[0].IDENTIFIER().GetText();

            //Attempt to grab the plugin from the plugins manager.
            if (!PluginExists(className))
            {
                Error.Fatal(context, "Plugin name '" + className + "' is invalid or not loaded.");
                return null;
            }
            IFunctionPlugin classPlugin = GetPlugin(className);

            //Does the plugin contain a function with the given name?
            if (classPlugin.Functions.FindIndex(x => x.Name == funcName) == -1)
            {
                Error.Fatal(context, "Plugin '" + className + "' does not contain a function of name '" + funcName + "', so cannot load it.");
                return null;
            }

            //Return an EmulatedFunction AlgoValue.
            return new AlgoValue()
            {
                Type = AlgoValueType.EmulatedFunction,
                Value = classPlugin.Functions.Find(x => x.Name == funcName)
            };
        }
    }
}
