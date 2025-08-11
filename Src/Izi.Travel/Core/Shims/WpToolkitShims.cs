// Minimal WP Toolkit shims to allow compiling behaviors that referenced WP8 controls.
// These are placeholders; behaviors relying on these should be migrated to UWP controls.
using System;
using System.Collections;

namespace Microsoft.Phone.Controls
{
    public enum LongListSelectorItemKind { Item, GroupHeader, ListHeader, ListFooter }

    public class LongListSelectorItem
    {
        public object Content { get; set; }
    }

    public class ItemRealizationEventArgs : EventArgs
    {
        public LongListSelectorItemKind ItemKind { get; set; }
        public LongListSelectorItem Container { get; set; }
    }

    public class LongListSelector
    {
        public IList ItemsSource { get; set; }
        public event EventHandler<ItemRealizationEventArgs> ItemRealized;

        // Helper to raise in tests; not used at runtime.
        public void RaiseItemRealized(ItemRealizationEventArgs args) => ItemRealized?.Invoke(this, args);
    }
}
