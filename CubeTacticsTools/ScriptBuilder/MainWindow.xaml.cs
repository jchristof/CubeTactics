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

namespace ScriptBuilder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow : Window {
    public MainWindow(){
        InitializeComponent();
        DataContext = new MainformViewModel();
    }

MainformViewModel ViewModel {
    get {
        return DataContext as MainformViewModel;
        }
    }

    void NewScriptCancel(object sender, RoutedEventArgs e) {
        NewScriptPopup.IsOpen = false;
    }

    void NewScriptOk(object sender, RoutedEventArgs e) {
        ViewModel.NewScript();
        NewScriptPopup.IsOpen = false;
    }

    void NewScript_Click(object sender, RoutedEventArgs e) {
        NewScriptPopup.IsOpen = true;
    }

    void DeleteScript_Click(object sender, RoutedEventArgs e) {
        ViewModel.RemoveSelectedScript();
    }

    private void NewCommand_Click(object sender, RoutedEventArgs e) {
        NewCommandPopup.IsOpen = true;
    }

    private void NewCommand_Ok(object sender, RoutedEventArgs e) {
        ViewModel.CreateNewCommandObject();
        NewCommandPopup.IsOpen = false;
    }

    void NewCommand_Cancel(object sender, RoutedEventArgs e) {
        NewCommandPopup.IsOpen = false;
    }

    void DeleteCommand_Click(object sender, RoutedEventArgs e) {
        ViewModel.DeleteSelectedCommandObject();
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
