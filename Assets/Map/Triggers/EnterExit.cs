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
                Tile = new List<int>(new int[] { 6, 8 })
                }
             );

            _commandList.Commands.Add(new Command{
                ObjectCommand  = ObjectCommand.Create,
                Tile = new List<int>(new int[]{ 6, 8 }),
                TileIndex = 12
            });
        }

        public override void OnEnter() {
            base.OnEnter();

            ExecuteScript(_commandList);
        }
    }
}
