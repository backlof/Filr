using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommandLine;

namespace Filr
{
	public class GenericCommandLineParser<T>
	{
		public GenericCommandLineParser()
		{

		}

		public void RunWithArguments(string[] args, Action<T> action)
		{
			var result = CommandLine.Parser.Default.ParseArguments<T>(args);
			result.WithParsed(action);
			result.WithNotParsed(OnParseFail);
		}

		public void GenerateArguments(T options)
		{
			Console.WriteLine(CommandLine.Parser.Default.FormatCommandLine<T>(options));
			Console.ReadLine();
		}

		public void OnParseFail(IEnumerable<Error> errors)
		{
			Debugger.Break();
		}
	}
}
