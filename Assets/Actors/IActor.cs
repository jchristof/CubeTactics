using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Actors {
    public interface IActor {
        void SetPosition(Vector3 position);
        Vector3 GetPosition();
        void SetFade(float fade);
    }
}
