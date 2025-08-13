// ********************************************************************
// Type: Izi.Travel.Core.Resources.LocalizedStrings
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
namespace Izi.Travel.Core.Resources
{
  public class LocalizedStrings
  {
    private static readonly AppResources LocalLocalizedResources = new AppResources();
    private static readonly ManifestResources LocalManifestResources = new ManifestResources();

    public AppResources LocalizedResources => LocalizedStrings.LocalLocalizedResources;

    public ManifestResources ManifestResources => LocalizedStrings.LocalManifestResources;
  }
}
