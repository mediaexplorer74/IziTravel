// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Model.ButtonInfo
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
  public class ButtonInfo : PropertyChangedBase
  {
    private int _order;
    private string _text;
    private string _imageUrl;
    private bool _showAlternative;
    private string _alternativeText;
    private string _alternativeImageUrl;
    private ICommand _command;

    public string Key { get; set; }

    public int Order
    {
      get => this._order;
      set
      {
        if (this._order == value)
          return;
        this._order = value;
        this.NotifyOfPropertyChange<int>((Expression<Func<int>>) (() => this.Order));
      }
    }

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

    public string ImageUrl
    {
      get => this._imageUrl;
      set
      {
        if (!(this._imageUrl != value))
          return;
        this._imageUrl = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.ImageUrl));
      }
    }

    public string AlternativeText
    {
      get => this._alternativeText;
      set
      {
        if (!(this._alternativeText != value))
          return;
        this._alternativeText = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.AlternativeText));
      }
    }

    public string AlternativeImageUrl
    {
      get => this._alternativeImageUrl;
      set
      {
        if (!(this._alternativeImageUrl != value))
          return;
        this._alternativeImageUrl = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.AlternativeImageUrl));
      }
    }

    public bool ShowAlternative
    {
      get => this._showAlternative;
      set
      {
        if (this._showAlternative == value)
          return;
        this._showAlternative = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.ShowAlternative));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.CurrentText));
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.CurrentImageUrl));
      }
    }

    public string CurrentText => this.ShowAlternative ? this.AlternativeText : this.Text;

    public string CurrentImageUrl
    {
      get => this.ShowAlternative ? this.AlternativeImageUrl : this.ImageUrl;
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
