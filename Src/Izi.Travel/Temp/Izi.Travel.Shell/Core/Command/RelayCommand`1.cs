// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Command.RelayCommand`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Command
{
  public class RelayCommand<T> : BaseCommand
  {
    private readonly Action<T> _execute;
    private readonly Predicate<T> _canExecute;

    public RelayCommand(Action<T> execute)
      : this(execute, (Predicate<T>) null)
    {
    }

    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
      this._execute = execute != null ? execute : throw new ArgumentNullException(nameof (execute));
      this._canExecute = canExecute;
    }

    public override bool CanExecute(object parameter)
    {
      return this._canExecute == null || this._canExecute((T) parameter);
    }

    public override void Execute(object parameter)
    {
      if (!this.CanExecute(parameter))
        return;
      this._execute((T) parameter);
    }
  }
}
