using ScriptBuilder.Controls;
using ScriptBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ScriptBuilder.PropertyGridEditors {
    public class PositionEditor : ITypeEditor {
        //public FrameworkElement ResolveEditor(PropertyItem propertyItem) {
        //    //return new PositionEditoryControl(new PositionEditorViewModel(((SFXEditor)propertyItem.Instance).Position));
        //    SfxEditableCommand sfxEditor = propertyItem.Instance as SfxEditableCommand;

        //    TextBox textBoxX = new TextBox();
        //    var textBoxXBinding = new Binding("X");
        //    textBoxXBinding.Source = sfxEditor.Position.X;
        //    textBoxX.SetBinding(TextBox.TextProperty, textBoxXBinding);


        //    TextBox textBoxY = new TextBox();
        //    var textBoxYBinding = new Binding("Y");
        //    textBoxYBinding.Source = sfxEditor.Position.Y;
        //    textBoxY.SetBinding(TextBox.TextProperty, textBoxYBinding);

        //    TextBox textBoxZ = new TextBox();
        //    var textBoxZBinding = new Binding("Z");
        //    textBoxZBinding.Source = sfxEditor.Position.Z;
        //    textBoxZ.SetBinding(TextBox.TextProperty, textBoxZBinding);

        //    StackPanel stackPanel = new StackPanel();
        //    stackPanel.Children.Add(textBoxX);
        //    stackPanel.Children.Add(textBoxY);
        //    stackPanel.Children.Add(textBoxZ);

        //    return stackPanel;
        //}
        public FrameworkElement ResolveEditor(PropertyItem propertyItem) {
            throw new NotImplementedException();
        }
    }

    //public class ActiveNameEditor : ITypeEditor {
    //    public FrameworkElement ResolveEditor(PropertyItem propertyItem) {
    //        ComboBox box = new ComboBox();

    //        ItemSet source = propertyItem.Instance as ItemSet;
    //        Debug.Assert(source != null);

    //        var itemSourcebinding = new Binding("ActiveObject.Names") {
    //            Source = propertyItem.Instance,
    //            ValidatesOnExceptions = true,
    //            ValidatesOnDataErrors = true,
    //            Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
    //        };

    //        var selBinding = new Binding("Value") {
    //            Source = propertyItem,
    //            ValidatesOnExceptions = true,
    //            ValidatesOnDataErrors = true,
    //            Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay
    //        };

    //        BindingOperations.SetBinding(box, ItemsControl.ItemsSourceProperty, itemSourcebinding);
    //        BindingOperations.SetBinding(box, Selector.SelectedValueProperty, selBinding);

    //        return box;
    //    }
    //}
}
