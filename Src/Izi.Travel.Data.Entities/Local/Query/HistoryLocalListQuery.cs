﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Local.Query.HistoryLocalListQuery
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using Izi.Travel.Data.Entities.Local.Query.Base;
using System;

#nullable disable
namespace Izi.Travel.Data.Entities.Local.Query
{
  public class HistoryLocalListQuery : LocalListQueryBase
  {
    public DateTime From { get; set; }

    public DateTime To { get; set; }
  }
}
