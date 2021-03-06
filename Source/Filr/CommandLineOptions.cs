﻿using CommandLine;
using System.Collections.Generic;

namespace Filr
{
	public class CommandLineOptions
	{
		[Option('d', "dir", HelpText = "Directory to find files in.", Required = true)]
		public string DirectoryPath { get; set; }

		[Option("min", HelpText = "Minimum file size in MB.", Required = false)]
		public int? MinSizeInMegabytes { get; set; }

		[Option("max", HelpText = "Maximum file size in MB.", Required = false)]
		public int? MaxSizeInMegabytes { get; set; }

		[Option("exc", HelpText = "Exclude filenames with terms.", Required = false, Separator = ',')]
		public IEnumerable<string> Exclude { get; set; }

		[Option("ext", HelpText = "File extensions to include.", Required = false, Separator = ',')]
		public IEnumerable<string> FileExtensions { get; set; }

		[Option('i', "sub", Default = false, HelpText = "Include subdirectories.", Required = false)]
		public bool IncludeSubdirectories { get; set; }

		[Option('s', "exp", Default = false, HelpText = "Show file in file explorer.", Required = false)]
		public bool ShowFileInExplorer { get; set; }
	}
}