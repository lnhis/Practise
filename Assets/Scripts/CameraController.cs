using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = Target.position;
        pos.z = -10;
        this.transform.position = pos;
	}
}
