using UnityEngine;
using System.Collections;
using Assets.Language;
using Assets;
using Assets.Movement.PlayerMovement;
using Assets.Movement;
using Assets.Camera;
using Assets.Script;

public class PlayerController : MonoBehaviour {

    readonly float speed = 10.0f;
    readonly float halfCubeSize = 0.5f;

    Transform rotator;
    Vector3 refPoint;
    Vector3 rotationAxis;

    AutomatedMove automatedMove;
    SwipeInputDirection _swipeInputDirection;

    bool rotating = false;
    float angle = 0.0f;
    bool _keyHeld;

    void Start() {

        rotator = (new GameObject("Rotator")).transform;
        _swipeInputDirection = new SwipeInputDirection();
        MoveToFinished();

        Camera.main.GetComponent<CameraScript>().Height = 10.0f;
        Camera.main.GetComponent<CameraScript>().Distance = 4.0f;
        Camera.main.GetComponent<CameraScript>().Target = GameObject.Find("CameraLookAt").transform;
    }

    public void SpawnAt(Vector3 position) {
        position.y += 0.5f;
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
        //cube center is a half tile above the tile level
        moveTo.y += 0.5f;
        automatedMove = new AutomatedMove { moveTo = moveTo };
    }

    void MoveToFinished(){
        PlayfieldScript playfieldScript = CompositionRoot.Playfield;
        playfieldScript.PlayerMoved(transform.position);
        
    }
}
