// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.AboutPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class AboutPrompt : ActionPopUp<object, PopUpResult>
  {
    public static readonly DependencyProperty IsPromptModeProperty = DependencyProperty.Register(nameof (IsPromptMode), typeof (bool), typeof (AboutPrompt), new PropertyMetadata((object) true, new PropertyChangedCallback(AboutPrompt.OnIsPromptModeChanged)));
    public static readonly DependencyProperty WaterMarkProperty = DependencyProperty.Register(nameof (WaterMark), typeof (object), typeof (AboutPrompt), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty VersionNumberProperty = DependencyProperty.Register(nameof (VersionNumber), typeof (object), typeof (AboutPrompt), new PropertyMetadata((object) ("v" + PhoneHelper.GetAppAttribute("Version").Replace(".0.0", ""))));
    public static readonly DependencyProperty BodyProperty = DependencyProperty.Register(nameof (Body), typeof (object), typeof (AboutPrompt), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(nameof (Footer), typeof (object), typeof (AboutPrompt), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (AboutPrompt), new PropertyMetadata((object) PhoneHelper.GetAppAttribute(nameof (Title))));

    public bool IsPromptMode
    {
      get => (bool) this.GetValue(AboutPrompt.IsPromptModeProperty);
      set => this.SetValue(AboutPrompt.IsPromptModeProperty, (object) value);
    }

    public object WaterMark
    {
      get => this.GetValue(AboutPrompt.WaterMarkProperty);
      set => this.SetValue(AboutPrompt.WaterMarkProperty, value);
    }

    public string VersionNumber
    {
      get => (string) this.GetValue(AboutPrompt.VersionNumberProperty);
      set => this.SetValue(AboutPrompt.VersionNumberProperty, (object) value);
    }

    public object Body
    {
      get => this.GetValue(AboutPrompt.BodyProperty);
      set => this.SetValue(AboutPrompt.BodyProperty, value);
    }

    public object Footer
    {
      get => this.GetValue(AboutPrompt.FooterProperty);
      set => this.SetValue(AboutPrompt.FooterProperty, value);
    }

    public string Title
    {
      get => (string) this.GetValue(AboutPrompt.TitleProperty);
      set => this.SetValue(AboutPrompt.TitleProperty, (object) value);
    }

    public AboutPrompt()
    {
      this.DefaultStyleKey = (object) typeof (AboutPrompt);
      RoundButton roundButton = new RoundButton();
      roundButton.Click += new RoutedEventHandler(this.ok_Click);
      this.ActionPopUpButtons.Add((Button) roundButton);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetIsPromptMode(this.IsPromptMode);
    }

    private static void OnIsPromptModeChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      AboutPrompt aboutPrompt = (AboutPrompt) o;
      if (aboutPrompt == null || aboutPrompt.ActionButtonArea == null || e.NewValue == e.OldValue)
        return;
      aboutPrompt.SetIsPromptMode((bool) e.NewValue);
    }

    private void SetIsPromptMode(bool value)
    {
      if (this.ActionButtonArea == null)
        return;
      this.ActionButtonArea.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ok_Click(object sender, RoutedEventArgs e)
    {
      this.OnCompleted(new PopUpEventArgs<object, PopUpResult>()
      {
        PopUpResult = PopUpResult.Ok
      });
    }

    public void Show(
      string authorName,
      string twitterName = null,
      string emailAddress = null,
      string websiteUrl = null)
    {
      List<AboutPromptItem> aboutPromptItemList = new List<AboutPromptItem>()
      {
        new AboutPromptItem()
        {
          Role = "me",
          AuthorName = authorName
        }
      };
      if (!string.IsNullOrEmpty(twitterName))
        aboutPromptItemList.Add(new AboutPromptItem()
        {
          Role = "twitter",
          WebSiteUrl = "http://www.twitter.com/" + twitterName.TrimStart('@')
        });
      if (!string.IsNullOrEmpty(websiteUrl))
        aboutPromptItemList.Add(new AboutPromptItem()
        {
          Role = "web",
          WebSiteUrl = websiteUrl
        });
      if (!string.IsNullOrEmpty(emailAddress))
        aboutPromptItemList.Add(new AboutPromptItem()
        {
          Role = "email",
          EmailAddress = emailAddress
        });
      this.Show(aboutPromptItemList.ToArray());
    }

    public void Show(params AboutPromptItem[] people)
    {
      if (people != null && people.Length > 0)
      {
        StackPanel stackPanel1 = new StackPanel();
        stackPanel1.HorizontalAlignment = HorizontalAlignment.Stretch;
        stackPanel1.VerticalAlignment = VerticalAlignment.Stretch;
        StackPanel stackPanel2 = stackPanel1;
        for (int index = people.Length - 1; index >= 0; --index)
          stackPanel2.Children.Insert(0, (UIElement) people[index]);
        this.Body = (object) stackPanel2;
      }
      this.Show();
    }
  }
}
