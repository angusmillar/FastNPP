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

namespace FastNPP.Launcher.View.Config
{
  public class ConfigMainGrid : Grid
  {

    public ConfigVM ConfigVM { get; private set; }
    public ConfiController ConfiController { get; private set; }
    public ConfigMainGrid(ConfigVM ConfigVM, ConfiController ConfiController)
    {
      
      this.ConfiController = ConfiController;
      this.ConfigVM = ConfigVM;
      ConfiController.LoadProfiles();
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
        if (ConfigVM.ProfileName == "[Profile Name]")
        {
          MessageBox.Show("Only one new profile at a time, rename this profile first.");
        }
        else
        {
          ConfiController.NewProfile();
        }        
      });
     

      var ProfileRemoveButton = ComponentFactory.GetButton("Delete");
      ProfileRemoveButton.Click += new System.Windows.RoutedEventHandler((Obj, e) =>
      {
        ConfiController.RemoveProfile();
      });
      

      var ProfileSaveButton = ComponentFactory.GetButton("Save");
      ProfileSaveButton.SetBinding(Button.IsEnabledProperty, "CanSave");
      ProfileSaveButton.Click += new System.Windows.RoutedEventHandler((Obj, e) =>
      {
        if (ConfigVM.ConfigProfileList.SingleOrDefault(x => x.ProfileName == ConfigVM.ProfileName) != null)
        {
          MessageBox.Show("You must use a profile name not already in use.");
        }
        else if (string.IsNullOrWhiteSpace(ConfigVM.ProfileName))
        {
          MessageBox.Show("Profile name can not be empty.");
        }
        else
        {
          ConfiController.SaveProfile();
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
      ProfileGroup.Header = "Selected Configuration Profile";
      var SelectedProfileGrid = ComponentFactory.GetGrid(7, 16);
      ProfileGroup.Content = SelectedProfileGrid;

      var ProfileName = ComponentFactory.GetValueParameterDockPanel("Profile Name", 120, "ProfileName");
      Grid.SetRow(ProfileName, 0);
      Grid.SetColumn(ProfileName, 0);
      Grid.SetColumnSpan(ProfileName, 16);
      SelectedProfileGrid.Children.Add(ProfileName);
      
      //var CertSelector = ComponentFactory.GetValueComboBoxEnumDockPanel("Nash Certificate", 120, "Cert", ConfigVM.NashCertDictonary.Select(x => x.Key).ToList());
      var CertSelector = GetCertificateComboBoxDockPanel("Nash Certificate", 120, "Cert");
      Grid.SetRow(CertSelector, 1);
      Grid.SetColumn(CertSelector, 0);
      Grid.SetColumnSpan(CertSelector, 16);
      SelectedProfileGrid.Children.Add(CertSelector);

      var Hpii = ComponentFactory.GetValueParameterDockPanel("HPI-O Linked HPI-I", 120, "Hpii");
      Grid.SetRow(Hpii, 2);
      Grid.SetColumn(Hpii, 0);
      Grid.SetColumnSpan(Hpii, 16);
      SelectedProfileGrid.Children.Add(Hpii);

      var ProductName = ComponentFactory.GetValueParameterDockPanel("Product Name", 120, "ProductName");
      Grid.SetRow(ProductName, 3);
      Grid.SetColumn(ProductName, 0);
      Grid.SetColumnSpan(ProductName, 16);
      SelectedProfileGrid.Children.Add(ProductName);

      var ProductVersion = ComponentFactory.GetValueParameterDockPanel("Product Version", 120, "ProductVersion");
      Grid.SetRow(ProductVersion, 4);
      Grid.SetColumn(ProductVersion, 0);
      Grid.SetColumnSpan(ProductVersion, 16);
      SelectedProfileGrid.Children.Add(ProductVersion);

      var ClientId = ComponentFactory.GetValueParameterDockPanel("Client Id", 120, "ClientId");
      Grid.SetRow(ClientId, 5);
      Grid.SetColumn(ClientId, 0);
      Grid.SetColumnSpan(ClientId, 16);
      SelectedProfileGrid.Children.Add(ClientId);

      var Endpoint = ComponentFactory.GetValueParameterDockPanel("Endpoint", 120, "Endpoint");
      Grid.SetRow(Endpoint, 6);
      Grid.SetColumn(Endpoint, 0);
      Grid.SetColumnSpan(Endpoint, 16);
      SelectedProfileGrid.Children.Add(Endpoint);

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
        var test = ConfigVM.SelectedProfileName;
        ConfiController.ProfileSelectionChanged();
      });
      DockPanel.SetDock(IdTypeComoBox, Dock.Left);

      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(IdTypeComoBox);

      return Panel;
    }

    private DockPanel GetCertificateComboBoxDockPanel(string LabelName, int LabelWidth, string ValueBindingName)
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
      
      IdTypeComoBox.SetBinding(ComboBox.ItemsSourceProperty, "HpioCertList");
      IdTypeComoBox.DisplayMemberPath = "HpioFomratted";
      IdTypeComoBox.SetBinding(ComboBox.SelectedItemProperty, Binding);
      IdTypeComoBox.VerticalContentAlignment = VerticalAlignment.Center;
      IdTypeComoBox.Margin = new Thickness(3);
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
