// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.BindingScope
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Provides methods for searching a given scope for named elements.
  /// </summary>
  public static class BindingScope
  {
    private static readonly List<Func<Type, bool>> ChildResolverFilters = new List<Func<Type, bool>>();
    private static readonly List<Func<DependencyObject, IEnumerable<DependencyObject>>> ChildResolvers = new List<Func<DependencyObject, IEnumerable<DependencyObject>>>();
    private static readonly Dictionary<Type, object> NonResolvableChildTypes = new Dictionary<Type, object>();
    /// <summary>
    /// Gets all the <see cref="T:System.Windows.FrameworkElement" /> instances with names in the scope.
    /// </summary>
    /// <returns>Named <see cref="T:System.Windows.FrameworkElement" /> instances in the provided scope.</returns>
    /// <remarks>Pass in a <see cref="T:System.Windows.DependencyObject" /> and receive a list of named <see cref="T:System.Windows.FrameworkElement" /> instances in the same scope.</remarks>
    public static Func<DependencyObject, IEnumerable<FrameworkElement>> GetNamedElements = (Func<DependencyObject, IEnumerable<FrameworkElement>>) (elementInScope =>
    {
      BindingScope.ScopeNamingRoute scopeNamingRoute = BindingScope.FindScopeNamingRoute(elementInScope);
      return BindingScope.FindNamedDescendants(scopeNamingRoute);
    });
    /// <summary>
    /// Finds a set of named <see cref="T:System.Windows.FrameworkElement" /> instances in each hop in a <see cref="T:Caliburn.Micro.BindingScope.ScopeNamingRoute" />.
    /// </summary>
    /// <remarks>
    /// Searches all the elements in the <see cref="T:Caliburn.Micro.BindingScope.ScopeNamingRoute" /> parameter as well as the visual children of
    /// each of these elements, the <see cref="P:System.Windows.Controls.ContentControl.Content" />, the <c>HeaderedContentControl.Header</c>,
    /// the <see cref="P:System.Windows.Controls.ItemsControl.Items" />, or the <c>HeaderedItemsControl.Header</c>, if any are found.
    /// </remarks>
    public static Func<BindingScope.ScopeNamingRoute, IEnumerable<FrameworkElement>> FindNamedDescendants = (Func<BindingScope.ScopeNamingRoute, IEnumerable<FrameworkElement>>) (routeHops =>
    {
      if (routeHops == null)
        throw new ArgumentNullException(nameof (routeHops));
      if (routeHops.Root == null)
        throw new ArgumentException(string.Format("Root is null on the given {0}", (object) typeof (BindingScope.ScopeNamingRoute)));
      List<FrameworkElement> frameworkElementList = new List<FrameworkElement>();
      Queue<DependencyObject> dependencyObjectQueue = new Queue<DependencyObject>();
      dependencyObjectQueue.Enqueue(routeHops.Root);
      while (dependencyObjectQueue.Count > 0)
      {
        DependencyObject current = dependencyObjectQueue.Dequeue();
        if (current is FrameworkElement frameworkElement2 && !string.IsNullOrEmpty(frameworkElement2.Name))
          frameworkElementList.Add(frameworkElement2);
        if (!(current is UserControl) || object.ReferenceEquals((object) current, (object) routeHops.Root))
        {
          DependencyObject hopTarget;
          if (routeHops.TryGetHop(current, out hopTarget))
          {
            dependencyObjectQueue.Enqueue(hopTarget);
          }
          else
          {
            int childrenCount = current is UIElement ? VisualTreeHelper.GetChildrenCount(current) : 0;
            if (childrenCount > 0)
            {
              for (int childIndex = 0; childIndex < childrenCount; ++childIndex)
              {
                DependencyObject child = VisualTreeHelper.GetChild(current, childIndex);
                dependencyObjectQueue.Enqueue(child);
              }
            }
            else
            {
              if (current is ContentControl contentControl2 && contentControl2.Content is DependencyObject)
                dependencyObjectQueue.Enqueue(contentControl2.Content as DependencyObject);
              if (current is ItemsControl itemsControl2)
              {
                itemsControl2.Items.OfType<DependencyObject>().Apply<DependencyObject>(new Action<DependencyObject>(dependencyObjectQueue.Enqueue));
              }
              else
              {
                Type currentType = current.GetType();
                if (!BindingScope.NonResolvableChildTypes.ContainsKey(currentType))
                {
                  if (!BindingScope.ChildResolverFilters.Any<Func<Type, bool>>((Func<Func<Type, bool>, bool>) (f => f(currentType))))
                    BindingScope.NonResolvableChildTypes[currentType] = (object) null;
                  else
                    BindingScope.ChildResolvers.SelectMany<Func<DependencyObject, IEnumerable<DependencyObject>>, DependencyObject>((Func<Func<DependencyObject, IEnumerable<DependencyObject>>, IEnumerable<DependencyObject>>) (r => r(current) ?? Enumerable.Empty<DependencyObject>())).Where<DependencyObject>((Func<DependencyObject, bool>) (c => c != null)).Apply<DependencyObject>(new Action<DependencyObject>(dependencyObjectQueue.Enqueue));
                }
              }
            }
          }
        }
      }
      return (IEnumerable<FrameworkElement>) frameworkElementList;
    });
    /// <summary>
    /// Finds a path of dependency objects which traces through visual anscestry until a root which is <see langword="null" />,
    /// a <see cref="T:System.Windows.Controls.UserControl" />, a <c>Page</c> with a dependency object <c>Page.ContentProperty</c> value,
    /// a dependency object with <see cref="F:Caliburn.Micro.View.IsScopeRootProperty" /> set to <see langword="true" />. <see cref="T:System.Windows.Controls.ContentPresenter" />
    /// and <see cref="T:System.Windows.Controls.ItemsPresenter" /> are included in the resulting <see cref="T:Caliburn.Micro.BindingScope.ScopeNamingRoute" /> in order to track which item
    /// in an items control we are scoped to.
    /// </summary>
    public static Func<DependencyObject, BindingScope.ScopeNamingRoute> FindScopeNamingRoute = (Func<DependencyObject, BindingScope.ScopeNamingRoute>) (elementInScope =>
    {
      DependencyObject from = elementInScope;
      DependencyObject reference = elementInScope;
      DependencyObject to = (DependencyObject) null;
      BindingScope.ScopeNamingRoute scopeNamingRoute = new BindingScope.ScopeNamingRoute();
      for (; from != null; from = VisualTreeHelper.GetParent(reference))
      {
        if (!(from is UserControl) && !(bool) from.GetValue(View.IsScopeRootProperty))
        {
          switch (from)
          {
            case ContentPresenter _:
              to = from;
              break;
            case ItemsPresenter _:
              if (to != null)
              {
                scopeNamingRoute.AddHop(from, to);
                to = (DependencyObject) null;
                break;
              }
              break;
          }
          reference = from;
        }
        else
          goto label_9;
      }
      from = reference;
label_9:
      scopeNamingRoute.Root = from;
      return scopeNamingRoute;
    });

    /// <summary>
    /// Searches through the list of named elements looking for a case-insensitive match.
    /// </summary>
    /// <param name="elementsToSearch">The named elements to search through.</param>
    /// <param name="name">The name to search for.</param>
    /// <returns>The named element or null if not found.</returns>
    public static FrameworkElement FindName(
      this IEnumerable<FrameworkElement> elementsToSearch,
      string name)
    {
      return elementsToSearch.FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>) (x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)));
    }

    /// <summary>Adds a child resolver.</summary>
    /// <param name="filter">The type filter.</param>
    /// <param name="resolver">The resolver.</param>
    public static void AddChildResolver(
      Func<Type, bool> filter,
      Func<DependencyObject, IEnumerable<DependencyObject>> resolver)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      if (resolver == null)
        throw new ArgumentNullException(nameof (resolver));
      BindingScope.NonResolvableChildTypes.Clear();
      BindingScope.ChildResolverFilters.Add(filter);
      BindingScope.ChildResolvers.Add(resolver);
    }

    /// <summary>Removes a child resolver.</summary>
    /// <param name="resolver">The resolver to remove.</param>
    /// <returns>true, when the resolver was (found and) removed.</returns>
    public static bool RemoveChildResolver(
      Func<DependencyObject, IEnumerable<DependencyObject>> resolver)
    {
      int index = resolver != null ? BindingScope.ChildResolvers.IndexOf(resolver) : throw new ArgumentNullException(nameof (resolver));
      if (index < 0)
        return false;
      BindingScope.ChildResolverFilters.RemoveAt(index);
      BindingScope.ChildResolvers.RemoveAt(index);
      return true;
    }

    /// <summary>
    /// Maintains a connection in the visual tree of dependency objects in order to record a route through it.
    /// </summary>
    public class ScopeNamingRoute
    {
      private readonly Dictionary<DependencyObject, DependencyObject> path = new Dictionary<DependencyObject, DependencyObject>();
      private DependencyObject root;

      /// <summary>Gets or sets the starting point of the route.</summary>
      public DependencyObject Root
      {
        get => this.root;
        set
        {
          if (this.path.Count > 0 && !this.path.ContainsKey(value))
            throw new ArgumentException("Value is not a hop source in the route.");
          this.root = !this.path.ContainsValue(value) ? value : throw new ArgumentException("Value is a target of some route hop; cannot be a root.");
        }
      }

      /// <summary>Adds a segment to the route.</summary>
      /// <param name="from">The source dependency object.</param>
      /// <param name="to">The target dependency object.</param>
      public void AddHop(DependencyObject from, DependencyObject to)
      {
        if (from == null)
          throw new ArgumentNullException(nameof (from));
        if (to == null)
          throw new ArgumentNullException(nameof (to));
        if (this.path.Count > 0 && !this.path.ContainsKey(from) && !this.path.ContainsKey(to) && !this.path.ContainsValue(from) && !this.path.ContainsValue(from))
          throw new ArgumentException("Hop pair not part of existing route.");
        this.path[from] = !this.path.ContainsKey(to) ? to : throw new ArgumentException("Cycle detected when adding hop.");
      }

      /// <summary>Tries to get a target dependency object given a source.</summary>
      /// <param name="hopSource">The possible beginning of a route segment (hop).</param>
      /// <param name="hopTarget">The target of a route segment (hop).</param>
      /// <returns><see langword="true" /> if <paramref name="hopSource" /> had a target recorded; <see langword="false" /> otherwise.</returns>
      public bool TryGetHop(DependencyObject hopSource, out DependencyObject hopTarget)
      {
        return this.path.TryGetValue(hopSource, out hopTarget);
      }
    }
  }
}
