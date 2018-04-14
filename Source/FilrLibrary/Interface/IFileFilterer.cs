namespace FilrLibrary
{
	public interface IFileFilterer
	{
		bool Filter(FileData data, FileEnumeratorCriteria options);
	}
}