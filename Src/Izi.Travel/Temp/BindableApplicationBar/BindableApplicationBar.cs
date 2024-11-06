// Decompiled with JetBrains decompiler
// Type: BindableApplicationBar.BindableApplicationBar
// Assembly: BindableApplicationBar, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A535FD52-CB2F-4C72-99D3-803485FA9E0A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BindableApplicationBar.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace BindableApplicationBar
{
  [ContentProperty("Buttons")]
  public class BindableApplicationBar : Control
  {
    private ApplicationBar applicationBar;
    private PhoneApplicationPage page;
    private bool backgroundColorChanged;
    private bool foregroundColorChanged;
    private bool isMenuEnabledChanged;
    private bool isVisibleChanged;
    private bool modeChanged;
    private bool bindableOpacityChanged;
    private readonly DependencyObjectCollection<BindableApplicationBarButton> buttonsSourceButtons = new DependencyObjectCollection<BindableApplicationBarButton>();
    private readonly DependencyObjectCollection<BindableApplicationBarMenuItem> menuItemsSourceMenuItems = new DependencyObjectCollection<BindableApplicationBarMenuItem>();
    public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(nameof (Buttons), typeof (DependencyObjectCollection<BindableApplicationBarButton>), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnButtonsChanged)));
    public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(nameof (MenuItems), typeof (DependencyObjectCollection<BindableApplicationBarMenuItem>), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnMenuItemsChanged)));
    public static readonly DependencyProperty ButtonsSourceProperty = DependencyProperty.Register(nameof (ButtonsSource), typeof (IEnumerable), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnButtonsSourceChanged)));
    public static readonly DependencyProperty ButtonTemplateProperty = DependencyProperty.Register(nameof (ButtonTemplate), typeof (DataTemplate), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnButtonTemplateChanged)));
    public static readonly DependencyProperty MenuItemsSourceProperty = DependencyProperty.Register(nameof (MenuItemsSource), typeof (IEnumerable), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnMenuItemsSourceChanged)));
    public static readonly DependencyProperty MenuItemTemplateProperty = DependencyProperty.Register(nameof (MenuItemTemplate), typeof (DataTemplate), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) null, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnMenuItemTemplateChanged)));
    public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof (IsVisible), typeof (bool), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) true, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnIsVisibleChanged)));
    public static readonly DependencyProperty IsMenuEnabledProperty = DependencyProperty.Register(nameof (IsMenuEnabled), typeof (bool), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) true, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnIsMenuEnabledChanged)));
    public static readonly DependencyProperty IsMenuVisibleProperty = DependencyProperty.Register(nameof (IsMenuVisible), typeof (bool), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) false, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnIsMenuVisibleChanged)));
    private bool isMenuVisible;
    public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(nameof (BackgroundColor), typeof (Color), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) Colors.Magenta, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnBackgroundColorChanged)));
    public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register(nameof (ForegroundColor), typeof (Color), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) Colors.Magenta, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnForegroundColorChanged)));
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (ApplicationBarMode), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) ApplicationBarMode.Default, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnModeChanged)));
    public static readonly DependencyProperty BindableOpacityProperty = DependencyProperty.Register(nameof (BindableOpacity), typeof (double), typeof (BindableApplicationBar.BindableApplicationBar), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(BindableApplicationBar.BindableApplicationBar.OnBindableOpacityChanged)));

    public DependencyObjectCollection<BindableApplicationBarButton> Buttons
    {
      get
      {
        return (DependencyObjectCollection<BindableApplicationBarButton>) this.GetValue(BindableApplicationBar.BindableApplicationBar.ButtonsProperty);
      }
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.ButtonsProperty, (object) value);
    }

    private static void OnButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      DependencyObjectCollection<BindableApplicationBarButton> oldValue = (DependencyObjectCollection<BindableApplicationBarButton>) e.OldValue;
      DependencyObjectCollection<BindableApplicationBarButton> buttons = bindableApplicationBar.Buttons;
      bindableApplicationBar.OnButtonsChanged(oldValue, buttons);
    }

    protected virtual void OnButtonsChanged(
      DependencyObjectCollection<BindableApplicationBarButton> oldButtons,
      DependencyObjectCollection<BindableApplicationBarButton> newButtons)
    {
      if (oldButtons != null)
        oldButtons.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
      if (newButtons == null)
        return;
      newButtons.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
    }

    public DependencyObjectCollection<BindableApplicationBarMenuItem> MenuItems
    {
      get
      {
        return (DependencyObjectCollection<BindableApplicationBarMenuItem>) this.GetValue(BindableApplicationBar.BindableApplicationBar.MenuItemsProperty);
      }
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.MenuItemsProperty, (object) value);
    }

    private static void OnMenuItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      DependencyObjectCollection<BindableApplicationBarMenuItem> oldValue = (DependencyObjectCollection<BindableApplicationBarMenuItem>) e.OldValue;
      DependencyObjectCollection<BindableApplicationBarMenuItem> menuItems = bindableApplicationBar.MenuItems;
      bindableApplicationBar.OnMenuItemsChanged(oldValue, menuItems);
    }

    protected virtual void OnMenuItemsChanged(
      DependencyObjectCollection<BindableApplicationBarMenuItem> oldMenuItems,
      DependencyObjectCollection<BindableApplicationBarMenuItem> newMenuItems)
    {
      if (oldMenuItems != null)
        oldMenuItems.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.MenuItemsCollectionChanged);
      if (newMenuItems == null)
        return;
      newMenuItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.MenuItemsCollectionChanged);
    }

    public IEnumerable ButtonsSource
    {
      get => (IEnumerable) this.GetValue(BindableApplicationBar.BindableApplicationBar.ButtonsSourceProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.ButtonsSourceProperty, (object) value);
    }

    private static void OnButtonsSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      IEnumerable oldValue = (IEnumerable) e.OldValue;
      IEnumerable buttonsSource = bindableApplicationBar.ButtonsSource;
      bindableApplicationBar.OnButtonsSourceChanged(oldValue, buttonsSource);
    }

    protected virtual void OnButtonsSourceChanged(
      IEnumerable oldButtonsSource,
      IEnumerable newButtonsSource)
    {
      if (oldButtonsSource != null && oldButtonsSource is INotifyCollectionChanged)
        ((INotifyCollectionChanged) oldButtonsSource).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ButtonsSourceCollectionChanged);
      this.GenerateButtonsFromSource();
      if (newButtonsSource == null || !(newButtonsSource is INotifyCollectionChanged))
        return;
      ((INotifyCollectionChanged) newButtonsSource).CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsSourceCollectionChanged);
    }

    private void ButtonsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.OldItems != null)
      {
        foreach (object oldItem in (IEnumerable) e.OldItems)
        {
          object dataContext = oldItem;
          BindableApplicationBarButton applicationBarButton = this.buttonsSourceButtons.FirstOrDefault<BindableApplicationBarButton>((Func<BindableApplicationBarButton, bool>) (b => b.DataContext == dataContext));
          if (applicationBarButton != null)
            this.buttonsSourceButtons.Remove(applicationBarButton);
        }
      }
      if (this.ButtonsSource == null || this.ButtonTemplate == null || e.NewItems == null)
        return;
      foreach (object newItem in (IEnumerable) e.NewItems)
      {
        BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) this.ButtonTemplate.LoadContent();
        if (applicationBarButton == null)
          throw new InvalidOperationException("BindableApplicationBar cannot use the ButtonsSource property without a valid ButtonTemplate");
        applicationBarButton.DataContext = newItem;
        this.buttonsSourceButtons.Add(applicationBarButton);
      }
    }

    private void GenerateButtonsFromSource()
    {
      this.buttonsSourceButtons.Clear();
      if (this.applicationBar != null)
        this.applicationBar.Buttons.Clear();
      if (this.ButtonsSource == null || this.ButtonTemplate == null)
        return;
      foreach (object obj in this.ButtonsSource)
      {
        BindableApplicationBarButton applicationBarButton = (BindableApplicationBarButton) this.ButtonTemplate.LoadContent();
        if (applicationBarButton == null)
          throw new InvalidOperationException("BindableApplicationBar cannot use the ButtonsSource property without a valid ButtonTemplate");
        applicationBarButton.DataContext = obj;
        this.buttonsSourceButtons.Add(applicationBarButton);
      }
    }

    private void GenerateMenuItemsFromSource()
    {
      this.menuItemsSourceMenuItems.Clear();
      if (this.applicationBar != null)
        this.applicationBar.MenuItems.Clear();
      if (this.MenuItemsSource == null || this.MenuItemTemplate == null)
        return;
      foreach (object obj in this.MenuItemsSource)
      {
        BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) this.MenuItemTemplate.LoadContent();
        if (applicationBarMenuItem == null)
          throw new InvalidOperationException("BindableApplicationBar cannot use the MenuItemsSource property without a valid MenuItemTemplate");
        applicationBarMenuItem.DataContext = obj;
        this.menuItemsSourceMenuItems.Add(applicationBarMenuItem);
      }
    }

    public DataTemplate ButtonTemplate
    {
      get => (DataTemplate) this.GetValue(BindableApplicationBar.BindableApplicationBar.ButtonTemplateProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.ButtonTemplateProperty, (object) value);
    }

    private static void OnButtonTemplateChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      DataTemplate oldValue = (DataTemplate) e.OldValue;
      DataTemplate buttonTemplate = bindableApplicationBar.ButtonTemplate;
      bindableApplicationBar.OnButtonTemplateChanged(oldValue, buttonTemplate);
    }

    protected virtual void OnButtonTemplateChanged(
      DataTemplate oldButtonTemplate,
      DataTemplate newButtonTemplate)
    {
      this.GenerateButtonsFromSource();
    }

    public IEnumerable MenuItemsSource
    {
      get => (IEnumerable) this.GetValue(BindableApplicationBar.BindableApplicationBar.MenuItemsSourceProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.MenuItemsSourceProperty, (object) value);
    }

    private static void OnMenuItemsSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      IEnumerable oldValue = (IEnumerable) e.OldValue;
      IEnumerable menuItemsSource = bindableApplicationBar.MenuItemsSource;
      bindableApplicationBar.OnMenuItemsSourceChanged(oldValue, menuItemsSource);
    }

    protected virtual void OnMenuItemsSourceChanged(
      IEnumerable oldMenuItemsSource,
      IEnumerable newMenuItemsSource)
    {
      if (oldMenuItemsSource != null && oldMenuItemsSource is INotifyCollectionChanged)
        ((INotifyCollectionChanged) oldMenuItemsSource).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.MenuItemsSourceCollectionChanged);
      this.GenerateMenuItemsFromSource();
      if (newMenuItemsSource == null || !(newMenuItemsSource is INotifyCollectionChanged))
        return;
      ((INotifyCollectionChanged) newMenuItemsSource).CollectionChanged += new NotifyCollectionChangedEventHandler(this.MenuItemsSourceCollectionChanged);
    }

    private void MenuItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.OldItems != null)
      {
        foreach (object oldItem in (IEnumerable) e.OldItems)
        {
          object dataContext = oldItem;
          BindableApplicationBarMenuItem applicationBarMenuItem = this.menuItemsSourceMenuItems.FirstOrDefault<BindableApplicationBarMenuItem>((Func<BindableApplicationBarMenuItem, bool>) (b => b.DataContext == dataContext));
          if (applicationBarMenuItem != null)
            this.menuItemsSourceMenuItems.Remove(applicationBarMenuItem);
        }
      }
      if (this.MenuItemsSource == null || this.MenuItemTemplate == null || e.NewItems == null)
        return;
      foreach (object newItem in (IEnumerable) e.NewItems)
      {
        BindableApplicationBarMenuItem applicationBarMenuItem = (BindableApplicationBarMenuItem) this.MenuItemTemplate.LoadContent();
        if (applicationBarMenuItem == null)
          throw new InvalidOperationException("BindableApplicationBar cannot use the MenuItemsSource property without a valid MenuItemTemplate");
        applicationBarMenuItem.DataContext = newItem;
        this.menuItemsSourceMenuItems.Add(applicationBarMenuItem);
      }
    }

    public DataTemplate MenuItemTemplate
    {
      get => (DataTemplate) this.GetValue(BindableApplicationBar.BindableApplicationBar.MenuItemTemplateProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.MenuItemTemplateProperty, (object) value);
    }

    private static void OnMenuItemTemplateChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      DataTemplate oldValue = (DataTemplate) e.OldValue;
      DataTemplate menuItemTemplate = bindableApplicationBar.MenuItemTemplate;
      bindableApplicationBar.OnMenuItemTemplateChanged(oldValue, menuItemTemplate);
    }

    protected virtual void OnMenuItemTemplateChanged(
      DataTemplate oldMenuItemTemplate,
      DataTemplate newMenuItemTemplate)
    {
      this.GenerateMenuItemsFromSource();
    }

    public bool IsVisible
    {
      get => (bool) this.GetValue(BindableApplicationBar.BindableApplicationBar.IsVisibleProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.IsVisibleProperty, (object) value);
    }

    private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      bool oldValue = (bool) e.OldValue;
      bool isVisible = bindableApplicationBar.IsVisible;
      bindableApplicationBar.OnIsVisibleChanged(oldValue, isVisible);
    }

    protected virtual void OnIsVisibleChanged(bool oldIsVisible, bool newIsVisible)
    {
      if (this.applicationBar != null)
        this.applicationBar.IsVisible = newIsVisible;
      this.isVisibleChanged = true;
    }

    public bool IsMenuEnabled
    {
      get => (bool) this.GetValue(BindableApplicationBar.BindableApplicationBar.IsMenuEnabledProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.IsMenuEnabledProperty, (object) value);
    }

    private static void OnIsMenuEnabledChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      bool oldValue = (bool) e.OldValue;
      bool isMenuEnabled = bindableApplicationBar.IsMenuEnabled;
      bindableApplicationBar.OnIsMenuEnabledChanged(oldValue, isMenuEnabled);
    }

    protected virtual void OnIsMenuEnabledChanged(bool oldIsMenuEnabled, bool newIsMenuEnabled)
    {
      if (this.applicationBar != null)
        this.applicationBar.IsMenuEnabled = newIsMenuEnabled;
      this.isMenuEnabledChanged = true;
    }

    public bool IsMenuVisible
    {
      get => (bool) this.GetValue(BindableApplicationBar.BindableApplicationBar.IsMenuVisibleProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.IsMenuVisibleProperty, (object) value);
    }

    private static void OnIsMenuVisibleChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      bool oldValue = (bool) e.OldValue;
      bool isMenuVisible = bindableApplicationBar.IsMenuVisible;
      bindableApplicationBar.OnIsMenuVisibleChanged(oldValue, isMenuVisible);
    }

    protected virtual void OnIsMenuVisibleChanged(bool oldIsMenuVisible, bool newIsMenuVisible)
    {
      if (this.isMenuVisible == newIsMenuVisible)
        return;
      this.IsMenuVisible = this.isMenuVisible;
    }

    public Color BackgroundColor
    {
      get => (Color) this.GetValue(BindableApplicationBar.BindableApplicationBar.BackgroundColorProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.BackgroundColorProperty, (object) value);
    }

    private static void OnBackgroundColorChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      Color oldValue = (Color) e.OldValue;
      Color backgroundColor = bindableApplicationBar.BackgroundColor;
      bindableApplicationBar.OnBackgroundColorChanged(oldValue, backgroundColor);
    }

    protected virtual void OnBackgroundColorChanged(
      Color oldBackgroundColor,
      Color newBackgroundColor)
    {
      if (this.applicationBar != null)
        this.applicationBar.BackgroundColor = this.BackgroundColor;
      this.backgroundColorChanged = true;
    }

    public Color ForegroundColor
    {
      get => (Color) this.GetValue(BindableApplicationBar.BindableApplicationBar.ForegroundColorProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.ForegroundColorProperty, (object) value);
    }

    private static void OnForegroundColorChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      Color oldValue = (Color) e.OldValue;
      Color foregroundColor = bindableApplicationBar.ForegroundColor;
      bindableApplicationBar.OnForegroundColorChanged(oldValue, foregroundColor);
    }

    protected virtual void OnForegroundColorChanged(
      Color oldForegroundColor,
      Color newForegroundColor)
    {
      if (this.applicationBar != null)
        this.applicationBar.ForegroundColor = this.ForegroundColor;
      this.foregroundColorChanged = true;
    }

    public ApplicationBarMode Mode
    {
      get => (ApplicationBarMode) this.GetValue(BindableApplicationBar.BindableApplicationBar.ModeProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.ModeProperty, (object) value);
    }

    private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      ApplicationBarMode oldValue = (ApplicationBarMode) e.OldValue;
      ApplicationBarMode mode = bindableApplicationBar.Mode;
      bindableApplicationBar.OnModeChanged(oldValue, mode);
    }

    protected virtual void OnModeChanged(ApplicationBarMode oldMode, ApplicationBarMode newMode)
    {
      if (this.applicationBar != null)
        this.applicationBar.Mode = newMode;
      this.modeChanged = true;
    }

    public double BindableOpacity
    {
      get => (double) this.GetValue(BindableApplicationBar.BindableApplicationBar.BindableOpacityProperty);
      set => this.SetValue(BindableApplicationBar.BindableApplicationBar.BindableOpacityProperty, (object) value);
    }

    private static void OnBindableOpacityChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      BindableApplicationBar.BindableApplicationBar bindableApplicationBar = (BindableApplicationBar.BindableApplicationBar) d;
      double oldValue = (double) e.OldValue;
      double bindableOpacity = bindableApplicationBar.BindableOpacity;
      bindableApplicationBar.OnBindableOpacityChanged(oldValue, bindableOpacity);
    }

    protected virtual void OnBindableOpacityChanged(
      double oldBindableOpacity,
      double newBindableOpacity)
    {
      if (this.applicationBar != null)
        this.applicationBar.Opacity = newBindableOpacity;
      this.bindableOpacityChanged = true;
    }

    public BindableApplicationBar()
    {
      this.Buttons = new DependencyObjectCollection<BindableApplicationBarButton>();
      this.MenuItems = new DependencyObjectCollection<BindableApplicationBarMenuItem>();
      this.DefaultStyleKey = (object) typeof (BindableApplicationBar.BindableApplicationBar);
      this.Buttons.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
      this.MenuItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.MenuItemsCollectionChanged);
      this.buttonsSourceButtons.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
      this.menuItemsSourceMenuItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.MenuItemsCollectionChanged);
      this.SetBinding(BindableApplicationBar.BindableApplicationBar.BindableOpacityProperty, new Binding("Opacity")
      {
        RelativeSource = new RelativeSource(RelativeSourceMode.Self)
      });
    }

    public void Attach(PhoneApplicationPage parentPage)
    {
      this.page = parentPage;
      this.applicationBar = (ApplicationBar) (parentPage.ApplicationBar = (IApplicationBar) new ApplicationBar());
      this.applicationBar.StateChanged += new EventHandler<ApplicationBarStateChangedEventArgs>(this.ApplicationBarStateChanged);
      if (this.GetBindingExpression(FrameworkElement.DataContextProperty) == null && this.DataContext == null)
        this.SetBinding(FrameworkElement.DataContextProperty, new Binding("DataContext")
        {
          Source = (object) this.page
        });
      this.SynchronizeProperties();
      this.AttachButtons((IEnumerable<BindableApplicationBarButton>) this.Buttons);
      this.AttachButtons((IEnumerable<BindableApplicationBarButton>) this.buttonsSourceButtons);
      this.AttachMenuItems((IEnumerable<BindableApplicationBarMenuItem>) this.MenuItems);
      this.AttachMenuItems((IEnumerable<BindableApplicationBarMenuItem>) this.menuItemsSourceMenuItems);
    }

    private void SynchronizeProperties()
    {
      if (this.isVisibleChanged)
        this.applicationBar.IsVisible = this.IsVisible;
      else if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.IsVisibleProperty) == null)
        this.IsVisible = this.applicationBar.IsVisible;
      if (this.isMenuEnabledChanged)
        this.applicationBar.IsMenuEnabled = this.IsMenuEnabled;
      else if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.IsMenuEnabledProperty) == null)
        this.IsMenuEnabled = this.applicationBar.IsMenuEnabled;
      if (this.backgroundColorChanged)
        this.applicationBar.BackgroundColor = this.BackgroundColor;
      else if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.BackgroundColorProperty) == null)
        this.BackgroundColor = this.applicationBar.BackgroundColor;
      if (this.foregroundColorChanged)
        this.applicationBar.ForegroundColor = this.ForegroundColor;
      else if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.ForegroundColorProperty) == null)
        this.ForegroundColor = this.applicationBar.ForegroundColor;
      if (this.modeChanged)
        this.applicationBar.Mode = this.Mode;
      else if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.ModeProperty) == null)
        this.Mode = this.applicationBar.Mode;
      if (this.bindableOpacityChanged)
      {
        this.applicationBar.Opacity = this.BindableOpacity;
      }
      else
      {
        if (this.GetBindingExpression(BindableApplicationBar.BindableApplicationBar.BindableOpacityProperty) != null)
          return;
        this.BindableOpacity = this.applicationBar.Opacity;
      }
    }

    private void AttachButtons(IEnumerable<BindableApplicationBarButton> buttons)
    {
      int count = this.applicationBar.Buttons.Count;
      foreach (BindableApplicationBarButton button in buttons)
      {
        button.Attach(this.applicationBar, count++);
        if (button.GetBindingExpression(FrameworkElement.DataContextProperty) == null && button.DataContext == null)
          button.SetBinding(FrameworkElement.DataContextProperty, new Binding("DataContext")
          {
            Source = (object) this
          });
      }
    }

    private void AttachMenuItems(
      IEnumerable<BindableApplicationBarMenuItem> menuItems)
    {
      int count = this.applicationBar.MenuItems.Count;
      foreach (BindableApplicationBarMenuItem menuItem in menuItems)
      {
        menuItem.Attach(this.applicationBar, count++);
        if (menuItem.GetBindingExpression(FrameworkElement.DataContextProperty) == null && menuItem.DataContext == null)
          menuItem.SetBinding(FrameworkElement.DataContextProperty, new Binding("DataContext")
          {
            Source = (object) this
          });
      }
    }

    public void Detach(PhoneApplicationPage parentPage)
    {
      if (parentPage != this.page)
        throw new InvalidOperationException();
      if (this.page != parentPage)
        return;
      BindingExpression bindingExpression = this.GetBindingExpression(FrameworkElement.DataContextProperty);
      if (bindingExpression != null && bindingExpression.ParentBinding.Source == this.page)
        this.DataContext = (object) null;
      this.DetachButtons((IEnumerable<BindableApplicationBarButton>) this.buttonsSourceButtons);
      this.DetachButtons((IEnumerable<BindableApplicationBarButton>) this.Buttons);
      this.DetachMenuItems((IEnumerable<BindableApplicationBarMenuItem>) this.menuItemsSourceMenuItems);
      this.DetachMenuItems((IEnumerable<BindableApplicationBarMenuItem>) this.MenuItems);
      this.applicationBar.StateChanged -= new EventHandler<ApplicationBarStateChangedEventArgs>(this.ApplicationBarStateChanged);
      this.applicationBar = (ApplicationBar) null;
    }

    private void DetachButtons(IEnumerable<BindableApplicationBarButton> buttons)
    {
      foreach (BindableApplicationBarButton button in buttons)
      {
        button.Detach();
        BindingExpression bindingExpression = button.GetBindingExpression(FrameworkElement.DataContextProperty);
        if (bindingExpression != null && bindingExpression.ParentBinding.Source == this)
          this.DataContext = (object) null;
      }
    }

    private void DetachMenuItems(
      IEnumerable<BindableApplicationBarMenuItem> menuItems)
    {
      foreach (BindableApplicationBarMenuItem menuItem in menuItems)
      {
        menuItem.Detach();
        BindingExpression bindingExpression = menuItem.GetBindingExpression(FrameworkElement.DataContextProperty);
        if (bindingExpression != null && bindingExpression.ParentBinding.Source == this)
          this.DataContext = (object) null;
      }
    }

    private void AttachButton(BindableApplicationBarButton button, int i)
    {
      if (button.GetBindingExpression(FrameworkElement.DataContextProperty) == null && button.GetValue(FrameworkElement.DataContextProperty) == null)
        button.DataContext = this.DataContext;
      button.Attach(this.applicationBar, i);
    }

    private void DetachButton(BindableApplicationBarButton button)
    {
      if (button.GetBindingExpression(FrameworkElement.DataContextProperty) == null && button.GetValue(FrameworkElement.DataContextProperty) == this.DataContext)
        button.DataContext = (object) null;
      button.Detach();
    }

    private void AttachMenuItem(BindableApplicationBarMenuItem menuItem, int i)
    {
      if (menuItem.GetBindingExpression(FrameworkElement.DataContextProperty) == null && menuItem.GetValue(FrameworkElement.DataContextProperty) == null)
        menuItem.DataContext = this.DataContext;
      menuItem.Attach(this.applicationBar, i);
    }

    private void DetachMenuItem(BindableApplicationBarMenuItem menuItem)
    {
      if (menuItem.GetBindingExpression(FrameworkElement.DataContextProperty) == null && menuItem.GetValue(FrameworkElement.DataContextProperty) == this.DataContext)
        menuItem.DataContext = (object) null;
      menuItem.Detach();
    }

    private void ApplicationBarStateChanged(object sender, ApplicationBarStateChangedEventArgs e)
    {
      this.isMenuVisible = e.IsMenuVisible;
      this.IsMenuVisible = this.isMenuVisible;
    }

    private void ButtonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.applicationBar == null)
        return;
      if (e.OldItems != null)
      {
        foreach (BindableApplicationBarButton oldItem in (IEnumerable) e.OldItems)
          this.DetachButton(oldItem);
      }
      if (e.NewItems == null)
        return;
      int num = 0;
      foreach (BindableApplicationBarButton newItem in (IEnumerable) e.NewItems)
        this.AttachButton(newItem, e.NewStartingIndex + num++);
    }

    private void MenuItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.applicationBar == null)
        return;
      if (e.OldItems != null)
      {
        foreach (BindableApplicationBarMenuItem oldItem in (IEnumerable) e.OldItems)
          this.DetachMenuItem(oldItem);
      }
      if (e.NewItems == null)
        return;
      int num = 0;
      foreach (BindableApplicationBarMenuItem newItem in (IEnumerable) e.NewItems)
        this.AttachMenuItem(newItem, e.NewStartingIndex + num++);
    }
  }
}
