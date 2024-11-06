// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.PasswordInputPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class PasswordInputPrompt : InputPrompt
  {
    private readonly StringBuilder _inputText = new StringBuilder();
    private DateTime _lastUpdated = DateTime.Now;
    public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(nameof (PasswordChar), typeof (char), typeof (PasswordInputPrompt), new PropertyMetadata((object) '●'));

    public PasswordInputPrompt() => this.DefaultStyleKey = (object) typeof (PasswordInputPrompt);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (this.InputBox == null)
        return;
      this.InputBox.TextChanged += new TextChangedEventHandler(this.InputBoxTextChanged);
      this.InputBox.SelectionChanged += new RoutedEventHandler(this.InputBoxSelectionChanged);
    }

    private void InputBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
      if (this.InputBox.SelectionLength <= 0)
        return;
      this.InputBox.SelectionLength = 0;
    }

    private void InputBoxTextChanged(object sender, TextChangedEventArgs e)
    {
      int length1 = this.InputBox.Text.Length - this._inputText.Length;
      if (length1 < 0)
      {
        int length2 = length1 * -1;
        int startIndex = this.InputBox.SelectionStart + 1 - length2;
        if (startIndex < 0)
          startIndex = 0;
        this._inputText.Remove(startIndex, length2);
        this.Value = this._inputText.ToString();
      }
      else
      {
        if (length1 <= 0)
          return;
        int selectionStart = this.InputBox.SelectionStart;
        int num = selectionStart - length1;
        string str = this.InputBox.Text.Substring(num, length1);
        this._inputText.Insert(num, str);
        this.Value = this._inputText.ToString();
        if (length1 > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Insert(0, this.PasswordChar.ToString((IFormatProvider) CultureInfo.InvariantCulture), this.InputBox.Text.Length);
          this.InputBox.Text = stringBuilder.ToString();
        }
        else
        {
          if (this.InputBox.Text.Length >= 2)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Insert(0, this.PasswordChar.ToString((IFormatProvider) CultureInfo.InvariantCulture), this.InputBox.Text.Length - length1);
            stringBuilder.Insert(num, str);
            this.InputBox.Text = stringBuilder.ToString();
          }
          this.ExecuteDelayedOverwrite();
          this._lastUpdated = DateTime.Now;
        }
        this.InputBox.SelectionStart = selectionStart;
      }
    }

    private void ExecuteDelayedOverwrite()
    {
      ThreadPool.QueueUserWorkItem((WaitCallback) (state =>
      {
        Thread.Sleep(TimeSpan.FromMilliseconds(500.0));
        if (DateTime.Now - this._lastUpdated < TimeSpan.FromMilliseconds(500.0))
          return;
        this.Dispatcher.BeginInvoke((Action) (() =>
        {
          int selectionStart = this.InputBox.SelectionStart;
          this.InputBox.Text = Regex.Replace(this.InputBox.Text, ".", this.PasswordChar.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          this.InputBox.SelectionStart = selectionStart;
        }));
      }));
    }

    public char PasswordChar
    {
      get => (char) this.GetValue(PasswordInputPrompt.PasswordCharProperty);
      set => this.SetValue(PasswordInputPrompt.PasswordCharProperty, (object) value);
    }
  }
}
