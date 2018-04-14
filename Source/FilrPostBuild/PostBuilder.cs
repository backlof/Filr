using System;
using System.IO;
using System.Linq;
using ILRepacking;

namespace FilrPostBuild
{
	public class PostBuilder
	{
		private readonly string _location;

		public PostBuilder(string location)
		{
			_location = location;
		}

		public static void Run(string location, Action<PostBuilder> action)
		{
			action(new PostBuilder(location));
		}

		public void Repack()
		{
			var executable = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.exe";

			var executables = Directory.GetFiles(_location)
				.Select(x => Path.GetFileName(x))
				.Where(x => x != "FilrPostBuild.exe")
				.Where(x => x.EndsWith(".exe"))
				.ToList();

			Console.WriteLine(string.Join(",", executables));

			var assemblies = Directory.GetFiles(_location)
				.Select(x => Path.GetFileName(x))
				.Where(x => x != "ILRepack.dll")
				.Where(x => x.EndsWith(".dll"))
				.ToList();

			Console.WriteLine(string.Join(",", assemblies));

			if (executables.Count != 1)
			{
				throw new ArgumentException();
			}
			if (!assemblies.Any())
			{
				throw new ArgumentException();
			}

			new ILRepack(new RepackOptions
			{
				Parallel = true,
				Internalize = true,
				InputAssemblies = executables.Concat(assemblies).ToArray(),
				AllowWildCards = false,
				TargetKind = ILRepack.Kind.Exe,
				SearchDirectories = new string[] { _location },
				OutputFile =  executables.First()

			}).Repack();

			foreach (var assembly in assemblies.Where(x => x != "ILRepack.dll").Select(x => Path.Combine(_location, x)))
			{
				File.Delete(assembly);
			}
		}

		public void RemoveFiles(params string[] paths)
		{
			foreach (var path in paths)
			{
				File.Delete(path);
			}
		}

		public void RemoveFilesWithExtensions(params string[] extensions)
		{
			foreach (var path in Directory.GetFiles(_location).Where(path => extensions.Any(extension => path.EndsWith(extension))))
			{
				File.Delete(path);
			}
		}
	}
}
