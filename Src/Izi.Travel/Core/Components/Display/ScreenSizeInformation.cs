// ********************************************************************
// Type: Izi.Travel.Core.Components.Display.ScreenSizeInformation
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;

namespace Izi.Travel.Core.Components.Display
{
    public class ScreenSizeInformation
    {
        // In UWP, we use Window.Current.Bounds to get the screen dimensions
        private static readonly double ScreenWidth = Window.Current.Bounds.Width;
        private static readonly double ScreenHeight = Window.Current.Bounds.Height;

        public double Width => ScreenWidth;

        public double WidthNegative => -ScreenWidth;

        public double Height => ScreenHeight;

        public double HeightNegative => -ScreenHeight;
    }
}

