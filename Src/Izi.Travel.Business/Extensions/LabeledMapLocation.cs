// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Extensions.MtgObjectExtensions
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Data.Entities.Common;

namespace Izi.Travel.Business.Extensions
{
    internal class LabeledMapLocation
    {
        private string title;
        private GeoCoordinate geoCoordinate;

        public LabeledMapLocation(string title, GeoCoordinate geoCoordinate)
        {
            this.title = title;
            this.geoCoordinate = geoCoordinate;
        }
    }
}