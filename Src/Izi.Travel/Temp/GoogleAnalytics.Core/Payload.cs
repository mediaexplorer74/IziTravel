// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.Payload
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class Payload
  {
    public Payload(IDictionary<string, string> data)
    {
      this.Data = data;
      this.TimeStamp = DateTimeOffset.UtcNow;
    }

    public IDictionary<string, string> Data { get; private set; }

    public DateTimeOffset TimeStamp { get; private set; }

    public bool IsUseSecure { get; set; }
  }
}
