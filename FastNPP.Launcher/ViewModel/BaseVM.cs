using System.ComponentModel;

namespace FastNPP.Launcher.ViewModel
{
  public abstract class BaseVM : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    // Raise a property change notification
    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
