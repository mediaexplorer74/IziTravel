// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MessagePrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class MessagePrompt : UserPrompt
  {
    public static readonly DependencyProperty BodyProperty = DependencyProperty.Register(nameof (Body), typeof (object), typeof (MessagePrompt), new PropertyMetadata((PropertyChangedCallback) null));

    public MessagePrompt()
    {
      this.DefaultStyleKey = (object) typeof (MessagePrompt);
      this.MessageChanged = new Action(this.SetBodyMessage);
    }

    public object Body
    {
      get => this.GetValue(MessagePrompt.BodyProperty);
      set => this.SetValue(MessagePrompt.BodyProperty, value);
    }

    protected internal void SetBodyMessage()
    {
      this.Body = (object) new TextBlock()
      {
        Text = this.Message,
        TextWrapping = TextWrapping.Wrap
      };
    }
  }
}
