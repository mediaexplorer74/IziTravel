// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Events.BaseEvent
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Events
{
  public class BaseEvent
  {
    public virtual string Label { get; set; }

    public virtual long Value { get; set; }

    public virtual EventAction Action => EventAction.Empty;

    public virtual IEnumerable<BaseParameter> GetParameters() => (IEnumerable<BaseParameter>) null;
  }
}
