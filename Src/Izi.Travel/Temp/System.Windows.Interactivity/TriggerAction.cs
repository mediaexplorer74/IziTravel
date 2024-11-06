// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TriggerAction
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls.Primitives;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents an attachable object that encapsulates a unit of functionality.
  /// </summary>
  /// <remarks>This is an infrastructure class. Action authors should derive from TriggerAction&lt;T&gt; instead of this class.</remarks>
  [DefaultTrigger(typeof (ButtonBase), typeof (EventTrigger), "Click")]
  [DefaultTrigger(typeof (UIElement), typeof (EventTrigger), "MouseLeftButtonDown")]
  public abstract class TriggerAction : DependencyObject, IAttachedObject
  {
    private bool isHosted;
    private DependencyObject associatedObject;
    private Type associatedObjectTypeConstraint;
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof (IsEnabled), typeof (bool), typeof (TriggerAction), new PropertyMetadata((object) true));

    /// <summary>
    /// Gets or sets a value indicating whether this action will run when invoked. This is a dependency property.
    /// </summary>
    /// <value>
    /// 	<c>True</c> if this action will be run when invoked; otherwise, <c>False</c>.
    /// </value>
    [DefaultValue(true)]
    public bool IsEnabled
    {
      get => (bool) this.GetValue(TriggerAction.IsEnabledProperty);
      set => this.SetValue(TriggerAction.IsEnabledProperty, (object) value);
    }

    /// <summary>Gets the object to which this action is attached.</summary>
    /// <value>The associated object.</value>
    protected DependencyObject AssociatedObject => this.associatedObject;

    /// <summary>Gets the associated object type constraint.</summary>
    /// <value>The associated object type constraint.</value>
    protected virtual Type AssociatedObjectTypeConstraint => this.associatedObjectTypeConstraint;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is attached.
    /// </summary>
    /// <value><c>True</c> if this instance is attached; otherwise, <c>False</c>.</value>
    internal bool IsHosted
    {
      get => this.isHosted;
      set => this.isHosted = value;
    }

    internal TriggerAction(Type associatedObjectTypeConstraint)
    {
      this.associatedObjectTypeConstraint = associatedObjectTypeConstraint;
    }

    /// <summary>Attempts to invoke the action.</summary>
    /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
    internal void CallInvoke(object parameter)
    {
      if (!this.IsEnabled)
        return;
      this.Invoke(parameter);
    }

    /// <summary>Invokes the action.</summary>
    /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
    protected abstract void Invoke(object parameter);

    /// <summary>
    /// Called after the action is attached to an AssociatedObject.
    /// </summary>
    protected virtual void OnAttached()
    {
    }

    /// <summary>
    /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected virtual void OnDetaching()
    {
    }

    /// <summary>Gets the associated object.</summary>
    /// <value>The associated object.</value>
    DependencyObject IAttachedObject.AssociatedObject => this.AssociatedObject;

    /// <summary>Attaches to the specified object.</summary>
    /// <param name="dependencyObject">The object to attach to.</param>
    /// <exception cref="T:System.InvalidOperationException">Cannot host the same TriggerAction on more than one object at a time.</exception>
    /// <exception cref="T:System.InvalidOperationException">dependencyObject does not satisfy the TriggerAction type constraint.</exception>
    public void Attach(DependencyObject dependencyObject)
    {
      if (dependencyObject == this.AssociatedObject)
        return;
      if (this.AssociatedObject != null)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
      this.associatedObject = dependencyObject == null || this.AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()) ? dependencyObject : throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, (object) this.GetType().Name, (object) dependencyObject.GetType().Name, (object) this.AssociatedObjectTypeConstraint.Name));
      this.OnAttached();
    }

    /// <summary>Detaches this instance from its associated object.</summary>
    public void Detach()
    {
      this.OnDetaching();
      this.associatedObject = (DependencyObject) null;
    }
  }
}
