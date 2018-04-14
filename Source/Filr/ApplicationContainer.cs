using FilrLibrary;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filr
{
	class ApplicationContainer : IDisposable
	{
		private readonly IKernel _kernel;


		public ApplicationContainer()
		{
			_kernel = new StandardKernel();
			_kernel.Bind<IFileFilterer>().To<FileFilterer>();
			_kernel.Bind<IMatchingFileEnumerator>().To<MatchingFileEnumerator>();
			_kernel.Bind<IRandomFileFinder>().To<RandomFileFinder>();
		}

		private void InternalRun(CommandLineOptions options, Action<FileInfo> action)
		{
			action(_kernel.Get<IRandomFileFinder>().GetRandomFile(options.DirectoryPath, new FileEnumeratorCriteria
			{
				IncludeSubdirectories = options.IncludeSubdirectories,
				Exclude = options.Exclude == null ? null : options.Exclude.ToList(),
				Extensions = options.FileExtensions == null ? null : options.FileExtensions.Select(extension => $".{extension}").ToList(),
				MaxByteSize = options.MaxSizeInMegabytes == null ? null : (long?)(options.MaxSizeInMegabytes * Math.Pow(1024, 2)),
				MinByteSize = options.MinSizeInMegabytes == null ? null : (long?)(options.MinSizeInMegabytes * Math.Pow(1024, 2))
			}));
		}

		public static void Run(CommandLineOptions options, Action<FileInfo> action)
		{
			using (var application = new ApplicationContainer())
			{
				application.InternalRun(options, action);
			}
		}

		public void Dispose()
		{
			_kernel.Dispose();
		}
	}
}
