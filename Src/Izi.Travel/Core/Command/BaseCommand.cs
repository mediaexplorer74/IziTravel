// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Command.BaseCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using System;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Core.Command
{
  public abstract class BaseCommand : PropertyChangedBase, ICommand
  {
    public abstract bool CanExecute(object parameter);

    public abstract void Execute(object parameter);

    public void RaiseCanExecuteChanged()
    {
      EventHandler canExecuteChanged = this.CanExecuteChanged;
      if (canExecuteChanged == null)
        return;
      canExecuteChanged((object) this, new EventArgs());
    }

    public event EventHandler CanExecuteChanged;
  }
}
