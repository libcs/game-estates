using System.Collections.Generic;
using static Gamer.Core.Debug;

namespace Gamer.Core
{
    public class VirtualEntry
    {
        public string Name;
        public VirtualDirectory Parent;

        public VirtualEntry(string name = "") => Name = name;

        public string GetAbsolutePath()
        {
            var absolutePath = Name;
            var curParent = Parent;
            while (curParent != null)
            {
                if (curParent.Name != string.Empty)
                    absolutePath = $"{curParent.Name}/{absolutePath}";
                curParent = curParent.Parent;
            }
            return absolutePath;
        }

        public override string ToString() => Name;
    }

    public class VirtualDirectory : VirtualEntry
    {
        public readonly List<VirtualEntry> Children = new List<VirtualEntry>();

        public VirtualDirectory(string name = "") : base(name) { }

        public VirtualEntry AddChild(VirtualEntry entry)
        {
            Assert(entry.Name != string.Empty);
            Assert(!ContainsChildEntry(entry.Name));
            entry.Parent = this;
            Children.Add(entry);
            return entry;
        }

        public VirtualDirectory CreateChildDirectory(string directoryName) => (VirtualDirectory)AddChild(new VirtualDirectory(directoryName));

        public VirtualFile CreateChildFile(string fileName) => (VirtualFile)AddChild(new VirtualFile(fileName));

        /// <summary>
        /// Adds a file to a descendant directory. Creates directories if necessary.
        /// </summary>
        /// <remarks>TODO: This doesn't properly handle invalid paths.</remarks>
        public void CreateDescendantFile(string descendantFilePath)
        {
            var firstPathSeparatorIndex = descendantFilePath.IndexOfAny(_pathSeparators);
            if (firstPathSeparatorIndex >= 0)
            {
                var childDirName = descendantFilePath.Substring(0, firstPathSeparatorIndex);
                var restOfDescendantFilePath = descendantFilePath.Substring(firstPathSeparatorIndex + 1);
                var childDir = FindChildDirectory(childDirName);
                if (childDir == null)
                    childDir = CreateChildDirectory(childDirName);
                childDir.CreateDescendantFile(restOfDescendantFilePath);
            }
            else CreateChildFile(descendantFilePath);
        }

        public VirtualEntry FindChildEntry(string entryName) => Children.Find(entry => entry.Name == entryName);

        public VirtualDirectory FindChildDirectory(string directoryName) => FindChildEntry(directoryName) is VirtualDirectory d ? d : null;

        public VirtualFile FindChildFile(string fileName) => FindChildEntry(fileName) is VirtualFile f ? f : null;

        public VirtualEntry[] FindDescendantEntries(string entryName)
        {
            var descendantEntries = new List<VirtualEntry>();
            FindDescendantEntries(entryName, descendantEntries);
            return descendantEntries.ToArray();
        }

        public bool ContainsChildEntry(string entryName) => FindChildEntry(entryName) != null;

        public bool ContainsChildDirectory(string directoryName) => FindChildDirectory(directoryName) != null;

        public bool ContainsChildFile(string fileName) => FindChildFile(fileName) != null;

        static char[] _pathSeparators = new char[] { '/', '\\' };

        void FindDescendantEntries(string entryName, List<VirtualEntry> descendantEntries)
        {
            var childEntry = FindChildEntry(entryName);
            if (childEntry != null)
                descendantEntries.Add(childEntry);
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child is VirtualDirectory d)
                    d.FindDescendantEntries(entryName, descendantEntries);
            }
        }
    }

    public class VirtualFile : VirtualEntry
    {
        public VirtualFile(string name = "") : base(name) { }
    }
}