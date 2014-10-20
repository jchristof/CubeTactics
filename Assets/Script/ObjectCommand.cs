using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public enum ObjectCommand {
        Destroy,
        Disable,
        Enable,
        Create,
        Activate,
        Reset,
        LookAt,
        CameraZoom,
        Wait,
        Sfx,
        Fade,
        MoveObject,      
        PlayerMove,
        PlayerInputState,
        LevelExit,    
        LevelCompletionShow,
    }
}
