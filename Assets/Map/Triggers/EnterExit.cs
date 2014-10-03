using Assets.Script;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Assets.Map.Triggers {
    public class EnterExit : Trigger {
        CommandList _commandList;

        public EnterExit(IMap map, MapObject mapObject, IScriptExecutor scriptExecutor, ReadOnlyCollection<Trigger> triggers) :
            base(mapObject, map, scriptExecutor) {

            _commandList = new CommandList();

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.Disable,
                ObjectName = "Player",
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.LookAt,
                ObjectName = "removetile"
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.Wait,
                Params = "{ time = 2.0 }"
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.Destroy,
                ObjectName = "removetile",
            });

            _commandList.Commands.Add(new Command{
                ObjectCommand  = ObjectCommand.Create,
                ObjectName = "addtile",
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.LookAt,
                ObjectName = "goal"
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.Wait,
                Params = "{ time = 2.0 }"
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.LookAt,
                ObjectName = "Player"
            });

            _commandList.Commands.Add(new Command {
                ObjectCommand = ObjectCommand.Enable,
                ObjectName = "Player",
            });
        }

        public override void OnEnter() {
            base.OnEnter();

            ExecuteScript(_commandList);
        }
    }
}
