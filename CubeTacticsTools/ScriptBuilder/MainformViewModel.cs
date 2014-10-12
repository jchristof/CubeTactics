﻿using Assets.Script;
using Assets.Script.Commands;
using Newtonsoft.Json;
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
            ScriptList = new Dictionary<string, IList<Command>>();
        }

        Dictionary<string, IList<Command>> _scriptList;
        public Dictionary<string, IList<Command>> ScriptList {
            get { return _scriptList; }
            set {
                _scriptList = value;
                Scripts.Clear();
                _scriptList.Keys.ToList().ForEach(
                    x => { Scripts.Add(x); }
                );
            }
        }

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

        int _selectedCommandIndex;
        public int SelectedCommandIndex {
            get { return _selectedCommandIndex; }
            set {
                if (_selectedCommandIndex == value)
                    return;
                _selectedCommandIndex = value;
                RaisePropertyChanged("SelectedCommandIndex");
                RaisePropertyChanged("SelectedCommandIsNotFirst");
                RaisePropertyChanged("SelectedCommandIsNotLast");
            }
        }

        public bool SelectedCommandIsNotFirst {
            get { return SelectedCommandIndex != 0; }
        }

        public bool SelectedCommandIsNotLast {
            get { return SelectedCommandIndex < Commands.Count - 1; }
        }

        public void MoveSelectedCommandUp(){
            Commands.Swap(Commands[SelectedCommandIndex], Commands[SelectedCommandIndex - 1]);
        }

        public void MoveSelectedCommandDown() {
            Commands.Swap(Commands[SelectedCommandIndex], Commands[SelectedCommandIndex + 1]);
        }

        string _selectedScriptName;
        public string SelectedScriptName {
            get { return _selectedScriptName; }
            set {
                if (_selectedScriptName == value)
                    return;

                //save the script that is about to be deselected
                if (!string.IsNullOrEmpty(_selectedScriptName)) {
                    StoreCurrentScriptData(_selectedScriptName);          
                }

                Commands.Clear();

                //see if the newly selected has already been created
                if (ScriptList.Keys.Where(x => x == value).FirstOrDefault() != null) {
                    var commands = ScriptList.Where(x => x.Key == value).First();

                    commands.Value.ToList().ForEach(x => {
                        Commands.Add(x);
                    });
                }
                else
                    //this is a brand new script
                    ScriptList.Add(value, new List<Command>());
                _selectedScriptName = value;
                RaisePropertyChanged("SelectedScriptName");
            }
        }

        public string Serialize() {
            StoreCurrentScriptData(SelectedScriptName);

            return JsonConvert.SerializeObject(ScriptList);
        }

        public void Deserialize(string json) {
            ScriptList = (Dictionary<string, IList<Command>>)JsonConvert.DeserializeObject(json, ScriptList.GetType());
        }

        public void StoreCurrentScriptData(string scriptName) {
            ScriptList.Remove(_selectedScriptName);
            List<Command> commandList = new List<Command>(Commands);
            ScriptList.Add(_selectedScriptName, commandList);       
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

        public void CreateNewCommandObject(int index = -1) {
            Command command = SelectedNewCommandType.ClassOfEnumType();
            command.ObjectCommand = SelectedNewCommandType;

            if (index == -1)
                Commands.Add(command);
            else
                Commands.Insert(index, command);
        }

        public string SelectedScript { get; set; }

        public void DeleteSelectedCommandObject() {
            Commands.Remove(SelectedCommand);
        }

        public bool CanInsertCommand {
            get { return SelectedCommandIndex > 0; }
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
