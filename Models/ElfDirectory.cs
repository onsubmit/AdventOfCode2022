//-----------------------------------------------------------------------
// <copyright file="ElfDirectory.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///  Represents a directory on the elves' filesystem.
    /// </summary>
    internal class ElfDirectory
    {
        private readonly string name;
        private readonly List<ElfFile> files = new();
        private readonly Dictionary<string, ElfDirectory> subDirectories = new();

        private int? cachedTotalSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElfDirectory"/> class.
        /// </summary>
        /// <param name="name">Diorectory name.</param>
        /// <param name="parent">Parent directory.</param>
        internal ElfDirectory(string name, ElfDirectory? parent = null)
        {
            this.name = name;
            this.Parent = parent;
        }

        /// <summary>
        /// Gets the parent directory.
        /// </summary>
        public ElfDirectory? Parent { get; private set; }

        /// <summary>
        /// Gets the sub directories.
        /// </summary>
        public IEnumerable<ElfDirectory> SubDirectores => this.subDirectories.Values;

        /// <summary>
        /// Gets the total size.
        /// </summary>
        public int TotalSize
        {
            get
            {
                if (!this.cachedTotalSize.HasValue)
                {
                    this.cachedTotalSize = this.files.Sum(f => f.Size) + this.subDirectories.Values.Sum(s => s.TotalSize);
                }

                return this.cachedTotalSize.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the directory has sub directories.
        /// </summary>
        public bool HasSubDirectories => this.subDirectories.Any();

        /// <summary>
        /// Adds a sub directory.
        /// </summary>
        /// <param name="subDirectory">The sub directory to add.</param>
        public void AddSubDirectory(ElfDirectory subDirectory)
        {
            subDirectory.Parent = this;
            this.subDirectories.Add(subDirectory.name, subDirectory);
        }

        /// <summary>
        /// Adds a file.
        /// </summary>
        /// <param name="file">The file to add.</param>
        public void AddFile(ElfFile file)
        {
            this.files.Add(file);
        }

        /// <summary>
        /// Gets a value indicating whether the directory has a sub directory with the specified name.
        /// </summary>
        /// <param name="name">Sub directory name.</param>
        /// <returns><c>true</c> if the directory has a sub directory with the give name, <c>false</c> otherwise.</returns>
        public bool HasSubDirectory(string name) => this.subDirectories.ContainsKey(name);

        /// <summary>
        /// Gets the sub directory associated with the specified name.
        /// </summary>
        /// <param name="name">Sub directory name.</param>
        /// <param name="subDirectory">When this method returns, contains the sub directory associated with the specified name, if the key is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the sub directory was found, <c>false</c> otherwise.</returns>
        public bool TryGetSubDirectory(string name, [NotNullWhen(true)] out ElfDirectory? subDirectory) => this.subDirectories.TryGetValue(name, out subDirectory);
    }
}
