using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FastNPP.Common.Certificate
{
  public static class CertificateSupport
  {
    public static X509Certificate2 GetCertificate(String findValue, X509FindType findType, StoreName storeName, StoreLocation storeLocation, bool valid)
    {
      X509Store certStore = new X509Store(storeName, storeLocation);
      certStore.Open(OpenFlags.ReadOnly);
      
      X509Certificate2Collection foundCerts =
          certStore.Certificates.Find(findType, findValue, valid);
      certStore.Close();

      // Check if any certificates were found with the criteria
      if (foundCerts.Count == 0)
        throw new ArgumentException("Certificate was not found with criteria '" + findValue + "'");

      // Check if more than one certificate was found with the criteria
      if (foundCerts.Count > 1)
        throw new ArgumentException("More than one certificate found with criteria '" + findValue + "'");

      return foundCerts[0];
    }

    public static List<HpioCertificateInfo> GetNASHCertificateDictonary(StoreName storeName, StoreLocation storeLocation)
    {
      X509Store certStore = new X509Store(storeName, storeLocation);
      certStore.Open(OpenFlags.ReadOnly);
      List<HpioCertificateInfo> CertInfoList = new List<HpioCertificateInfo>();      
      foreach (var Cert in certStore.Certificates)
      {
        if (Cert.Issuer.Contains("Medicare") && Cert.Subject.Contains(".id.electronichealth.net.au"))
        {
          string hpio = (Cert != null ? Cert.Subject.Split('.')[1] : "");          
          CertInfoList.Add(new HpioCertificateInfo()
          {
            Hpio = hpio,
            FingerPrint = Cert.Thumbprint,
            ToDate = Cert.NotAfter,
            FromDate = Cert.NotBefore
          });
        }
      }
      return CertInfoList;
    }
  }
}
