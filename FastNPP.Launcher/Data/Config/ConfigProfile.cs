using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Launcher.Data.Config
{
  public class ConfigProfile
  {
    public string Key { get; set; }
    public string ProfileName { get; set; }
    public string CertificateFingerPrint { get; set; }
    public string Hpii { get; set; }
    public string Hpio { get; set; }
    public string ProductName { get; set; }
    public string ProductVersion { get; set; }
    public string ClientId { get; set; }
    public string Endpoint { get; set; }
  }
}
