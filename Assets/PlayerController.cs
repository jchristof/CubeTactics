using UnityEngine;
using System.Collections;
using Assets.Language;
using Assets;

public class PlayerController : MonoBehaviour {

    readonly float speed = 10.0f;
    readonly float halfCubeSize = 0.5f;

    Transform rotator;
    Vector3 refPoint;
    Vector3 rotationAxis;
    Vector3 direction;

    bool rotating = false;
    float angle = 0.0f;
    bool _keyHeld;

    void Start() {
        rotator = (new GameObject("Rotator")).transform;
        MoveToFinished();
    }

    public void SpawnAt(Vector3 position) {
        transform.position = position;
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

    #region Input
    bool _inputEnabled;

    public bool InputEnabled {
        get { return _inputEnabled; }
        set { _inputEnabled = value; }
    }

    bool InputKeyLeft() { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); }
    bool InputKeyUp() { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); }
    bool InputKeyRight() { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); }
    bool InputKeyDown() { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); }

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
                print(string.Format("Request next position: {0}", requestPosition));
                if (!CompositionRoot.Playfield.RequestPlayerMoveTo(requestPosition)) {
                    return;
                }

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

    void MoveToFinished(){
        PlayfieldScript playfieldScript = CompositionRoot.Playfield;

        print(string.Format("New Position: {0}", transform.position));
        playfieldScript.PlayerMoved(transform.position);
        
    }
}
