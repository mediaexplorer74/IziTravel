// ********************************************************************
// Type: Izi.Travel.Core.Resources.ManifestResources
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
namespace Izi.Travel.Core.Resources
{
  public class ManifestResources
  {
    private static ManifestManager _manifestManager;

    public static ManifestManager ManifestManager
    {
      get
      {
        return ManifestResources._manifestManager ?? (ManifestResources._manifestManager = new ManifestManager());
      }
    }

    public static string ApplicationTitle
    {
      get => ManifestResources.ManifestManager.GetAppAttributeValue("Title");
    }
  }
}
