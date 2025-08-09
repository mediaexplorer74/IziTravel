using System.Windows.Input;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Izi.Travel.Shell.Core.Components.Behaviors
{
    public class ListViewSelectBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof(SelectCommand), typeof(ICommand), typeof(ListViewSelectBehavior), new PropertyMetadata(null));

        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ItemClick += AssociatedObject_ItemClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ItemClick -= AssociatedObject_ItemClick;
        }

        private void AssociatedObject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SelectCommand != null && SelectCommand.CanExecute(e.ClickedItem))
            {
                SelectCommand.Execute(e.ClickedItem);
            }
        }
    }
}