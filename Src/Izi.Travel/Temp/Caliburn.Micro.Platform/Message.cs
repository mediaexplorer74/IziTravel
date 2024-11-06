// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Message
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Host's attached properties related to routed UI messaging.
  /// </summary>
  public static class Message
  {
    internal static readonly DependencyProperty HandlerProperty = DependencyProperty.RegisterAttached("Handler", typeof (object), typeof (Message), (PropertyMetadata) null);
    private static readonly DependencyProperty MessageTriggersProperty = DependencyProperty.RegisterAttached("MessageTriggers", typeof (System.Windows.Interactivity.TriggerBase[]), typeof (Message), (PropertyMetadata) null);
    /// <summary>
    ///   A property definition representing attached triggers and messages.
    /// </summary>
    public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof (string), typeof (Message), new PropertyMetadata((object) null, new PropertyChangedCallback(Message.OnAttachChanged)));

    /// <summary>Places a message handler on this element.</summary>
    /// <param name="d"> The element. </param>
    /// <param name="value"> The message handler. </param>
    public static void SetHandler(DependencyObject d, object value)
    {
      d.SetValue(Message.HandlerProperty, value);
    }

    /// <summary>Gets the message handler for this element.</summary>
    /// <param name="d"> The element. </param>
    /// <returns> The message handler. </returns>
    public static object GetHandler(DependencyObject d) => d.GetValue(Message.HandlerProperty);

    /// <summary>Sets the attached triggers and messages.</summary>
    /// <param name="d"> The element to attach to. </param>
    /// <param name="attachText"> The parsable attachment text. </param>
    public static void SetAttach(DependencyObject d, string attachText)
    {
      d.SetValue(Message.AttachProperty, (object) attachText);
    }

    /// <summary>Gets the attached triggers and messages.</summary>
    /// <param name="d"> The element that was attached to. </param>
    /// <returns> The parsable attachment text. </returns>
    public static string GetAttach(DependencyObject d)
    {
      return d.GetValue(Message.AttachProperty) as string;
    }

    private static void OnAttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue == e.OldValue)
        return;
      System.Windows.Interactivity.TriggerBase[] enumerable = (System.Windows.Interactivity.TriggerBase[]) d.GetValue(Message.MessageTriggersProperty);
      System.Windows.Interactivity.TriggerCollection allTriggers = Interaction.GetTriggers(d);
      if (enumerable != null)
        ((IEnumerable<System.Windows.Interactivity.TriggerBase>) enumerable).Apply<System.Windows.Interactivity.TriggerBase>((Action<System.Windows.Interactivity.TriggerBase>) (x => allTriggers.Remove(x)));
      System.Windows.Interactivity.TriggerBase[] array = Parser.Parse(d, e.NewValue as string).ToArray<System.Windows.Interactivity.TriggerBase>();
      ((IEnumerable<System.Windows.Interactivity.TriggerBase>) array).Apply<System.Windows.Interactivity.TriggerBase>(new Action<System.Windows.Interactivity.TriggerBase>(((DependencyObjectCollection<System.Windows.Interactivity.TriggerBase>) allTriggers).Add));
      if (array.Length > 0)
        d.SetValue(Message.MessageTriggersProperty, (object) array);
      else
        d.ClearValue(Message.MessageTriggersProperty);
    }
  }
}
