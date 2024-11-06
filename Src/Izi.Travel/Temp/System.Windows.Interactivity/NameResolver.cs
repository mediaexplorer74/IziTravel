﻿// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.NameResolver
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Helper class to handle the logic of resolving a TargetName into a Target element
  /// based on the context provided by a host element.
  /// </summary>
  internal sealed class NameResolver
  {
    private string name;
    private FrameworkElement nameScopeReferenceElement;

    /// <summary>Occurs when the resolved element has changed.</summary>
    public event EventHandler<NameResolvedEventArgs> ResolvedElementChanged;

    /// <summary>
    /// Gets or sets the name of the element to attempt to resolve.
    /// </summary>
    /// <value>The name to attempt to resolve.</value>
    public string Name
    {
      get => this.name;
      set
      {
        DependencyObject oldObject = this.Object;
        this.name = value;
        this.UpdateObjectFromName(oldObject);
      }
    }

    /// <summary>
    /// The resolved object. Will return the reference element if TargetName is null or empty, or if a resolve has not been attempted.
    /// </summary>
    public DependencyObject Object
    {
      get
      {
        return string.IsNullOrEmpty(this.Name) && this.HasAttempedResolve ? (DependencyObject) this.NameScopeReferenceElement : this.ResolvedObject;
      }
    }

    /// <summary>
    /// Gets or sets the reference element from which to perform the name resolution.
    /// </summary>
    /// <value>The reference element.</value>
    public FrameworkElement NameScopeReferenceElement
    {
      get => this.nameScopeReferenceElement;
      set
      {
        FrameworkElement referenceElement = this.NameScopeReferenceElement;
        this.nameScopeReferenceElement = value;
        this.OnNameScopeReferenceElementChanged(referenceElement);
      }
    }

    private FrameworkElement ActualNameScopeReferenceElement
    {
      get
      {
        return this.NameScopeReferenceElement == null || !Interaction.IsElementLoaded(this.NameScopeReferenceElement) ? (FrameworkElement) null : this.GetActualNameScopeReference(this.NameScopeReferenceElement);
      }
    }

    private DependencyObject ResolvedObject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the reference element load is pending.
    /// </summary>
    /// <value>
    /// 	<c>True</c> if [pending reference element load]; otherwise, <c>False</c>.
    /// </value>
    /// <remarks>
    /// If the Host has not been loaded, the name will not be resolved.
    /// In that case, delay the resolution and track that fact with this property.
    /// </remarks>
    private bool PendingReferenceElementLoad { get; set; }

    private bool HasAttempedResolve { get; set; }

    private void OnNameScopeReferenceElementChanged(FrameworkElement oldNameScopeReference)
    {
      if (this.PendingReferenceElementLoad)
      {
        oldNameScopeReference.Loaded -= new RoutedEventHandler(this.OnNameScopeReferenceLoaded);
        this.PendingReferenceElementLoad = false;
      }
      this.HasAttempedResolve = false;
      this.UpdateObjectFromName(this.Object);
    }

    /// <summary>
    /// Attempts to update the resolved object from the name within the context of the namescope reference element.
    /// </summary>
    /// <param name="oldObject">The old resolved object.</param>
    /// <remarks>
    /// Resets the existing target and attempts to resolve the current TargetName from the
    /// context of the current Host. If it cannot resolve from the context of the Host, it will
    /// continue up the visual tree until it resolves. If it has not resolved it when it reaches
    /// the root, it will set the Target to null and write a warning message to Debug output.
    /// </remarks>
    private void UpdateObjectFromName(DependencyObject oldObject)
    {
      DependencyObject dependencyObject = (DependencyObject) null;
      this.ResolvedObject = (DependencyObject) null;
      if (this.NameScopeReferenceElement != null)
      {
        if (!Interaction.IsElementLoaded(this.NameScopeReferenceElement))
        {
          this.NameScopeReferenceElement.Loaded += new RoutedEventHandler(this.OnNameScopeReferenceLoaded);
          this.PendingReferenceElementLoad = true;
          return;
        }
        if (!string.IsNullOrEmpty(this.Name))
        {
          FrameworkElement referenceElement = this.ActualNameScopeReferenceElement;
          if (referenceElement != null)
            dependencyObject = referenceElement.FindName(this.Name) as DependencyObject;
        }
      }
      this.HasAttempedResolve = true;
      this.ResolvedObject = dependencyObject;
      if (oldObject == this.Object)
        return;
      this.OnObjectChanged(oldObject, this.Object);
    }

    private void OnObjectChanged(DependencyObject oldTarget, DependencyObject newTarget)
    {
      if (this.ResolvedElementChanged == null)
        return;
      this.ResolvedElementChanged((object) this, new NameResolvedEventArgs((object) oldTarget, (object) newTarget));
    }

    private FrameworkElement GetActualNameScopeReference(FrameworkElement initialReferenceElement)
    {
      FrameworkElement nameScopeReference = initialReferenceElement;
      if (this.IsNameScope(initialReferenceElement))
      {
        if (!(initialReferenceElement.Parent is FrameworkElement frameworkElement))
          frameworkElement = nameScopeReference;
        nameScopeReference = frameworkElement;
      }
      return nameScopeReference;
    }

    private bool IsNameScope(FrameworkElement frameworkElement)
    {
      return frameworkElement.Parent is FrameworkElement parent && parent.FindName(this.Name) != null;
    }

    private void OnNameScopeReferenceLoaded(object sender, RoutedEventArgs e)
    {
      this.PendingReferenceElementLoad = false;
      this.NameScopeReferenceElement.Loaded -= new RoutedEventHandler(this.OnNameScopeReferenceLoaded);
      this.UpdateObjectFromName(this.Object);
    }
  }
}
