// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.CanonicalPhoneName
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

#nullable disable
namespace GoogleAnalytics
{
  public class CanonicalPhoneName
  {
    public string ReportedManufacturer { get; set; }

    public string ReportedModel { get; set; }

    public string CanonicalManufacturer { get; set; }

    public string CanonicalModel { get; set; }

    public string Comments { get; set; }

    public bool IsResolved { get; set; }

    public string FullCanonicalName => this.CanonicalManufacturer + " " + this.CanonicalModel;
  }
}
