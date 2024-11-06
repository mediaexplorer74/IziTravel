// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapItemsSourceChangeManager
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Specialized;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  internal class MapItemsSourceChangeManager : CollectionChangeListener<object>
  {
    public MapItemsSourceChangeManager(INotifyCollectionChanged sourceCollection)
    {
      this.SourceCollection = sourceCollection;
      this.SourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
    }

    public MapChildCollection Items { get; set; }

    private INotifyCollectionChanged SourceCollection { get; set; }

    public void Disconnect()
    {
      this.SourceCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
      this.SourceCollection = (INotifyCollectionChanged) null;
    }

    protected override void InsertItemInternal(int index, object obj)
    {
      this.Items.InsertInternal(index, obj);
    }

    protected override void RemoveItemInternal(object obj)
    {
      this.Items.RemoveInternal(this.Items.IndexOf(obj));
    }

    protected override void ResetInternal() => this.Items.ClearInternal();

    protected override void AddInternal(object obj) => this.Items.AddInternal(obj);

    protected override void MoveInternal(object obj, int newIndex)
    {
      this.Items.MoveInternal(this.Items.IndexOf(obj), newIndex);
    }
  }
}
