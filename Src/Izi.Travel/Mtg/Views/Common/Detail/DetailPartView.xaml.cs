// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Common.Detail.DetailPartView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Utility;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Mtg.Views.Common.Detail
{
  public partial class DetailPartView : Page
  {
    
    public DetailPartView()
    {
       this.InitializeComponent();
    }

    ~DetailPartView()
    {
      
    }

    //protected override void OnBackKeyPress(CancelEventArgs e)
    //{
    //  if (!(this.DataContext is DetailPartViewModel dataContext) || dataContext.MtgObject == null)
    //    return;
    //  NavigationHelper.TryGoBack(e, dataContext.MtgObject.ParentUid, dataContext.MtgObject.Language);
    //}
    
  }
}

