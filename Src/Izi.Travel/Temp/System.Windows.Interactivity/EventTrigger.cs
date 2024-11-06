// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.EventTrigger
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// A trigger that listens for a specified event on its source and fires when that event is fired.
  /// </summary>
  public class EventTrigger : EventTriggerBase<object>
  {
    public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(nameof (EventName), typeof (string), typeof (EventTrigger), new PropertyMetadata((object) "Loaded", new PropertyChangedCallback(EventTrigger.OnEventNameChanged)));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.EventTrigger" /> class.
    /// </summary>
    public EventTrigger()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.EventTrigger" /> class.
    /// </summary>
    /// <param name="eventName">Name of the event.</param>
    public EventTrigger(string eventName) => this.EventName = eventName;

    /// <summary>
    /// Gets or sets the name of the event to listen for. This is a dependency property.
    /// </summary>
    /// <value>The name of the event.</value>
    public string EventName
    {
      get => (string) this.GetValue(EventTrigger.EventNameProperty);
      set => this.SetValue(EventTrigger.EventNameProperty, (object) value);
    }

    protected override string GetEventName() => this.EventName;

    private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
      ((EventTriggerBase) sender).OnEventNameChanged((string) args.OldValue, (string) args.NewValue);
    }
  }
}
