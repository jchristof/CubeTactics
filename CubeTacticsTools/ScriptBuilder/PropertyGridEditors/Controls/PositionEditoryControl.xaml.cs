using Assets.Script;
using Assets.Script.Positioning;
using ScriptBuilder.ViewModels;
using System;
using System.Collections.Generic;
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

namespace ScriptBuilder.Controls {
    /// <summary>
    /// Interaction logic for PositionEditoryControl.xaml
    /// </summary>
    public partial class PositionEditoryControl : UserControl {
        public PositionEditoryControl(PositionEditorViewModel viewModel) {
            DataContext = viewModel;
            InitializeComponent();

            
        }

        
    }
}
