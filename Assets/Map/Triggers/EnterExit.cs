using Assets.Script;
using Assets.Script.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Assets.Map.Triggers {
    public class EnterExit : Trigger {

        public EnterExit(IMap map, IScriptExecutor scriptExecutor) :
            base(map, scriptExecutor) {

            

            //_commandList = new CommandList();

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Disable,
            //    ObjectName = "Player",
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.LookAt,
            //    ObjectName = "removetile"
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Wait,
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Destroy,
            //    ObjectName = "removetile",
            //});

            //_commandList.Commands.Add(new Command{
            //    ObjectCommand  = ObjectCommand.Create,
            //    ObjectName = "addtile",
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.LookAt,
            //    ObjectName = "goal"
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Wait,
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Activate,
            //    ObjectName = "goal"
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Wait,
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.LookAt,
            //    ObjectName = "Player"
            //});

            //_commandList.Commands.Add(new Command {
            //    ObjectCommand = ObjectCommand.Enable,
            //    ObjectName = "Player",
            //});
        }

        public override void OnEnter() {
            base.OnEnter();

            if(!string.IsNullOrEmpty(Properties.OnEnter)){
                if(Map.ScriptList.Keys.Contains(Properties.OnEnter)){
                    IList<Command> commandList = Map.ScriptList[Properties.OnEnter];
                    ExecuteScript(commandList);
                }
            }
                
                
        }
    }
}
