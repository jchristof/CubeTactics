using UnityEngine;
using System.Collections;
using Assets.Language;
using Assets;
using Assets.Movement.PlayerMovement;
using Assets.Movement;
using Assets.Camera;

public class PlayerController : MonoBehaviour {

    readonly float speed = 10.0f;
    readonly float halfCubeSize = 0.5f;

    Transform rotator;
    Vector3 refPoint;
    Vector3 rotationAxis;

    AutomatedMove automatedMove;
    SwipeInputDirection _swipeInputDirection;
    //Transform easingCamTarget;
    //Assets.Movement.Interpolate.Function ease;


    bool rotating = false;
    float angle = 0.0f;
    bool _keyHeld;

    void Start() {
        rotator = (new GameObject("Rotator")).transform;
        _swipeInputDirection = new SwipeInputDirection();
        MoveToFinished();
        //ease = Interpolate.Ease(Interpolate.EaseType.Linear);
        //Transform camTarget = GameObject.Find("CameraLookAt").transform;
        //easingCamTarget = Instantiate(camTarget, camTarget.position, camTarget.rotation) as Transform;
        Camera.main.GetComponent<CameraScript>().Height = 10.0f;
        Camera.main.GetComponent<CameraScript>().Distance = 4.0f;
        Camera.main.GetComponent<CameraScript>().Target = GameObject.Find("CameraLookAt").transform;
    }

    public void SpawnAt(Vector3 position) {
        transform.position = position;
        MoveToFinished();
    }

    #region Input
    bool _inputEnabled;

    public bool InputEnabled {
        get { return _inputEnabled; }
        set { _inputEnabled = value; }
    }

    bool InputKeyLeft() { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || _swipeInputDirection.InputKeyLeft(); }
    bool InputKeyUp() { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || _swipeInputDirection.InputKeyUp(); }
    bool InputKeyRight() { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || _swipeInputDirection.InputKeyRight(); }
    bool InputKeyDown() { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || _swipeInputDirection.InputKeyDown(); }

    bool AnyMoveKeyDown() {
        return
            InputKeyLeft() ||
            InputKeyUp() ||
            InputKeyRight() ||
            InputKeyDown();
    }

    Tuple<Vector3, Vector3> InputDirection() {
        if (InputKeyLeft())
            return Tuple<Vector3, Vector3>.Create(-Vector3.right, Vector3.forward);
        if (InputKeyRight())
            return Tuple<Vector3, Vector3>.Create(Vector3.right, -Vector3.forward);
        if (InputKeyUp())
            return Tuple<Vector3, Vector3>.Create(Vector3.forward, Vector3.right);
        if (InputKeyDown())
            return Tuple<Vector3, Vector3>.Create(-Vector3.forward, -Vector3.right);

        return Tuple<Vector3, Vector3>.Create(Vector3.zero, Vector3.zero);
    }

    #endregion
    void Update() {
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
                Camera.main.GetComponent<CameraScript>().Height = Mathf.Max(Camera.main.GetComponent<CameraScript>().Height + .1f, 10);
            else if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
                Camera.main.GetComponent<CameraScript>().Height = Mathf.Min(Camera.main.GetComponent<CameraScript>().Height - .1f, 5);

        _swipeInputDirection.Update();

        if (automatedMove != null) {
            transform.position = automatedMove.moveTo;
            automatedMove = null;
            return;
        }


        if (!rotating) {
            if (!InputEnabled)
                return;

            if (!AnyMoveKeyDown()) {
                _keyHeld = false;
                return;
            }

            if (_keyHeld)
                return;

            _keyHeld = true;

            var direction = InputDirection();


            if (direction.Item1 != Vector3.zero) {
                Vector3 requestPosition = transform.position + direction.Item1;
                if (!CompositionRoot.Playfield.RequestPlayerMoveTo(requestPosition))
                    return;

                rotating = true;
                refPoint = direction.Item1 * halfCubeSize;
                rotationAxis = direction.Item2;
                
                rotator.localRotation = Quaternion.identity;
                rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
                transform.parent = rotator;
            }
         
            return;
        }
        
        if (angle < 90.0f) {
            angle += Time.deltaTime * 90.0f * speed;
            rotator.rotation = Quaternion.AngleAxis(Mathf.Min(angle, 90.0f), rotationAxis);
            return;

        }
        transform.parent = null;
        rotating = false;
        angle = 0;
        MoveToFinished();
    }

    //float distance = 4.0f;
    //float height = 10.5f;
    //float heightDamping = 2.0f;
    //float elapsedTime = 0.0f;
    //void LateUpdate() {
    //    if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
    //        height = Mathf.Max(height + .1f, 10);
    //    else if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
    //        height = Mathf.Min(height - .1f, 5);

    //    Vector3 cameraTarget = GameObject.Find("Player").transform.position;

    //    if (Vector3.Distance(cameraTarget, easingCamTarget.position) > .01f) {
    //        easingCamTarget.position = Interpolate.Ease(ease, easingCamTarget.position, cameraTarget - easingCamTarget.position, elapsedTime, 50.0f);
    //        elapsedTime += Time.deltaTime;
    //    }
    //    else {
    //        elapsedTime = 0.0f;
    //        easingCamTarget.position = cameraTarget;
    //    }
    //    // Calculate the current rotation angles
    //    //float wantedRotationAngle = cameraTarget.eulerAngles.y;
    //    float wantedHeight = /*cameraTarget.position.y +*/ height;

    //    float currentRotationAngle = Camera.main.transform.eulerAngles.y;
    //    float currentHeight = Camera.main.transform.position.y;

    //    // Damp the rotation around the y-axis
    //    //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    //    // Damp the height
    //    currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

    //    // Convert the angle into a rotation
    //    Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

    //    // Set the position of the camera on the x-z plane to:
    //    // distance meters behind the target
    //    Vector3 desiredPosition = easingCamTarget.position - currentRotation * Vector3.forward * distance;
    //    desiredPosition.y = currentHeight;

    //    Camera.main.transform.position = desiredPosition;
    //    Camera.main.transform.LookAt(easingCamTarget.transform);
    //}

    void RotateCube(Vector3 refPoint, Vector3 rotationAxis) {
        rotator.localRotation = Quaternion.identity;
        rotator.position = transform.position - Vector3.up * halfCubeSize + refPoint;
        transform.parent = rotator;

        if (angle < 90.0f) {
            angle += Time.deltaTime * 90.0f * speed;
            rotator.rotation = Quaternion.AngleAxis(Mathf.Min(angle, 90.0f), rotationAxis);
            return;
        }

        transform.parent = null;
        rotating = false;
        angle = 0;
    }

    public void AutoMatedMoveTo(Vector3 moveTo) {
        automatedMove = new AutomatedMove { moveTo = moveTo };
    }

    void MoveToFinished(){
        PlayfieldScript playfieldScript = CompositionRoot.Playfield;
        playfieldScript.PlayerMoved(transform.position);
        
    }
}
