// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AppBarItemTrigger
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Interactivity;

#nullable disable
namespace Caliburn.Micro
{
  internal class AppBarItemTrigger : TriggerBase<PhoneApplicationPage>
  {
    public AppBarItemTrigger(IApplicationBarMenuItem button)
    {
      button.Click += new EventHandler(this.ButtonClicked);
    }

    private void ButtonClicked(object sender, EventArgs e) => this.InvokeActions((object) e);
  }
}
