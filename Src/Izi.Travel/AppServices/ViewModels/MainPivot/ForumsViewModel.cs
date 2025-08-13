// IziTravel.AppServices.ViewModels.MainPivot.ForumsViewModel

using Caliburn.Micro;
using Izi.Travel.AppServices.Controllers;
using Izi.Travel.AppServices.DataModels;
using Izi.Travel.AppServices.ViewModels.Forum;
using Izi.Travel.Communication;
using Izi.Travel.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Izi.Travel.AppServices.Navigation;

#nullable disable
namespace Izi.Travel.AppServices.ViewModels.MainPivot
{
  public class ForumsViewModel : Screen
  {
    private readonly IBusyIndicator _busyIndicator;
    private readonly INavigationService _navigationService;
    private readonly ForumController _forumController;
    private readonly ForumDataService _forumDataService;

    public ForumsViewModel(
      ForumController forumController,
      ForumDataService forumDataService,
      IBusyIndicator busyIndicator,
      INavigationService navigationService)
    {
      this._forumController = forumController;
      this._forumDataService = forumDataService;
      this._busyIndicator = busyIndicator;
      this._navigationService = navigationService;
    }

    private List<ForumDataModel> Forums_BackingField;
    public List<ForumDataModel> Forums
    {
      get => this.Forums_BackingField;
      set
      {
        if (this.Forums_BackingField == value)
          return;
        this.Forums_BackingField = value;
        this.NotifyOfPropertyChange(nameof (Forums));
      }
    }

    public void OpenForum(ForumDataModel forum)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
     
      // ISSUE: method reference
      // ISSUE: method reference
      //this._navigationService.UriFor<ForumPageViewModel>().WithParam<string>(
      //    Expression.Lambda<Func<ForumPageViewModel, string>>(
      //        (Expression) Expression.Property((Expression) parameterExpression1,
      //        (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle) 
      //        __methodref (ForumPageViewModel.get_ForumName))), parameterExpression1),
      //    forum.Title).WithParam<string>(Expression.Lambda<Func<ForumPageViewModel, string>>(
      //        (Expression) Expression.Property((Expression) parameterExpression2, 
      //        (MethodInfo) MethodBase.GetMethodFromHandle((RuntimeMethodHandle) __methodref
      //        (ForumPageViewModel.get_ForumId))), parameterExpression2), forum.Id).Navigate();
    }

    public async Task LoadDataAsync()
    {
      using (this._busyIndicator.StartJob())
      {
        ForumModel rootForum = await this._forumDataService.LoadForumHierarchyAsync();
        this.Forums = Enumerable.ToList<ForumDataModel>(((IEnumerable<ForumModel>) rootForum.Children)
            .Select<ForumModel, ForumDataModel>(new Func<ForumModel, ForumDataModel>(
                this._forumController.CreateDataModel)));
      }
    }
  }

    public interface INavigationService
    {
        /// <summary>
        /// Gets the key corresponding to the currently displayed page.
        /// </summary>
        string CurrentPageKey { get; }

        /// <summary>
        /// Gets a value indicating whether there is at least one entry in back navigation history.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets the navigation stack of journal entries.
        /// </summary>
        IEnumerable<JournalEntry> BackStack { get; }

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="pageKey">The key of the page to navigate to.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        bool NavigateTo(string pageKey);

        /// <summary>
        /// Navigates to the specified page with the specified parameter.
        /// </summary>
        /// <param name="pageKey">The key of the page to navigate to.</param>
        /// <param name="parameter">The navigation parameter.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        bool NavigateTo(string pageKey, object parameter);

        /// <summary>
        /// Navigates to the specified URI.
        /// </summary>
        /// <param name="uri">The URI to navigate to.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        bool Navigate(Uri uri);

        /// <summary>
        /// Navigates to the previous page in the navigation history.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Removes the most recent entry from the back stack.
        /// </summary>
        void RemoveBackEntry();

        /// <summary>
        /// Gets the parameter associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="key">The key of the parameter.</param>
        /// <returns>The parameter value, or default(T) if not found.</returns>
        T GetParameter<T>(string key);
    }
}
