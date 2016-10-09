using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        playerShoot();
    }

    void playerShoot()
    {
        if(Input.GetButton("Fire1"))
        {
            Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit rayHit;
            Debug.DrawRay(this.transform.position,ray.direction,Color.red,2f,true);
            if (Physics.Raycast(ray, out rayHit))
            {
                // print("You hit " + rayHit.collider.tag);
                rayHit.transform.SendMessage("displayHitMessage");
            }
            //else
            //{
            //    print("You hit nothing. You suck.");
            //}
        }
        
    }
}
