// ********************************************************************
// Type: Izi.Travel.Mtg.Controls.ImagesFlipViewItem
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Extensions;

#nullable disable
namespace Izi.Travel.Mtg.Controls
{
  public class ImagesFlipViewItem : PropertyChangedBase
  {
    private bool _isSelected;

    public string Uid { get; set; }

    public string Title { get; set; }

    public string ImageUrl { get; set; }

    public string PreviewUrl { get; set; }

    public bool IsSelected
    {
      get => this._isSelected;
      set => this.SetProperty<bool>(ref this._isSelected, value, propertyName: nameof (IsSelected));
    }
  }
}
