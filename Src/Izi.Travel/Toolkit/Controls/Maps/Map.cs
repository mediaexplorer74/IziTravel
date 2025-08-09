// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.UserLocationMarker
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using Windows.UI.Xaml;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public class Map
    {
        public System.Action<object, MapZoomLevelChangedEventArgs> ZoomLevelChanged;
        public double ZoomLevel;

        internal DependencyObjectCollection<DependencyObject> GetValue(DependencyProperty childrenProperty)
        {
            throw new NotImplementedException();
        }

        internal void SetValue(DependencyProperty childrenProperty, object sourceCollection)
        {
            throw new NotImplementedException();
        }
    }
}