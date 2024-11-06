// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail.PublisherDetailInfoViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Model;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailInfoViewModel : MtgObjectTabViewModel
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (PublisherDetailInfoViewModel));
    private string _title;
    private string _description;
    private PublisherContactInfo[] _contacts;
    private RelayCommand _navigateSocialCommand;

    public override string DisplayName
    {
      get => AppResources.LabelInfo;
      set => throw new NotImplementedException();
    }

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, propertyName: nameof (Title));
    }

    public string Description
    {
      get => this._description;
      set
      {
        this.SetProperty<string>(ref this._description, value, propertyName: nameof (Description));
      }
    }

    public PublisherContactInfo[] Contacts
    {
      get => this._contacts;
      set
      {
        this.SetProperty<PublisherContactInfo[]>(ref this._contacts, value, (System.Action) (() => this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasContacts))), nameof (Contacts));
      }
    }

    public bool HasContacts => this.Contacts != null && this.Contacts.Length != 0;

    public RelayCommand NavigateSocialCommand
    {
      get
      {
        return this._navigateSocialCommand ?? (this._navigateSocialCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateSocialCommand)));
      }
    }

    private void ExecuteNavigateSocialCommand(object parameter)
    {
      if (!(parameter is PublisherContactInfo publisherContactInfo))
        return;
      try
      {
        string uriString = publisherContactInfo.NavigationUrl;
        if (!string.IsNullOrWhiteSpace(uriString) && !uriString.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) && !uriString.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
          uriString = "http://" + uriString;
        new WebBrowserTask()
        {
          Uri = new Uri(uriString, UriKind.RelativeOrAbsolute)
        }.Show();
      }
      catch (Exception ex)
      {
        PublisherDetailInfoViewModel.Logger.Error(ex);
        ShellServiceFacade.DialogService.Show(AppResources.ErrorInvalidExternalLinkTitle, AppResources.ErrorInvalidExternalLinkInfo, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) null);
      }
    }

    protected override void OnActivate()
    {
      if (this.MtgObject == null)
        return;
      if (this.MtgObjectContent != null)
      {
        this.Title = this.MtgObjectContent.Title;
        this.Description = !string.IsNullOrWhiteSpace(this.MtgObjectContent.Description) ? this.MtgObjectContent.Description : (string) null;
      }
      this.Contacts = PublisherDetailInfoViewModel.GetPublisherContacts(this.MtgObject.Contacts);
      base.OnActivate();
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[1]{ "NowPlaying" };
    }

    private static PublisherContactInfo[] GetPublisherContacts(Izi.Travel.Business.Entities.Data.Contacts mtgContacts)
    {
      if (mtgContacts == null)
        return (PublisherContactInfo[]) null;
      List<PublisherContactInfo> publisherContactInfoList = new List<PublisherContactInfo>();
      if (!string.IsNullOrWhiteSpace(mtgContacts.WebSite))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.website.png",
          NavigationUrl = mtgContacts.WebSite,
          Description = AppResources.ActionSocialWebSite
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.Facebook))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.facebook.png",
          NavigationUrl = mtgContacts.Facebook,
          Description = AppResources.ActionSocialFacebook
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.Twitter))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.twitter.png",
          NavigationUrl = mtgContacts.Twitter,
          Description = AppResources.ActionSocialTwitter
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.Instagram))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.instagram.png",
          NavigationUrl = mtgContacts.Instagram,
          Description = AppResources.ActionSocialInstagram
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.GooglePlus))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.googleplus.png",
          NavigationUrl = mtgContacts.GooglePlus,
          Description = AppResources.ActionSocialGooglePlus
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.Vk))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.vk.png",
          NavigationUrl = mtgContacts.Vk,
          Description = AppResources.ActionSocialVk
        });
      if (!string.IsNullOrWhiteSpace(mtgContacts.YouTube))
        publisherContactInfoList.Add(new PublisherContactInfo()
        {
          ImageUrl = "/Assets/Images/image.social.youtube.png",
          NavigationUrl = mtgContacts.YouTube,
          Description = AppResources.ActionSocialYouTube
        });
      return publisherContactInfoList.ToArray();
    }
  }
}
