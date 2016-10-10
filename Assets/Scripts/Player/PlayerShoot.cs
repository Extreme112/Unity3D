using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    float shootTimer;
    float shootDelay;
	// Use this for initialization
	void Start () {
        shootTimer = 1;
        shootDelay = shootTimer + Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        playerShoot();
	}

    void playerShoot()  //playerShoot 'function'
    {
        if (Input.GetButton("Fire1") && Time.time > shootDelay)
        {
            //Shooting
            Ray shootray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitObject;       //Store the object we hit (if we did hit something)
            if (Physics.Raycast(shootray, out hitObject))
            {
                if(hitObject.collider.tag == "enemy")   //Remember to 'tag' ememy in the scene
                {
                    hitObject.transform.SendMessage("displayHitMessage");
                    hitObject.transform.SendMessage("dealDamage", 10);
                }
            }
            shootDelay = Time.time + shootTimer;
        }
    }

   

    //void FixedUpdate()
    //{
    //    playerShoot();
    //}

    //void playerShoot()
    //{
    //    if(Input.GetButton("Fire1"))
    //    {
    //        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //        RaycastHit rayHit;
    //        Debug.DrawRay(this.transform.position,ray.direction,Color.red,2f,true);
    //        if (Physics.Raycast(ray, out rayHit))
    //        {
    //            // print("You hit " + rayHit.collider.tag);
    //            rayHit.transform.SendMessage("displayHitMessage");
    //        }

    //    }
        
    //}
}
