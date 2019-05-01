using FastNPP.Launcher.View.Config;
using FastNPP.Launcher.View.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastNPP.Launcher.View
{
  public class MainTab : TabControl
  {
    public MainTab()
    {
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Items.Add(new LauncherTabItem());
      Items.Add(new CofigTabItem());      
    }
  }
}
