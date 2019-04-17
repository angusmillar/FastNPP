using FastNPP.Launcher.View;
using System.Windows;

namespace FastNPP.Launcher
{
  public class MainWindow : Window
  {
    public MainWindow()
    {
      Title = "NPP Launcher";
      Width = 500;
      Height = 440;
      MinWidth = 500;
      MinHeight = 440;
      InitializeLayout();
    }

    public void InitializeLayout()
    {
      Content = new MainTab();
    }
  }
}