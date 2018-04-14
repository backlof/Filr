using Extensions;
using System.IO;

namespace FilrLibrary
{
	public class RandomFileFinder : IRandomFileFinder
	{
		private readonly IMatchingFileEnumerator _fileEnumerator;

		public RandomFileFinder(IMatchingFileEnumerator fileEnumerator)
		{
			_fileEnumerator = fileEnumerator;
		}

		public FileInfo GetRandomFile(string location, FileEnumeratorCriteria fileHandlerOptions)
		{
			return _fileEnumerator
				.EnumerateMatchingFiles(location, fileHandlerOptions)
				.Random();
		}
	}
}