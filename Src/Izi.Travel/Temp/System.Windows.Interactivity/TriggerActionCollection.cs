// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TriggerActionCollection
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents a collection of actions with a shared AssociatedObject and provides change notifications to its contents when that AssociatedObject changes.
  /// </summary>
  public class TriggerActionCollection : AttachableCollection<TriggerAction>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerActionCollection" /> class.
    /// </summary>
    /// <remarks>Internal, because this should not be inherited outside this assembly.</remarks>
    internal TriggerActionCollection()
    {
    }

    /// <summary>
    /// Called immediately after the collection is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached()
    {
      foreach (TriggerAction triggerAction in (DependencyObjectCollection<TriggerAction>) this)
        triggerAction.Attach(this.AssociatedObject);
    }

    /// <summary>
    /// Called when the collection is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected override void OnDetaching()
    {
      foreach (TriggerAction triggerAction in (DependencyObjectCollection<TriggerAction>) this)
        triggerAction.Detach();
    }

    /// <summary>Called when a new item is added to the collection.</summary>
    /// <param name="item">The new item.</param>
    internal override void ItemAdded(TriggerAction item)
    {
      if (item.IsHosted)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
      if (this.AssociatedObject != null)
        item.Attach(this.AssociatedObject);
      item.IsHosted = true;
    }

    /// <summary>Called when an item is removed from the collection.</summary>
    /// <param name="item">The removed item.</param>
    internal override void ItemRemoved(TriggerAction item)
    {
      if (((IAttachedObject) item).AssociatedObject != null)
        item.Detach();
      item.IsHosted = false;
    }
  }
}
