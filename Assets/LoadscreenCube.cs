using UnityEngine;
using System.Collections;
using Assets.Movement;
using System.Collections.Generic;

public class LoadscreenCube : MonoBehaviour {

    Assets.Movement.Interpolate.Function _ease;
    IEnumerator _sequence;
	// Use this for initialization
	void Start () {
        _ease = Interpolate.Ease(Interpolate.EaseType.EaseInOutSine);
	}

    Vector3 _rotation;
	// Update is called once per frame
	void Update () {
        if (_sequence != null && _sequence.MoveNext()) {
            transform.rotation = Quaternion.Euler((Vector3)_sequence.Current);
        }
        else {
            _rotation = new Vector3(Random.value * 180, Random.value * 180, Random.value * 180);
            _sequence = Interpolate.NewEase(_ease, transform.rotation.eulerAngles, _rotation, 5.0f);
        }
	}
}
