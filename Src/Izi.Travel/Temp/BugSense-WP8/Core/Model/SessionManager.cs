﻿// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.SessionManager
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;

#nullable disable
namespace BugSense.Core.Model
{
  internal class SessionManager
  {
    public DateTime PingSessionStart { get; set; }

    public DateTime SessionStart { get; set; }

    public static SessionManager Instance => SessionManager.Nested.instance;

    private class Nested
    {
      internal static readonly SessionManager instance = new SessionManager();
    }
  }
}
