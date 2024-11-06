// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.IMessageService
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>Interface for the message service.</summary>
  public interface IMessageService
  {
    /// <summary>Shows the specified message and returns the result.</summary>
    /// <param name="message">The message.</param>
    /// <param name="caption">The caption.</param>
    /// <param name="button">The button.</param>
    /// <param name="icon">The icon.</param>
    /// <returns>The <see cref="T:Caliburn.Micro.Extras.MessageResult" />.</returns>
    MessageResult Show(string message, string caption = "", MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None);

    /// <summary>
    /// Shows the specified message and allows to await for the message to complete.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="caption">The caption.</param>
    /// <param name="button">The button.</param>
    /// <param name="icon">The icon.</param>
    /// <returns>A Task containing the <see cref="T:Caliburn.Micro.Extras.MessageResult" />.</returns>
    Task<MessageResult> ShowAsync(
      string message,
      string caption = "",
      MessageButton button = MessageButton.OK,
      MessageImage icon = MessageImage.None);
  }
}
