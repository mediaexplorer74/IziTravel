// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.MessengerResult
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Linq;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>A Caliburn.Micro Result that lets you show messages.</summary>
  public class MessengerResult : IResult<MessageResult>, IResult
  {
    private readonly string message;
    private string caption = "";
    private MessageButton button;
    private MessageImage image;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Extras.MessengerResult" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public MessengerResult(string message)
    {
      this.message = message;
      this.Result = MessageResult.None;
    }

    /// <summary>Gets the message</summary>
    public MessageResult Result { get; protected set; }

    /// <summary>Sets the caption.</summary>
    /// <param name="text">The caption text.</param>
    /// <returns></returns>
    public MessengerResult Caption(string text = "")
    {
      this.caption = text;
      return this;
    }

    /// <summary>Sets the button.</summary>
    /// <param name="buttons">The button.</param>
    /// <returns></returns>
    public MessengerResult Buttons(MessageButton buttons = MessageButton.OK)
    {
      this.button = buttons;
      return this;
    }

    /// <summary>Sets the image.</summary>
    /// <param name="icon">The image.</param>
    /// <returns></returns>
    public MessengerResult Image(MessageImage icon = MessageImage.None)
    {
      this.image = icon;
      return this;
    }

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      this.Result = ((IMessageService) IoC.GetAllInstances(typeof (IMessageService)).FirstOrDefault<object>() ?? (IMessageService) new MessageService()).Show(this.message, this.caption, this.button, this.image);
      this.Completed((object) this, new ResultCompletionEventArgs());
    }

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };
  }
}
