using System.Collections.Generic;

namespace FilrLibrary
{
	public class FileEnumeratorCriteria
	{
		public bool IncludeSubdirectories { get; set; }
		public long? MinByteSize { get; set; }
		public long? MaxByteSize { get; set; }
		public IReadOnlyCollection<string> Extensions { get; set; }
		public IReadOnlyCollection<string> Exclude { get; set; }
	}
}