﻿using Assets.Script;
using ScriptBuilder.ScriptCommands;
using ScriptBuilder.ScriptCommands.Commands;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.Extensions {
    public static class ObjectCommandExtensions {
        public static Command ClassOfEnumType(this ObjectCommand self) {
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
                    return new SfxCommand();
                case ObjectCommand.MoveObject:
                    return new MoveObjectCommand();
                case ObjectCommand.Fade:
                    return new FadeCommand();
                case ObjectCommand.PlayerMove:
                    return new PlayerMoveCommand();
                case ObjectCommand.PlayerInputState:
                    return new PlayerInputCommand();
                case ObjectCommand.LevelExit:
                    return new LevelExitCommand();
                case ObjectCommand.CameraZoom:
                    return new CameraZoomCommand();
                case ObjectCommand.LevelCompletionShow:
                    return new LevelCompletionShowCommand();
                case ObjectCommand.EvaluateExpression:
                    return new EvaluateExpressionCommand();
                case ObjectCommand.VarAssignment:
                    return new VarAssignmentCommand();
                case ObjectCommand.VarDecalare:
                    return new VarDeclareCommand();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
