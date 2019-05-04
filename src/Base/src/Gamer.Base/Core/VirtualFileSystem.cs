using System.Collections.Generic;
using System.Diagnostics;

namespace Gamer.Base.Core
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
        public List<VirtualEntry> children = new List<VirtualEntry>();

        public VirtualDirectory(string name = "") : base(name) { }

        public void AddChild(VirtualEntry entry)
        {
            Debug.Assert(entry.Name != string.Empty);
            Debug.Assert(!ContainsChildEntry(entry.Name));
            entry.Parent = this;
            children.Add(entry);
        }

        public VirtualDirectory CreateChildDirectory(string directoryName)
        {
            var childDir = new VirtualDirectory(directoryName);
            AddChild(childDir);
            return childDir;
        }

        public VirtualFile CreateChildFile(string fileName)
        {
            var childFile = new VirtualFile(fileName);
            AddChild(childFile);
            return childFile;
        }

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

        public VirtualEntry FindChildEntry(string entryName) => children.Find(entry => entry.Name == entryName);

        public VirtualDirectory FindChildDirectory(string directoryName)
        {
            var childEntry = FindChildEntry(directoryName);
            return childEntry is VirtualDirectory d ? d : null;
        }

        public VirtualFile FindChildFile(string fileName)
        {
            var childEntry = FindChildEntry(fileName);
            return childEntry is VirtualFile f ? f : null;
        }

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
            for (var i = 0; i < children.Count; i++)
            {
                var child = children[i];
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