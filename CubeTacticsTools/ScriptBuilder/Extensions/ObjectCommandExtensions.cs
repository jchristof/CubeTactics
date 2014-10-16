using Assets.Script;
using ScriptBuilder.ScriptCommands.Commands;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.Extensions {
    public static class ObjectCommandExtensions {
        public static ScriptBuilder.Command ClassOfEnumType(this ObjectCommand self) {
            switch (self) {
                case ObjectCommand.Activate:
                    return new ActivateCommand();
                case ObjectCommand.Create:
                    return new CreateCommand();
                case ObjectCommand.Destroy:
                    return new DestroyCommand();
                case ObjectCommand.Disable:
                    return new DisableCommand();
                case ObjectCommand.Enable:
                    return new EnableCommand();
                case ObjectCommand.LookAt:
                    return new LookAtCommand();
                case ObjectCommand.Reset:
                    throw new NotImplementedException();
                case ObjectCommand.Wait:
                    return new WaitCommand();
                case ObjectCommand.Sfx:
                    SfxCommand command = new SfxCommand();
                    command.Position = new Position();
                    return command;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
