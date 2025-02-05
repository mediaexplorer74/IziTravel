// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Model.MenuItemInfo
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using System;
using System.Linq.Expressions;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Common.Model
{
  public class MenuItemInfo : PropertyChangedBase
  {
    private string _text;
    private ICommand _command;

    public string Key { get; set; }

    public int Order { get; set; }

    public string Text
    {
      get => this._text;
      set
      {
        if (!(this._text != value))
          return;
        this._text = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Text));
      }
    }

    public ICommand Command
    {
      get => this._command;
      set
      {
        if (this._command == value)
          return;
        this._command = value;
        this.NotifyOfPropertyChange<ICommand>((Expression<Func<ICommand>>) (() => this.Command));
      }
    }
  }
}
