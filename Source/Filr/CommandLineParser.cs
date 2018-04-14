using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommandLine;

namespace Filr
{
	public class CommandLineParser
	{
		public CommandLineParser()
		{

		}

		public static void Run<T>(string[] args, Action<T> action)
		{
			(new CommandLineParser()).RunWithArguments(args, action);
		}

		public void RunWithArguments<T>(string[] args, Action<T> action)
		{
			var result = CommandLine.Parser.Default.ParseArguments<T>(args);
			result.WithParsed(action);
			result.WithNotParsed(OnParseFail);
		}

		public static void FormatArguments<T>(T options)
		{
			System.Diagnostics.Debug.WriteLine(CommandLine.Parser.Default.FormatCommandLine<T>(options));
		}

		public void OnParseFail(IEnumerable<Error> errors)
		{
#if DEBUG
			Debugger.Break();
#endif
			Console.ReadLine();
		}
	}
}
