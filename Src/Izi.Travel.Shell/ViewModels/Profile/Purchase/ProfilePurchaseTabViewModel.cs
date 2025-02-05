// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Purchase.ProfilePurchaseTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Shell.Views.Profile;
using System;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Purchase
{
  [View(typeof (ProfileTabView))]
  public class ProfilePurchaseTabViewModel : ProfileTabViewModel<ProfilePurchaseListViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelPurchases;
      set => throw new NotImplementedException();
    }

    public override ProfileType Type => ProfileType.Purchase;
  }
}
