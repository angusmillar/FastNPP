using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using FastNPP.Launcher.ViewModel;
using FastNPP.Launcher.Controller;
using System.Windows;
using System.Windows.Data;

namespace FastNPP.Launcher.View.Launcher

{
  public class LauncherMainGrid : Grid
  {

    public LauncherVM LauncherVM { get; private set; }
    public LauncherController LauncherController { get; private set; }
    public LauncherMainGrid(LauncherVM LauncherVM, LauncherController LauncherController)
    {
      
      this.LauncherController = LauncherController;
      this.LauncherVM = LauncherVM;
      LauncherController.LoadProfiles();
      InitializeLayout();
      
    }

    private void InitializeLayout()
    {
      this.SetGrid(4, 16);

      var ProfileSelectorGroup = new GroupBox();
      Grid.SetRow(ProfileSelectorGroup, 0);
      Grid.SetColumn(ProfileSelectorGroup, 0);
      Grid.SetColumnSpan(ProfileSelectorGroup, 16);
      this.Children.Add(ProfileSelectorGroup);
      ProfileSelectorGroup.Header = "Profiles";
      var ProfileSelectorGrid = ComponentFactory.GetGrid(1, 16);
      ProfileSelectorGroup.Content = ProfileSelectorGrid;


      var ProfileSelector = GetProfileComboBoxDockPanel("Profile", 50, "SelectedProfileName", "ProfileNameList");
      Grid.SetRow(ProfileSelector, 0);
      Grid.SetColumn(ProfileSelector, 0);
      Grid.SetColumnSpan(ProfileSelector, 8);
      ProfileSelectorGrid.Children.Add(ProfileSelector);

      

      var ProfileNewButton = ComponentFactory.GetButton("New");
      ProfileNewButton.Click += new System.Windows.RoutedEventHandler((Obj, e) =>
      {
        if (LauncherVM.ProfileName == "[Profile Name]")
        {
          MessageBox.Show("Only one new profile at a time, rename this profile first.");
        }
        else
        {
          LauncherController.NewProfile();
        }        
      });
     

      var ProfileRemoveButton = ComponentFactory.GetButton("Delete");
      ProfileRemoveButton.Click += new System.Windows.RoutedEventHandler((Obj, e) =>
      {
        LauncherController.RemoveProfile();
      });
      

      var ProfileSaveButton = ComponentFactory.GetButton("Save");
      ProfileSaveButton.SetBinding(Button.IsEnabledProperty, "CanSave");
      ProfileSaveButton.Click += new System.Windows.RoutedEventHandler((Obj, e) =>
      {
        if (LauncherVM.LauncherProfileList.SingleOrDefault(x => x.ProfileName == LauncherVM.ProfileName) != null)
        {
          MessageBox.Show("You must use a profile name not already in use.");
        }
        else if (string.IsNullOrWhiteSpace(LauncherVM.ProfileName))
        {
          MessageBox.Show("Profile name can not be empty.");
        }
        else
        {
          LauncherController.SaveProfile();
        }        
      });
          
      var ProfileButtonStackPanel = new StackPanel();
      ProfileButtonStackPanel.Orientation = Orientation.Horizontal;
      ProfileButtonStackPanel.Children.Add(ProfileNewButton);
      ProfileButtonStackPanel.Children.Add(ProfileRemoveButton);
      ProfileButtonStackPanel.Children.Add(ProfileSaveButton);
      Grid.SetRow(ProfileButtonStackPanel, 0);
      Grid.SetColumn(ProfileButtonStackPanel, 8);
      Grid.SetColumnSpan(ProfileButtonStackPanel, 12);
      ProfileSelectorGrid.Children.Add(ProfileButtonStackPanel);
      
      var ProfileGroup = new GroupBox();
      Grid.SetRow(ProfileGroup, 1);
      Grid.SetColumn(ProfileGroup, 0);
      Grid.SetColumnSpan(ProfileGroup, 16);
      this.Children.Add(ProfileGroup);
      ProfileGroup.Header = "Selected Patient Profile";
      var SelectedProfileGrid = ComponentFactory.GetGrid(10, 16);
      ProfileGroup.Content = SelectedProfileGrid;

      var ProfileName = ComponentFactory.GetValueParameterDockPanel("Profile Name", 120, "ProfileName");
      Grid.SetRow(ProfileName, 0);
      Grid.SetColumn(ProfileName, 0);
      Grid.SetColumnSpan(ProfileName, 16);
      SelectedProfileGrid.Children.Add(ProfileName);
      
      var Family = ComponentFactory.GetValueParameterDockPanel("Family Name", 120, "Family");
      Grid.SetRow(Family, 1);
      Grid.SetColumn(Family, 0);
      Grid.SetColumnSpan(Family, 16);
      SelectedProfileGrid.Children.Add(Family);

      var Dob = ComponentFactory.GetValueDatePickerDockPanel("Date of Birth", 120, "Dob");
      Grid.SetRow(Dob, 2);
      Grid.SetColumn(Dob, 0);
      Grid.SetColumnSpan(Dob, 16);
      SelectedProfileGrid.Children.Add(Dob);

      var Gender = ComponentFactory.GetValueComboBoxEnumDockPanel("Gender", 120, "Gender", LauncherVM.GenderList);      
      Grid.SetRow(Gender, 3);
      Grid.SetColumn(Gender, 0);
      Grid.SetColumnSpan(Gender, 16);
      SelectedProfileGrid.Children.Add(Gender);

      var Ihi = ComponentFactory.GetValueParameterDockPanel("IHI", 120, "Ihi");
      Grid.SetRow(Ihi, 4);
      Grid.SetColumn(Ihi, 0);
      Grid.SetColumnSpan(Ihi, 16);
      SelectedProfileGrid.Children.Add(Ihi);

      var MedicareNumber = ComponentFactory.GetValueParameterDockPanel("Medicare Number", 120, "MedicareNumber");
      Grid.SetRow(MedicareNumber, 5);
      Grid.SetColumn(MedicareNumber, 0);
      Grid.SetColumnSpan(MedicareNumber, 16);
      SelectedProfileGrid.Children.Add(MedicareNumber);

      var DvaNumber = ComponentFactory.GetValueParameterDockPanel("DVA Number", 120, "DvaNumber");
      Grid.SetRow(DvaNumber, 6);
      Grid.SetColumn(DvaNumber, 0);
      Grid.SetColumnSpan(DvaNumber, 16);
      SelectedProfileGrid.Children.Add(DvaNumber);

      var LaunchButton = new Button();
      LaunchButton.Content = "Launch Provider Portal";
      LaunchButton.Height = 60;
      LaunchButton.VerticalAlignment = VerticalAlignment.Stretch;
      LaunchButton.Click += new RoutedEventHandler((obj, e) =>
      {
        LauncherController.LaunchProviderPortal();        
      });
     
      Grid.SetRow(LaunchButton, 7);
      Grid.SetRowSpan(LaunchButton, 3);
      Grid.SetColumn(LaunchButton, 0);
      Grid.SetColumnSpan(LaunchButton, 16);
      SelectedProfileGrid.Children.Add(LaunchButton);

    }

    private DockPanel GetProfileComboBoxDockPanel(string LabelName, int LabelWidth, string ValueBindingName, string ItemsSource)
    {
      Label ValueLabel = new Label();
      ValueLabel.Content = LabelName;
      ValueLabel.Width = LabelWidth;
      ValueLabel.VerticalContentAlignment = VerticalAlignment.Center;
      ValueLabel.FontWeight = FontWeights.DemiBold;
      ValueLabel.Margin = new Thickness(3);
      DockPanel.SetDock(ValueLabel, Dock.Left);

      Binding Binding = new Binding(ValueBindingName);
      Binding.ValidationRules.Clear();
      Binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
      Binding.NotifyOnValidationError = true;
      Binding.ValidatesOnDataErrors = true;

      

      ComboBox IdTypeComoBox = new ComboBox();
      IdTypeComoBox.SetBinding(ComboBox.ItemsSourceProperty, ItemsSource);
      //IdTypeComoBox.ItemsSource = ItemsSource;
      IdTypeComoBox.SetBinding(ComboBox.SelectedItemProperty, Binding);
      IdTypeComoBox.VerticalContentAlignment = VerticalAlignment.Center;
      IdTypeComoBox.Margin = new Thickness(3);
      IdTypeComoBox.SelectionChanged += new SelectionChangedEventHandler((obj, e) =>
      {
        var test = LauncherVM.SelectedProfileName;
        LauncherController.ProfileSelectionChanged();
      });
      DockPanel.SetDock(IdTypeComoBox, Dock.Left);

      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(IdTypeComoBox);

      return Panel;
    }

    
  }
}
