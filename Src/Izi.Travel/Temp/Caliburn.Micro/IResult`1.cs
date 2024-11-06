// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IResult`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Allows custom code to execute after the return of a action.
  /// </summary>
  /// <typeparam name="TResult">The type of the result.</typeparam>
  public interface IResult<out TResult> : IResult
  {
    /// <summary>Gets the result of the asynchronous operation.</summary>
    TResult Result { get; }
  }
}
