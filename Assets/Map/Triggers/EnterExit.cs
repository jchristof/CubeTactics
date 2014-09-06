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
            _commandList.Commands.Add(new Command{
                ObjectCommand = ObjectCommand.Destroy,
                Tile = {6,9}
                }
             );

            _commandList.Commands.Add(new Command{
                ObjectCommand  = ObjectCommand.Create,
                Tile = {6,9},
                TileIndex = 11
            });
        }

        public override void OnEnter() {
            base.OnEnter();

            ExecuteScript(_commandList);
        }
    }
}
