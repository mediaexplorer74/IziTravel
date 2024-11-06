// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSenseMessageException
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;

#nullable disable
namespace BugSense.Core.Model
{
  public class BugSenseMessageException : Exception
  {
    private string BugSenseStackTrace { get; set; }

    private string BugSenseMessage { get; set; }

    public BugSenseMessageException(string message, string stacktrace)
    {
      if (string.IsNullOrWhiteSpace(message))
        throw new ArgumentNullException(nameof (message), "Parameter cannot be null.");
      this.BugSenseStackTrace = !string.IsNullOrWhiteSpace(stacktrace) ? stacktrace : throw new ArgumentNullException(nameof (stacktrace), "Parameter cannot be null.");
      this.BugSenseMessage = message;
    }

    public override string Message => this.BugSenseMessage;

    public override string StackTrace => this.BugSenseStackTrace;
  }
}
