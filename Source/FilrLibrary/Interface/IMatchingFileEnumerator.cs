using System.Collections.Generic;
using System.IO;

namespace FilrLibrary
{
	public interface IMatchingFileEnumerator
	{
		IEnumerable<FileInfo> EnumerateMatchingFiles(string location, FileEnumeratorCriteria fileHandlerOptions);
	}
}