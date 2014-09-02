using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class Trigger {

        public Trigger(string name, string type, int id, int xpos, int ypos, int linkTo, bool visible, bool enabled){
            X = xpos;
            Y = ypos;
            Name = name;
            Type = type;
            Id = id;
            LinkTo = linkTo;
            Visible = visible;
            Enabled = enabled;
        }

        public virtual void OnTriggered() {

        }

        //public override string ToString() {
        //    return JsonConvert.SerializeObject(this);
        //}

        public int X { get; protected set; }
        public int Y { get; protected set; }
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public int Id { get; protected set; }
        public int LinkTo { get; protected set; }
        public bool Visible { get; protected set; }
        public bool Enabled { get; protected set; }
    }
}
