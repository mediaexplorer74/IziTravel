using System;
using Windows.Foundation;

namespace Izi.Travel.Shell.AppServices.Navigation
{
    /// <summary>
    /// Represents an entry in the navigation history of a Frame.
    /// This is a UWP-compatible replacement for System.Windows.Navigation.JournalEntry.
    /// </summary>
    public class JournalEntry
    {
        /// <summary>
        /// Gets the uniform resource identifier (URI) that is the navigation target for the current entry.
        /// </summary>
        public Uri Source { get; }

        /// <summary>
        /// Gets the parameter object for the current entry.
        /// </summary>
        public object Parameter { get; }

        /// <summary>
        /// Gets the type of the content that is being navigated to.
        /// </summary>
        public Type SourcePageType { get; }

        /// <summary>
        /// Initializes a new instance of the JournalEntry class with the specified source URI, parameter, and source page type.
        /// </summary>
        /// <param name="source">The URI of the navigation target.</param>
        /// <param name="parameter">The parameter object for the navigation.</param>
        /// <param name="sourcePageType">The type of the page being navigated to.</param>
        public JournalEntry(Uri source, object parameter, Type sourcePageType)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Parameter = parameter;
            SourcePageType = sourcePageType ?? throw new ArgumentNullException(nameof(sourcePageType));
        }

        /// <summary>
        /// Creates a new JournalEntry from a PageStackEntry (UWP's equivalent).
        /// </summary>
        /// <param name="entry">The PageStackEntry to create a JournalEntry from.</param>
        /// <returns>A new JournalEntry instance.</returns>
        public static JournalEntry FromPageStackEntry(Windows.UI.Xaml.Navigation.PageStackEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            return new JournalEntry(
                new Uri(entry.SourcePageType.FullName, UriKind.Relative),
                entry.Parameter,
                entry.SourcePageType);
        }
    }
}
