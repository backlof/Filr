using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilrLibrary
{
	public class MatchingFileEnumerator : IMatchingFileEnumerator
	{
		private readonly IFileFilterer _fileFilterer;

		public MatchingFileEnumerator(IFileFilterer fileFilterer)
		{
			_fileFilterer = fileFilterer;
		}

		public IEnumerable<FileInfo> EnumerateMatchingFiles(string location, FileEnumeratorCriteria fileHandlerOptions)
		{
			return new DirectoryInfo(location)
				.EnumerateFiles("*", fileHandlerOptions.IncludeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				.Where(x => _fileFilterer.Filter(new FileData { FileName = x.Name, Extension = x.Extension, FullPath = x.FullName, ByteSize = x.Length }, fileHandlerOptions));
		}
	}
}