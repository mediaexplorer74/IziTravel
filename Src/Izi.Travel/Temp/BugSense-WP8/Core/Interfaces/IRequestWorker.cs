// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Interfaces.IRequestWorker
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;
using System;
using System.Threading.Tasks;

#nullable disable
namespace BugSense.Core.Interfaces
{
  public interface IRequestWorker
  {
    event EventHandler<BugSenseLoggedRequestEventArgs> LoggedRequestHandled;

    event EventHandler<BugSenseResponseResultEventArgs> PingEventCompleted;

    bool IsInitialized { get; set; }

    void Init();

    string GetErrorHash(string jsonRequest);

    Task Flush();

    Task<TransactionStartResult> TransactionStart(string transactionId);

    Task<TransactionStopResult> TransactionStop(string transactionId);

    Task<TransactionStopResult> TransactionCancel(string transactionId, string reason);

    void StopAllTransactions(string errorHash);

    Task<long> GetLastCrashId();

    Task<int> GetTotalCrashesNum();

    Task<bool> ClearTotalCrashesNum();

    Task<BugSenseResponseResult> SendEventAsync(BugSenseEventTag tag);

    BugSenseLogResult LogEvent(BugSenseEventTag tag);

    Task<BugSenseLogResult> LogEventAsync(BugSenseEventTag tag);

    Task<BugSenseResponseResult> SendEventAsync(string tag);

    Task<BugSenseLogResult> LogEventAsync(string tag);

    BugSenseLogResult LogEvent(string tag);

    Task<BugSenseResponseResult> SendExceptionAsync(
      Exception exception,
      LimitedCrashExtraDataList extraData = null,
      string filePath = null);

    Task<BugSenseLogResult> LogExceptionAsync(
      Exception exception,
      LimitedCrashExtraDataList extraData = null,
      string filePath = null);

    BugSenseLogResult LogException(
      Exception exception,
      LimitedCrashExtraDataList extraData = null,
      string filePath = null);

    BugSenseLogResult HandleException(
      Exception exception,
      LimitedCrashExtraDataList extraData = null,
      string filePath = null);

    Task<BugSenseLogResult> HandleExceptionAsync(
      Exception exception,
      LimitedCrashExtraDataList extraData = null,
      string filePath = null);

    Task ProcessPreviousLoggedCrashesAsync();

    BugSenseLogResult StartSession();

    Task<BugSenseLogResult> StartSessionAsync();

    BugSenseLogResult CloseSession();

    Task<BugSenseLogResult> CloseSessionAsync();
  }
}
