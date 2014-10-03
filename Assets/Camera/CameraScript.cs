using Assets.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Camera {
    public class CameraScript : MonoBehaviour {
        Transform _easingCamTarget;
        Assets.Movement.Interpolate.Function _ease;

        float _heightDamping = 2.0f;
        float _elapsedTime = 0.0f;

        Transform _target;
        public Transform Target {
            get { return _target; }
            set {
                _target = value;           
            }
        }

        public float Height { get; set; }
        public float Distance { get; set; }
        void Awake() {
            _ease = Interpolate.Ease(Interpolate.EaseType.Linear);
            _easingCamTarget = GameObject.Find("EasingCameraTarget").transform;
        }

        void LateUpdate() {
            if (Target == null)
                return;

            if (Vector3.Distance(Target.position, _easingCamTarget.position) > .01f) {
                _easingCamTarget.position = Interpolate.Ease(_ease, _easingCamTarget.position, Target.position - _easingCamTarget.position, _elapsedTime, 50.0f);
                _elapsedTime += Time.deltaTime;
            }
            else {
                _elapsedTime = 0.0f;
                _easingCamTarget.position = Target.position;
            }
            // Calculate the current rotation angles
            //float wantedRotationAngle = cameraTarget.eulerAngles.y;
            float wantedHeight = /*cameraTarget.position.y +*/ Height;

            float currentRotationAngle = UnityEngine.Camera.main.transform.eulerAngles.y;
            float currentHeight = UnityEngine.Camera.main.transform.position.y;

            // Damp the rotation around the y-axis
            //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            Vector3 desiredPosition = _easingCamTarget.position - currentRotation * Vector3.forward * Distance;
            desiredPosition.y = currentHeight;

            UnityEngine.Camera.main.transform.position = desiredPosition;
            UnityEngine.Camera.main.transform.LookAt(_easingCamTarget.transform);
        }
    }
}
