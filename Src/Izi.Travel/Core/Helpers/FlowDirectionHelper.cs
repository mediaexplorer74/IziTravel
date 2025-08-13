// ********************************************************************
// Type: Izi.Travel.Core.Helpers.FlowDirectionHelper
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Services;
using System.Globalization;
using Windows.UI.Xaml;
#nullable disable
namespace Izi.Travel.Core.Helpers
{
  public static class FlowDirectionHelper
  {
    public static FlowDirection GetFlowDirection(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        return FlowDirection.LeftToRight;
      LanguageData languageByName = ServiceFacade.CultureService.GetLanguageByName(name);
      return languageByName == null || !languageByName.IsRightToLeft ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
    }

    public static FlowDirection GetCurrentFlowDirection()
    {
      return FlowDirectionHelper.GetFlowDirection(CultureInfo.CurrentCulture.Name);
    }
  }
}

