// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.AppBarPromptAction
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows.Input;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class AppBarPromptAction
  {
    public object Content { get; set; }

    internal ICommand Command { get; private set; }

    internal AppBarPrompt Parent { get; set; }

    public AppBarPromptAction(object content, Action execute)
      : this(content, execute, (Func<bool>) (() => true))
    {
    }

    public AppBarPromptAction(object content, Action execute, Func<bool> canExecute)
    {
      this.Content = content;
      this.Command = (ICommand) new AppBarPromptAction.NotificationCommand(execute, canExecute, (Action) (() => this.Parent.Hide()));
    }

    private class NotificationCommand : ICommand
    {
      private readonly Action _execute;
      private readonly Action _finish;
      private readonly Func<bool> _canExecute;

      public NotificationCommand(Action execute, Func<bool> canExecute, Action finishCommand)
      {
        this._execute = execute;
        this._canExecute = canExecute;
        this._finish = finishCommand;
      }

      bool ICommand.CanExecute(object parameter) => this._canExecute();

      event EventHandler ICommand.CanExecuteChanged
      {
        add
        {
        }
        remove
        {
        }
      }

      void ICommand.Execute(object parameter)
      {
        this._execute();
        this._finish();
      }
    }
  }
}
