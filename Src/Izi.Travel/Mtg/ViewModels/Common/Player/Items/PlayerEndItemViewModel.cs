using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using System;
using System.Linq;
using System.Linq.Expressions;
using Windows.UI.Xaml.Navigation;

namespace Izi.Travel.Mtg.ViewModels.Common.Player.Items
{
    public class PlayerEndItemViewModel : PlayerItemViewModel
    {
        private RelayCommand _returnCommand;

        public RelayCommand ReturnCommand
        {
            get
            {
                return _returnCommand ??= new RelayCommand(new Action<object>(Return));
            }
        }

        private void Return(object parameter)
        {
            var navigationService = ShellServiceFacade.NavigationService;
            var uri = navigationService.UriFor<DetailPartViewModel>()
                .WithParam(x => x.Uid, MtgObjectRoot.Uid)
                .WithParam(x => x.Language, MtgObjectRoot.Language)
                .BuildUri();

            // In UWP, we use Frame.BackStack instead of JournalEntry
            var frame = Windows.UI.Xaml.Window.Current?.Content as Windows.UI.Xaml.Controls.Frame;
            if (frame != null && frame.BackStack.Any())
            {
                var lastPage = frame.BackStack.Last();
                if (lastPage != null/* && UriHelper.EqualsByCommonParameters(uri, lastPage.SourcePageType.Name)*/)
                {
                    navigationService.GoBack();
                    return;
                }
            }

            //navigationService.Navigate(uri);
        }

        public PlayerEndItemViewModel(
            PlayerViewModel playerViewModel,
            MtgObject mtgObjectRoot,
            MtgObject mtgObject)
            : base(playerViewModel, -1, mtgObjectRoot, null, mtgObject)
        {
        }

        protected override string GetImageUrl()
        {
            return MtgObject?.MainImageMedia == null || MtgObject.ContentProvider == null 
                ? null 
                : ServiceFacade.MediaService.GetImageUrl(
                    MtgObject.MainImageMedia.Uid, 
                    MtgObject.ContentProvider.Uid, 
                    ImageFormat.High800X600);
        }
    }
}

