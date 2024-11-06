// Decompiled with JetBrains decompiler
// Type: BindableApplicationBar.BindableApplicationBarButton
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
  public class BindableApplicationBarButton : FrameworkElement
  {
    private ApplicationBar applicationBar;
    private ApplicationBarIconButton applicationBarIconButton;
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (BindableApplicationBarButton), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarButton.OnTextChanged)));
    public static readonly DependencyProperty IconUriProperty = DependencyProperty.Register(nameof (IconUri), typeof (Uri), typeof (BindableApplicationBarButton), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarButton.OnIconUriChanged)));
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof (IsEnabled), typeof (bool), typeof (BindableApplicationBarButton), new PropertyMetadata((object) true, new PropertyChangedCallback(BindableApplicationBarButton.OnIsEnabledChanged)));
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof (Command), typeof (ICommand), typeof (BindableApplicationBarButton), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarButton.OnCommandChanged)));
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof (CommandParameter), typeof (object), typeof (BindableApplicationBarButton), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBarButton.OnCommandParameterChanged)));

    public string Text
    {
      get => (string) this.GetValue(BindableApplicationBarButton.TextProperty);
      set => this.SetValue(BindableApplicationBarButton.TextProperty, (object) value);
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) d;
      string oldValue = (string) e.OldValue;
      string text = applicationBarButton.Text;
      applicationBarButton.OnTextChanged(oldValue, text);
    }

    protected virtual void OnTextChanged(string oldText, string newText)
    {
      if (this.applicationBarIconButton == null)
        return;
      this.applicationBarIconButton.Text = newText;
    }

    public Uri IconUri
    {
      get => (Uri) this.GetValue(BindableApplicationBarButton.IconUriProperty);
      set => this.SetValue(BindableApplicationBarButton.IconUriProperty, (object) value);
    }

    private static void OnIconUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) d;
      Uri oldValue = (Uri) e.OldValue;
      Uri iconUri = applicationBarButton.IconUri;
      applicationBarButton.OnIconUriChanged(oldValue, iconUri);
    }

    protected virtual void OnIconUriChanged(Uri oldIconUri, Uri newIconUri)
    {
      if (this.applicationBarIconButton == null)
        return;
      this.applicationBarIconButton.IconUri = this.IconUri;
    }

    public bool IsEnabled
    {
      get => (bool) this.GetValue(BindableApplicationBarButton.IsEnabledProperty);
      set => this.SetValue(BindableApplicationBarButton.IsEnabledProperty, (object) value);
    }

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) d;
      bool oldValue = (bool) e.OldValue;
      bool isEnabled = applicationBarButton.IsEnabled;
      applicationBarButton.OnIsEnabledChanged(oldValue, isEnabled);
    }

    protected virtual void OnIsEnabledChanged(bool oldIsEnabled, bool newIsEnabled)
    {
      if (this.applicationBarIconButton == null)
        return;
      this.applicationBarIconButton.IsEnabled = this.IsEnabled;
    }

    public ICommand Command
    {
      get => (ICommand) this.GetValue(BindableApplicationBarButton.CommandProperty);
      set => this.SetValue(BindableApplicationBarButton.CommandProperty, (object) value);
    }

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) d;
      ICommand oldValue = (ICommand) e.OldValue;
      ICommand command = applicationBarButton.Command;
      applicationBarButton.OnCommandChanged(oldValue, command);
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
      get => this.GetValue(BindableApplicationBarButton.CommandParameterProperty);
      set => this.SetValue(BindableApplicationBarButton.CommandParameterProperty, value);
    }

    private static void OnCommandParameterChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) d;
      object oldValue = e.OldValue;
      object commandParameter = applicationBarButton.CommandParameter;
      applicationBarButton.OnCommandParameterChanged(oldValue, commandParameter);
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
      if (this.applicationBarIconButton != null)
        return;
      this.applicationBar = parentApplicationBar;
      this.applicationBarIconButton = new ApplicationBarIconButton(this.IconUri)
      {
        Text = string.IsNullOrEmpty(this.Text) ? "." : this.Text,
        IsEnabled = this.IsEnabled
      };
      this.applicationBarIconButton.Click += new EventHandler(this.ApplicationBarIconButtonClick);
      try
      {
        this.applicationBar.Buttons.Insert(i, (object) this.applicationBarIconButton);
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
      this.applicationBarIconButton.Click -= new EventHandler(this.ApplicationBarIconButtonClick);
      this.applicationBar.Buttons.Remove((object) this.applicationBarIconButton);
      this.applicationBar = (ApplicationBar) null;
      this.applicationBarIconButton = (ApplicationBarIconButton) null;
    }

    private void ApplicationBarIconButtonClick(object sender, EventArgs e)
    {
      if (this.Command == null || !this.Command.CanExecute(this.CommandParameter))
        return;
      this.Command.Execute(this.CommandParameter);
    }
  }
}
