// Forum Page

using IziTravel.AppServices.ViewModels.Forum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace IziTravel.Views.Forum
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumPage : Page
    {
        public ForumPage()
        {
            this.InitializeComponent();
        }

        public ForumPageViewModel ViewModel
        {
            get => (ForumPageViewModel)((FrameworkElement)this).DataContext;
        }

        protected /*override*/ void OnBackKeyPress(EventArgs e)
        {
            //base.OnBackKeyPress(e);
            if (!this.ViewModel.CanReturnBack)
                return;
            //this.Title.IsBackTransition = true;
            //this.Title.NewContentTransitionEnded += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
            this.ViewModel.GoBack();
            //e.Cancel = true;
        }

        private void Title_NewContentTransitionEnded(object sender, EventArgs e)
        {
            //this.Title.NewContentTransitionEnded -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
            //this.Title.IsBackTransition = false;
        }
    }
}

/*
 // Decompiled with JetBrains decompiler
// Type: IziTravel.Views.Forum.ForumPage
// Assembly: IziTravel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB98E47-00BC-4074-98E2-E8BD94FCE6F3
// Assembly location: C:\Users\Admin\Desktop\RE\IziTravel\IziTravel.dll

using IziTravel.AppServices.ViewModels.Forum;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Telerik.Windows.Controls;

#nullable disable
namespace IziTravel.Views.Forum
{
  public sealed partial class ForumPage : PhoneApplicationPage
  {
   
    public ForumPage() => this.InitializeComponent();

    public ForumPageViewModel ViewModel
    {
      get => (ForumPageViewModel) ((FrameworkElement) this).DataContext;
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      base.OnBackKeyPress(e);
      if (!this.ViewModel.CanReturnBack)
        return;
      this.Title.IsBackTransition = true;
      this.Title.NewContentTransitionEnded += new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
      this.ViewModel.GoBack();
      e.Cancel = true;
    }

    private void Title_NewContentTransitionEnded(object sender, EventArgs e)
    {
      this.Title.NewContentTransitionEnded -= new EventHandler<EventArgs>(this.Title_NewContentTransitionEnded);
      this.Title.IsBackTransition = false;
    }  
  }
}

 */
