// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Interfaces.IBugSenseServiceClient
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;
using System.Threading.Tasks;

#nullable disable
namespace BugSense.Core.Interfaces
{
  public interface IBugSenseServiceClient
  {
    Task<BugSenseResponseResult> ExecuteBugSenseRequestAsync(
      string url,
      string requestData,
      bool isError,
      string contentType);
  }
}
