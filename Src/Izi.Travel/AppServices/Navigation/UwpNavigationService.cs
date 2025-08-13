using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using INavigationService = Izi.Travel.AppServices.ViewModels.MainPivot.INavigationService;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

namespace Izi.Travel.AppServices.Navigation
{
    /// <summary>
    /// A UWP implementation of the INavigationService interface.
    /// </summary>
    public class UwpNavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Dictionary<string, object> _parametersByKey = new Dictionary<string, object>();
        private Frame _frame;

        /// <summary>
        /// Gets the key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_frame.CurrentSourcePageType == null)
                        return null;

                    var type = _frame.CurrentSourcePageType;
                    return _pagesByKey.ContainsValue(type) ? 
                        _pagesByKey.First(p => p.Value == type).Key : null;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is at least one entry in back navigation history.
        /// </summary>
        public bool CanGoBack => _frame?.CanGoBack ?? false;

        /// <summary>
        /// Gets the navigation stack of journal entries.
        /// </summary>
        public IEnumerable<JournalEntry> BackStack => 
            _frame?.BackStack?.Select(entry => new JournalEntry(
                new Uri(entry.SourcePageType.Name, UriKind.Relative),
                entry.Parameter,
                entry.SourcePageType)) ?? Enumerable.Empty<JournalEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UwpNavigationService"/> class.
        /// </summary>
        /// <param name="frame">The frame to use for navigation.</param>
        public UwpNavigationService(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
            _frame.Navigated += OnNavigated;
            
            // Configure known pages
            // TODO: Add your page configurations here
            // Example: Configure("MainPage", typeof(MainPage));
        }

        /// <summary>
        /// Configures the navigation service with a page key and page type.
        /// </summary>
        /// <param name="key">The key to associate with the page.</param>
        /// <param name="pageType">The type of the page.</param>
        public void Configure(string key, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="pageKey">The key of the page to navigate to.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        public bool NavigateTo(string pageKey)
        {
            return NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Navigates to the specified page with the specified parameter.
        /// </summary>
        /// <param name="pageKey">The key of the page to navigate to.</param>
        /// <param name="parameter">The navigation parameter.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        public bool NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (!_pagesByKey.TryGetValue(pageKey, out var pageType))
                {
                    return false;
                }

                var parameterKey = Guid.NewGuid().ToString();
                _parametersByKey[parameterKey] = parameter;
                
                var navigationResult = _frame.Navigate(pageType, parameterKey);
                return navigationResult;
            }
        }

        /// <summary>
        /// Gets the parameter associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="key">The key of the parameter.</param>
        /// <returns>The parameter value, or default(T) if not found.</returns>
        public T GetParameter<T>(string key)
        {
            lock (_parametersByKey)
            {
                if (_parametersByKey.TryGetValue(key, out var parameter) && parameter is T typedParameter)
                {
                    return typedParameter;
                }
                return default;
            }
        }

        /// <summary>
        /// Navigates to the specified URI.
        /// </summary>
        /// <param name="uri">The URI to navigate to.</param>
        /// <returns>True if navigation was successful; otherwise, false.</returns>
        public bool Navigate(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            
            // Extract page key from URI (assuming URI is in format "/PageKey")
            var pageKey = uri.OriginalString.TrimStart('/');
            return NavigateTo(pageKey);
        }

        /// <summary>
        /// Navigates to the previous page in the navigation history.
        /// </summary>
        public void GoBack()
        {
            if (CanGoBack)
            {
                _frame.GoBack();
            }
        }

        /// <summary>
        /// Removes the most recent entry from the back stack.
        /// </summary>
        public void RemoveBackEntry()
        {
            if (_frame.BackStack.Any())
            {
                _frame.BackStack.RemoveAt(_frame.BackStack.Count - 1);
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Clean up parameters after navigation
            if (e.Parameter is string parameterKey && _parametersByKey.ContainsKey(parameterKey))
            {
                _parametersByKey.Remove(parameterKey);
            }
        }

        /// <summary>
        /// Gets the navigation state as a string.
        /// </summary>
        /// <returns>A string representing the navigation state.</returns>
        public string GetNavigationState()
        {
            return _frame.GetNavigationState();
        }

        /// <summary>
        /// Sets the navigation state from a string.
        /// </summary>
        /// <param name="navigationState">The navigation state string.</param>
        public void SetNavigationState(string navigationState)
        {
            _frame.SetNavigationState(navigationState);
        }
    }
}
