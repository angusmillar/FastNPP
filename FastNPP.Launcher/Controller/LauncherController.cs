using FastNPP.Common;
using FastNPP.Launcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastNPP.Launcher.Data.Launcher;
using FastNPP.Client;
using FastNPP.Launcher.Data.Config;
using FastNPP.Common.Certificate;
using System.Security.Cryptography.X509Certificates;

namespace FastNPP.Launcher.Controller
{
  public class LauncherController
  {
    public LauncherVM LauncherVM { get; private set; }
    private string AppDirectory;
    private string ConfigDirectory;
    private string LaunchDirectory;
    private string ConfigProfileFilePath;
    private string PatientProfileFilePath;
    private string FastPassLauncherHtmlFileName;
    public LauncherController(LauncherVM LauncherVM)
    {
      AppDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FastNPP.Launcher.Data.LocalConfiStore.AppDirectory);
      ConfigDirectory = System.IO.Path.Combine(AppDirectory, FastNPP.Launcher.Data.LocalConfiStore.AppConfigDirectory);
      LaunchDirectory = System.IO.Path.Combine(AppDirectory, FastNPP.Launcher.Data.LocalConfiStore.LaunchFileDirectory);
      ConfigProfileFilePath = System.IO.Path.Combine(ConfigDirectory, FastNPP.Launcher.Data.LocalConfiStore.ConfigProfileFileName);
      PatientProfileFilePath = System.IO.Path.Combine(ConfigDirectory, FastNPP.Launcher.Data.LocalConfiStore.PatientProfileFileName);
      FastPassLauncherHtmlFileName = System.IO.Path.Combine(LaunchDirectory, FastNPP.Launcher.Data.LocalConfiStore.FastPassLauncherHtmlFileName);
      this.LauncherVM = LauncherVM;
    }

    public string LaunchProviderPortal()
    {
      if (!System.IO.Directory.Exists(LaunchDirectory))
      {
        System.IO.Directory.CreateDirectory(LaunchDirectory);
      }
      ConfigProfileCollection ConfigProfileList = SerializerSupport.DeserializeFrom<ConfigProfileCollection>(ConfigProfileFilePath);
      var CurrentProfile = ConfigProfileList.ProfileList.SingleOrDefault(x => x.Key == ConfigProfileList.SelectedProfileKey);
      X509Certificate2 Cert = CertificateSupport.GetCertificate(CurrentProfile.CertificateFingerPrint, X509FindType.FindByThumbprint, StoreName.My, StoreLocation.CurrentUser, true);
      MhrRestClient Client = new MhrRestClient(CurrentProfile.Endpoint, CurrentProfile.ClientId, Cert, CurrentProfile.ProductName, CurrentProfile.ProductVersion);
      MhrRestClientResponse Response = Client.GetAccessToNpp(CurrentProfile.Hpio, CurrentProfile.Hpii, LauncherVM.Dob.ToString("dd-MM-yyyy"), LauncherVM.Gender, LauncherVM.Family, LauncherVM.Ihi, LauncherVM.MedicareNumber, LauncherVM.DvaNumber);
      if (Response.HttpStatus == System.Net.HttpStatusCode.OK)
      {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(FastPassLauncherHtmlFileName);
        sw.WriteLine(Response.Content);
        sw.Close();
        System.Diagnostics.Process.Start(FastPassLauncherHtmlFileName);
        return string.Empty;
      }
      else
      {
        return $"{Response.Message}";
      }
    }

    public void SaveProfile()
    {
      if (!System.IO.Directory.Exists(ConfigDirectory))
      {
        System.IO.Directory.CreateDirectory(ConfigDirectory);
      }
      var x = new LauncherProfileCollection();
      x.ProfileList = LauncherVM.LauncherProfileList.ToList();
      x.SelectedProfileKey = LauncherVM.SelectedLauncherProfile.Key;
      var SelectedItem = x.ProfileList.Single(y => y.Key == x.SelectedProfileKey);
      SelectedItem.ProfileName = LauncherVM.ProfileName;            
      SelectedItem.Dob = LauncherVM.Dob;
      SelectedItem.DvaNumber = LauncherVM.DvaNumber;
      SelectedItem.Family = LauncherVM.Family;
      SelectedItem.Gender = LauncherVM.Gender;
      SelectedItem.Ihi = LauncherVM.Ihi;
      SelectedItem.MedicareNumber = LauncherVM.MedicareNumber;      
      SerializerSupport.SerializeTo(PatientProfileFilePath, x, x.GetType());
      LoadProfiles();
    }

    public void LoadProfiles()
    {
      LauncherProfileCollection LoadedProfiles = SerializerSupport.DeserializeFrom<LauncherProfileCollection>(PatientProfileFilePath);
      if (LoadedProfiles != null)
      {
        var LauncherProfileList = new System.Collections.ObjectModel.ObservableCollection<LauncherProfile>(LoadedProfiles.ProfileList);
        LauncherVM.SelectedProfileName = LoadedProfiles.ProfileList.Single(x => x.Key == LoadedProfiles.SelectedProfileKey).ProfileName;
        LauncherVM.LauncherProfileList = LauncherProfileList;      
        LauncherVM.SelectedLauncherProfile = LoadedProfiles.ProfileList.Single(x => x.Key == LoadedProfiles.SelectedProfileKey);       
        LauncherVM.ProfileName = LauncherVM.SelectedLauncherProfile.ProfileName;
        LauncherVM.Dob = LauncherVM.Dob;
        LauncherVM.DvaNumber = LauncherVM.SelectedLauncherProfile.DvaNumber;
        LauncherVM.Family = LauncherVM.SelectedLauncherProfile.Family;
        LauncherVM.Gender = LauncherVM.SelectedLauncherProfile.Gender;
        LauncherVM.Ihi = LauncherVM.SelectedLauncherProfile.Ihi;
        LauncherVM.MedicareNumber = LauncherVM.SelectedLauncherProfile.MedicareNumber;
      }
      else
      {
        NewProfile();
      }
    }

    public void NewProfile()
    {
      var NewProfileKey = Guid.NewGuid().ToString();
      var NewProfile = new LauncherProfile()
      {
        Key = NewProfileKey,
        ProfileName = "[Profile Name]"        
      };
      if (LauncherVM.LauncherProfileList == null)
      {
        LauncherVM.LauncherProfileList = new System.Collections.ObjectModel.ObservableCollection<LauncherProfile>();       
      }
      LauncherVM.LauncherProfileList.Add(NewProfile);

      if (LauncherVM.SelectedLauncherProfile == null)
      {
        LauncherVM.SelectedLauncherProfile = NewProfile;
        LauncherVM.ProfileName = NewProfile.ProfileName;
        //ConfigVM.Cert = ConfigVM.HpioCertList.SingleOrDefault(x => x.FingerPrint == NewProfile.CertificateFingerPrint);
        LauncherVM.ProfileName = NewProfile.ProfileName;
        LauncherVM.Dob = NewProfile.Dob;
        LauncherVM.DvaNumber = NewProfile.DvaNumber;
        LauncherVM.Family = NewProfile.Family;
        LauncherVM.Gender = NewProfile.Gender;
        LauncherVM.Ihi = NewProfile.Ihi;
        LauncherVM.MedicareNumber = NewProfile.MedicareNumber;

      }
      SaveProfile();
      LoadProfiles();

      LauncherVM.SelectedProfileName = LauncherVM.LauncherProfileList.Single(x => x.Key == NewProfileKey).ProfileName;
      
      LauncherVM.SelectedLauncherProfile = LauncherVM.LauncherProfileList.Single(x => x.Key == NewProfileKey);
      LauncherVM.ProfileName = LauncherVM.SelectedLauncherProfile.ProfileName;
      LauncherVM.Dob = LauncherVM.SelectedLauncherProfile.Dob;
      LauncherVM.DvaNumber = LauncherVM.SelectedLauncherProfile.DvaNumber;
      LauncherVM.Family = LauncherVM.SelectedLauncherProfile.Family;
      LauncherVM.Gender = LauncherVM.SelectedLauncherProfile.Gender;
      LauncherVM.Ihi = LauncherVM.SelectedLauncherProfile.Ihi;
      LauncherVM.MedicareNumber = LauncherVM.SelectedLauncherProfile.MedicareNumber;
    }

    public void RemoveProfile()
    {
      LauncherVM.LauncherProfileList.Remove(LauncherVM.SelectedLauncherProfile);
      if (LauncherVM.LauncherProfileList.Count > 0)
      {
        var NewProfileKey = LauncherVM.LauncherProfileList[0].Key;
        var NewProfile = LauncherVM.LauncherProfileList[0];
        LauncherVM.SelectedLauncherProfile = NewProfile;
        LauncherVM.ProfileName = NewProfile.ProfileName;
        LauncherVM.Dob = NewProfile.Dob;
        LauncherVM.DvaNumber = NewProfile.DvaNumber;
        LauncherVM.Family = NewProfile.Family;
        LauncherVM.Gender = NewProfile.Gender;
        LauncherVM.Ihi = NewProfile.Ihi;
        LauncherVM.MedicareNumber = NewProfile.MedicareNumber;

        SaveProfile();
        LoadProfiles();

        LauncherVM.SelectedProfileName = LauncherVM.LauncherProfileList.Single(x => x.Key == NewProfileKey).ProfileName;

        LauncherVM.SelectedLauncherProfile = LauncherVM.LauncherProfileList.Single(x => x.Key == NewProfileKey);
        LauncherVM.ProfileName = LauncherVM.SelectedLauncherProfile.ProfileName;
        LauncherVM.Dob = LauncherVM.SelectedLauncherProfile.Dob; 
        LauncherVM.DvaNumber = LauncherVM.SelectedLauncherProfile.DvaNumber;
        LauncherVM.Family = LauncherVM.SelectedLauncherProfile.Family;
        LauncherVM.Gender = LauncherVM.SelectedLauncherProfile.Gender;
        LauncherVM.Ihi = LauncherVM.SelectedLauncherProfile.Ihi;
        LauncherVM.MedicareNumber = LauncherVM.SelectedLauncherProfile.MedicareNumber;
      }
      else
      {
        LauncherVM.SelectedLauncherProfile = null;
        NewProfile();
      }      
    }

    public void ProfileSelectionChanged()
    {
      LauncherVM.SelectedLauncherProfile = LauncherVM.LauncherProfileList.Single(x => x.ProfileName == LauncherVM.SelectedProfileName);      
      LauncherVM.ProfileName = LauncherVM.SelectedLauncherProfile.ProfileName;      
      LauncherVM.Dob = LauncherVM.SelectedLauncherProfile.Dob;
      LauncherVM.DvaNumber = LauncherVM.SelectedLauncherProfile.DvaNumber;
      LauncherVM.Family = LauncherVM.SelectedLauncherProfile.Family;
      LauncherVM.Gender = LauncherVM.SelectedLauncherProfile.Gender;
      LauncherVM.Ihi = LauncherVM.SelectedLauncherProfile.Ihi;
      LauncherVM.MedicareNumber = LauncherVM.SelectedLauncherProfile.MedicareNumber;
    }

    
  }
}
