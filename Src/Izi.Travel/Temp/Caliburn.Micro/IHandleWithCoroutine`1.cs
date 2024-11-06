// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IHandleWithCoroutine`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///  Denotes a class which can handle a particular type of message and uses a Coroutine to do so.
  /// </summary>
  public interface IHandleWithCoroutine<TMessage> : IHandle
  {
    /// <summary>Handle the message with a Coroutine.</summary>
    /// <param name="message">The message.</param>
    /// <returns>The coroutine to execute.</returns>
    IEnumerable<IResult> Handle(TMessage message);
  }
}
