using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;

namespace Izi.Travel.Shell.Core.Components.Behaviors
{
    public class ListViewSelectBehavior : Behavior<ListView>
    {
        public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof(SelectCommand), typeof(System.Windows.Input.ICommand), typeof(ListViewSelectBehavior), new PropertyMetadata(null));

        public System.Windows.Input.ICommand SelectCommand
        {
            get => (System.Windows.Input.ICommand)GetValue(SelectCommandProperty);
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
