// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.MapperBase`2
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping
{
  public abstract class MapperBase<TSource, TTarget>
  {
    public abstract TTarget Convert(TSource source);

    public abstract TSource ConvertBack(TTarget target);
  }
}
