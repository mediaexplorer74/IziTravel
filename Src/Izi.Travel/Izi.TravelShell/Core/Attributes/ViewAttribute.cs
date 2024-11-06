// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Attributes.ViewAttribute
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class ViewAttribute : Attribute
  {
    public object Context { get; set; }

    public Type ViewType { get; private set; }

    public ViewAttribute(Type viewType) => this.ViewType = viewType;
  }
}
