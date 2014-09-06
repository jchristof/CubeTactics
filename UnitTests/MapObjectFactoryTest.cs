using Assets.Map;
using Assets.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests {
    [TestClass]
    public class MapObjectFactoryTest {
        [TestMethod]
        public void DeserializeScriptTest() {
            MapObjectFactory f = new MapObjectFactory();
            string script = @"{ 
            ""Commands"":[
            {
             ""ObjectCommand"":""Disable"",
             ""Layer"":""Board"",
             ""Object"":""Tile"",
             ""Position"": [9,6]
            }]
            }";

            CommandList scriptlist = f.DeserializeScript(script);
        }
    }
}
