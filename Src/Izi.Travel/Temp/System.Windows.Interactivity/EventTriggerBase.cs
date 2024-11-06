// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.EventTriggerBase
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Globalization;
using System.Reflection;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents a trigger that can listen to an object other than its AssociatedObject.
  /// </summary>
  /// <remarks>This is an infrastructure class. Trigger authors should derive from EventTriggerBase&lt;T&gt; instead of this class.</remarks>
  public abstract class EventTriggerBase : TriggerBase
  {
    private Type sourceTypeConstraint;
    private bool isSourceChangedRegistered;
    private NameResolver sourceNameResolver;
    private MethodInfo eventHandlerMethodInfo;
    public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register(nameof (SourceObject), typeof (object), typeof (EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceObjectChanged)));
    public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register(nameof (SourceName), typeof (string), typeof (EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceNameChanged)));

    /// <summary>Gets the type constraint of the associated object.</summary>
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

    /// <summary>Gets the source type constraint.</summary>
    /// <value>The source type constraint.</value>
    protected Type SourceTypeConstraint => this.sourceTypeConstraint;

    /// <summary>
    /// Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by TargetName. If an element referred to by TargetName cannot be found, the target will default to the AssociatedObject. This is a dependency property.
    /// </summary>
    /// <value>The target object.</value>
    public object SourceObject
    {
      get => this.GetValue(EventTriggerBase.SourceObjectProperty);
      set => this.SetValue(EventTriggerBase.SourceObjectProperty, value);
    }

    /// <summary>
    /// Gets or sets the name of the element this EventTriggerBase listens for as a source. If the name is not set or cannot be resolved, the AssociatedObject will be used.  This is a dependency property.
    /// </summary>
    /// <value>The name of the source element.</value>
    public string SourceName
    {
      get => (string) this.GetValue(EventTriggerBase.SourceNameProperty);
      set => this.SetValue(EventTriggerBase.SourceNameProperty, (object) value);
    }

    /// <summary>
    /// Gets the resolved source. If <c ref="SourceName" /> is not set or cannot be resolved, defaults to AssociatedObject.
    /// </summary>
    /// <value>The resolved source object.</value>
    /// <remarks>In general, this property should be used in place of AssociatedObject in derived classes.</remarks>
    /// <exception cref="T:System.InvalidOperationException">The element pointed to by <c cref="P:System.Windows.Interactivity.EventTriggerBase.Source" /> does not satisify the type constraint.</exception>
    public object Source
    {
      get
      {
        object source = (object) this.AssociatedObject;
        if (this.SourceObject != null)
          source = this.SourceObject;
        else if (this.IsSourceNameSet)
        {
          source = (object) this.SourceNameResolver.Object;
          if (source != null && !this.SourceTypeConstraint.IsAssignableFrom(source.GetType()))
            throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage, (object) this.GetType().Name, (object) source.GetType(), (object) this.SourceTypeConstraint, (object) nameof (Source)));
        }
        return source;
      }
    }

    private NameResolver SourceNameResolver => this.sourceNameResolver;

    private bool IsSourceChangedRegistered
    {
      get => this.isSourceChangedRegistered;
      set => this.isSourceChangedRegistered = value;
    }

    private bool IsSourceNameSet
    {
      get
      {
        return !string.IsNullOrEmpty(this.SourceName) || this.ReadLocalValue(EventTriggerBase.SourceNameProperty) != DependencyProperty.UnsetValue;
      }
    }

    private bool IsLoadedRegistered { get; set; }

    internal EventTriggerBase(Type sourceTypeConstraint)
      : base(typeof (DependencyObject))
    {
      this.sourceTypeConstraint = sourceTypeConstraint;
      this.sourceNameResolver = new NameResolver();
      this.RegisterSourceChanged();
    }

    /// <summary>
    /// Specifies the name of the Event this EventTriggerBase is listening for.
    /// </summary>
    /// <returns></returns>
    protected abstract string GetEventName();

    /// <summary>
    /// Called when the event associated with this EventTriggerBase is fired. By default, this will invoke all actions on the trigger.
    /// </summary>
    /// <param name="eventArgs">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    /// <remarks>Override this to provide more granular control over when actions associated with this trigger will be invoked.</remarks>
    protected virtual void OnEvent(EventArgs eventArgs) => this.InvokeActions((object) eventArgs);

    private void OnSourceChanged(object oldSource, object newSource)
    {
      if (this.AssociatedObject == null)
        return;
      this.OnSourceChangedImpl(oldSource, newSource);
    }

    /// <summary>Called when the source changes.</summary>
    /// <param name="oldSource">The old source.</param>
    /// <param name="newSource">The new source.</param>
    /// <remarks>This function should be overridden in derived classes to hook functionality to and unhook functionality from the changing source objects.</remarks>
    internal virtual void OnSourceChangedImpl(object oldSource, object newSource)
    {
      if (string.IsNullOrEmpty(this.GetEventName()) || string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0)
        return;
      if (oldSource != null && this.SourceTypeConstraint.IsAssignableFrom(oldSource.GetType()))
        this.UnregisterEvent(oldSource, this.GetEventName());
      if (newSource == null || !this.SourceTypeConstraint.IsAssignableFrom(newSource.GetType()))
        return;
      this.RegisterEvent(newSource, this.GetEventName());
    }

    /// <summary>
    /// Called after the trigger is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached()
    {
      base.OnAttached();
      DependencyObject associatedObject1 = this.AssociatedObject;
      Behavior behavior = associatedObject1 as Behavior;
      FrameworkElement frameworkElement = associatedObject1 as FrameworkElement;
      this.RegisterSourceChanged();
      if (behavior != null)
      {
        DependencyObject associatedObject2 = ((IAttachedObject) behavior).AssociatedObject;
        behavior.AssociatedObjectChanged += new EventHandler(this.OnBehaviorHostChanged);
      }
      else
      {
        if (this.SourceObject == null)
        {
          if (frameworkElement != null)
          {
            this.SourceNameResolver.NameScopeReferenceElement = frameworkElement;
            goto label_7;
          }
        }
        try
        {
          this.OnSourceChanged((object) null, this.Source);
        }
        catch (InvalidOperationException ex)
        {
        }
      }
label_7:
      if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || frameworkElement == null || Interaction.IsElementLoaded(frameworkElement))
        return;
      this.RegisterLoaded(frameworkElement);
    }

    /// <summary>
    /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected override void OnDetaching()
    {
      base.OnDetaching();
      Behavior associatedObject1 = this.AssociatedObject as Behavior;
      FrameworkElement associatedObject2 = this.AssociatedObject as FrameworkElement;
      try
      {
        this.OnSourceChanged(this.Source, (object) null);
      }
      catch (InvalidOperationException ex)
      {
      }
      this.UnregisterSourceChanged();
      if (associatedObject1 != null)
        associatedObject1.AssociatedObjectChanged -= new EventHandler(this.OnBehaviorHostChanged);
      this.SourceNameResolver.NameScopeReferenceElement = (FrameworkElement) null;
      if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || associatedObject2 == null)
        return;
      this.UnregisterLoaded(associatedObject2);
    }

    private void OnBehaviorHostChanged(object sender, EventArgs e)
    {
      this.SourceNameResolver.NameScopeReferenceElement = ((IAttachedObject) sender).AssociatedObject as FrameworkElement;
    }

    private static void OnSourceObjectChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      EventTriggerBase eventTriggerBase = (EventTriggerBase) obj;
      object newSource = (object) eventTriggerBase.SourceNameResolver.Object;
      if (args.NewValue == null)
      {
        eventTriggerBase.OnSourceChanged(args.OldValue, newSource);
      }
      else
      {
        if (args.OldValue == null && newSource != null)
          eventTriggerBase.UnregisterEvent(newSource, eventTriggerBase.GetEventName());
        eventTriggerBase.OnSourceChanged(args.OldValue, args.NewValue);
      }
    }

    private static void OnSourceNameChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      ((EventTriggerBase) obj).SourceNameResolver.Name = (string) args.NewValue;
    }

    private void RegisterSourceChanged()
    {
      if (this.IsSourceChangedRegistered)
        return;
      this.SourceNameResolver.ResolvedElementChanged += new EventHandler<NameResolvedEventArgs>(this.OnSourceNameResolverElementChanged);
      this.IsSourceChangedRegistered = true;
    }

    private void UnregisterSourceChanged()
    {
      if (!this.IsSourceChangedRegistered)
        return;
      this.SourceNameResolver.ResolvedElementChanged -= new EventHandler<NameResolvedEventArgs>(this.OnSourceNameResolverElementChanged);
      this.IsSourceChangedRegistered = false;
    }

    private void OnSourceNameResolverElementChanged(object sender, NameResolvedEventArgs e)
    {
      if (this.SourceObject != null)
        return;
      this.OnSourceChanged(e.OldObject, e.NewObject);
    }

    private void RegisterLoaded(FrameworkElement associatedElement)
    {
      if (this.IsLoadedRegistered || associatedElement == null)
        return;
      associatedElement.Loaded += new RoutedEventHandler(this.OnEventImpl);
      this.IsLoadedRegistered = true;
    }

    private void UnregisterLoaded(FrameworkElement associatedElement)
    {
      if (!this.IsLoadedRegistered || associatedElement == null)
        return;
      associatedElement.Loaded -= new RoutedEventHandler(this.OnEventImpl);
      this.IsLoadedRegistered = false;
    }

    /// <exception cref="T:System.ArgumentException">Could not find eventName on the Target.</exception>
    private void RegisterEvent(object obj, string eventName)
    {
      EventInfo eventInfo = obj.GetType().GetEvent(eventName);
      if (eventInfo == null)
      {
        if (this.SourceObject != null)
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerCannotFindEventNameExceptionMessage, (object) eventName, (object) obj.GetType().Name));
      }
      else if (!EventTriggerBase.IsValidEvent(eventInfo))
      {
        if (this.SourceObject != null)
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerBaseInvalidEventExceptionMessage, (object) eventName, (object) obj.GetType().Name));
      }
      else
      {
        this.eventHandlerMethodInfo = typeof (EventTriggerBase).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
        eventInfo.AddEventHandler(obj, Delegate.CreateDelegate(eventInfo.EventHandlerType, (object) this, this.eventHandlerMethodInfo));
      }
    }

    private static bool IsValidEvent(EventInfo eventInfo)
    {
      Type eventHandlerType = eventInfo.EventHandlerType;
      if (!typeof (Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
        return false;
      ParameterInfo[] parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
      return parameters.Length == 2 && typeof (object).IsAssignableFrom(parameters[0].ParameterType) && typeof (EventArgs).IsAssignableFrom(parameters[1].ParameterType);
    }

    private void UnregisterEvent(object obj, string eventName)
    {
      if (string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
      {
        if (!(obj is FrameworkElement associatedElement))
          return;
        this.UnregisterLoaded(associatedElement);
      }
      else
        this.UnregisterEventImpl(obj, eventName);
    }

    private void UnregisterEventImpl(object obj, string eventName)
    {
      Type type = obj.GetType();
      if (this.eventHandlerMethodInfo == null)
        return;
      EventInfo eventInfo = type.GetEvent(eventName);
      eventInfo.RemoveEventHandler(obj, Delegate.CreateDelegate(eventInfo.EventHandlerType, (object) this, this.eventHandlerMethodInfo));
      this.eventHandlerMethodInfo = (MethodInfo) null;
    }

    private void OnEventImpl(object sender, EventArgs eventArgs) => this.OnEvent(eventArgs);

    internal void OnEventNameChanged(string oldEventName, string newEventName)
    {
      if (this.AssociatedObject == null)
        return;
      if (this.Source is FrameworkElement source && string.Compare(oldEventName, "Loaded", StringComparison.Ordinal) == 0)
        this.UnregisterLoaded(source);
      else if (!string.IsNullOrEmpty(oldEventName))
        this.UnregisterEvent(this.Source, oldEventName);
      if (source != null && string.Compare(newEventName, "Loaded", StringComparison.Ordinal) == 0)
      {
        this.RegisterLoaded(source);
      }
      else
      {
        if (string.IsNullOrEmpty(newEventName))
          return;
        this.RegisterEvent(this.Source, newEventName);
      }
    }
  }
}
