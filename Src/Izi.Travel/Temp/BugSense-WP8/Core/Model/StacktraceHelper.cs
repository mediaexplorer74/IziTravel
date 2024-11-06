// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.StacktraceHelper
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Text;

#nullable disable
namespace BugSense.Core.Model
{
  public static class StacktraceHelper
  {
    public static string GetStackTrace(Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(!string.IsNullOrWhiteSpace(ex.ToString()) ? ex.ToString() : "not available");
      for (Exception innerException = ex.InnerException; innerException != null; innerException = innerException.InnerException)
      {
        string fullName = innerException.GetType().FullName;
        stringBuilder.AppendLine(string.Format("--- Inner exception of type {0} start ---", (object) fullName));
        stringBuilder.AppendLine(string.Format("--- Message: {0} ---", (object) innerException.Message.Replace(Environment.NewLine, " ")));
        stringBuilder.AppendLine(!string.IsNullOrWhiteSpace(innerException.ToString()) ? innerException.ToString() : "not available");
        stringBuilder.AppendLine("--- End of inner exception stack trace ---");
      }
      return stringBuilder.ToString();
    }
  }
}
