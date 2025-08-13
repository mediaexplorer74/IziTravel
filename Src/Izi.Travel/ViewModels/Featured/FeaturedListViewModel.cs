using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Core.Command;
using Izi.Travel.Mtg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace Izi.Travel.ViewModels.Featured
{
    /// <summary>
    /// ViewModel for the featured list view
    /// </summary>
    public class FeaturedListViewModel : BaseListViewModel<FeaturedListItemViewModel>
    {
        private bool _isLoadingMore;
        private bool _hasMoreItems = true;

        /// <summary>
        /// Gets or sets a value indicating whether the header is visible
        /// </summary>
        public bool IsHeaderVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the footer is visible
        /// </summary>
        public bool IsFooterVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether more items are being loaded
        /// </summary>
        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set => Set(ref _isLoadingMore, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether there are more items to load
        /// </summary>
        public bool HasMoreItems
        {
            get => _hasMoreItems;
            set => Set(ref _hasMoreItems, value);
        }

        /// <summary>
        /// Command to explore more items
        /// </summary>
        public IAsyncCommand ExploreCommand { get; set; }

        /// <summary>
        /// Command to load more items
        /// </summary>
        public IAsyncCommand LoadMoreCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturedListViewModel"/> class
        /// </summary>
        public FeaturedListViewModel()
        {
            InitializeCommands();
        }

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
            base.OnInitialize();
            LoadDataAsync();
        }

        /// <inheritdoc/>
        protected override void OnActivate()
        {
            base.OnActivate();
            
            // Load initial data if needed
            if (Items == null || !Items.Any())
            {
                LoadDataAsync();
            }
        }

        /// <inheritdoc/>
        protected override async Task<IEnumerable<FeaturedListItemViewModel>> GetDataAsync()
        {
            try
            {
                
                var languages = ServiceFacade.SettingsService.GetAppSettings()?.Languages;
                if (languages == null)
                    return Enumerable.Empty<FeaturedListItemViewModel>();

                var featuredItems = await ServiceFacade.MtgObjectService.GetFeaturedListAsync(languages);
                if (featuredItems == null)
                    return Enumerable.Empty<FeaturedListItemViewModel>();

                return featuredItems.Select(x => new FeaturedListItemViewModel(this, x));
            }
            catch (Exception ex)
            {
                // TODO: Log error
                System.Diagnostics.Debug.WriteLine($"Error loading featured items: {ex.Message}");
                return Enumerable.Empty<FeaturedListItemViewModel>();
            }
            finally
            {
                // no-op
            }
        }

        /// <summary>
        /// Loads more items asynchronously
        /// </summary>
        public async Task LoadMoreItemsAsync()
        {
            if (IsLoadingMore || !HasMoreItems)
                return;

            try
            {
                IsLoadingMore = true;
                
                // TODO: Implement pagination if the API supports it
                // For now, we'll just load all items at once
                var newItems = await GetDataAsync();
                
                if (newItems != null && newItems.Any())
                {
                    // Ensure Items is an ObservableCollection and add the new items
                    if (Items == null)
                        Items = new ObservableCollection<FeaturedListItemViewModel>();

                    foreach (var vm in newItems)
                        Items.Add(vm);
                    
                    // For now, we'll assume there are no more items after the first load
                    // Update this when implementing proper pagination
                    HasMoreItems = false;
                }
                else
                {
                    HasMoreItems = false;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log error
                System.Diagnostics.Debug.WriteLine($"Error loading more featured items: {ex.Message}");
            }
            finally
            {
                IsLoadingMore = false;
            }
        }

        private void InitializeCommands()
        {
            ExploreCommand = new RelayCommand(ExecuteExploreCommand);
            LoadMoreCommand = new RelayCommand(async _ => await LoadMoreItemsAsync(), _ => !IsLoadingMore && HasMoreItems);
        }

        private async void ExecuteExploreCommand(object parameter)
        {
            // TODO: Implement explore command
            await Task.CompletedTask;
        }
    }
}
