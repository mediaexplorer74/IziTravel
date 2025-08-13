// ********************************************************************
// Type: Izi.Travel.Toolkit.Controls.Maps.QueryExtensions
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;

namespace Izi.Travel.Toolkit.Controls.Maps
{
    public class Query<TResult>
    {
        public EventHandler<QueryCompletedEventArgs<TResult>> QueryCompleted;
    }
}