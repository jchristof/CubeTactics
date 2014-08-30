using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game {
    public enum GameState {
        Load,
        Intro,
        Start,
        Running,
        Paused,
        Win,
        Lose,
        Finish
    }
}
