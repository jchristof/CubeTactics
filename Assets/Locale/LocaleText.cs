using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Locale {
    public class LocaleText {
        LocaleText(string textFile) {
            _text = JsonConvert.DeserializeObject<Dictionary<string, object>>(textFile);
        }

        static public void CreateText(string textFile) {
            Text = new LocaleText(textFile);
        }

        static public LocaleText Text { get; set; }

        Dictionary<string, object> _text;

        public string this[string key] {
            get {
                if(!_text.ContainsKey(key))
                    return "Missing Text";

                return (string)_text[key];
            }
        }
    }
}
