using Caliburn.Micro;
using Izi.Travel.Common.ViewModels.Flyout;
using Izi.Travel.Core.Command;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.ViewModels.Featured
{
    public sealed class FeaturedListFlyoutViewModel : FlyoutViewModel
    {
        private readonly FeaturedListViewModel _listViewModel;
        private bool _isRefreshing;

        public FeaturedListViewModel ListViewModel => _listViewModel;

        public FeaturedListFlyoutViewModel()
        {
            _listViewModel = IoC.Get<FeaturedListViewModel>();
            _listViewModel.ExploreCommand = CloseCommand;
        }

        protected override void OnOpening()
        {
            base.OnOpening();
            
            if (_isRefreshing)
                return;
                
            _isRefreshing = true;
            
            try
            {
                var refreshCommand = _listViewModel.RefreshCommand;
                if (refreshCommand == null || !refreshCommand.CanExecute(null))
                    return;

                if (refreshCommand is IAsyncCommand asyncCommand)
                {
                    // Fire and forget the async operation
                    _ = asyncCommand.ExecuteAsync(null)
                        .ContinueWith(t => 
                        {
                            if (t.IsFaulted)
                            {
                                // Log the error or handle it appropriately
                                System.Diagnostics.Debug.WriteLine($"Error executing refresh command: {t.Exception}");
                            }
                        });
                }
                else if (refreshCommand is ICommand syncCommand)
                {
                    // Execute sync command directly
                    syncCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OnOpening: {ex}");
                throw;
            }
            finally
            {
                _isRefreshing = false;
            }
        }
    }
}
