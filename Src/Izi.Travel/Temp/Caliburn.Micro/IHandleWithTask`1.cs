// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IHandleWithTask`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///  Denotes a class which can handle a particular type of message and uses a Task to do so.
  /// </summary>
  public interface IHandleWithTask<TMessage> : IHandle
  {
    /// <summary>Handle the message with a Task.</summary>
    /// <param name="message">The message.</param>
    /// <returns>The Task that represents the operation.</returns>
    Task Handle(TMessage message);
  }
}
