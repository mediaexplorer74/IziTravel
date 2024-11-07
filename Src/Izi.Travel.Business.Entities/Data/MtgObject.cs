// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.MtgObject
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class MtgObject : CoreObject
  {
    public string ParentUid { get; set; }

    public MtgObjectType Type { get; set; }

    public MtgObjectStatus Status { get; set; }

    public MtgObjectCategory Category { get; set; }

    public string[] Languages { get; set; }

    public string Language => this.MainContent == null ? (string) null : this.MainContent.Language;

    public Location Location { get; set; }

    public TriggerZone[] TriggerZones { get; set; }

    public MapInfo Map { get; set; }

    public ContentProvider ContentProvider { get; set; }

    public MtgObject Publisher { get; set; }

    public Purchase Purchase { get; set; }

    public Izi.Travel.Business.Entities.Data.Content[] Content { get; set; }

    public Izi.Travel.Business.Entities.Data.Content MainContent
    {
      get => this.Content == null || this.Content.Length == 0 ? (Izi.Travel.Business.Entities.Data.Content) null : this.Content[0];
    }

    public Media MainAudioMedia
    {
      get
      {
        return this.MainContent == null || this.MainContent.Audio == null || this.MainContent.Audio.Length == 0 ? (Media) null : this.MainContent.Audio[0];
      }
    }

    public Media MainImageMedia
    {
      get
      {
        return this.MainContent == null || this.MainContent.Images == null || this.MainContent.Images.Length == 0 ? (Media) null : this.MainContent.Images[0];
      }
    }

    public Contacts Contacts { get; set; }

    public Schedule Schedule { get; set; }

    public DateTime DateTime { get; set; }

    public int ChildrenCount { get; set; }

    public bool Hidden { get; set; }

    public string CountryCode { get; set; }

    public int Duration { get; set; }

    public int Distance { get; set; }

    public int Size { get; set; }

    public int SizeInMegabytes => this.Size / 1024 / 1024;

    public string Hash { get; set; }

    public MtgObjectAccessType AccessType { get; set; }

    public string Key
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.Uid) || string.IsNullOrWhiteSpace(this.Language) ? (string) null : string.Format("{0}_{1}", (object) this.Uid, (object) this.Language).ToLower();
      }
    }

    public string Title => this.MainContent == null ? (string) null : this.MainContent.Title;

    public Rating Rating { get; set; }

    public Sponsor[] Sponsors { get; set; }
  }
}
