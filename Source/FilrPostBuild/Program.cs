namespace FilrPostBuild
{
	public class Program
	{
		private static void Main(string[] args)
		{
			// Main project has reference to this project
			// Post build runs this executable (in same folder - only in Release)

			// (1) Remove unnecessary files
			// (2) Run "FilrPostBuild.exe" with parameter pointing towards build directory
			// (3) Delete "ILRepack.dll"
			// (4) Delete "FilrPostBuild.exe"

			PostBuilder.Run(args[0], (builder) =>
			{
				builder.RemoveFilesWithExtensions(".pdb", ".xml", ".exe.config");
				builder.Repack();
			});
		}
	}
}