// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.IExceptionManager
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Windows;

#nullable disable
namespace BugSense.Core.Model
{
  public interface IExceptionManager
  {
    event EventHandler<ApplicationUnhandledExceptionEventArgs> UnhandledException;
  }
}
