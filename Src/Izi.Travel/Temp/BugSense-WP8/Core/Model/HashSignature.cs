﻿// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.HashSignature
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System.Collections.Generic;

#nullable disable
namespace BugSense.Core.Model
{
  public class HashSignature
  {
    public string Signature { get; set; }

    public string Where { get; set; }

    public List<string> Tags { get; set; }

    public HashSignature()
    {
      this.Signature = "Unknown";
      this.Tags = new List<string>();
      this.Where = "Unknown";
    }
  }
}
