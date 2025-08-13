// ********************************************************************
// Type: Izi.Travel.Core.Context.NavigationContext
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml.Navigation;
#nullable disable

namespace Izi.Travel.Core.Context
{
    public class NavigationContext : INavigationContext
    {
        private static volatile NavigationContext _instance;
        private static readonly object SyncRoot = new object();

        private NavigationContext()
        {
        }

        public static NavigationContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new NavigationContext();
                    }
                }
                return _instance;
            }
        }

        public object Parameter { get; private set; }
        public Type SourcePageType { get; private set; }
        public NavigationMode NavigationMode { get; private set; }

        internal void SetContext(object parameter, Type sourcePageType, NavigationMode navigationMode)
        {
            Parameter = parameter;
            SourcePageType = sourcePageType;
            NavigationMode = navigationMode;
        }
    }
}
