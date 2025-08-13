// ********************************************************************
// Type: Izi.Travel.Core.Controls.Tiles.FlipTileControlBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using Windows.Foundation;
using System.Windows.Input;
using System;

namespace Izi.Travel.Core.Controls.Tiles
{
    public class FlipTileControlBehavior : Behavior<FlipTileControl>
    {
        private TypedEventHandler<FlipTileControl, FlipTileState> _stateChangedHandler;

        public static readonly DependencyProperty StateChangeCommandProperty = 
            DependencyProperty.Register(
                nameof(StateChangeCommand), 
                typeof(ICommand), 
                typeof(FlipTileControlBehavior), 
                new PropertyMetadata(null));

        

        public ICommand StateChangeCommand
        {
            get => (ICommand)GetValue(StateChangeCommandProperty);
            set => SetValue(StateChangeCommandProperty, value);
        }

        
        protected override void OnAttached()
        {
            base.OnAttached();
            _stateChangedHandler = OnStateChanged;
            //AssociatedObject.StateChanged += _stateChangedHandler;
        }

        protected override void OnDetaching()
        {
            //if (_stateChangedHandler != null)
            //    AssociatedObject.StateChanged -= _stateChangedHandler;

            _stateChangedHandler = null;
            base.OnDetaching();
        }

        private void OnStateChanged(FlipTileControl control, FlipTileState state)
        {
            if (StateChangeCommand?.CanExecute(state) == true)
                StateChangeCommand.Execute(state);
        }







    }
}

