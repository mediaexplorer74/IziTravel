// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.MessageService
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>
  /// Message service that implements the <see cref="T:Caliburn.Micro.Extras.IMessageService" /> by using the <see cref="T:System.Windows.MessageBox" /> class.
  /// </summary>
  public class MessageService : IMessageService
  {
    private static MessageResult TranslateMessageBoxResult(MessageBoxResult result)
    {
      return (MessageResult) Enum.Parse(typeof (MessageResult), result.ToString(), true);
    }

    private static MessageBoxButton TranslateMessageButton(MessageButton button)
    {
      try
      {
        return (MessageBoxButton) Enum.Parse(typeof (MessageBoxButton), button.ToString(), true);
      }
      catch (Exception ex)
      {
        throw new NotSupportedException(string.Format("Unfortunately, the default MessageBox class of does not support '{0}' button.", (object) button));
      }
    }

    /// <summary>Shows the specified message and returns the result.</summary>
    /// <param name="message">The message.</param>
    /// <param name="caption">The caption.</param>
    /// <param name="button">The button.</param>
    /// <param name="icon">The icon.</param>
    /// <returns>The <see cref="T:Caliburn.Micro.Extras.MessageResult" />.</returns>
    public MessageResult Show(
      string message,
      string caption = "",
      MessageButton button = MessageButton.OK,
      MessageImage icon = MessageImage.None)
    {
      return MessageService.ShowMessageBox(message, caption, button, icon);
    }

    /// <summary>
    /// Shows the specified message and allows to await for the message to complete.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="caption">The caption.</param>
    /// <param name="button">The button.</param>
    /// <param name="icon">The icon.</param>
    /// <returns>A Task containing the <see cref="T:Caliburn.Micro.Extras.MessageResult" />.</returns>
    public Task<MessageResult> ShowAsync(
      string message,
      string caption = "",
      MessageButton button = MessageButton.OK,
      MessageImage icon = MessageImage.None)
    {
      TaskCompletionSource<MessageResult> completionSource = new TaskCompletionSource<MessageResult>();
      try
      {
        MessageResult result = MessageService.ShowMessageBox(message, caption, button, icon);
        completionSource.SetResult(result);
      }
      catch (Exception ex)
      {
        completionSource.SetException(ex);
      }
      return completionSource.Task;
    }

    private static MessageResult ShowMessageBox(
      string message,
      string caption,
      MessageButton button,
      MessageImage icon)
    {
      if (string.IsNullOrEmpty(message))
        throw new ArgumentNullException(nameof (message));
      MessageBoxButton button1 = MessageService.TranslateMessageButton(button);
      return MessageService.TranslateMessageBoxResult(MessageBox.Show(message, caption, button1));
    }
  }
}
