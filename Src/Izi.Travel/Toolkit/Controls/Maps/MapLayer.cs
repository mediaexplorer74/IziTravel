// ********************************************************************
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.MapItemsControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.ObjectModel;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public class MapLayer
    {
        private MapControl mapControl;

        public MapLayer()
        {
        }

        public MapLayer(MapControl mapControl)
        {
            this.mapControl = mapControl;
        }

        internal void Add(MapOverlay overlay)
        {
            throw new NotImplementedException();
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }

        public static explicit operator Collection<object>(MapLayer v)
        {
            throw new NotImplementedException();
        }
    }
}