using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Common.Certificate
{
  public class HpioCertificateInfo
  {
    public string Hpio { get; set; }
    public string HpioFomratted
    {
      get
      {
        if (Hpio.Length == 16)
        {
          return $"HPI-O: {Hpio.Substring(0, 4)} {Hpio.Substring(4, 4)} {Hpio.Substring(8, 4)} {Hpio.Substring(12, 4)}, Expiry: {ToDate.ToString("dd/MMM/yyyy")}";
        }
        else
        {
          return Hpio;
        }
      }
      set { Hpio = value; }
    }
    public string FingerPrint { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
  }
}
