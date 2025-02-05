// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.FlipViewer
{
  [TemplatePart(Name = "ContentStrip", Type = typeof (Canvas))]
  [TemplatePart(Name = "ContentStripCompositeTransform", Type = typeof (CompositeTransform))]
  public class FlipViewer : Control
  {
    private const string PartContentStrip = "ContentStrip";
    private const string PartContentStripCompositeTransform = "ContentStripCompositeTransform";
    private const int VirtualizedItemPoolSize = 3;
    private const double ItemGutter = 18.0;
    private const double MaxDraggingSquishDistance = 150.0;
    private const double MinDraggingSquishScale = 0.9;
    private const double DragStagnationTimeThreshold = 300.0;
    private const double DragStagnationDistanceThreshold = 15.0;
    private const int UnsquishAnimationMilliseconds = 100;
    private const double FlickMinInputVelocity = 0.0;
    private const double FlickMaxInputVelocity = 5.0;
    private const double FlickMinOutputMilliseconds = 100.0;
    private const double FlickMaxOutputMilliseconds = 800.0;
    private Canvas _contentStrip;
    private CompositeTransform _compositeTransform;
    private Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState _state;
    private List<FlipViewerItem> _virtualizedItemPool;
    private Size? _size;
    private DragState _dragState = new DragState(150.0);
    private int? _displayedElementIndex;
    private Storyboard _dragInertiaAnimation;
    private Storyboard _unsquishAnimation;
    private DoubleAnimation _dragInertiaAnimationTranslation;
    private DoubleAnimation _unsquishAnimationTranslation;
    private FlipViewerItem _displayedVirtualizedItem;
    private FrameworkElement _headerTemplateInstance;
    private FrameworkElement _footerTemplateInstance;
    private FrameworkElement _headerOnMediaStrip;
    private FrameworkElement _footerOnMediaStrip;
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (DataTemplate), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnItemTemplatePropertyChanged)));
    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(nameof (Items), typeof (IList), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnItemsPropertyChanged)));
    public static readonly DependencyProperty DisplayedElementProperty = DependencyProperty.Register(nameof (DisplayedElement), typeof (FlipViewerDisplayedElementType), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) FlipViewerDisplayedElementType.None));
    public static readonly DependencyProperty DisplayedItemIndexProperty = DependencyProperty.Register(nameof (DisplayedItemIndex), typeof (int), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty InitiallyDisplayedElementProperty = DependencyProperty.Register(nameof (InitiallyDisplayedElement), typeof (FlipViewerInitiallyDisplayedElementType), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) FlipViewerInitiallyDisplayedElementType.First, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnInitiallyDisplayedElementPropertyPropertyChanged)));
    public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register(nameof (HeaderVisibility), typeof (Visibility), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) Visibility.Collapsed, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnHeaderVisibilityPropertyChanged)));
    public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register(nameof (FooterVisibility), typeof (Visibility), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) Visibility.Collapsed, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnFooterVisibilityPropertyChanged)));
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnHeaderPropertyChanged)));
    public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(nameof (Footer), typeof (object), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnFooterPropertyChanged)));
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(nameof (HeaderTemplate), typeof (DataTemplate), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata(new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnHeaderTemplatePropertyChanged)));
    public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register(nameof (FooterTemplate), typeof (DataTemplate), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata(new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.OnFooterTemplatePropertyChanged)));
    public static readonly DependencyProperty DragEnabledProperty = DependencyProperty.Register(nameof (DragEnabled), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer), new PropertyMetadata((object) true));

    private double ScrollOffset
    {
      get => this._compositeTransform.TranslateX;
      set => this._compositeTransform.TranslateX = value;
    }

    public DataTemplate ItemTemplate
    {
      get => (DataTemplate) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.ItemTemplateProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.ItemTemplateProperty, (object) value);
    }

    public IList Items
    {
      get => (IList) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.ItemsProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.ItemsProperty, (object) value);
    }

    public FlipViewerDisplayedElementType DisplayedElement
    {
      get => (FlipViewerDisplayedElementType) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DisplayedElementProperty);
      private set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DisplayedElementProperty, (object) value);
    }

    public int DisplayedItemIndex
    {
      get => (int) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DisplayedItemIndexProperty);
      private set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DisplayedItemIndexProperty, (object) value);
    }

    public FlipViewerInitiallyDisplayedElementType InitiallyDisplayedElement
    {
      get
      {
        return (FlipViewerInitiallyDisplayedElementType) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.InitiallyDisplayedElementProperty);
      }
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.InitiallyDisplayedElementProperty, (object) value);
    }

    public Visibility HeaderVisibility
    {
      get => (Visibility) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderVisibilityProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderVisibilityProperty, (object) value);
    }

    public Visibility FooterVisibility
    {
      get => (Visibility) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterVisibilityProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterVisibilityProperty, (object) value);
    }

    public object Header
    {
      get => this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderProperty, value);
    }

    public object Footer
    {
      get => this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterProperty, value);
    }

    public DataTemplate HeaderTemplate
    {
      get => (DataTemplate) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderTemplateProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.HeaderTemplateProperty, (object) value);
    }

    public DataTemplate FooterTemplate
    {
      get => (DataTemplate) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterTemplateProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FooterTemplateProperty, (object) value);
    }

    public bool DragEnabled
    {
      get => (bool) this.GetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DragEnabledProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.DragEnabledProperty, (object) value);
    }

    public event EventHandler HeaderDisplayed;

    public event EventHandler<FlipViewerItemDisplayedEventArgs> ItemDisplayed;

    public event EventHandler FooterDisplayed;

    public FlipViewer()
    {
      this.DefaultStyleKey = (object) typeof (Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer);
      this.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
    }

    public bool JumpToHeader()
    {
      if (this.HeaderVisibility == Visibility.Collapsed)
        return false;
      this.JumpToFirstElement();
      return true;
    }

    public bool JumpToFooter()
    {
      if (this.FooterVisibility == Visibility.Collapsed)
        return false;
      this.JumpToLastElement();
      return true;
    }

    public bool JumpToItem(int itemIndex)
    {
      if (this.Items == null || itemIndex >= this.Items.Count || itemIndex < 0)
        return false;
      this.JumpToElement(this.HeaderVisibility == Visibility.Visible ? itemIndex + 1 : itemIndex);
      return true;
    }

    public bool ScrollLeftOneElement()
    {
      if (this._displayedElementIndex.HasValue)
      {
        int? displayedElementIndex = this._displayedElementIndex;
        int num = 0;
        if ((displayedElementIndex.GetValueOrDefault() == num ? (displayedElementIndex.HasValue ? 1 : 0) : 0) == 0)
        {
          this.AnimateToElement(this._displayedElementIndex.Value - 1, new TimeSpan(0, 0, 0, 0, 800));
          return true;
        }
      }
      return false;
    }

    public object FindNameInHeader(string name)
    {
      return this._headerOnMediaStrip != null ? this._headerOnMediaStrip.FindName(name) : (object) null;
    }

    public object FindNameInFooter(string name)
    {
      return this._footerOnMediaStrip != null ? this._footerOnMediaStrip.FindName(name) : (object) null;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._contentStrip = (Canvas) this.GetTemplateChild("ContentStrip");
      this._compositeTransform = (CompositeTransform) this.GetTemplateChild("ContentStripCompositeTransform");
      this.InitializeVirtualizationIfReady();
    }

    private void InitializeVirtualizationIfReady()
    {
      if (this._contentStrip == null || !this._size.HasValue)
        return;
      this._virtualizedItemPool = new List<FlipViewerItem>();
      this._contentStrip.Children.Clear();
      for (int index = 0; index < 3; ++index)
      {
        Size size = this._size.Value;
        double width = size.Width;
        size = this._size.Value;
        double height = size.Height;
        FlipViewerItem flipViewerItem = new FlipViewerItem(new Size(width, height))
        {
          DataTemplate = this.ItemTemplate
        };
        this._contentStrip.Children.Add((UIElement) flipViewerItem.RootFrameworkElement);
        this._virtualizedItemPool.Add(flipViewerItem);
      }
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Uninitialized)
        this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized;
      this.ResetDisplayedElement();
      this.ResetItemLayout();
    }

    private void ResetDisplayedElement()
    {
      if (this.HeaderVisibility == Visibility.Collapsed && this.FooterVisibility == Visibility.Collapsed && (this.Items == null || this.Items.Count == 0))
      {
        this.ScrollOffset = 0.0;
        this.UpdateDisplayedElement(new int?());
      }
      else if (this.InitiallyDisplayedElement == FlipViewerInitiallyDisplayedElementType.First)
        this.JumpToFirstElement();
      else
        this.JumpToLastElement();
    }

    private void UpdateDisplayedElementPropertiesBasedOnIndex()
    {
      if (!this._displayedElementIndex.HasValue)
      {
        this.DisplayedElement = FlipViewerDisplayedElementType.None;
        this.DisplayedItemIndex = -1;
      }
      else
      {
        int? displayedElementIndex = this._displayedElementIndex;
        int num1 = 0;
        if ((displayedElementIndex.GetValueOrDefault() == num1 ? (displayedElementIndex.HasValue ? 1 : 0) : 0) != 0 && this.HeaderVisibility == Visibility.Visible)
        {
          this.DisplayedElement = FlipViewerDisplayedElementType.Header;
          this.DisplayedItemIndex = -1;
          EventHandler headerDisplayed = this.HeaderDisplayed;
          if (headerDisplayed == null)
            return;
          headerDisplayed((object) this, EventArgs.Empty);
        }
        else
        {
          displayedElementIndex = this._displayedElementIndex;
          int num2 = this.GetElementCount() - 1;
          if ((displayedElementIndex.GetValueOrDefault() == num2 ? (displayedElementIndex.HasValue ? 1 : 0) : 0) != 0 && this.FooterVisibility == Visibility.Visible)
          {
            this.DisplayedElement = FlipViewerDisplayedElementType.Footer;
            this.DisplayedItemIndex = -1;
            EventHandler footerDisplayed = this.FooterDisplayed;
            if (footerDisplayed == null)
              return;
            footerDisplayed((object) this, EventArgs.Empty);
          }
          else
          {
            int itemIndex = this._displayedElementIndex.Value;
            if (this.HeaderVisibility == Visibility.Visible)
              --itemIndex;
            this.DisplayedElement = FlipViewerDisplayedElementType.Item;
            this.DisplayedItemIndex = itemIndex;
            EventHandler<FlipViewerItemDisplayedEventArgs> itemDisplayed = this.ItemDisplayed;
            if (itemDisplayed == null)
              return;
            itemDisplayed((object) this, new FlipViewerItemDisplayedEventArgs(itemIndex));
          }
        }
      }
    }

    private void PlaceHeader()
    {
      if (!(this.Header is FrameworkElement frameworkElement1))
      {
        frameworkElement1 = this._headerTemplateInstance;
        if (frameworkElement1 != null && this.Header != null)
          frameworkElement1.DataContext = this.Header;
      }
      if (this._headerOnMediaStrip != null && this._headerOnMediaStrip != frameworkElement1)
      {
        this._contentStrip.Children.Remove((UIElement) this._headerOnMediaStrip);
        this._headerOnMediaStrip = (FrameworkElement) null;
      }
      if (this.HeaderVisibility == Visibility.Visible)
      {
        if (frameworkElement1 == null)
          return;
        frameworkElement1.SetValue(Canvas.TopProperty, (object) 0.0);
        frameworkElement1.SetValue(Canvas.LeftProperty, (object) 0.0);
        if (this._size.HasValue)
        {
          FrameworkElement frameworkElement2 = frameworkElement1;
          Size size = this._size.Value;
          double height = size.Height;
          frameworkElement2.Height = height;
          FrameworkElement frameworkElement3 = frameworkElement1;
          size = this._size.Value;
          double width = size.Width;
          frameworkElement3.Width = width;
        }
        if (this._headerOnMediaStrip == frameworkElement1)
          return;
        this._contentStrip.Children.Add((UIElement) frameworkElement1);
        this._headerOnMediaStrip = frameworkElement1;
      }
      else
      {
        if (this._headerOnMediaStrip == null)
          return;
        this._contentStrip.Children.Remove((UIElement) this._headerOnMediaStrip);
        this._headerOnMediaStrip = (FrameworkElement) null;
      }
    }

    private void PlaceFooter()
    {
      if (!(this.Footer is FrameworkElement frameworkElement))
      {
        frameworkElement = this._footerTemplateInstance;
        if (frameworkElement != null && this.Footer != null)
          frameworkElement.DataContext = this.Footer;
      }
      if (this._footerOnMediaStrip != null && this._footerOnMediaStrip != frameworkElement)
      {
        this._contentStrip.Children.Remove((UIElement) this._footerOnMediaStrip);
        this._footerOnMediaStrip = (FrameworkElement) null;
      }
      if (this.FooterVisibility == Visibility.Visible)
      {
        if (frameworkElement == null)
          return;
        Size size = this._size.HasValue ? this._size.Value : new Size(0.0, 0.0);
        frameworkElement.SetValue(Canvas.TopProperty, (object) 0.0);
        frameworkElement.SetValue(Canvas.LeftProperty, (object) (this._contentStrip.Width - size.Width));
        frameworkElement.Height = size.Height;
        frameworkElement.Width = size.Width;
        if (this._footerOnMediaStrip == frameworkElement)
          return;
        this._contentStrip.Children.Add((UIElement) frameworkElement);
        this._footerOnMediaStrip = frameworkElement;
      }
      else
      {
        if (this._footerOnMediaStrip == null)
          return;
        this._contentStrip.Children.Remove((UIElement) this._footerOnMediaStrip);
        this._footerOnMediaStrip = (FrameworkElement) null;
      }
    }

    private void ResetItemLayout()
    {
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Uninitialized)
        return;
      this.ResetContentStripGeometry();
      this.PlaceHeader();
      this.PlaceFooter();
      this.UpdateVirtualizedItemPositions();
    }

    private void UpdateVirtualizedItemSizes()
    {
      foreach (FlipViewerItem flipViewerItem in this._virtualizedItemPool)
        flipViewerItem.Size = this._size.HasValue ? this._size.Value : new Size(0.0, 0.0);
    }

    private void UpdateVirtualizedItemPositions()
    {
      int? nullable = new int?();
      switch (this.DisplayedElement)
      {
        case FlipViewerDisplayedElementType.Header:
          nullable = this.Items == null || this.Items.Count <= 0 ? new int?() : new int?(0);
          break;
        case FlipViewerDisplayedElementType.Item:
          nullable = new int?(this.DisplayedItemIndex);
          break;
        case FlipViewerDisplayedElementType.Footer:
          nullable = this.Items == null || this.Items.Count <= 0 ? new int?() : new int?(this.Items.Count - 1);
          break;
      }
      if (!nullable.HasValue)
        return;
      int num1 = Math.Max(0, nullable.Value - 1);
      int num2 = Math.Min(this.Items.Count - 1, nullable.Value + 1);
      for (int index = num1; index <= num2; ++index)
      {
        bool flag = false;
        double itemOffset = this.CalculateItemOffset(index);
        FlipViewerItem flipViewerItem1 = (FlipViewerItem) null;
        foreach (FlipViewerItem flipViewerItem2 in this._virtualizedItemPool)
        {
          if (this.DisplayedItemIndex != -1)
          {
            int? representingItemIndex = flipViewerItem2.RepresentingItemIndex;
            int displayedItemIndex = this.DisplayedItemIndex;
            if ((representingItemIndex.GetValueOrDefault() == displayedItemIndex ? (representingItemIndex.HasValue ? 1 : 0) : 0) != 0)
              this._displayedVirtualizedItem = flipViewerItem2;
          }
          int? representingItemIndex1 = flipViewerItem2.RepresentingItemIndex;
          int num3 = index;
          if ((representingItemIndex1.GetValueOrDefault() == num3 ? (representingItemIndex1.HasValue ? 1 : 0) : 0) != 0)
          {
            flag = true;
            if (Math.Abs((double) flipViewerItem2.RootFrameworkElement.GetValue(Canvas.LeftProperty) - itemOffset) > double.Epsilon)
            {
              flipViewerItem2.RootFrameworkElement.SetValue(Canvas.LeftProperty, (object) itemOffset);
              break;
            }
            break;
          }
          if (flipViewerItem1 == null || !flipViewerItem2.RepresentingItemIndex.HasValue)
            flipViewerItem1 = flipViewerItem2;
          else if (flipViewerItem1.RepresentingItemIndex.HasValue)
          {
            int num4 = Math.Abs(flipViewerItem1.RepresentingItemIndex.Value - nullable.Value);
            if (Math.Abs(flipViewerItem2.RepresentingItemIndex.Value - nullable.Value) > num4)
              flipViewerItem1 = flipViewerItem2;
          }
        }
        if (!flag && flipViewerItem1 != null)
        {
          flipViewerItem1.DataContext = this.Items[index];
          flipViewerItem1.RepresentingItemIndex = new int?(index);
          flipViewerItem1.RootFrameworkElement.SetValue(Canvas.LeftProperty, (object) itemOffset);
          flipViewerItem1.RootFrameworkElement.Visibility = Visibility.Visible;
          if (this.DisplayedItemIndex != -1)
          {
            int? representingItemIndex = flipViewerItem1.RepresentingItemIndex;
            int displayedItemIndex = this.DisplayedItemIndex;
            if ((representingItemIndex.GetValueOrDefault() == displayedItemIndex ? (representingItemIndex.HasValue ? 1 : 0) : 0) != 0)
              this._displayedVirtualizedItem = flipViewerItem1;
          }
        }
      }
    }

    private void ResetContentStripGeometry()
    {
      if (!this._size.HasValue)
        return;
      this._contentStrip.Height = this._size.Value.Height;
      int elementCount = this.GetElementCount();
      if (elementCount == 0)
        return;
      this._contentStrip.Width = this._size.Value.Width * (double) elementCount + 18.0 * (double) (elementCount - 1);
      this._dragState.MinDraggingBoundary = 150.0;
      this._dragState.MaxDraggingBoundary = -1.0 * (this._contentStrip.Width - this._size.Value.Width + 150.0);
    }

    protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
    {
      base.OnManipulationStarted(e);
      if (this._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.InertiaAnimating)
        return;
      this.CompleteDragInertiaAnimation();
    }

    protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
    {
      base.OnManipulationDelta(e);
      if (e.PinchManipulation != null)
        return;
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized && this.DragEnabled && this.GetElementCount() > 0)
      {
        this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging;
        this.DragStartedEventHandler();
      }
      if (this._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging && this._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.DraggingAndSquishing)
        return;
      Point translation = e.DeltaManipulation.Translation;
      double x = translation.X;
      translation = e.DeltaManipulation.Translation;
      double y = translation.Y;
      this.DragDeltaEventHandler(x, y);
    }

    protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
    {
      base.OnManipulationCompleted(e);
      if (this._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging && this._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.DraggingAndSquishing)
        return;
      if (!this._dragState.GotDragDelta)
        this.ProcessDragDelta(e.TotalManipulation.Translation.X, e.TotalManipulation.Translation.Y);
      this.DragCompletedEventHandler();
    }

    private void DragStartedEventHandler()
    {
      int elementCount = this.GetElementCount();
      this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging;
      this._dragState.LastDragUpdateTime = DateTime.Now;
      this._dragState.DragStartingMediaStripOffset = this.ScrollOffset;
      this._dragState.NetDragDistanceSincleLastDragStagnation = 0.0;
      DragState dragState1 = this._dragState;
      int? displayedElementIndex = this._displayedElementIndex;
      int num1 = 0;
      int num2 = displayedElementIndex.GetValueOrDefault() == num1 ? (displayedElementIndex.HasValue ? 1 : 0) : 0;
      dragState1.IsDraggingFirstElement = num2 != 0;
      DragState dragState2 = this._dragState;
      displayedElementIndex = this._displayedElementIndex;
      int num3 = elementCount - 1;
      int num4 = displayedElementIndex.GetValueOrDefault() == num3 ? (displayedElementIndex.HasValue ? 1 : 0) : 0;
      dragState2.IsDraggingLastElement = num4 != 0;
      this._dragState.GotDragDelta = false;
    }

    private void DragDeltaEventHandler(double horizontalChange, double verticalChange)
    {
      this._dragState.GotDragDelta = true;
      this.ProcessDragDelta(horizontalChange, verticalChange);
    }

    private void DragCompletedEventHandler()
    {
      switch (this._state)
      {
        case Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging:
          this.StartDragInertiaAnimation();
          break;
        case Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.DraggingAndSquishing:
          this.StartUndoSquishAnimation();
          break;
      }
    }

    private void ProcessDragDelta(double horizontalChange, double verticalChange)
    {
      DateTime now = DateTime.Now;
      double totalMilliseconds = (now - this._dragState.LastDragUpdateTime).TotalMilliseconds;
      this._dragState.LastDragUpdateTime = now;
      if (totalMilliseconds > 300.0)
        this._dragState.NetDragDistanceSincleLastDragStagnation = 0.0;
      this._dragState.LastDragDistanceDelta = horizontalChange;
      double val1 = this.ScrollOffset + horizontalChange;
      this._dragState.NetDragDistanceSincleLastDragStagnation += horizontalChange;
      double newTranslation = Math.Max(Math.Min(val1, this._dragState.MinDraggingBoundary), this._dragState.MaxDraggingBoundary);
      if (this._dragState.IsDraggingFirstElement || this._dragState.IsDraggingLastElement)
        this.HandleSquishingWhileDragging(newTranslation);
      this.ScrollOffset = newTranslation;
    }

    private void ConstructDragInertiaAnimation(
      double animationEndingValue,
      TimeSpan animationDuration)
    {
      this._dragInertiaAnimation = new Storyboard();
      this._dragInertiaAnimationTranslation = new DoubleAnimation();
      Storyboard.SetTarget((Timeline) this._dragInertiaAnimationTranslation, (DependencyObject) this._compositeTransform);
      Storyboard.SetTargetProperty((Timeline) this._dragInertiaAnimationTranslation, new PropertyPath((object) CompositeTransform.TranslateXProperty));
      QuadraticEase quadraticEase1 = new QuadraticEase();
      quadraticEase1.EasingMode = EasingMode.EaseOut;
      QuadraticEase quadraticEase2 = quadraticEase1;
      this._dragInertiaAnimationTranslation.From = new double?(this.ScrollOffset);
      this._dragInertiaAnimationTranslation.To = new double?(animationEndingValue);
      this._dragInertiaAnimationTranslation.Duration = (Duration) animationDuration;
      this._dragInertiaAnimationTranslation.EasingFunction = (IEasingFunction) quadraticEase2;
      this._dragInertiaAnimation.Children.Add((Timeline) this._dragInertiaAnimationTranslation);
      this._dragInertiaAnimation.Completed += new EventHandler(this.DragInertiaAnimationComplete);
      this._dragInertiaAnimation.FillBehavior = FillBehavior.HoldEnd;
    }

    private int CalculateDragInertiaAnimationEndingValue()
    {
      int num1 = Math.Abs(this._dragState.NetDragDistanceSincleLastDragStagnation) <= 15.0 ? 1 : 0;
      int animationEndingValue1 = 0;
      if (num1 != 0)
        return animationEndingValue1;
      int animationEndingValue2 = -1 * Math.Sign(this._dragState.LastDragDistanceDelta);
      int? displayedElementIndex = this._displayedElementIndex;
      int num2 = 0;
      if ((displayedElementIndex.GetValueOrDefault() == num2 ? (displayedElementIndex.HasValue ? 1 : 0) : 0) != 0 && animationEndingValue2 == -1)
      {
        animationEndingValue2 = 0;
      }
      else
      {
        displayedElementIndex = this._displayedElementIndex;
        int num3 = this.GetElementCount() - 1;
        if ((displayedElementIndex.GetValueOrDefault() == num3 ? (displayedElementIndex.HasValue ? 1 : 0) : 0) != 0 && animationEndingValue2 == 1)
          animationEndingValue2 = 0;
      }
      return animationEndingValue2;
    }

    private TimeSpan CalculateDragInertiaAnimationDuration(TimeSpan lastDragTimeDelta)
    {
      return new TimeSpan(0, 0, 0, 0, Math.Max(100, Math.Min(800, (int) (700.0 * (1.0 - (Math.Max(0.0, Math.Min(5.0, Math.Abs(this._dragState.LastDragDistanceDelta / lastDragTimeDelta.TotalMilliseconds))) - 0.0) / 5.0) + 100.0))));
    }

    private void StartDragInertiaAnimation()
    {
      if (!this._displayedElementIndex.HasValue)
        return;
      TimeSpan lastDragTimeDelta = DateTime.Now - this._dragState.LastDragUpdateTime;
      this.AnimateToElement(this._displayedElementIndex.Value + this.CalculateDragInertiaAnimationEndingValue(), this.CalculateDragInertiaAnimationDuration(lastDragTimeDelta));
      this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.InertiaAnimating;
    }

    private void AnimateToElement(int elementIndex, TimeSpan animationDuration)
    {
      this.ConstructDragInertiaAnimation(-1.0 * this.CalculateElementOffset(elementIndex), animationDuration);
      this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.InertiaAnimating;
      this._dragInertiaAnimation.Begin();
      this._dragState.NewDisplayedElementIndex = elementIndex;
    }

    private void DragInertiaAnimationComplete(object sender, EventArgs e)
    {
      this.CompleteDragInertiaAnimation();
    }

    private void CompleteDragInertiaAnimation()
    {
      if (this._dragInertiaAnimation == null)
        return;
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.InertiaAnimating)
        this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized;
      if (this._dragInertiaAnimationTranslation.To.HasValue)
        this.ScrollOffset = this._dragInertiaAnimationTranslation.To.Value;
      this._dragInertiaAnimation.Stop();
      this._dragInertiaAnimation = (Storyboard) null;
      this._dragInertiaAnimationTranslation = (DoubleAnimation) null;
      this.UpdateDisplayedElement(new int?(this._dragState.NewDisplayedElementIndex));
    }

    private void HandleSquishingWhileDragging(double newTranslation)
    {
      double num1 = -1.0 * this.CalculateElementOffset(this.GetElementCount() - 1);
      double num2 = 0.0;
      if (newTranslation > 0.0)
      {
        num2 = newTranslation;
        this._dragState.UnsquishTranslationAnimationTarget = 0.0;
        this._contentStrip.RenderTransformOrigin = new Point(0.0, 0.0);
      }
      else if (newTranslation < num1)
      {
        num2 = num1 - newTranslation;
        this._dragState.UnsquishTranslationAnimationTarget = num1;
        this._contentStrip.RenderTransformOrigin = new Point(1.0, 0.0);
      }
      double num3 = 1.0 - num2 / 150.0 * (1.0 / 10.0);
      this._compositeTransform.ScaleX = num3;
      this._state = Math.Abs(num3 - 1.0) < double.Epsilon ? Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Dragging : Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.DraggingAndSquishing;
    }

    private void StartUndoSquishAnimation()
    {
      this._unsquishAnimation = new Storyboard();
      DoubleAnimation element = new DoubleAnimation();
      this._unsquishAnimationTranslation = new DoubleAnimation();
      Storyboard.SetTarget((Timeline) element, (DependencyObject) this._compositeTransform);
      Storyboard.SetTarget((Timeline) this._unsquishAnimationTranslation, (DependencyObject) this._compositeTransform);
      Storyboard.SetTargetProperty((Timeline) element, new PropertyPath((object) CompositeTransform.ScaleXProperty));
      Storyboard.SetTargetProperty((Timeline) this._unsquishAnimationTranslation, new PropertyPath((object) CompositeTransform.TranslateXProperty));
      element.From = new double?(this._compositeTransform.ScaleX);
      this._unsquishAnimationTranslation.From = new double?(this._compositeTransform.TranslateX);
      element.To = new double?(1.0);
      this._unsquishAnimationTranslation.To = new double?(this._dragState.UnsquishTranslationAnimationTarget);
      element.Duration = (Duration) new TimeSpan(0, 0, 0, 0, 100);
      this._unsquishAnimationTranslation.Duration = element.Duration;
      this._unsquishAnimation.Children.Add((Timeline) element);
      this._unsquishAnimation.Children.Add((Timeline) this._unsquishAnimationTranslation);
      this._unsquishAnimation.FillBehavior = FillBehavior.Stop;
      this._unsquishAnimation.Completed += new EventHandler(this.UnsquishAnimationComplete);
      this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.UnsquishAnimating;
      this._unsquishAnimation.Begin();
      this._compositeTransform.ScaleX = element.To.Value;
      this._compositeTransform.TranslateX = this._unsquishAnimationTranslation.To.Value;
    }

    private void UnsquishAnimationComplete(object sender, EventArgs e)
    {
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.UnsquishAnimating)
        this._state = Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized;
      if (this._unsquishAnimationTranslation.To.HasValue)
        this.ScrollOffset = this._unsquishAnimationTranslation.To.Value;
      this._unsquishAnimation.Stop();
      this._unsquishAnimation = (Storyboard) null;
      this._unsquishAnimationTranslation = (DoubleAnimation) null;
    }

    private int GetElementCount()
    {
      int elementCount = 0;
      if (this.HeaderVisibility == Visibility.Visible)
        ++elementCount;
      if (this.Items != null)
        elementCount += this.Items.Count;
      if (this.FooterVisibility == Visibility.Visible)
        ++elementCount;
      return elementCount;
    }

    private void UpdateDisplayedElement(int? newElementIndex)
    {
      this._displayedElementIndex = newElementIndex;
      this.UpdateDisplayedElementPropertiesBasedOnIndex();
      this.UpdateVirtualizedItemPositions();
    }

    private double RoundOffsetDownToElementStart(double offset)
    {
      if (!this._size.HasValue)
        return 0.0;
      double num1 = offset;
      Size size = this._size.Value;
      double num2 = size.Width + 18.0;
      double num3 = (double) (int) (num1 / num2);
      size = this._size.Value;
      double num4 = size.Width + 18.0;
      return num3 * num4;
    }

    private double CalculateElementOffset(int elementIndex)
    {
      return !this._size.HasValue ? 0.0 : (double) elementIndex * (this._size.Value.Width + 18.0);
    }

    private double CalculateItemOffset(int itemIndex)
    {
      double itemOffset = 0.0;
      if (this._size.HasValue)
      {
        Size size;
        if (this.HeaderVisibility == Visibility.Visible)
        {
          double num1 = itemOffset;
          size = this._size.Value;
          double num2 = size.Width + 18.0;
          itemOffset = num1 + num2;
        }
        double num3 = itemOffset;
        double num4 = (double) itemIndex;
        size = this._size.Value;
        double num5 = size.Width + 18.0;
        double num6 = num4 * num5;
        itemOffset = num3 + num6;
      }
      return itemOffset;
    }

    private void JumpToFirstElement() => this.JumpToElement(0);

    private void JumpToLastElement()
    {
      int elementCount = this.GetElementCount();
      if (elementCount <= 0)
        return;
      this.JumpToElement(elementCount - 1);
    }

    private void JumpToElement(int elementIndex)
    {
      this.ScrollOffset = -1.0 * this.CalculateElementOffset(elementIndex);
      this.UpdateDisplayedElement(new int?(elementIndex));
    }

    private void InitializeOrReset()
    {
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Uninitialized)
      {
        this.InitializeVirtualizationIfReady();
      }
      else
      {
        foreach (FlipViewerItem flipViewerItem in this._virtualizedItemPool)
        {
          flipViewerItem.DataContext = (object) null;
          flipViewerItem.RepresentingItemIndex = new int?();
        }
        this.ResetDisplayedElement();
        this.ResetItemLayout();
      }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      this._size = new Size?(e.NewSize);
      if (this._state == Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Uninitialized)
      {
        this.InitializeVirtualizationIfReady();
      }
      else
      {
        this.UpdateVirtualizedItemSizes();
        this.ResetItemLayout();
        this.ScrollOffset = this._displayedElementIndex.HasValue ? -1.0 * this.CalculateElementOffset(this._displayedElementIndex.Value) : 0.0;
      }
    }

    private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          int count1 = e.NewItems.Count;
          this.ResetContentStripGeometry();
          this.PlaceFooter();
          foreach (FlipViewerItem flipViewerItem1 in this._virtualizedItemPool)
          {
            int? representingItemIndex = flipViewerItem1.RepresentingItemIndex;
            if (representingItemIndex.HasValue)
            {
              representingItemIndex = flipViewerItem1.RepresentingItemIndex;
              int newStartingIndex = e.NewStartingIndex;
              if ((representingItemIndex.GetValueOrDefault() >= newStartingIndex ? (representingItemIndex.HasValue ? 1 : 0) : 0) != 0)
              {
                FlipViewerItem flipViewerItem2 = flipViewerItem1;
                representingItemIndex = flipViewerItem2.RepresentingItemIndex;
                int num = count1;
                flipViewerItem2.RepresentingItemIndex = representingItemIndex.HasValue ? new int?(representingItemIndex.GetValueOrDefault() + num) : new int?();
              }
            }
          }
          if (!this._displayedElementIndex.HasValue)
          {
            this.JumpToElement(0);
            break;
          }
          if (this._displayedElementIndex.Value < e.NewStartingIndex)
            break;
          this.JumpToElement(this._displayedElementIndex.Value + count1);
          break;
        case NotifyCollectionChangedAction.Remove:
          int count2 = e.OldItems.Count;
          this.ResetContentStripGeometry();
          this.PlaceFooter();
          foreach (FlipViewerItem flipViewerItem3 in this._virtualizedItemPool)
          {
            int? nullable1 = flipViewerItem3.RepresentingItemIndex;
            if (nullable1.HasValue)
            {
              nullable1 = flipViewerItem3.RepresentingItemIndex;
              int oldStartingIndex = e.OldStartingIndex;
              if ((nullable1.GetValueOrDefault() >= oldStartingIndex ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
              {
                nullable1 = flipViewerItem3.RepresentingItemIndex;
                int num = e.OldStartingIndex + count2;
                if ((nullable1.GetValueOrDefault() < num ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                {
                  FlipViewerItem flipViewerItem4 = flipViewerItem3;
                  nullable1 = new int?();
                  int? nullable2 = nullable1;
                  flipViewerItem4.RepresentingItemIndex = nullable2;
                  flipViewerItem3.DataContext = (object) null;
                }
              }
            }
            nullable1 = flipViewerItem3.RepresentingItemIndex;
            if (nullable1.HasValue)
            {
              nullable1 = flipViewerItem3.RepresentingItemIndex;
              int oldStartingIndex = e.OldStartingIndex;
              if ((nullable1.GetValueOrDefault() > oldStartingIndex ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
              {
                FlipViewerItem flipViewerItem5 = flipViewerItem3;
                nullable1 = flipViewerItem5.RepresentingItemIndex;
                int num = count2;
                flipViewerItem5.RepresentingItemIndex = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - num) : new int?();
              }
            }
          }
          if (!this._displayedElementIndex.HasValue)
            break;
          int elementCount = this.GetElementCount();
          if (elementCount == 0)
          {
            this.UpdateDisplayedElement(new int?());
            break;
          }
          int elementIndex = this._displayedElementIndex.Value;
          int num1 = this.HeaderVisibility == Visibility.Visible ? e.OldStartingIndex + 1 : e.OldStartingIndex;
          if (this._displayedElementIndex.Value > e.OldStartingIndex)
          {
            int num2 = -1 * Math.Min(count2, this._displayedElementIndex.Value - num1);
            elementIndex += num2;
          }
          if (elementIndex >= elementCount)
            elementIndex = elementCount - 1;
          this.JumpToElement(elementIndex);
          break;
        case NotifyCollectionChangedAction.Replace:
        case NotifyCollectionChangedAction.Move:
        case NotifyCollectionChangedAction.Reset:
          this.InitializeOrReset();
          break;
      }
    }

    private static void OnItemTemplatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer))
        return;
      flipViewer.InitializeVirtualizationIfReady();
    }

    private static void OnItemsPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer))
        return;
      INotifyCollectionChanged oldValue = e.OldValue as INotifyCollectionChanged;
      INotifyCollectionChanged newValue = e.NewValue as INotifyCollectionChanged;
      if (oldValue != null)
        oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(flipViewer.OnItemsCollectionChanged);
      if (newValue != null)
        newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(flipViewer.OnItemsCollectionChanged);
      flipViewer.InitializeOrReset();
    }

    private static void OnInitiallyDisplayedElementPropertyPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer && (FlipViewerDisplayedElementType) e.NewValue == FlipViewerDisplayedElementType.None)
        throw new ArgumentException("InitiallyDisplayedElement cannot be set to DisplayedElementType.None");
    }

    private static void OnHeaderVisibilityPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer) d).InitializeOrReset();
    }

    private static void OnFooterVisibilityPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer) d).InitializeOrReset();
    }

    private static void OnHeaderPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer) || flipViewer._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized)
        return;
      flipViewer.PlaceHeader();
    }

    private static void OnFooterPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer) || flipViewer._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized)
        return;
      flipViewer.PlaceFooter();
    }

    private static void OnHeaderTemplatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer))
        return;
      DataTemplate newValue = (DataTemplate) e.NewValue;
      flipViewer._headerTemplateInstance = newValue != null ? newValue.LoadContent() as FrameworkElement : (FrameworkElement) null;
      if (flipViewer._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized)
        return;
      flipViewer.PlaceHeader();
    }

    private static void OnFooterTemplatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer flipViewer))
        return;
      DataTemplate newValue = (DataTemplate) e.NewValue;
      flipViewer._footerTemplateInstance = newValue != null ? newValue.LoadContent() as FrameworkElement : (FrameworkElement) null;
      if (flipViewer._state != Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewer.FlipViewerState.Initialized)
        return;
      flipViewer.PlaceFooter();
    }

    private enum FlipViewerState
    {
      Uninitialized,
      Initialized,
      InertiaAnimating,
      Dragging,
      DraggingAndSquishing,
      UnsquishAnimating,
    }
  }
}
