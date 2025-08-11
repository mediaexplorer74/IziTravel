// ********************************************************************
// Type: Izi.Travel.Shell.Core.Command.BaseCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5

using Caliburn.Micro;
using System;
using System.ComponentModel;

namespace Izi.Travel.Shell.Core.Command
{
    public abstract class BaseCommand : PropertyChangedBase, System.Windows.Input.ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}

