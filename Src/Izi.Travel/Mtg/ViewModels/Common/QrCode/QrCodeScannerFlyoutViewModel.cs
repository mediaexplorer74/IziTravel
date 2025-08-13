// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.QrCode.QrCodeScannerFlyoutViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Controls.Flyout;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Core.Helpers;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Core.Services.Entities;
using Izi.Travel.Mtg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.System;
using Izi.Travel.Utility.Extensions;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using System.Diagnostics;
using System.ComponentModel;
#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.QrCode
{
  public class QrCodeScannerFlyoutViewModel : BaseSearchFlyoutViewModel, INotifyPropertyChanged
  {
    private bool _isEmpty;
    private readonly CoreDispatcher _dispatcher;

    public bool IsEmpty
    {
      get => this._isEmpty;
      set => this.SetProperty<bool>(ref this._isEmpty, value, propertyName: nameof (IsEmpty));
    }

    public QrCodeScannerFlyoutViewModel(IScreen parentScreen)
      : base(parentScreen)
    {
        _dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
    }

    protected override async Task<SearchFlyoutResult> SearchTask(object parameter)
    {
        var result = new SearchFlyoutResult();
        var data = parameter as string;
        
        if (string.IsNullOrEmpty(data))
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IsEmpty = true;
            });
            return SearchFlyoutResult.Empty;
        }

        // Try to parse as MTG link first
        var mtgLinkInfo = MtgLinkInfo.Parse(data);
        if (mtgLinkInfo != null)
        {
            try
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    IsBusy = true;
                    IsEmpty = false;
                });

                var appSettings = ServiceFacade.SettingsService.GetAppSettings();
                var languages = new List<string>();
                
                if (!string.IsNullOrWhiteSpace(mtgLinkInfo.Language))
                    languages.Add(mtgLinkInfo.Language);
                if (!string.IsNullOrWhiteSpace(this.ParentLanguage))
                    languages.Add(this.ParentLanguage);
                    
                languages.AddRange(ServiceFacade.CultureService
                    .GetNeutralLanguageCodes()
                    .OrderAs(ServiceFacade.SettingsService.GetAppSettings().Languages)
                    .Where(x => !languages.Contains(x, StringComparer.OrdinalIgnoreCase)));

                if (!string.IsNullOrWhiteSpace(mtgLinkInfo.Passcode))
                {
                    appSettings.CodeName = mtgLinkInfo.Passcode;
                    ServiceFacade.SettingsService.SaveAppSettings(appSettings);
                }

                MtgObject mtgObject = null;
                try
                {
                    var filter = new MtgObjectFilter
                    {
                        Uid = mtgLinkInfo.ObjectId,
                        Languages = languages.ToArray(),
                        Includes = ContentSection.None,
                        Excludes = ContentSection.All,
                        Form = MtgObjectForm.Full
                    };
                    
                    mtgObject = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
                    
                    if (mtgObject == null)
                    {
                        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            IsEmpty = true;
                        });
                        return SearchFlyoutResult.Empty;
                    }

                    string parentUid = mtgLinkInfo.ParentId ?? mtgObject.ParentUid;
                    
                    result.Success = true;
                    result.MtgObject = mtgObject;
                    
                    var mtgObjectParent = new MtgObject
                    {
                        Uid = parentUid,
                        Type = this.ParentType
                    };
                    
                    result.MtgObjectParent = mtgObjectParent;
                    
                    await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        this.ActivateInternal(mtgObjectParent, mtgObject, ActivationTypeParameter.QrCode);
                        this.IsBusy = false;
                        this.NavigateInternal(mtgObject, parentUid);
                    });
                    
                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        IsEmpty = true;
                    });
                    return SearchFlyoutResult.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    IsEmpty = true;
                });
                return SearchFlyoutResult.Empty;
            }
        }
        
        // If not an MTG link, try to handle as URI
        if (Uri.TryCreate(data, UriKind.RelativeOrAbsolute, out var uri))
        {
            try
            {
                if (!uri.OriginalString.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
                    !uri.OriginalString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    uri = new Uri("http://" + uri.OriginalString.TrimStart('/'), UriKind.Absolute);
                }

                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        await Launcher.LaunchUriAsync(uri);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error launching URI: {ex.Message}");
                    }
                });
                
                return new SearchFlyoutResult { Success = true };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        await ShellServiceFacade.DialogService.ShowAsync(
                            AppResources.ErrorInvalidExternalLinkTitle, 
                            AppResources.ErrorInvalidExternalLinkInfo, 
                            MessageBoxButtonContent.Ok);
                        IsEmpty = true;
                    }
                    catch (Exception dialogEx)
                    {
                        Debug.WriteLine($"Error showing dialog: {dialogEx.Message}");
                    }
                });
                return SearchFlyoutResult.Empty;
            }
        }
        
        // If we get here, the data couldn't be processed as either an MTG link or a URI
        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
        {
            try
            {
                await ShellServiceFacade.DialogService.ShowAsync(
                    AppResources.LabelQrScanner, 
                    string.Format(AppResources.ErrorBarcodeIncorrectData, data), 
                    MessageBoxButtonContent.Ok);
                IsEmpty = true;
            }
            catch (Exception dialogEx)
            {
                Debug.WriteLine($"Error showing dialog: {dialogEx.Message}");
            }
        });
        
        return SearchFlyoutResult.Empty;
    }
  }
}

