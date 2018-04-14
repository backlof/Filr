using System.IO;

namespace FilrLibrary
{
	public interface IRandomFileFinder
	{
		FileInfo GetRandomFile(string location, FileEnumeratorCriteria fileHandlerOptions);
	}
}