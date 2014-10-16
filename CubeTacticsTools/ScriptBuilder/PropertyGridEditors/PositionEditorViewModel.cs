using Assets.Script;
using Assets.Script.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ViewModels {
    public class PositionEditorViewModel : INotifyPropertyChanged {
        public PositionEditorViewModel(Position position) {
            _position = position;
        }

        Position _position;

        public float X {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public float Z {
            get { return _position.Z; }
            set { _position.Z = value; }
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
