// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.LogViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.ViewModels
{
  public class LogViewModel : Screen
  {
    public IObservableCollection<string> Lines { get; set; }

    public LogViewModel()
    {
      this.Lines = (IObservableCollection<string>) new BindableCollection<string>();
    }

    protected override async void OnActivate()
    {
      this.Lines.Clear();
      IObservableCollection<string> observableCollection = this.Lines;
      string logAsync = await CustomLogger.GetLogAsync();
      string[] separator = new string[1]
      {
        Environment.NewLine
      };
      observableCollection.AddRange((IEnumerable<string>) ((IEnumerable<string>) logAsync.Split(separator, StringSplitOptions.None)).Reverse<string>().ToList<string>());
      observableCollection = (IObservableCollection<string>) null;
    }

    protected override void OnDeactivate(bool close)
    {
    }
  }
}
