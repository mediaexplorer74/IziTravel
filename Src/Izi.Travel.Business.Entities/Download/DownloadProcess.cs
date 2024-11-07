// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Download.DownloadProcess
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

#nullable disable
namespace Izi.Travel.Business.Entities.Download
{
  [DataContract]
  public class DownloadProcess
  {
    [IgnoreDataMember]
    public bool IsRestored { get; internal set; }

    [DataMember]
    public MtgObject MtgObject { get; internal set; }

    [DataMember]
    internal List<DownloadProcessStep> Steps { get; private set; }

    [IgnoreDataMember]
    public double Progress { get; internal set; }

    [IgnoreDataMember]
    public DownloadProcessState State { get; internal set; }

    [IgnoreDataMember]
    public DownloadProcessError Error { get; internal set; }

    [IgnoreDataMember]
    public string Uid => this.MtgObject == null ? (string) null : this.MtgObject.Uid;

    [IgnoreDataMember]
    public string Language => this.MtgObject == null ? (string) null : this.MtgObject.Language;

    [IgnoreDataMember]
    public string Key => this.MtgObject == null ? (string) null : this.MtgObject.Key;

    [IgnoreDataMember]
    public string Title
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.MainContent == null ? (string) null : this.MtgObject.MainContent.Title;
      }
    }

    [IgnoreDataMember]
    internal CancellationTokenSource CancellationTokenSource { get; set; }

    [IgnoreDataMember]
    internal System.Threading.Tasks.Task<bool> Task { get; set; }

    public DownloadProcess() => this.Steps = new List<DownloadProcessStep>();
  }
}
