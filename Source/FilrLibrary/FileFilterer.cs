using System.Linq;

namespace FilrLibrary
{
	public class FileFilterer : IFileFilterer
	{
		public FileFilterer()
		{

		}

		public bool Filter(FileData fileInfo, FileEnumeratorCriteria options)
		{
			if (options.Extensions != null && options.Extensions.All(extension => extension != fileInfo.Extension))
			{
				return false;
			}
			if (options.MinByteSize != null && fileInfo.ByteSize < options.MinByteSize)
			{
				return false;
			}
			if (options.MaxByteSize != null && fileInfo.ByteSize > options.MaxByteSize)
			{
				return false;
			}
			if (options.Exclude != null && options.Exclude.Any(excludeTerm => fileInfo.FileName.Contains(excludeTerm)))
			{
				return false;
			}

			return true;
		}
	}
}
