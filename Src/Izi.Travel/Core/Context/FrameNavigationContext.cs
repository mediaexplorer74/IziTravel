// ********************************************************************
// Type: Izi.Travel.Core.Context.FrameNavigationContext
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml.Navigation;
#nullable disable
namespace Izi.Travel.Core.Context
{
  public class FrameNavigationContext : IFrameNavigationContext
  {
    private static volatile FrameNavigationContext _instance;
    private static readonly object SyncRoot = new object();

    private FrameNavigationContext()
    {
    }

    public static FrameNavigationContext Instance
    {
      get
      {
        if (FrameNavigationContext._instance == null)
        {
          lock (FrameNavigationContext.SyncRoot)
          {
            if (FrameNavigationContext._instance == null)
              FrameNavigationContext._instance = new FrameNavigationContext();
          }
        }
        return FrameNavigationContext._instance;
      }
    }

    public Uri Uri { get; private set; }

    public object Content { get; private set; }

    public NavigationMode NavigationMode { get; private set; }

    internal void SetContext(Uri uri, object content, NavigationMode navigationMode)
    {
      this.Uri = uri;
      this.Content = content;
      this.NavigationMode = navigationMode;
    }
  }
}

