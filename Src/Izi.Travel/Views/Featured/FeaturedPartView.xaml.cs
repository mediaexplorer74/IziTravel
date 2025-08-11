using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Izi.Travel.Shell.Controls;

namespace Izi.Travel.Shell.Views.Featured
{
    public sealed partial class FeaturedPartView : Page
    {
        private readonly List<object> _flipViewItems = new List<object>();
        
        public FeaturedPartView()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Initialize flip view items
            _flipViewItems.Clear();
            _flipViewItems.Add(new { TemplateKey = "WelcomeTemplate" });
            _flipViewItems.Add(new { TemplateKey = "ListTemplate" });
            
            // Set the items source
            PartFlipView.ItemsSource = _flipViewItems;

            // Set the initial selected index if needed
            dynamic viewModel = DataContext;
            if (viewModel != null && viewModel.SelectedIndex >= 0)
            {
                PartFlipView.SelectedIndex = viewModel.SelectedIndex;
            }
            
            // Set up the item template selector if needed
            PartFlipView.ItemTemplateSelector = new FlipViewTemplateSelector(this.Resources);
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Additional navigation logic if needed
        }
    }
    
    public class FlipViewTemplateSelector : Windows.UI.Xaml.Controls.DataTemplateSelector
    {
        private readonly ResourceDictionary _resources;
        
        public FlipViewTemplateSelector(ResourceDictionary resources)
        {
            _resources = resources;
        }
        
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            dynamic dataItem = item;
            if (dataItem != null)
            {
                return _resources[dataItem.TemplateKey] as Windows.UI.Xaml.DataTemplate;
            }
            
            return base.SelectTemplateCore(item, container);
        }
    }
}
