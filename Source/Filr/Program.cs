using System;
using System.Diagnostics;
using System.IO;

namespace Filr
{
	public class Program
	{
		private static void Main(string[] args)
		{
			CommandLineParser.Run<CommandLineOptions>(args, (options) =>
			{
				if (Directory.Exists(options.DirectoryPath))
				{
					ApplicationContainer.Run(options, (fileInfo) =>
					{
						if (fileInfo == null)
						{
							Console.WriteLine("There is no file matching the criterias");
							Console.ReadLine();
						}
						else
						{
							if (options.ShowFileInExplorer)
							{
								System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{fileInfo.FullName}\"");
							}
							System.Diagnostics.Process.Start(fileInfo.FullName);
						}
					});
				}
				else
				{
					Console.WriteLine("There is no such directory");
#if DEBUG
					Debugger.Break();
#endif
					Console.ReadLine();
				}
			});
		}
	}
}