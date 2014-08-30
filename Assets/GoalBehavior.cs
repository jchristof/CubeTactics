using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animator a = GetComponent<Animator>();
        a.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
