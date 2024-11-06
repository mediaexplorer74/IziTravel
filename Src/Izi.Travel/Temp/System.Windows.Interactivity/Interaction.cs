// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.Interaction
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Windows.Media;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Static class that owns the Triggers and Behaviors attached properties. Handles propagation of AssociatedObject change notifications.
  /// </summary>
  public static class Interaction
  {
    /// <summary>
    /// This property is used as the internal backing store for the public Triggers attached property.
    /// </summary>
    public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("Triggers", typeof (TriggerCollection), typeof (Interaction), new PropertyMetadata(new PropertyChangedCallback(Interaction.OnTriggersChanged)));
    /// <summary>
    /// This property is used as the internal backing store for the public Behaviors attached property.
    /// </summary>
    public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("Behaviors", typeof (BehaviorCollection), typeof (Interaction), new PropertyMetadata(new PropertyChangedCallback(Interaction.OnBehaviorsChanged)));

    /// <summary>
    /// Gets the TriggerCollection containing the triggers associated with the specified object.
    /// </summary>
    /// <param name="obj">The object from which to retrieve the triggers.</param>
    /// <returns>A TriggerCollection containing the triggers associated with the specified object.</returns>
    public static TriggerCollection GetTriggers(DependencyObject obj)
    {
      TriggerCollection triggers = (TriggerCollection) obj.GetValue(Interaction.TriggersProperty);
      if (triggers == null)
      {
        triggers = new TriggerCollection();
        obj.SetValue(Interaction.TriggersProperty, (object) triggers);
      }
      return triggers;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Windows.Interactivity.BehaviorCollection" /> associated with a specified object.
    /// </summary>
    /// <param name="obj">The object from which to retrieve the <see cref="T:System.Windows.Interactivity.BehaviorCollection" />.</param>
    /// <returns>A <see cref="T:System.Windows.Interactivity.BehaviorCollection" /> containing the behaviors associated with the specified object.</returns>
    public static BehaviorCollection GetBehaviors(DependencyObject obj)
    {
      BehaviorCollection behaviors = (BehaviorCollection) obj.GetValue(Interaction.BehaviorsProperty);
      if (behaviors == null)
      {
        behaviors = new BehaviorCollection();
        obj.SetValue(Interaction.BehaviorsProperty, (object) behaviors);
      }
      return behaviors;
    }

    /// <exception cref="T:System.InvalidOperationException">Cannot host the same BehaviorCollection on more than one object at a time.</exception>
    private static void OnBehaviorsChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      BehaviorCollection oldValue = (BehaviorCollection) args.OldValue;
      BehaviorCollection newValue = (BehaviorCollection) args.NewValue;
      if (oldValue == newValue)
        return;
      if (oldValue != null && ((IAttachedObject) oldValue).AssociatedObject != null)
        oldValue.Detach();
      if (newValue == null || obj == null)
        return;
      if (((IAttachedObject) newValue).AssociatedObject != null)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostBehaviorCollectionMultipleTimesExceptionMessage);
      newValue.Attach(obj);
    }

    /// <exception cref="T:System.InvalidOperationException">Cannot host the same TriggerCollection on more than one object at a time.</exception>
    private static void OnTriggersChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs args)
    {
      TriggerCollection oldValue = args.OldValue as TriggerCollection;
      TriggerCollection newValue = args.NewValue as TriggerCollection;
      if (oldValue == newValue)
        return;
      if (oldValue != null && ((IAttachedObject) oldValue).AssociatedObject != null)
        oldValue.Detach();
      if (newValue == null || obj == null)
        return;
      if (((IAttachedObject) newValue).AssociatedObject != null)
        throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerCollectionMultipleTimesExceptionMessage);
      newValue.Attach(obj);
    }

    /// <summary>
    /// A helper function to take the place of FrameworkElement.IsLoaded, as this property is not available in Silverlight.
    /// </summary>
    /// <param name="element">The element of interest.</param>
    /// <returns>True if the element has been loaded; otherwise, False.</returns>
    internal static bool IsElementLoaded(FrameworkElement element)
    {
      UIElement rootVisual = Application.Current.RootVisual;
      if ((element.Parent ?? VisualTreeHelper.GetParent((DependencyObject) element)) != null)
        return true;
      return rootVisual != null && element == rootVisual;
    }
  }
}
