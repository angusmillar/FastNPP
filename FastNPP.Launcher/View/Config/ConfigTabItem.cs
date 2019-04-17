using FastNPP.Launcher.Controller;
using FastNPP.Launcher.ViewModel;
using System.Windows.Controls;

namespace FastNPP.Launcher.View.Config
{
  public class CofigTabItem : TabItem
  {
    public ConfigVM ConfigVM { get; private set; }
    public ConfiController ConfiController { get; private set; }
    public CofigTabItem()
    {
      this.ConfigVM = new ConfigVM();
      this.ConfiController = new ConfiController(ConfigVM);      
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Header = "Configuration";
      this.DataContext = ConfigVM;
      this.Content = new ConfigMainGrid(ConfigVM, ConfiController);
    }
  }

}

