using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Algo.CLI
{
    /// <summary>
    /// All possible options and flags for the inline CLI.
    /// </summary>
    public class InlineCLIOptions
    {
        [Option("dev", Default = false, HelpText = "Enables developer mode, with more advanced language debugging features.")]
        public bool DeveloperMode { get; set; }

        [Option("nohead", Default = false, HelpText = "Disables the default header on the Algo interpreter.")]
        public bool NoHeader { get; set; }

        [Option('c', "compile", Default = null, HelpText = "Specifies an Algo script file to compile to a standalone executable.")]
        public string Compile { get; set; }

        [Option('v', "version", Default = false, HelpText = "Displays the version text for this version of Algo, then terminates.")]
        public bool ShowVersionOnly { get; set; }

        [Value(0, Default = null, HelpText = "The Algo script file to run with the interpreter.")]
        public string ScriptFile { get; set; }
    }

    /// <summary>
    /// All CLI options for the package manager verb.
    /// </summary>
    [Verb("pkg", HelpText = "Use the package manager for Algo to install/manage your local packages and sources.")]
    public class PackageManagerCLIOptions
    {
        [Option("help", Default = false, HelpText = "Displays help for the Sharpie package manager.")]
        public bool Help { get; set; }

        [Option('l', "list", Default = false, HelpText = "Lists all current packages installed locally.")]
        public bool ListPackages { get; set; }

        [Option('s', "sources", Default = false, HelpText = "Lists all sources currently on the Sharpie master list.")]
        public bool ListSources { get; set; }

        [Option("addsrc", Default = null, HelpText = "Adds a comma delimited list of sources to the package manager's master source list.", Separator = ',')]
        public IEnumerable<string> AddSources { get; set; }

        [Option("removesrc", Default = null, HelpText = "Removes a comma delimited list of sources (by name) from the package manager.", Separator = ',')]
        public IEnumerable<string> RemoveSources { get; set; }

        [Option('i', "install", HelpText = "Installs a comma delimited list of packages (by name) to your local package source.")]
        public IEnumerable<string> AddPackages { get; set; }

        [Option('r', "remove", HelpText = "Removes a comma delimited list of packages (by name) from your local package source.")]
        public IEnumerable<string> RemovePackages { get; set; }

        [Option('u', "update", HelpText = "Updates a comma delimited list of packages (by name) that are in your local package source.")]
        public IEnumerable<string> UpdatePackages { get; set; }
    }
}
