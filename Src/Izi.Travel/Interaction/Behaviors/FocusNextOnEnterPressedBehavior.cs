// IziTravel.Interaction.Behaviors.FocusNextOnEnterPressedBehavior



using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

#nullable disable
namespace Izi.Travel.Shell.Interaction.Behaviors
{
  public class FocusNextOnEnterPressedBehavior //: SafeBehavior<Control>
  {
    public static readonly DependencyProperty NextControlNameProperty = DependencyProperty.Register(nameof (NextControlName), typeof (string), typeof (FocusNextOnEnterPressedBehavior), (PropertyMetadata) null);

    public string NextControlName
    {
        get
        {
                return default;//(string)this.GetValue(FocusNextOnEnterPressedBehavior.NextControlNameProperty);
        }

        set
        {
                value = default;//this.SetValue(FocusNextOnEnterPressedBehavior.NextControlNameProperty, (object)value);
        }
    }

    public FocusNextOnEnterPressedBehavior()
    {
        //this.ListenToPageBackEvent = true;
    }

    protected /*override*/ void OnSetup()
    {
      //base.OnSetup();
      //((UIElement) this.AssociatedObject).KeyUp += new KeyEventHandler(this.OnKeyUp);
    }

    protected /*override*/ void OnCleanup()
    {
       //base.OnCleanup();
       //((UIElement) this.AssociatedObject).KeyUp -= new KeyEventHandler(this.OnKeyUp);
    }

    private void OnKeyUp(object sender, EventArgs e)
    {
      /*
      if (e.Key != 3)
        return;
      Page phoneApplicationPage = ((DependencyObject) this.AssociatedObject).Ancestors<PhoneApplicationPage>().First<PhoneApplicationPage>();
      if (phoneApplicationPage == null)
        return;
      if (string.IsNullOrEmpty(this.NextControlName))
      {
        ((Control) phoneApplicationPage).Focus();
      }
      else
      {
        if (!(((FrameworkElement) phoneApplicationPage).FindName(this.NextControlName) is Control name))
          throw new InvalidOperationException(string.Format("Control '{0}' couldn't be found", (object) this.NextControlName));
        name.Focus();
      }
      */
    }
  }
}

