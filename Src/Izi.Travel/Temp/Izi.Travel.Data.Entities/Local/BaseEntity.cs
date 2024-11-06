// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Local.BaseEntity
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable
namespace Izi.Travel.Data.Entities.Local
{
  public abstract class BaseEntity : INotifyPropertyChanging, INotifyPropertyChanged
  {
    public event PropertyChangingEventHandler PropertyChanging;

    protected virtual void NotifyPropertyChanging([CallerMemberName] string propertyName = null)
    {
      PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
      if (propertyChanging == null)
        return;
      propertyChanging((object) this, new PropertyChangingEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) field, (object) value))
        return;
      PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
      if (propertyChanging != null)
        propertyChanging((object) this, new PropertyChangingEventArgs(propertyName));
      field = value;
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
