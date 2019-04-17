using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastNPP.Common;

namespace FastNPP.Launcher.Data.Config
{
  public class ConfigProfileCollection
  {
    public ConfigProfileCollection()
    {
      ProfileList = new List<ConfigProfile>();
    }

    public List<ConfigProfile> ProfileList { get; set; }
    public string SelectedProfileKey { get; set; }    
  }
}
