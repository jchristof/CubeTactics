using Assets.Script;
using Assets.Script.Commands;
using ScriptBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScriptBuilder {
    public class MainformViewModel : INotifyPropertyChanged  {
        public MainformViewModel() {
            Scripts = new ObservableCollection<string>();          
            Commands = new ObservableCollection<Command>();
            _scriptList = new Dictionary<string, IList<Command>>();
        }

        Dictionary<string, IList<Command>> _scriptList;

        public ObservableCollection<string> Scripts { get; set; }
        public ObservableCollection<Command> Commands { get; set; }

        Command _selectedCommand;
        public Command SelectedCommand {
            get { return _selectedCommand; }
            set {
                if (_selectedCommand == value)
                    return;

                _selectedCommand = value;
                RaisePropertyChanged("SelectedCommand");
            }
        }

        string _selectedScriptName;
        public string SelectedScriptName {
            get { return _selectedScriptName; }
            set {
                if (_selectedScriptName == value)
                    return;

                //save the script that is about to be deselected
                if (!string.IsNullOrEmpty(_selectedScriptName)) {
                    _scriptList.Remove(_selectedScriptName);
                    List<Command> commandList = new List<Command>(Commands);
                    _scriptList.Add(_selectedScriptName, commandList);
                    
                }

                Commands.Clear();

                //see if the newly selected has already been created
                if (_scriptList.Keys.Where(x => x == value).FirstOrDefault() != null) {
                    var commands = _scriptList.Where(x => x.Key == value).First();

                    commands.Value.ToList().ForEach(x => {
                        Commands.Add(x);
                    });
                }

                _selectedScriptName = value;
                RaisePropertyChanged("SelectedScriptName");
            }
        }

        public void RemoveSelectedScript() {
            Scripts.Remove(SelectedScriptName);
        }

        string _newScriptName;
        public string NewScriptName {
            get { return _newScriptName; }
            set {
                if (_newScriptName != value)
                    _newScriptName = value;
                RaisePropertyChanged("NewScriptName");
            }
        }

        public void NewScript() {
            if(!string.IsNullOrEmpty(_newScriptName))
                Scripts.Add(_newScriptName);
        }

        ObjectCommand _selectedNewCommandType;
        public ObjectCommand SelectedNewCommandType {
            get { return _selectedNewCommandType; }
            set {
                _selectedNewCommandType = value;
                RaisePropertyChanged("SelectedNewCommandType");
            }
        }

        public void CreateNewCommandObject() {
            Command command = SelectedNewCommandType.ClassOfEnumType();
            command.ObjectCommand = SelectedNewCommandType;
            Commands.Add(command);
        }

        public string SelectedScript { get; set; }

        public void DeleteSelectedCommandObject() {
            Commands.Remove(SelectedCommand);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
    }
}
