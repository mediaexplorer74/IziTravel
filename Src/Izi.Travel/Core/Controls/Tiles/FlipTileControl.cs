// ********************************************************************
// Type: Izi.Travel.Shell.Core.Controls.Tiles.FlipTileControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace Izi.Travel.Shell.Core.Controls.Tiles
{
    public class FlipTileControl : BaseTileControl
    {
        private EventHandler<FlipTileState> _stateChanged;
        
        public static readonly DependencyProperty FrontContentProperty = 
            DependencyProperty.Register(
                nameof(FrontContent), 
                typeof(object), 
                typeof(FlipTileControl), 
                new PropertyMetadata(null));
                
        public static readonly DependencyProperty FrontContentTemplateProperty = 
            DependencyProperty.Register(
                nameof(FrontContentTemplate), 
                typeof(DataTemplate), 
                typeof(FlipTileControl), 
                new PropertyMetadata(null));
                
        public static readonly DependencyProperty BackContentProperty = 
            DependencyProperty.Register(
                nameof(BackContent), 
                typeof(object), 
                typeof(FlipTileControl), 
                new PropertyMetadata(null));
                
        public static readonly DependencyProperty BackContentTemplateProperty = 
            DependencyProperty.Register(
                nameof(BackContentTemplate), 
                typeof(DataTemplate), 
                typeof(FlipTileControl), 
                new PropertyMetadata(null));
                
        private static readonly DependencyProperty StateProperty = 
            DependencyProperty.Register(
                nameof(State), 
                typeof(FlipTileState), 
                typeof(FlipTileControl), 
                new PropertyMetadata(FlipTileState.Front, OnStatePropertyChanged));

        public object FrontContent
        {
            get => GetValue(FrontContentProperty);
            set => SetValue(FrontContentProperty, value);
        }

        public DataTemplate FrontContentTemplate
        {
            get => (DataTemplate)GetValue(FrontContentTemplateProperty);
            set => SetValue(FrontContentTemplateProperty, value);
        }

        public object BackContent
        {
            get => GetValue(BackContentProperty);
            set => SetValue(BackContentProperty, value);
        }

        public DataTemplate BackContentTemplate
        {
            get => (DataTemplate)GetValue(BackContentTemplateProperty);
            set => SetValue(BackContentTemplateProperty, value);
        }

        internal FlipTileState State
        {
            get => (FlipTileState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public event EventHandler<FlipTileState> StateChanged
        {
            add { _stateChanged += value; }
            remove { _stateChanged -= value; }
        }

        public FlipTileControl()
        {
            DefaultStyleKey = typeof(FlipTileControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualState();
        }

        protected override ITileService CreateTileService() => new FlipTileService();

        private void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, State == FlipTileState.Front ? "Front" : "Back", true);
        }

        private void OnStateChanged()
        {
            _stateChanged?.Invoke(this, State);
        }

        private static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FlipTileControl flipTileControl)
            {
                flipTileControl.UpdateVisualState();
                flipTileControl.OnStateChanged();
            }
        }
    }
}

