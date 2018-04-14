using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using FilrLibrary;
using Ninject;

namespace Filr
{
	public class Program
	{
		static void Main(string[] args)
		{
			new GenericCommandLineParser<CommandLineOptions>().RunWithArguments(args, (options) =>
			{
				if (Directory.Exists(options.DirectoryPath))
				{
					var _kernel = new StandardKernel();
					_kernel.Bind<IFileFilterer>().To<FileFilterer>();
					_kernel.Bind<IMatchingFileEnumerator>().To<MatchingFileEnumerator>();
					_kernel.Bind<IRandomFileFinder>().To<RandomFileFinder>();

					var fileInfo = _kernel.Get<IRandomFileFinder>().GetRandomFile(options.DirectoryPath, new FileEnumeratorCriteria
					{
						IncludeSubdirectories = options.IncludeSubdirectories,
						Exclude = options.Exclude == null ? null : options.Exclude.ToList(),
						Extensions = options.FileExtensions == null ? null : options.FileExtensions.Select(extension => $".{extension}").ToList(),
						MaxByteSize = options.MaxSizeInMegabytes == null ? null : (long?)(options.MaxSizeInMegabytes * Math.Pow(1024, 2)),
						MinByteSize = options.MinSizeInMegabytes == null ? null : (long?)(options.MinSizeInMegabytes * Math.Pow(1024, 2))
					});

					if (fileInfo == null)
					{
						Console.WriteLine("There is no file matching the criterias");
						Console.ReadLine();
					}
					else
					{
						System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{fileInfo.FullName}\"");
						System.Diagnostics.Process.Start(fileInfo.FullName);
					}
				}
				else
				{
					Console.WriteLine("There is no such directory");
					Debugger.Break();
					Console.ReadLine();
				}
			});
		}
	}
}
