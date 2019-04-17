using FastNPP.Common;
using FastNPP.Launcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastNPP.Launcher.Data.Config;

namespace FastNPP.Launcher.Controller
{
  public class ConfiController
  {
    public ConfigVM ConfigVM { get; private set; }
    private string AppDirectory;
    private string ConfigDirectory;
    private string ConfigProfileFilePath;
    public ConfiController(ConfigVM ConfigVM)
    {
      AppDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FastNPP.Launcher.Data.LocalConfiStore.AppDirectory);
      ConfigDirectory = System.IO.Path.Combine(AppDirectory, FastNPP.Launcher.Data.LocalConfiStore.AppConfigDirectory);
      ConfigProfileFilePath = System.IO.Path.Combine(ConfigDirectory, FastNPP.Launcher.Data.LocalConfiStore.ConfigProfileFileName);
      this.ConfigVM = ConfigVM;
    }
    
    public void SaveProfile()
    {
      if (!System.IO.Directory.Exists(ConfigDirectory))
      {
        System.IO.Directory.CreateDirectory(ConfigDirectory);
      }
      var x = new ConfigProfileCollection();
      x.ProfileList = ConfigVM.ConfigProfileList.ToList();
      x.SelectedProfileKey = ConfigVM.SelectedConfigProfile.Key;
      var SelectedItem = x.ProfileList.Single(y => y.Key == x.SelectedProfileKey);
      SelectedItem.ProfileName = ConfigVM.ProfileName;
      if (ConfigVM.Cert == null)
      {
        SelectedItem.CertificateFingerPrint = "";
      }
      else
      {
        SelectedItem.CertificateFingerPrint = ConfigVM.Cert.FingerPrint ?? "";
        SelectedItem.Hpio = ConfigVM.Cert.Hpio ?? "";
      }
      
      SelectedItem.Hpii = ConfigVM.Hpii;
      SelectedItem.ProductName = ConfigVM.ProductName;
      SelectedItem.ProductVersion = ConfigVM.ProductVersion;
      SelectedItem.ClientId = ConfigVM.ClientId;
      SelectedItem.Endpoint = ConfigVM.Endpoint;
      SerializerSupport.SerializeTo(ConfigProfileFilePath, x, x.GetType());
      LoadProfiles();
    }

    public void LoadProfiles()
    {
      ConfigProfileCollection LoadedProfiles = SerializerSupport.DeserializeFrom<ConfigProfileCollection>(ConfigProfileFilePath);
      if (LoadedProfiles != null)
      {
        var ConfigProfileList = new System.Collections.ObjectModel.ObservableCollection<ConfigProfile>(LoadedProfiles.ProfileList);
        ConfigVM.SelectedProfileName = LoadedProfiles.ProfileList.Single(x => x.Key == LoadedProfiles.SelectedProfileKey).ProfileName;
        ConfigVM.ConfigProfileList = ConfigProfileList;      
        ConfigVM.SelectedConfigProfile = LoadedProfiles.ProfileList.Single(x => x.Key == LoadedProfiles.SelectedProfileKey);       
        ConfigVM.ProfileName = ConfigVM.SelectedConfigProfile.ProfileName;
        ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == ConfigVM.SelectedConfigProfile.CertificateFingerPrint);
        ConfigVM.Hpii = ConfigVM.SelectedConfigProfile.Hpii;
        ConfigVM.ProductName = ConfigVM.SelectedConfigProfile.ProductName;
        ConfigVM.ProductVersion = ConfigVM.SelectedConfigProfile.ProductVersion;
        ConfigVM.ClientId = ConfigVM.SelectedConfigProfile.ClientId;
        ConfigVM.Endpoint = ConfigVM.SelectedConfigProfile.Endpoint;
      }
      else
      {
        NewProfile();
      }
    }

    public void NewProfile()
    {
      var NewProfileKey = Guid.NewGuid().ToString();
      var NewProfile = new Data.Config.ConfigProfile()
      {
        Key = NewProfileKey,
        ProfileName = "[Profile Name]",   
        Endpoint = "https://b2b.ehealthvendortest.health.gov.au/CIStoNPP"
      };
      if (ConfigVM.ConfigProfileList == null)
      {
        ConfigVM.ConfigProfileList = new System.Collections.ObjectModel.ObservableCollection<ConfigProfile>();       
      }
      ConfigVM.ConfigProfileList.Add(NewProfile);

      if (ConfigVM.SelectedConfigProfile == null)
      {
        ConfigVM.SelectedConfigProfile = NewProfile;
        ConfigVM.ProfileName = NewProfile.ProfileName;        
        ConfigVM.Cert = null;
        ConfigVM.Hpii = NewProfile.Hpii;
        ConfigVM.ProductName = NewProfile.ProductName;
        ConfigVM.ProductVersion = NewProfile.ProductVersion;
        ConfigVM.ClientId = NewProfile.ClientId;
        ConfigVM.Endpoint = NewProfile.Endpoint;
      }
      SaveProfile();
      LoadProfiles();

      ConfigVM.SelectedProfileName = ConfigVM.ConfigProfileList.Single(x => x.Key == NewProfileKey).ProfileName;      
      ConfigVM.SelectedConfigProfile = ConfigVM.ConfigProfileList.Single(x => x.Key == NewProfileKey);
      ConfigVM.ProfileName = ConfigVM.SelectedConfigProfile.ProfileName;
      ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == ConfigVM.SelectedConfigProfile.CertificateFingerPrint);
      ConfigVM.Hpii = ConfigVM.SelectedConfigProfile.Hpii;
      ConfigVM.ProductName = ConfigVM.SelectedConfigProfile.ProductName;
      ConfigVM.ProductVersion = ConfigVM.SelectedConfigProfile.ProductVersion;
      ConfigVM.ClientId = ConfigVM.SelectedConfigProfile.ClientId;
      ConfigVM.Endpoint = ConfigVM.SelectedConfigProfile.Endpoint;
    }

    public void RemoveProfile()
    {
      ConfigVM.ConfigProfileList.Remove(ConfigVM.SelectedConfigProfile);
      if (ConfigVM.ConfigProfileList.Count > 0)
      {
        var NewProfileKey = ConfigVM.ConfigProfileList[0].Key;
        var NewProfile = ConfigVM.ConfigProfileList[0];
        ConfigVM.SelectedConfigProfile = NewProfile;
        ConfigVM.ProfileName = NewProfile.ProfileName;
        ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == NewProfile.CertificateFingerPrint);
        ConfigVM.Hpii = NewProfile.Hpii;
        ConfigVM.ProductName = NewProfile.ProductName;
        ConfigVM.ProductVersion = NewProfile.ProductVersion;
        ConfigVM.ClientId = NewProfile.ClientId;
        ConfigVM.Endpoint = NewProfile.Endpoint;

        SaveProfile();
        LoadProfiles();

        ConfigVM.SelectedProfileName = ConfigVM.ConfigProfileList.Single(x => x.Key == NewProfileKey).ProfileName;

        ConfigVM.SelectedConfigProfile = ConfigVM.ConfigProfileList.Single(x => x.Key == NewProfileKey);
        ConfigVM.ProfileName = ConfigVM.SelectedConfigProfile.ProfileName;
        ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == NewProfile.CertificateFingerPrint); 
        ConfigVM.Hpii = ConfigVM.SelectedConfigProfile.Hpii;
        ConfigVM.ProductName = ConfigVM.SelectedConfigProfile.ProductName;
        ConfigVM.ProductVersion = ConfigVM.SelectedConfigProfile.ProductVersion;
        ConfigVM.ClientId = ConfigVM.SelectedConfigProfile.ClientId;
        ConfigVM.Endpoint = ConfigVM.SelectedConfigProfile.Endpoint;
      }
      else
      {
        ConfigVM.SelectedConfigProfile = null;
        NewProfile();
      }      
    }

    public void ProfileSelectionChanged()
    {
      ConfigVM.SelectedConfigProfile = ConfigVM.ConfigProfileList.Single(x => x.ProfileName == ConfigVM.SelectedProfileName);      
      ConfigVM.ProfileName = ConfigVM.SelectedConfigProfile.ProfileName;      
      ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == ConfigVM.SelectedConfigProfile.CertificateFingerPrint);
      ConfigVM.Hpii = ConfigVM.SelectedConfigProfile.Hpii;
      ConfigVM.ProductName = ConfigVM.SelectedConfigProfile.ProductName;
      ConfigVM.ProductVersion = ConfigVM.SelectedConfigProfile.ProductVersion;
      ConfigVM.ClientId = ConfigVM.SelectedConfigProfile.ClientId;
      ConfigVM.Endpoint = ConfigVM.SelectedConfigProfile.Endpoint;
    }
  }
}
