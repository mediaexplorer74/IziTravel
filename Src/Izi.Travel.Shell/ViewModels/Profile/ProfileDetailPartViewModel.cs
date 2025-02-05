// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileDetailPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Shell.ViewModels.Profile.Bookmark;
using Izi.Travel.Shell.ViewModels.Profile.Download;
using Izi.Travel.Shell.ViewModels.Profile.History;
using Izi.Travel.Shell.ViewModels.Profile.Purchase;
using Izi.Travel.Shell.ViewModels.Profile.Quiz;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public class ProfileDetailPartViewModel : Conductor<IScreen>.Collection.OneActive
  {
    private readonly ProfileDownloadTabViewModel _downloadViewModel;
    private readonly ProfileBookmarkTabViewModel _bookmarkViewModel;
    private readonly ProfilePurchaseTabViewModel _purchaseViewModel;
    private readonly ProfileQuizTabViewModel _quizViewModel;
    private readonly ProfileHistoryTabViewModel _historyViewModel;

    public override string DisplayName
    {
      get => AppResources.LabelUserProfile.ToUpper();
      set => throw new NotImplementedException();
    }

    public ProfileType SelectedType { get; set; }

    public ProfileDetailPartViewModel(
      ProfileDownloadTabViewModel downloadViewModel,
      ProfileBookmarkTabViewModel bookmarkViewModel,
      ProfilePurchaseTabViewModel purchaseViewModel,
      ProfileQuizTabViewModel quizViewModel,
      ProfileHistoryTabViewModel historyViewModel)
    {
      this._downloadViewModel = downloadViewModel;
      this._bookmarkViewModel = bookmarkViewModel;
      this._purchaseViewModel = purchaseViewModel;
      this._quizViewModel = quizViewModel;
      this._historyViewModel = historyViewModel;
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Add((IScreen) this._downloadViewModel);
      this.Items.Add((IScreen) this._bookmarkViewModel);
      this.Items.Add((IScreen) this._purchaseViewModel);
      this.Items.Add((IScreen) this._quizViewModel);
      this.Items.Add((IScreen) this._historyViewModel);
      IProfileTabViewModel profileTabViewModel = this.Items.OfType<IProfileTabViewModel>().FirstOrDefault<IProfileTabViewModel>((Func<IProfileTabViewModel, bool>) (x => x.Type == this.SelectedType));
      if (profileTabViewModel == null)
        return;
      this.ActivateItem((IScreen) profileTabViewModel);
    }
  }
}
