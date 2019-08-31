using System.Collections.Generic;

namespace Game.Estate.Ultima.Data
{
    public class ContextMenuData
    {
        readonly List<ContextMenuItem> _entries = new List<ContextMenuItem>();

        public ContextMenuData(Serial serial) => Serial = serial;

        public Serial Serial { get; }

        public int Count => _entries.Count;

        public ContextMenuItem this[int index] => index < 0 || index >= _entries.Count ? null : _entries[index];

        // Add a new context menu entry.
        internal void AddItem(int responseCode, int stringID, int flags, int hue) => _entries.Add(new ContextMenuItem(responseCode, stringID, flags, hue));
    }
}