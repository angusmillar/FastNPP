using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Launcher.Data.Launcher
{
  public class LauncherProfile
  {
    public string Key { get; set; }
    public string ProfileName { get; set; }
    public string Ihi { get; set; }
    public string MedicareNumber { get; set; }
    public string DvaNumber { get; set; }
    public string Family { get; set; }
    public string Gender { get; set; }
    public DateTime Dob { get; set; }
  }
}
