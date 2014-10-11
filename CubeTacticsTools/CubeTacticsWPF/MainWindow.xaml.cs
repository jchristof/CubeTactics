using Assets.Game;
using Assets.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CubeTacticsWPf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".json"; // Default file extension
            dlg.Filter = "Text documents (.json)|*.json"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 

            if (result == true) {
                // Open document 
                string filename = dlg.FileName;
                MapModel map = JsonConvert.DeserializeObject<MapModel>(File.ReadAllText(filename));

                var dynObj = JObject.Parse((map.tilesets[0].tileproperties.ToString()));
                
                List<Tile> tilelist = new List<Tile>();
                foreach (var v in dynObj) {
                    Tile t = JsonConvert.DeserializeObject<Tile>(v.Value.ToString());
                    t.index = Convert.ToInt32(v.Key);
                    tilelist.Add(t);
                }
            }

            Type objType = typeof(Assets.Game.Conditions.DemoLevelConditions);

            Type elementType = Type.GetType("Assets.Game.Conditions.DemoLevelConditions, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            
            // Print the full assembly name.
            Console.WriteLine("Full assembly name:\n   {0}.",
                               objType.Assembly.FullName.ToString());

            // Print the qualified assembly name.
            Console.WriteLine("Qualified assembly name:\n   {0}.",
                               objType.AssemblyQualifiedName.ToString());

            LevelConditions _levelConditions = (LevelConditions)Activator.CreateInstance(objType);
        }
    }
}
