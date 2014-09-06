using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.Script;
using System.Collections.Generic;

namespace UnitTests {
    [TestClass]
    public class CommandTest {
        [TestMethod]
        public void Construct1() {
            CommandList c = new CommandList();
            c.Commands.Add(new Command());
        }

        [TestMethod]
        public void Construct2() {
            CommandList commandList = new CommandList();
            Command c = new Command {
                ObjectCommand = ObjectCommand.Destroy,
                Tile = new List<int>(new int[]{ 6, 9 })
            };
            commandList.Commands.Add(c);
        }
    }
}
