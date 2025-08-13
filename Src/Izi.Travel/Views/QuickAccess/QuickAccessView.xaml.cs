using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using System.Diagnostics;

namespace Izi.Travel.Views.QuickAccess
{
    public partial class QuickAccessView : UserControl
    {
        public QuickAccessView() => this.InitializeComponent();

        private void Hyperlink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            // For now, just log that the hyperlink was clicked
            // We'll implement proper navigation once we have the basic build working
            Debug.WriteLine("Hyperlink clicked in QuickAccessView");
            
            // In a real implementation, we would navigate to the explore page here
            // For now, we'll just log the click and continue
        }
    }
}


