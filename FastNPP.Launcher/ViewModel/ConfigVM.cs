using FastNPP.Launcher.Data.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FastNPP.Common.Certificate;

namespace FastNPP.Launcher.ViewModel
{
  public class ConfigVM : BaseVM
  {

    public List<HpioCertificateInfo> HpioCertList { get; private set; }
    public ConfigVM()
    {
      HpioCertList = CertificateSupport.GetNASHCertificateDictonary(StoreName.My, StoreLocation.CurrentUser);      
    }
    
    public ConfigProfile SelectedConfigProfile { get; set; }

    private ObservableCollection<ConfigProfile> _ConfigProfileList;
    public ObservableCollection<ConfigProfile> ConfigProfileList
    {
      get
      {
        return _ConfigProfileList;
      }
      set
      {
        _ConfigProfileList = value;
        ProfileNameList = new ObservableCollection<string>(value.Select(x => x.ProfileName).ToList());
        OnPropertyChanged("ConfigProfileList");
      }
    }

    private ObservableCollection<string> _ProfileNameList;    
    public ObservableCollection<string> ProfileNameList
    {
      get { return _ProfileNameList; }
      set {
        _ProfileNameList = value;
        OnPropertyChanged("ProfileNameList");
      }
    }


    public bool CanSave
    {
      get { return CalculateCanSave(); }
      set
      {
        OnPropertyChanged("CanSave");
      }
    }


    private string _SelectedProfileName;
    public string SelectedProfileName
    {
      get { return _SelectedProfileName; }
      set
      {
        _SelectedProfileName = value;
        OnPropertyChanged("SelectedProfileName");
      }
    }

    private string _ProfileName;
    public string ProfileName
    {
      get { return _ProfileName; }
      set
      {
        _ProfileName = value;
        OnPropertyChanged("ProfileName");
        OnPropertyChanged("CanSave");
      }
    }

    private HpioCertificateInfo _Cert;
    public HpioCertificateInfo Cert
    {
      get
      {
        return _Cert;
      }
      set
      {
        _Cert = value;
        OnPropertyChanged("Cert");
        OnPropertyChanged("CanSave");
      }
    }

    
    private string _Hpii;
    public string Hpii
    {
      get { return _Hpii; }
      set
      {
        _Hpii = value;
        OnPropertyChanged("Hpii");
        OnPropertyChanged("CanSave");
      }
    }


    private string _ProductName;
    public string ProductName
    {
      get { return _ProductName; }
      set
      {
        _ProductName = value;
        OnPropertyChanged("ProductName");
        OnPropertyChanged("CanSave");
      }
    }

    private string _ProductVersion;
    public string ProductVersion
    {
      get { return _ProductVersion; }
      set
      {
        _ProductVersion = value;
        OnPropertyChanged("ProductVersion");
        OnPropertyChanged("CanSave");
      }
    }

    private string _ClientId;
    public string ClientId
    {
      get { return _ClientId; }
      set
      {
        _ClientId = value;
        OnPropertyChanged("ClientId");
        OnPropertyChanged("CanSave");
      }
    }

    private string _Endpoint;
    public string Endpoint
    {
      get { return _Endpoint; }
      set
      {
        _Endpoint = value;
        OnPropertyChanged("Endpoint");
        OnPropertyChanged("CanSave");
      }
    }
    private bool CalculateCanSave()
    {
      if (this.ProfileName != this.SelectedProfileName)
        return true;
      //if (this.Cert != this.SelectedConfigProfile.Certificate)
      //  return true;
      if (this.Hpii != this.SelectedConfigProfile.Hpii)
        return true;

      return false;

    }

  }
}
