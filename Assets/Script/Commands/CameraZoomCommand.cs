using UnityEngine;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Camera;

namespace Assets.Script.Commands {
    public class CameraZoomCommand : Command {
        public int Height { get; set; }
        public int Milliseconds { get; set; }

        readonly float speed = 0.2f;
        public override void Execute(){
            float startHeight = 0.0f;
            bool runZoom = false;

            CompositionRoot.RunOnMainThread(() => {
                startHeight = UnityEngine.Camera.main.GetComponent<CameraScript>().Height;
                runZoom = true;
            });

            while (!runZoom)
                Thread.Sleep(10);   

            int direction = (startHeight < Height) ? 1 : -1;

            bool finished = false;
            while (!finished) {
                CompositionRoot.RunOnMainThread(() => {
                    float currentHeight = UnityEngine.Camera.main.GetComponent<CameraScript>().Height;

                    if (currentHeight.ApproximatelyEquals(Height, speed)) {
                        finished = true;
                        return;
                    }

                    UnityEngine.Camera.main.GetComponent<CameraScript>().Height += (direction * speed);

                });

                Thread.Sleep(10); 
            }
        }
    }
   
}
