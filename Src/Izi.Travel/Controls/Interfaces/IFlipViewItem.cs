using System.ComponentModel;

namespace Izi.Travel.Controls.Interfaces
{
    public interface IFlipViewItem : INotifyPropertyChanged
    {
        string PreviewUrl { get; }
        bool IsSelected { get; set; }
    }
}
