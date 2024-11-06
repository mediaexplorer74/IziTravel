// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TargetedTriggerAction
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Globalization;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents an action that can be targeted to affect an object other than its AssociatedObject.
  /// </summary>
  /// <remarks>This is an infrastructure class. Action authors should derive from TargetedTriggerAction&lt;T&gt; instead of this class.</remarks>
  public abstract class TargetedTriggerAction : TriggerAction
  {
    private Type targetTypeConstraint;
    private bool isTargetChangedRegistered;
    private NameResolver targetResolver;
    public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(nameof (TargetObject), typeof (object), typeof (TargetedTriggerAction), new PropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetObjectChanged)));
    public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(nameof (TargetName), typeof (string), typeof (TargetedTriggerAction), new PropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetNameChanged)));

    /// <summary>
    /// Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by TargetName. If an element referred to by TargetName cannot be found, the target will default to the AssociatedObject. This is a dependency property.
    /// </summary>
    /// <value>The target object.</value>
    public object TargetObject
    {
      get => this.GetValue(TargetedTriggerAction.TargetObjectProperty);
      set => this.SetValue(TargetedTriggerAction.TargetObjectProperty, value);
    }

    /// <summary>
    /// Gets or sets the name of the object this action targets. If Target is set, this property is ignored. If Target is not set and TargetName is not set or cannot be resolved, the target will default to the AssociatedObject. This is a dependency property.
    /// </summary>
    /// <value>The name of the target object.</value>
    public string TargetName
    {
      get => (string) this.GetValue(TargetedTriggerAction.TargetNameProperty);
      set => this.SetValue(TargetedTriggerAction.TargetNameProperty, (object) value);
    }

    /// <summary>
    /// Gets the target object. If TargetObject is set, returns TargetObject. Else, if TargetName is not set or cannot be resolved, defaults to the AssociatedObject.
    /// </summary>
    /// <value>The target object.</value>
    /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
    /// <exception cref="T:System.InvalidOperationException">The Target element does not satisfy the type constraint.</exception>
    protected object Target
    {
      get
      {
        object obj = (object) this.AssociatedObject;
        if (this.TargetObject != null)
          obj = this.TargetObject;
        else if (this.IsTargetNameSet)
          obj = (object) this.TargetResolver.Object;
        return obj == null || this.TargetTypeConstraint.IsAssignableFrom(obj.GetType()) ? obj : throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage, (object) this.GetType().Name, (object) obj.GetType(), (object) this.TargetTypeConstraint, (object) nameof (Target)));
      }
    }

    /// <summary>Gets the associated object type constraint.</summary>
    /// <value>The associated object type constraint.</value>
    /// <remarks>Define a TypeConstraintAttribute on a derived type to constrain the types it may be attached to.</remarks>
    protected override sealed Type AssociatedObjectTypeConstraint
    {
      get
      {
        object[] customAttributes = this.GetType().GetCustomAttributes(typeof (TypeConstraintAttribute), true);
        int index = 0;
        return index < customAttributes.Length ? ((TypeConstraintAttribute) customAttributes[index]).Constraint : typeof (DependencyObject);
      }
    }

    /// <summary>Gets the target type constraint.</summary>
    /// <value>The target type constraint.</value>
    protected Type TargetTypeConstraint => this.targetTypeConstraint;

    private bool IsTargetNameSet
    {
      get
      {
        return !string.IsNullOrEmpty(this.TargetName) || this.ReadLocalValue(TargetedTriggerAction.TargetNameProperty) != DependencyProperty.UnsetValue;
      }
    }

    private NameResolver TargetResolver => this.targetResolver;

    private bool IsTargetChangedRegistered
    {
      get => this.isTargetChangedRegistered;
      set => this.isTargetChangedRegistered = value;
    }

    internal TargetedTriggerAction(Type targetTypeConstraint)
      : base(typeof (DependencyObject))
    {
      this.targetTypeConstraint = targetTypeConstraint;
      this.targetResolver = new NameResolver();
      this.RegisterTargetChanged();
    }

    /// <summary>Called when the target changes.</summary>
    /// <param name="oldTarget">The old target.</param>
    /// <param name="newTarget">The new target.</param>
    /// <remarks>This function should be overriden in derived classes to hook and unhook functionality from the changing source objects.</remarks>
    internal virtual void OnTargetChangedImpl(object oldTarget, object newTarget)
    {
    }

    /// <summary>
    /// Called after the action is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached()
    {
      base.OnAttached();
      DependencyObject associatedObject = this.AssociatedObject;
      Behavior behavior = associatedObject as Behavior;
      this.RegisterTargetChanged();
      if (behavior != null)
      {
        associatedObject = ((IAttachedObject) behavior).AssociatedObject;
        behavior.AssociatedObjectChanged += new EventHandler(this.OnBehaviorHostChanged);
      }
      this.TargetResolver.NameScopeReferenceElement = associatedObject as FrameworkElement;
    }

    /// <summary>
    /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected override void OnDetaching()
    {
      Behavior associatedObject = this.AssociatedObject as Behavior;
      base.OnDetaching();
      this.OnTargetChangedImpl((object) this.TargetResolver.Object, (object) null);
      this.UnregisterTargetChanged();
      if (associatedObject != null)
        associatedObject.AssociatedObjectChanged -= new EventHandler(this.OnBehaviorHostChanged);
      this.TargetResolver.NameScopeReferenceElement = (FrameworkElement) null;
    }

    private void OnBehaviorHostChanged(object sender, EventArgs e)
    {
      this.TargetResolver.NameScopeReferenceElement = ((IAttachedObject) sender).AssociatedObject as FrameworkElement;
    }

    private void RegisterTargetChanged()
    {
      if (this.IsTargetChangedRegistered)
        return;
      this.TargetResolver.ResolvedElementChanged += new EventHandler<NameResolvedEventArgs>(this.OnTargetChanged);
      this.IsTargetChangedRegistered = true;
    }

    private void UnregisterTargetChanged()
    {
      if (!this.IsTargetChangedRegistered)
        return;
      this.TargetResolver.ResolvedElementChanged -= new EventHandler<NameResolvedEventArgs>(this.OnTargetChanged);
      this.IsTargetChangedRegistered = false;
    }

    private static void OnTargetObjectChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      ((TargetedTriggerAction) obj).OnTargetChanged((object) obj, new NameResolvedEventArgs(args.OldValue, args.NewValue));
    }

    private static void OnTargetNameChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      ((TargetedTriggerAction) obj).TargetResolver.Name = (string) args.NewValue;
    }

    private void OnTargetChanged(object sender, NameResolvedEventArgs e)
    {
      if (this.AssociatedObject == null)
        return;
      this.OnTargetChangedImpl(e.OldObject, e.NewObject);
    }
  }
}
