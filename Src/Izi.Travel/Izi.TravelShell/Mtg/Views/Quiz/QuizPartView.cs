﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Quiz.QuizPartView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Quiz
{
  public class QuizPartView : PhoneApplicationPage
  {
    private bool _contentLoaded;

    public QuizPartView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Quiz/QuizPartView.xaml", UriKind.Relative));
    }
  }
}
