using UnityEngine;
using System.Collections;

public class MouseLookY : MonoBehaviour {

    float pitch = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        pitch -= Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        pitch = Mathf.Clamp(pitch, -80, 80);
	}
}
