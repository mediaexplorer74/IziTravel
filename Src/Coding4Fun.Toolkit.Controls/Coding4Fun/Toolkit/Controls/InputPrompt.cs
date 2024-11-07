// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.InputPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class InputPrompt : UserPrompt
  {
    private const string InputBoxName = "inputBox";
    protected TextBox InputBox;
    public static readonly DependencyProperty MessageTextWrappingProperty = DependencyProperty.Register(nameof (MessageTextWrapping), typeof (TextWrapping), typeof (InputPrompt), new PropertyMetadata((object) TextWrapping.NoWrap));
    public static readonly DependencyProperty InputScopeProperty = DependencyProperty.Register(nameof (InputScope), typeof (InputScope), typeof (InputPrompt), (PropertyMetadata) null);
    public static readonly DependencyProperty IsSubmitOnEnterKeyProperty = DependencyProperty.Register(nameof (IsSubmitOnEnterKey), typeof (bool), typeof (InputPrompt), new PropertyMetadata((object) true, new PropertyChangedCallback(InputPrompt.OnIsSubmitOnEnterKeyPropertyChanged)));

    public InputPrompt() => this.DefaultStyleKey = (object) typeof (InputPrompt);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.InputBox = this.GetTemplateChild("inputBox") as TextBox;
      if (this.InputBox == null)
        return;
      Binding binding = new Binding()
      {
        Source = (object) this.InputBox,
        Path = new PropertyPath("Text", new object[0])
      };
      this.SetBinding(UserPrompt.ValueProperty, binding);
      this.HookUpEventForIsSubmitOnEnterKey();
      if (ApplicationSpace.IsDesignMode)
        return;
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.DelayInputSelect));
    }

    private void DelayInputSelect(object value)
    {
      Thread.Sleep(250);
      this.Dispatcher.BeginInvoke((Action) (() =>
      {
        this.InputBox.Focus();
        this.InputBox.SelectAll();
      }));
    }

    public TextWrapping MessageTextWrapping
    {
      get => (TextWrapping) this.GetValue(InputPrompt.MessageTextWrappingProperty);
      set => this.SetValue(InputPrompt.MessageTextWrappingProperty, (object) value);
    }

    public InputScope InputScope
    {
      get => (InputScope) this.GetValue(InputPrompt.InputScopeProperty);
      set => this.SetValue(InputPrompt.InputScopeProperty, (object) value);
    }

    public bool IsSubmitOnEnterKey
    {
      get => (bool) this.GetValue(InputPrompt.IsSubmitOnEnterKeyProperty);
      set => this.SetValue(InputPrompt.IsSubmitOnEnterKeyProperty, (object) value);
    }

    private static void OnIsSubmitOnEnterKeyPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is InputPrompt inputPrompt))
        return;
      inputPrompt.HookUpEventForIsSubmitOnEnterKey();
    }

    private void HookUpEventForIsSubmitOnEnterKey()
    {
      this.InputBox = this.GetTemplateChild("inputBox") as TextBox;
      if (this.InputBox == null)
        return;
      this.InputBox.KeyDown -= new KeyEventHandler(this.InputBoxKeyDown);
      if (!this.IsSubmitOnEnterKey)
        return;
      this.InputBox.KeyDown += new KeyEventHandler(this.InputBoxKeyDown);
    }

    private void InputBoxKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.Enter)
        return;
      this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
      {
        Result = this.Value,
        PopUpResult = PopUpResult.Ok
      });
    }
  }
}
