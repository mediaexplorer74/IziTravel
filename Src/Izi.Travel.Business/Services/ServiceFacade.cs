// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.ServiceFacade
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Services.Contract;

#nullable disable
namespace Izi.Travel.Business.Services
{
  public class ServiceFacade
  {
    private static IAudioService _audioService;
    private static ICultureService _cultureService;
    private static IAnalyticsService _analyticsService;
    private static IMediaService _mediaService;
    private static IMtgObjectService _mtgObjectService;
    private static IMtgObjectRegionService _mtgObjectRegionService;
    private static IMtgObjectDownloadService _mtgObjectDownloadService;
    private static IQuizService _quizService;
    private static Caliburn.Micro.ISettingsService _settingsService;

    public static IAudioService AudioService
    {
      get
      {
        return ServiceFacade._audioService ?? (ServiceFacade._audioService = IoC.Get<IAudioService>());
      }
    }

    public static ICultureService CultureService
    {
      get
      {
        return ServiceFacade._cultureService ?? 
                    (ServiceFacade._cultureService = IoC.Get<ICultureService>());
      }
    }

    public static IAnalyticsService AnalyticsService
    {
      get
      {
        return ServiceFacade._analyticsService 
                    ?? (ServiceFacade._analyticsService = IoC.Get<IAnalyticsService>());
      }
    }

    public static IMediaService MediaService
    {
      get
      {
        return ServiceFacade._mediaService ?? (ServiceFacade._mediaService = IoC.Get<IMediaService>());
      }
    }

    public static IMtgObjectService MtgObjectService
    {
      get
      {
        return ServiceFacade._mtgObjectService ?? (ServiceFacade._mtgObjectService 
                    = IoC.Get<IMtgObjectService>());
      }
    }

    public static IMtgObjectRegionService MtgObjectRegionService
    {
      get
      {
        return ServiceFacade._mtgObjectRegionService ?? 
                    (ServiceFacade._mtgObjectRegionService = IoC.Get<IMtgObjectRegionService>());
      }
    }

    public static IMtgObjectDownloadService MtgObjectDownloadService
    {
      get
      {
        return ServiceFacade._mtgObjectDownloadService ?? 
                    (ServiceFacade._mtgObjectDownloadService = IoC.Get<IMtgObjectDownloadService>());
      }
    }

    public static IQuizService QuizService
    {
      get => ServiceFacade._quizService ?? (ServiceFacade._quizService = IoC.Get<IQuizService>());
    }

    public static Caliburn.Micro.ISettingsService SettingsService
    {
      get
      {
        return ServiceFacade._settingsService ?? 
                    (ServiceFacade._settingsService = IoC.Get<Caliburn.Micro.ISettingsService>());
      }
    }
  }
}
