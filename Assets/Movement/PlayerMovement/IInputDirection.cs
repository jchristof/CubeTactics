using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Movement.PlayerMovement {
    public interface IInputDirection {
        void Update();
        bool InputKeyLeft();
        bool InputKeyUp();
        bool InputKeyRight();
        bool InputKeyDown();
    }
}
