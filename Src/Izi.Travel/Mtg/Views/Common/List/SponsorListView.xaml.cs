// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Common.List.SponsorListView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Mtg.ViewModels.Common.List;

#nullable disable
namespace Izi.Travel.Mtg.Views.Common.List
{
    public partial class SponsorListView : UserControl
    {
        public SponsorListView()
        {
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var viewModel = DataContext as IListViewModelBase;
            if (viewModel?.NavigateCommand != null && viewModel.NavigateCommand.CanExecute(e.ClickedItem))
            {
                viewModel.NavigateCommand.Execute(e.ClickedItem);
            }
        }
    }
}
