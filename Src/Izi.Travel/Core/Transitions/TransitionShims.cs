using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Izi.Travel.Shell.Core.Transitions
{
    // Minimal shims to replace WP8 TransitionElement/ITransition with UWP-friendly types
    public abstract class TransitionElement : DependencyObject
    {
        public abstract ITransition GetTransition(UIElement element);
    }

    public interface ITransition
    {
        event EventHandler Completed;
        void Begin();
        void Stop();
    }

    public sealed class Transition : ITransition
    {
        private readonly UIElement _element;
        private readonly Storyboard _storyboard;

        public event EventHandler Completed;

        public Transition(UIElement element, Storyboard storyboard)
        {
            _element = element;
            _storyboard = storyboard ?? new Storyboard();
            _storyboard.Completed += (s, e) => Completed?.Invoke(this, EventArgs.Empty);
        }

        public void Begin()
        {
            _storyboard.Begin();
        }

        public void Stop()
        {
            _storyboard.Stop();
        }
    }
}
