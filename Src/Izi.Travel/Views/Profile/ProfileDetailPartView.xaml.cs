// ********************************************************************
// Type: Izi.Travel.Views.Profile.ProfileDetailPartView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll


using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Izi.Travel.ViewModels.Profile;

#nullable disable
namespace Izi.Travel.Views.Profile
{
    public sealed partial class ProfileDetailPartView : Page
    {
        public ProfileDetailPartView()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => { /*this.Bindings.Update();*/ };
        }

        public ProfileDetailPartViewModel ViewModel => DataContext as ProfileDetailPartViewModel;
    }
}

