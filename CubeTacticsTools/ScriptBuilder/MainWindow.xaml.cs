using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Diagnostics;
using Xceed.Wpf.Toolkit.Primitives;
using Assets.Script.Commands;
using Assets.Script;
using Newtonsoft.Json;
using System.IO;

namespace ScriptBuilder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow : Window {
    #region Constructor

    public MainWindow(){
        DataContext = new MainformViewModel();
        InitializeComponent();
        
    }

    #endregion

    #region View Model

    MainformViewModel ViewModel {
        get { return DataContext as MainformViewModel; }
    }

    #endregion

    #region New Script Popup

    void NewScriptPopupCancel_Click(object sender, RoutedEventArgs e) {
        NewScriptPopup.IsOpen = false;
    }

    void NewScriptPopupOk_Click(object sender, RoutedEventArgs e) {
        ViewModel.NewScript();
        NewScriptPopup.IsOpen = false;
    }

    #endregion

    #region Script List Context

    void NewScriptContext_Click(object sender, RoutedEventArgs e) {
        NewScriptPopup.IsOpen = true;
    }

    void DeleteScriptContext_Click(object sender, RoutedEventArgs e) {
        ViewModel.RemoveSelectedScript();
    }

    #endregion

    #region New Command Popup

    void NewCommandPopupOk_Click(object sender, RoutedEventArgs e) {
        int insertIndex = NewCommandPopup.Tag == null ? -1 : (int)NewCommandPopup.Tag;
        ViewModel.CreateNewCommandObject(insertIndex);
        NewCommandPopup.Tag = null; 
        NewCommandPopup.IsOpen = false;
    }

    void NewCommandPopupCancel_Click(object sender, RoutedEventArgs e) {
        NewCommandPopup.IsOpen = false;
    }

    #endregion

    #region Command Context Menu
    void NewCommandContext_Click(object sender, RoutedEventArgs e) {
        NewCommandPopup.IsOpen = true;
    }

    void DeleteCommandContext_Click(object sender, RoutedEventArgs e) {
        ViewModel.DeleteSelectedCommandObject();
    }

    void InsertCommandContext_Click(object sender, RoutedEventArgs e) {
        NewCommandPopup.IsOpen = true;
        NewCommandPopup.Tag = ViewModel.SelectedCommandIndex;
    }

    void MoveCommandUp_Click(object sender, RoutedEventArgs e) {
        ViewModel.MoveSelectedCommandUp();
    }

    void MoveCommandDown_Click(object sender, RoutedEventArgs e) {
        ViewModel.MoveSelectedCommandDown();
    }

    #endregion
    #region Menu Items

    readonly string _scriptExtension = ".json";
    readonly string _scriptFilter = "CT Script (.script)|*.json";
    void MenuOpen_Click(object sender, RoutedEventArgs e) {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        dlg.DefaultExt = _scriptExtension;
        dlg.Filter = _scriptFilter;

        Nullable<bool> result = dlg.ShowDialog();

        if (result == true) {
  
            ViewModel.Filename = dlg.FileName;
            StreamReader file = File.OpenText(ViewModel.Filename);

            string json = file.ReadToEnd();
            ViewModel.Deserialize(json);
        }
    }

    void MenuSave_Click(object sender, RoutedEventArgs e) {

        if (!string.IsNullOrEmpty(ViewModel.Filename)) {
            var json = ViewModel.Serialize();

            using (StreamWriter writer = new StreamWriter(ViewModel.Filename)) {
                writer.Write(json);
            }
        }
        else
            MenuSaveAs_Click(sender, e);
    }

    void MenuSaveAs_Click(object sender, RoutedEventArgs e) {

        var json = ViewModel.Serialize();

        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        dlg.FileName = ViewModel.Filename;
        dlg.DefaultExt = _scriptExtension;
        dlg.Filter = _scriptFilter;

        Nullable<bool> result = dlg.ShowDialog();

        if (result == true) {
            ViewModel.Filename = dlg.FileName;

            using (StreamWriter writer = new StreamWriter(ViewModel.Filename)) {
                writer.Write(json);
            }
        }
    }

    void MenuExit_Click(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
    }

    #endregion

    void PropertyGridChanged_Event(object sender, PropertyChangedEventArgs e) {
        if (ViewModel == null || ViewModel.Commands == null)
            return;

        ICollectionView view = CollectionViewSource.GetDefaultView(ViewModel.Commands);
        view.Refresh();
    }
}



   public class ItemSet 
   {
      [ItemsSource(typeof(ObjectWithNamesItemsSource))]
      public ObjectWithNames ActiveObject { get; set; }

      [Editor(typeof(ActiveNameEditor), typeof(ActiveNameEditor))]
      public string ActiveName { get; set; }
   }

   public class ObjectWithNames 
   {
      public ObservableCollection<string> Names { get; set; }
   }

   public class ObjectWithNamesItemsSource : IItemsSource
   {
      public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
      {
          Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection values = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();

         ObjectWithNames objA = new ObjectWithNames { Names = new ObservableCollection<string> { "A1", "A2", "A3", "A4" } };
         ObjectWithNames objB = new ObjectWithNames { Names = new ObservableCollection<string> { "B1", "B2" } };
         ObjectWithNames objC = new ObjectWithNames { Names = new ObservableCollection<string> { "C1", "C2", "C3" } };

         values.Add(objA, "Object A");
         values.Add(objB, "Object B");
         values.Add(objC, "Object C");

         return values;
      }
   }

   public class ActiveNameEditor : ITypeEditor
   {
      public FrameworkElement ResolveEditor(PropertyItem propertyItem)
      {
         ComboBox box = new ComboBox();

         ItemSet source = propertyItem.Instance as ItemSet;
         Debug.Assert(source != null);

         var itemSourcebinding = new Binding("ActiveObject.Names")
         {
            Source = propertyItem.Instance,
            ValidatesOnExceptions = true,
            ValidatesOnDataErrors = true,
            Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
         };

         var selBinding = new Binding("Value")
         {
            Source = propertyItem,
            ValidatesOnExceptions = true,
            ValidatesOnDataErrors = true,
            Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
         };

         BindingOperations.SetBinding(box, ItemsControl.ItemsSourceProperty, itemSourcebinding);
         BindingOperations.SetBinding(box, Selector.SelectedValueProperty, selBinding);

         return box;
      }
   }
}
