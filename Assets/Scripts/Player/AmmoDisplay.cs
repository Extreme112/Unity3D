using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoDisplay : MonoBehaviour {

    public GameObject playerCamera;
    PlayerShoot playerShootScript;
    Text ammoDisplay;


	// Use this for initialization
	void Start () {
        playerShootScript = playerCamera.GetComponent<PlayerShoot>();
        ammoDisplay = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        ammoDisplay.text = playerShootScript.currentClipSize.ToString() + "/" + playerShootScript.reserveAmmo.ToString();
	}
}
