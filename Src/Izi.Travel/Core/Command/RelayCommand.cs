// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Command.RelayCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Command
{
  public class RelayCommand : BaseCommand
  {
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute)
      : this(execute, (Func<object, bool>) null)
    {
    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
    {
      this._execute = execute != null ? execute : throw new ArgumentNullException(nameof (execute));
      this._canExecute = canExecute;
    }

    public override bool CanExecute(object parameter)
    {
      return this._canExecute == null || this._canExecute(parameter);
    }

    public override void Execute(object parameter)
    {
      if (!this.CanExecute(parameter) || this._execute == null)
        return;
      this._execute(parameter);
    }
  }
}
