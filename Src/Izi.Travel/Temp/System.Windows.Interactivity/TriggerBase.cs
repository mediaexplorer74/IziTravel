// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TriggerBase
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Globalization;
using System.Windows.Markup;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents an object that can invoke Actions conditionally.
  /// </summary>
  /// <remarks>This is an infrastructure class. Trigger authors should derive from Trigger&lt;T&gt; instead of this class.</remarks>
  [ContentProperty("Actions")]
  public abstract class TriggerBase : DependencyObject, IAttachedObject
  {
    private DependencyObject associatedObject;
    private Type associatedObjectTypeConstraint;
    public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(nameof (Actions), typeof (TriggerActionCollection), typeof (TriggerBase), (PropertyMetadata) null);

    internal TriggerBase(Type associatedObjectTypeConstraint)
    {
      this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
      TriggerActionCollection actionCollection = new TriggerActionCollection();
      this.SetValue(TriggerBase.ActionsProperty, (object) actionCollection);
    }

    /// <summary>Gets the object to which the trigger is attached.</summary>
    /// <value>The associated object.</value>
    protected DependencyObject AssociatedObject => this.associatedObject;

    /// <summary>Gets the type constraint of the associated object.</summary>
    /// <value>The associated object type constraint.</value>
    protected virtual Type AssociatedObjectTypeConstraint => this.associatedObjectTypeConstraint;

    /// <summary>Gets the actions associated with this trigger.</summary>
    /// <value>The actions associated with this trigger.</value>
    public TriggerActionCollection Actions
    {
      get => (TriggerActionCollection) this.GetValue(TriggerBase.ActionsProperty);
    }

    /// <summary>Event handler for registering to PreviewInvoke.</summary>
    public event EventHandler<PreviewInvokeEventArgs> PreviewInvoke;

    /// <summary>Invoke all actions associated with this trigger.</summary>
    /// <remarks>Derived classes should call this to fire the trigger.</remarks>
    protected void InvokeActions(object parameter)
    {
      if (this.PreviewInvoke != null)
      {
        PreviewInvokeEventArgs e = new PreviewInvokeEventArgs();
        this.PreviewInvoke((object) this, e);
        if (e.Cancelling)
          return;
      }
      foreach (TriggerAction action in (DependencyObjectCollection<TriggerAction>) this.Actions)
        action.CallInvoke(parameter);
    }

    /// <summary>
    /// Called after the trigger is attached to an AssociatedObject.
    /// </summary>
    protected virtual void OnAttached()
    {
    }

    /// <summary>
    /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected virtual void OnDetaching()
    {
    }

    /// <summary>Gets the associated object.</summary>
    /// <value>The associated object.</value>
    DependencyObject IAttachedObject.AssociatedObject => this.AssociatedObject;

    /// <summary>Attaches to the specified object.</summary>
    /// <param name="dependencyObject">The object to attach to.</param>
    /// <exception cref="T:System.InvalidOperationException">Cannot host the same trigger on more than one object at a time.</exception>
    /// <exception cref="T:System.InvalidOperationException">dependencyObject does not satisfy the trigger type constraint.</exception>
    public void Attach(DependencyObject dependencyObject)
    {
      if (dependencyObject == this.AssociatedObject)
        return;
      if (this.AssociatedObject != null)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerMultipleTimesExceptionMessage);
      this.associatedObject = dependencyObject == null || this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()) ? dependencyObject : throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, (object) this.GetType().Name, (object) dependencyObject.GetType().Name, (object) this.AssociatedObjectTypeConstraint.Name));
      this.Actions.Attach(dependencyObject);
      this.OnAttached();
    }

    /// <summary>Detaches this instance from its associated object.</summary>
    public void Detach()
    {
      this.OnDetaching();
      this.associatedObject = (DependencyObject) null;
      this.Actions.Detach();
    }
  }
}
