using FastNPP.Launcher.Data.Launcher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FastNPP.Launcher.ViewModel
{
  public class LauncherVM : BaseVM
  {
    public LauncherVM()
    {
      GenderList = new List<string>() { "M", "F", "U", "O" };
    }

    public List<string> GenderList { get; private set; }
    public LauncherProfile SelectedLauncherProfile { get; set; }

    private ObservableCollection<LauncherProfile> _LauncherProfileList;
    public ObservableCollection<LauncherProfile> LauncherProfileList
    {
      get
      {
        return _LauncherProfileList;
      }
      set
      {
        _LauncherProfileList = value;
        ProfileNameList = new ObservableCollection<string>(value.Select(x => x.ProfileName).ToList());
        OnPropertyChanged("LauncherProfileList");
      }
    }

    private ObservableCollection<string> _ProfileNameList;
    public ObservableCollection<string> ProfileNameList
    {
      get { return _ProfileNameList; }
      set
      {
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
    
    private string _Ihi;
    public string Ihi
    {
      get { return _Ihi; }
      set
      {
        _Ihi = value;
        OnPropertyChanged("Ihi");
        OnPropertyChanged("CanSave");
      }
    }
    
    private string _MedicareNumber;
    public string MedicareNumber
    {
      get { return _MedicareNumber; }
      set
      {
        _MedicareNumber = value;
        OnPropertyChanged("MedicareNumber");
        OnPropertyChanged("CanSave");
      }
    }

    private string _DvaNumber;
    public string DvaNumber
    {
      get { return _DvaNumber; }
      set
      {
        _DvaNumber = value;
        OnPropertyChanged("DvaNumber");
        OnPropertyChanged("CanSave");
      }
    }

    private string _Family;
    public string Family
    {
      get { return _Family; }
      set
      {
        _Family = value;
        OnPropertyChanged("Family");
        OnPropertyChanged("CanSave");
      }
    }

    private DateTime _Dob;
    public DateTime Dob
    {
      get { return _Dob; }
      set
      {
        _Dob = value;
        OnPropertyChanged("Dob");
        OnPropertyChanged("CanSave");
      }
    }

    private string _Gender;
    public string Gender
    {
      get { return _Gender; }
      set
      {
        _Gender = value;
        OnPropertyChanged("Gender");
        OnPropertyChanged("CanSave");
      }
    }
    private bool CalculateCanSave()
    {
      if (this.ProfileName != this.SelectedProfileName)
        return true;

      if (this.MedicareNumber != this.SelectedLauncherProfile.MedicareNumber)
        return true;

      if (this.Ihi != this.SelectedLauncherProfile.Ihi)
        return true;

      if (this.DvaNumber != this.SelectedLauncherProfile.DvaNumber)
        return true;

      if (this.Family != this.SelectedLauncherProfile.Family)
        return true;

      if (this.Gender != this.SelectedLauncherProfile.Gender)
        return true;

      return false;
    }

  }
}
