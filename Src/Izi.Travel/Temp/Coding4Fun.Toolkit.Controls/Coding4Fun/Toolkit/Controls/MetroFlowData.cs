// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MetroFlowData
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class MetroFlowData : INotifyPropertyChanged
  {
    private Uri _imageUri;
    private string _title;

    public Uri ImageUri
    {
      get => this._imageUri;
      set
      {
        this._imageUri = value;
        this.RaisePropertyChanged(nameof (ImageUri));
      }
    }

    public string Title
    {
      get => this._title;
      set
      {
        this._title = value;
        this.RaisePropertyChanged(nameof (Title));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
