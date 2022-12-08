//-----------------------------------------------------------------------
// <copyright file="Day07.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using System.Text.RegularExpressions;
    using AdventOfCode2022.Models;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day07 : IDay
    {
        private const int MaxTotalSize = 100000;

        private static readonly Regex ListFileRegex = new(@"^(?<SIZE>\d+) (?<NAME>\S+)$", RegexOptions.Compiled);
        private static readonly Regex ListDirectoryRegex = new(@"^dir (?<NAME>\w+)$", RegexOptions.Compiled);
        private static readonly Regex ChangeDirectoryRegex = new(@"^\$ cd (?<DIRECTORY>((\w+)|(\.\.)|(\/)))$", RegexOptions.Compiled);

        private readonly Dictionary<Regex, Action<Match>> listActionMap;
        private readonly Dictionary<Regex, Action<Match>> changeDirectoryMap;

        private readonly ElfDirectory root = new("/");
        private ElfDirectory currentDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Day07"/> class.
        /// </summary>
        public Day07()
        {
            this.currentDirectory = this.root;
            this.listActionMap = new()
            {
                {
                    // 14848514 b.txt
                    ListFileRegex,
                    this.AddFile
                },
                {
                    // dir d
                    ListDirectoryRegex,
                    this.AddDirectory
                },
            };

            this.changeDirectoryMap = new()
            {
                {
                    // $ cd /
                    // $ cd a
                    // $ cd ..
                    ChangeDirectoryRegex,
                    this.ChangeDirectory
                },
            };
        }

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day07.txt");

            bool listing = false;

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "$ ls")
                {
                    listing = true;
                    continue;
                }

                if (listing)
                {
                    if (ExecuteActions(this.listActionMap, line))
                    {
                        continue;
                    }
                }

                listing = false;
                ExecuteActions(this.changeDirectoryMap, line);
            }

            int runningSum = 0;
            GetTotalSizeOfDirectories(this.root, ref runningSum);
            return runningSum.ToString();
        }

        private static void GetTotalSizeOfDirectories(ElfDirectory current, ref int runningSum)
        {
            int totalSize = current.TotalSize;
            if (totalSize < MaxTotalSize)
            {
                runningSum += totalSize;
            }

            if (!current.HasSubDirectories)
            {
                return;
            }

            foreach (ElfDirectory subDirectory in current.SubDirectores)
            {
                GetTotalSizeOfDirectories(subDirectory, ref runningSum);
            }
        }

        private static bool ExecuteActions(Dictionary<Regex, Action<Match>> actionMap, string line)
        {
            foreach (var kvp in actionMap)
            {
                Match match = kvp.Key.Match(line);
                if (match.Success)
                {
                    kvp.Value(match);
                    return true;
                }
            }

            return false;
        }

        private void AddFile(Match match)
        {
            string fileName = match.Groups["NAME"].Value;
            if (!int.TryParse(match.Groups["SIZE"].Value, out int fileSize))
            {
                throw new InvalidDataException();
            }

            ElfFile file = new(fileName, fileSize);
            this.currentDirectory.AddFile(file);
        }

        private void AddDirectory(Match match)
        {
            string dirName = match.Groups["NAME"].Value;
            ElfDirectory dir = new(dirName, this.currentDirectory);
            this.currentDirectory.AddSubDirectory(dir);
        }

        private void ChangeDirectory(Match match)
        {
            string directory = match.Groups["DIRECTORY"].Value;
            if (directory == "/")
            {
                // $ cd /
                this.currentDirectory = this.root;
                return;
            }

            if (directory == "..")
            {
                // $ cd ..
                if (this.currentDirectory.Parent == null)
                {
                    throw new InvalidDataException();
                }

                this.currentDirectory = this.currentDirectory.Parent;
                return;
            }

            // $ cd d
            if (!this.currentDirectory.HasSubDirectory(directory)
                || !this.currentDirectory.TryGetSubDirectory(directory, out ElfDirectory? subDirectory))
            {
                throw new InvalidDataException();
            }

            this.currentDirectory = subDirectory;
        }
    }
}
