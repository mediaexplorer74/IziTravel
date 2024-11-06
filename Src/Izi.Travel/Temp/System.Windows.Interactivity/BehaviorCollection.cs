// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.BehaviorCollection
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents a collection of behaviors with a shared AssociatedObject and provides change notifications to its contents when that AssociatedObject changes.
  /// </summary>
  public sealed class BehaviorCollection : AttachableCollection<Behavior>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.BehaviorCollection" /> class.
    /// </summary>
    /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
    internal BehaviorCollection()
    {
    }

    /// <summary>
    /// Called immediately after the collection is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached()
    {
      foreach (Behavior behavior in (DependencyObjectCollection<Behavior>) this)
        behavior.Attach(this.AssociatedObject);
    }

    /// <summary>
    /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected override void OnDetaching()
    {
      foreach (Behavior behavior in (DependencyObjectCollection<Behavior>) this)
        behavior.Detach();
    }

    /// <summary>Called when a new item is added to the collection.</summary>
    /// <param name="item">The new item.</param>
    internal override void ItemAdded(Behavior item)
    {
      if (this.AssociatedObject == null)
        return;
      item.Attach(this.AssociatedObject);
    }

    /// <summary>Called when an item is removed from the collection.</summary>
    /// <param name="item">The removed item.</param>
    internal override void ItemRemoved(Behavior item)
    {
      if (((IAttachedObject) item).AssociatedObject == null)
        return;
      item.Detach();
    }
  }
}
