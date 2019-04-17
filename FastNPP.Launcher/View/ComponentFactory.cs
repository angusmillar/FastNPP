using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FastNPP.Launcher.View
{
  public static class ComponentFactory
  {
    public static DockPanel GetValueParameterDockPanel(string LabelName, int LabelWidth, string ValueBindingName, bool IsReadOnly = false)
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
      if (IsReadOnly)
        Binding.Mode = BindingMode.OneWay;

      TextBox ValueTextBox = new TextBox();
      ValueTextBox.SetBinding(TextBox.TextProperty, Binding);
      ValueTextBox.VerticalContentAlignment = VerticalAlignment.Center;
      ValueTextBox.Margin = new Thickness(3);
      ValueTextBox.IsReadOnly = IsReadOnly;
      DockPanel.SetDock(ValueTextBox, Dock.Left);

      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(ValueTextBox);
      return Panel;
    }

    public static DockPanel GetValueComboBoxEnumDockPanel(string LabelName, int LabelWidth, string ValueBindingName, List<string> ItemsSource)
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
      IdTypeComoBox.ItemsSource = ItemsSource;
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

    public static Button GetButton(object Content)
    {
      Button Button = new Button();
      Button.Content = Content;
      Button.Width = 60;
      Button.Margin = new Thickness(3);
      Button.Padding = new Thickness(3);
      return Button;
    }

    public static Grid GetGrid(int RowCount, int ColCount)
    {
      var Grid = new Grid();
      Grid.SetGrid(RowCount, ColCount);
      return Grid;
    }

    public static void SetGrid(this Grid Grid, int RowCount, int ColCount)
    {
      for (int i = 0; i < RowCount; i++)
      {
        var Row = new RowDefinition();
        Row.Height = new GridLength(30, GridUnitType.Auto);
        Grid.RowDefinitions.Add(Row);
      }

      for (int i = 0; i < ColCount; i++)
      {
        var Col = new ColumnDefinition();
        Grid.ColumnDefinitions.Add(Col);
      }
    }

    public static DockPanel GetValueDatePickerDockPanel(string LabelName, int LabelWidth, string ValueBindingName)
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

      DatePicker DatePicker = new DatePicker();
      DatePicker.VerticalContentAlignment = VerticalAlignment.Center;
      DatePicker.Margin = new Thickness(3);
      DatePicker.SetBinding(DatePicker.SelectedDateProperty, Binding);

      DockPanel.SetDock(DatePicker, Dock.Left);

      DockPanel Panel = new DockPanel();
      Panel.LastChildFill = true;
      Panel.HorizontalAlignment = HorizontalAlignment.Stretch;
      Panel.Children.Add(ValueLabel);
      Panel.Children.Add(DatePicker);

      return Panel;
    }
  }
}
