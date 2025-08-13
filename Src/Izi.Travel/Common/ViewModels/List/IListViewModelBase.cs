using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Izi.Travel.Core.Command;

namespace Izi.Travel.Common.ViewModels.List
{
    /// <summary>
    /// Base interface for list view models
    /// </summary>
    public interface IListViewModelBase : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Gets the items to display in the list
        /// </summary>
        IList<object> Items { get; }
        
        /// <summary>
        /// Gets the command to refresh the list
        /// </summary>
        ICommand RefreshCommand { get; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the list is currently loading data
        /// </summary>
        bool IsLoading { get; set; }
        
        /// <summary>
        /// Gets or sets the selected item in the list
        /// </summary>
        object SelectedItem { get; set; }
        
        /// <summary>
        /// Gets the command to execute when an item is selected
        /// </summary>
        ICommand ItemSelectedCommand { get; }
        
        /// <summary>
        /// Gets the command to navigate to an item
        /// </summary>
        ICommand NavigateCommand { get; }
        
        /// <summary>
        /// Gets or sets the active item in the view model
        /// </summary>
        object ActiveItem { get; set; }
        
        /// <summary>
        /// Refreshes the list data
        /// </summary>
        void Refresh();
    }
}
