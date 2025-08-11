using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
    public class MapZoomLevelChangedEventArgs : EventArgs
    {
        public double NewZoomLevel { get; }

        public MapZoomLevelChangedEventArgs(double zoomLevel)
        {
            NewZoomLevel = zoomLevel;
        }
    }
}
