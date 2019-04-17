using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Launcher.Data
{
  public static class LocalConfiStore
  {
    public static string AppConfigDirectory { get { return "AppConfig"; } }
    public static string LaunchFileDirectory { get { return "LaunchFile"; } }
    public static string AppDirectory { get { return "FastNPPLauncher"; } }
    public static string FastPassLauncherHtmlFileName { get { return "FastPassLauncher.html"; } }
    
    public static string ConfigProfileFileName { get { return "ConfigProfiles.xml"; } }
    public static string PatientProfileFileName { get { return "PatientProfiles.xml"; } }
  }
}
