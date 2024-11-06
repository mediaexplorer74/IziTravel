// Decompiled with JetBrains decompiler
// Type: BindableApplicationBar.BindableApplicationBarMenuItem
// Assembly: BindableApplicationBar, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A535FD52-CB2F-4C72-99D3-803485FA9E0A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BindableApplicationBar.dll

using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
namespace BindableApplicationBar
{
  public class BindableApplicationBarMenuItem : FrameworkElement
  {
    private ApplicationBar applicationBar;
    private ApplicationBarMenuItem applicationBarMenuItem;
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (BindableApplicationBarMenuItem), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarMenuItem.OnTextChanged)));
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof (IsEnabled), typeof (bool), typeof (BindableApplicationBarMenuItem), new PropertyMetadata((object) true, new PropertyChangedCallback(BindableApplicationBarMenuItem.OnIsEnabledChanged)));
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof (Command), typeof (ICommand), typeof (BindableApplicationBarMenuItem), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarMenuItem.OnCommandChanged)));
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof (CommandParameter), typeof (object), typeof (BindableApplicationBarMenuItem), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarMenuItem.OnCommandParameterChanged)));

    public string Text
    {
      get => (string) this.GetValue(BindableApplicationBarMenuItem.TextProperty);
      set => this.SetValue(BindableApplicationBarMenuItem.TextProperty, (object) value);
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) d;
      string oldValue = (string) e.OldValue;
      string text = applicationBarMenuItem.Text;
      applicationBarMenuItem.OnTextChanged(oldValue, text);
    }

    protected virtual void OnTextChanged(string oldText, string newText)
    {
      if (this.applicationBarMenuItem == null)
        return;
      this.applicationBarMenuItem.Text = newText;
    }

    public bool IsEnabled
    {
      get => (bool) this.GetValue(BindableApplicationBarMenuItem.IsEnabledProperty);
      set => this.SetValue(BindableApplicationBarMenuItem.IsEnabledProperty, (object) value);
    }

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) d;
      bool oldValue = (bool) e.OldValue;
      bool isEnabled = applicationBarMenuItem.IsEnabled;
      applicationBarMenuItem.OnIsEnabledChanged(oldValue, isEnabled);
    }

    protected virtual void OnIsEnabledChanged(bool oldIsEnabled, bool newIsEnabled)
    {
      if (this.applicationBarMenuItem == null)
        return;
      this.applicationBarMenuItem.IsEnabled = this.IsEnabled;
    }

    public ICommand Command
    {
      get => (ICommand) this.GetValue(BindableApplicationBarMenuItem.CommandProperty);
      set => this.SetValue(BindableApplicationBarMenuItem.CommandProperty, (object) value);
    }

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) d;
      ICommand oldValue = (ICommand) e.OldValue;
      ICommand command = applicationBarMenuItem.Command;
      applicationBarMenuItem.OnCommandChanged(oldValue, command);
    }

    protected virtual void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
    {
      if (oldCommand != null)
        oldCommand.CanExecuteChanged -= new EventHandler(this.CommandCanExecuteChanged);
      if (newCommand == null)
        return;
      this.IsEnabled = newCommand.CanExecute(this.CommandParameter);
      newCommand.CanExecuteChanged += new EventHandler(this.CommandCanExecuteChanged);
    }

    private void CommandCanExecuteChanged(object sender, EventArgs e)
    {
      if (this.Command == null)
        return;
      this.IsEnabled = this.Command.CanExecute(this.CommandParameter);
    }

    public object CommandParameter
    {
      get => this.GetValue(BindableApplicationBarMenuItem.CommandParameterProperty);
      set => this.SetValue(BindableApplicationBarMenuItem.CommandParameterProperty, value);
    }

    private static void OnCommandParameterChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) d;
      object oldValue = e.OldValue;
      object commandParameter = applicationBarMenuItem.CommandParameter;
      applicationBarMenuItem.OnCommandParameterChanged(oldValue, commandParameter);
    }

    protected virtual void OnCommandParameterChanged(
      object oldCommandParameter,
      object newCommandParameter)
    {
      if (this.Command == null)
        return;
      this.IsEnabled = this.Command.CanExecute(this.CommandParameter);
    }

    public void Attach(ApplicationBar parentApplicationBar, int i)
    {
      if (this.applicationBarMenuItem != null)
        return;
      this.applicationBar = parentApplicationBar;
      this.applicationBarMenuItem = new ApplicationBarMenuItem()
      {
        Text = string.IsNullOrEmpty(this.Text) ? "." : this.Text,
        IsEnabled = this.IsEnabled
      };
      this.applicationBarMenuItem.Click += new EventHandler(this.ApplicationBarMenuItemClick);
      try
      {
        this.applicationBar.MenuItems.Insert(i, (object) this.applicationBarMenuItem);
      }
      catch (InvalidOperationException ex)
      {
        if (ex.Message == "Too many items in list" && Debugger.IsAttached)
          Debugger.Break();
        throw;
      }
    }

    public void Detach()
    {
      this.applicationBarMenuItem.Click -= new EventHandler(this.ApplicationBarMenuItemClick);
      this.applicationBar.MenuItems.Remove((object) this.applicationBarMenuItem);
      this.applicationBar = (ApplicationBar) null;
      this.applicationBarMenuItem = (ApplicationBarMenuItem) null;
    }

    private void ApplicationBarMenuItemClick(object sender, EventArgs e)
    {
      if (this.Command == null || !this.Command.CanExecute(this.CommandParameter))
        return;
      this.Command.Execute(this.CommandParameter);
    }
  }
}
