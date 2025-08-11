// ********************************************************************
// Type: Izi.Travel.Shell.Core.Controls.TextButton
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll
using Windows.UI.Xaml;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
    public class TextButton : Button
    {
        // Change TextDecorationCollection to TextDecorations to fix CS0246
        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register(nameof(TextDecorations),
            typeof(TextDecorations), typeof(TextButton), new PropertyMetadata((object)null));

        public TextDecorations TextDecorations
        {
            get => (TextDecorations)this.GetValue(TextButton.TextDecorationsProperty);
            set => this.SetValue(TextButton.TextDecorationsProperty, (object)value);
        }

        public TextButton() => this.DefaultStyleKey = (object)typeof(TextButton);
    }
}

