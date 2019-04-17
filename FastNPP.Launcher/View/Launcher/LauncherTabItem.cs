using FastNPP.Launcher.Controller;
using FastNPP.Launcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastNPP.Launcher.View.Launcher
{
    public class LauncherTabItem : TabItem
    {
    public LauncherVM LauncherVM { get; private set; }
    public LauncherController LauncherController { get; private set; }
    public LauncherTabItem()
      {
      this.LauncherVM = new LauncherVM();
      this.LauncherController = new LauncherController(LauncherVM);
      InitializeLayout();
      }

      public void InitializeLayout()
      {
      Header = "Launcher";
      this.DataContext = LauncherVM;
      this.Content = new LauncherMainGrid(LauncherVM, LauncherController);
    }
    }

  }

