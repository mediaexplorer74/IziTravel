using Izi.Travel.Views.Featured;
using Izi.Travel.Core.Command;
using System.Threading.Tasks;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.ViewModels.Featured
{
    public sealed class FeaturedListTask
    {
        public async Task ShowAsync()
        {
            var listFlyoutViewModel = new FeaturedListFlyoutViewModel();
            new FeaturedListFlyoutView().DataContext = listFlyoutViewModel;
            
            var command = listFlyoutViewModel.OpenCommand;
            if (command == null || !command.CanExecute(null))
                return;

            if (command is IAsyncCommand asyncCommand)
            {
                await asyncCommand.ExecuteAsync(null);
            }
            else if (command is ICommand syncCommand)
            {
                syncCommand.Execute(null);
            }
        }

        // Keep the old method for backward compatibility
        public void Show()
        {
            ShowAsync().GetAwaiter().GetResult();
        }
    }
}
