using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Izi.Travel.Mtg.Commands
{
    /// <summary>
    /// Command to toggle bookmark status of an MTG object
    /// </summary>
    public class ToggleBookmarkCommand : BaseCommand, INotifyPropertyChanged
    {
        private readonly MtgObject _mtgObject;
        private readonly string _parentUid;
        private bool _hasBookmark;

        /// <summary>
        /// Gets or sets a value indicating whether the object is bookmarked
        /// </summary>
        public bool HasBookmark
        {
            get => _hasBookmark;
            set
            {
                if (SetProperty(ref _hasBookmark, value))
                {
                    NotifyPropertyChanged(nameof(Label));
                }
            }
        }

        /// <summary>
        /// Gets the label to display based on bookmark state
        /// </summary>
        public string Label => !HasBookmark ? AddBookmarkLabel : RemoveBookmarkLabel;

        /// <summary>
        /// Gets or sets the text to display when adding a bookmark
        /// </summary>
        public string AddBookmarkLabel { get; set; }

        /// <summary>
        /// Gets or sets the text to display when removing a bookmark
        /// </summary>
        public string RemoveBookmarkLabel { get; set; }

        /// <summary>
        /// Initializes a new instance of the ToggleBookmarkCommand class
        /// </summary>
        /// <param name="mtgObject">The MTG object to bookmark</param>
        /// <param name="parentUid">The parent UID if applicable</param>
        public ToggleBookmarkCommand(MtgObject mtgObject, string parentUid) : base(null)
        {
            _mtgObject = mtgObject;
            _parentUid = parentUid;
            UpdateHasBookmark();
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return _mtgObject != null && _mtgObject.MainContent != null && _mtgObject.Type != MtgObjectType.StoryNavigation;
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(object parameter)
        {
            if (_mtgObject == null || _mtgObject.MainContent == null)
            {
                return;
            }

            
            if (HasBookmark)
            {
                await ServiceFacade.MtgObjectService.RemoveBookmarkAsync(new MtgObjectFilter(_mtgObject.Uid, _mtgObject.MainContent.Language));
                ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkRemoved, null, null, false);
            }
            else
            {
                await ServiceFacade.MtgObjectService.CreateBookmarkAsync(_mtgObject, _parentUid ?? _mtgObject.ParentUid);
                ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkAdded, null, null, false);
            }
            
            await UpdateHasBookmarkAsync();
        }

        /// <summary>
        /// Updates the bookmark status asynchronously
        /// </summary>
        private async Task UpdateHasBookmarkAsync()
        {
            if (_mtgObject == null || _mtgObject.MainContent == null)
                return;
                
            HasBookmark = await ServiceFacade.MtgObjectService.IsBookmarkExistsForMtgObjectAsync(
                new MtgObjectFilter(_mtgObject.Uid, _mtgObject.MainContent.Language));
        }
            
        /// <summary>
        /// Updates the bookmark status synchronously (for compatibility with existing code)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        public void UpdateHasBookmark()
        {
            // Call the async method and block until it completes
            UpdateHasBookmarkAsync().GetAwaiter().GetResult();
        }
    }
}
