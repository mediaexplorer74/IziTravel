// ********************************************************************
// Type: Izi.Travel.Core.Context.INavigationContext
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml.Navigation;
#nullable disable

namespace Izi.Travel.Core.Context
{
    public interface INavigationContext
    {
        // Define the minimum required members for the interface
        // These will be implemented by NavigationContext
        object Parameter { get; }
        Type SourcePageType { get; }
        NavigationMode NavigationMode { get; }
    }
}
