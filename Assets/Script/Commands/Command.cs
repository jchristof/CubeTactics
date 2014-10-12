﻿using Assets.Map;
using Assets.Script.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Script {
    [JsonConverter(typeof(CommandConverter))]
    public class Command : INotifyPropertyChanged {
        public string ObjectName { get; set; }
        [Browsable(false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectCommand ObjectCommand { get; set; }
        public ObjectType ObjectType { get; set; }
        //public object SecondaryInfo {
        //    get { return ObjectName;

        public virtual void Execute() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
