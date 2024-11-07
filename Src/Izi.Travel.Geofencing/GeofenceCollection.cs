// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.GeofenceCollection
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using System;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Izi.Travel.Geofencing
{
  public class GeofenceCollection : ObservableCollection<Geofence>
  {
    protected override void InsertItem(int index, Geofence item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (this.Items.Any<Geofence>((Func<Geofence, bool>) (x => x.Id == item.Id)))
        throw new Exception(string.Format("Geofence with Id={0} already exists.", (object) item.Id));
      base.InsertItem(index, item);
    }
  }
}
