// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.ViewModels.Flyout.FlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Common.ViewModels.Flyout
{
  public abstract class FlyoutViewModel : PropertyChangedBase
  {
    private bool _isOpen;
    private RelayCommand _openCommand;
    private RelayCommand _closeCommand;
    private RelayCommand _openingCommand;
    private RelayCommand _openedCommand;
    private RelayCommand _closingCommand;
    private RelayCommand _closedCommand;

    public bool IsOpen
    {
      get => this._isOpen;
      set
      {
        if (this._isOpen == value)
          return;
        this._isOpen = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsOpen));
      }
    }

    public RelayCommand OpenCommand
    {
      get
      {
        return this._openCommand ?? (this._openCommand = new RelayCommand(new Action<object>(this.ExecuteOpenCommand), new Func<object, bool>(this.CanExecuteOpenCommand)));
      }
    }

    protected virtual bool CanExecuteOpenCommand(object parameter) => true;

    private void ExecuteOpenCommand(object parameter)
    {
      this.OnOpening();
      this.IsOpen = true;
      this.OnOpened();
    }

    public RelayCommand CloseCommand
    {
      get
      {
        return this._closeCommand ?? (this._closeCommand = new RelayCommand(new Action<object>(this.ExecuteCloseCommand), new Func<object, bool>(this.CanExecuteCloseCommand)));
      }
    }

    protected virtual bool CanExecuteCloseCommand(object parameter) => true;

    private void ExecuteCloseCommand(object parameter)
    {
      this.OnClosing();
      this.IsOpen = false;
      this.OnClosed();
    }

    public RelayCommand OpeningCommand
    {
      get
      {
        return this._openingCommand ?? (this._openingCommand = new RelayCommand((Action<object>) (x => this.OnOpening())));
      }
    }

    public RelayCommand OpenedCommand
    {
      get
      {
        return this._openedCommand ?? (this._openedCommand = new RelayCommand((Action<object>) (x => this.OnOpened())));
      }
    }

    public RelayCommand ClosingCommand
    {
      get
      {
        return this._closedCommand ?? (this._closingCommand = new RelayCommand((Action<object>) (x => this.OnClosing())));
      }
    }

    public RelayCommand ClosedCommand
    {
      get
      {
        return this._closedCommand ?? (this._closedCommand = new RelayCommand((Action<object>) (x => this.OnClosed())));
      }
    }

    protected virtual void OnOpening()
    {
    }

    protected virtual void OnOpened()
    {
    }

    protected virtual void OnClosing()
    {
    }

    protected virtual void OnClosed()
    {
    }
  }
}
