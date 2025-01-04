// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.ImagesFlipViewItem
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Extensions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
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
