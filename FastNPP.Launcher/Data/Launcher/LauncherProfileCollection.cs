using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Launcher.Data.Launcher
{
  public class LauncherProfileCollection
  {
    public LauncherProfileCollection()
    {
      ProfileList = new List<LauncherProfile>();
    }

    public List<LauncherProfile> ProfileList { get; set; }
    public string SelectedProfileKey { get; set; }
  }
}
