// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Quiz.QuizAnswerViewModelExtended
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Extensions;
using System;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Quiz
{
  public class QuizAnswerViewModelExtended : PropertyChangedBase
  {
    private readonly QuizViewModel _quizViewModel;
    private bool _animationEnabled;
    private bool _isChecked;
    private bool _isEnabled;
    private bool _isOverflowed;

    public int Index { get; set; }

    public QuizViewModel QuizViewModel => this._quizViewModel;

    public bool AnimationEnabled
    {
      get => this._animationEnabled;
      set
      {
        this.SetProperty<bool>(ref this._animationEnabled, value, propertyName: nameof (AnimationEnabled));
      }
    }

    public TimeSpan AnimationBeginTime => TimeSpan.FromMilliseconds((double) (100 * this.Index));

    public bool IsEnabled
    {
      get => this._isEnabled;
      set => this.SetProperty<bool>(ref this._isEnabled, value, propertyName: nameof (IsEnabled));
    }

    public bool IsChecked
    {
      get => this._isChecked;
      set => this.SetProperty<bool>(ref this._isChecked, value, propertyName: nameof (IsChecked));
    }

    public string Title { get; set; }

    public bool IsCorrect { get; set; }

    public bool IsOverflowed
    {
      get => this._isOverflowed;
      set
      {
        this.SetProperty<bool>(ref this._isOverflowed, value, propertyName: nameof (IsOverflowed));
      }
    }

    public QuizAnswerViewModelExtended(QuizViewModel quizViewModel)
    {
      this._quizViewModel = quizViewModel;
      this.IsEnabled = true;
    }
  }
}
