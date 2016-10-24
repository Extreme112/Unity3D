using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    //Reloading
    int maxClipSize = 12;
    public int currentClipSize = 12;
    public int reserveAmmo = 36;

    //Reload delay mechanics
    public bool isReloading = false;        //player can shoot while not reloading (isReloading = false)
    float reloadTime = 3;       //how long before player can shoot again?
    public float currentReloadTime = 0;
         
    //Shooting
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
        reload();
	}

    void reload()
    {

        if (isReloading == true)
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0)      //are we finished reloading? Yes? then...
            {
                isReloading = false;        //say we are not reloading anymore
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && (currentClipSize < maxClipSize) && (reserveAmmo > 0) && (isReloading == false))
        {
            isReloading = true;
            currentReloadTime = reloadTime;

            print("Reload initiated");
            if(reserveAmmo >= maxClipSize)  //if we have enough reserve ammo so we can reload
            {
                int numberOfBulletsToChamber = maxClipSize - currentClipSize;
                currentClipSize += numberOfBulletsToChamber;
                reserveAmmo -= numberOfBulletsToChamber;
            }
            else //what if we don't have enough reserve ammo (don't have enough to fill clip)
            {
                //take whatever is left in the reserve ammo, then add it to the current clip
                currentClipSize += reserveAmmo;
                reserveAmmo = 0;
            }
        }
    }
        //    //only start reload if: 1) Player presses 'R' 2) Our current clip is not full 3) We have some reserve ammo
        //    if ((Input.GetKeyDown(KeyCode.R)) && (currentClipSize < maxClipSize) && (reserveAmmo > 0) && (isReloading == false))
        //    {
        //        isReloading = true;
        //        currentReloadTime = reloadTime;

        //        print("Reload iniated");
        //        //start the reload
        //        if (reserveAmmo >= maxClipSize)
        //        {
        //            int numberOfBulletsToChamber = maxClipSize - currentClipSize;
        //            currentClipSize += numberOfBulletsToChamber;
        //            reserveAmmo -= numberOfBulletsToChamber;
        //        }
        //        else
        //        {
        //            currentClipSize += reserveAmmo;
        //            reserveAmmo = 0;
        //        }
    

    void playerShoot()  //playerShoot 'function'
    {
        if ((Input.GetButton("Fire1")) && (Time.time > shootDelay) && (currentClipSize > 0) && (isReloading == false))        //'&&' == AND
        {
            //Deal with ammo
            currentClipSize--;
            //Creating the ray for shooting
            Ray shootray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitObject;       //Store the object we hit (if we did hit something)
            if (Physics.Raycast(shootray, out hitObject))
            {
                if (hitObject.collider.tag == "enemy")   //Remember to 'tag' ememy in the scene
                {
                    hitObject.transform.SendMessage("displayHitMessage");
                    hitObject.transform.SendMessage("dealDamage", 10);
                }
            }
            //Reset shoot delay
            shootDelay = Time.time + shootTimer;
        }
    }
}

//if (Input.GetButton("Fire1") && Time.time > shootDelay)
//{
//    //Shooting
//    Ray shootray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//    RaycastHit hitObject;       //Store the object we hit (if we did hit something)
//    if (Physics.Raycast(shootray, out hitObject))
//    {
//        if(hitObject.collider.tag == "enemy")   //Remember to 'tag' ememy in the scene
//        {
//            hitObject.transform.SendMessage("displayHitMessage");
//            hitObject.transform.SendMessage("dealDamage", 10);
//        }
//    }
//    shootDelay = Time.time + shootTimer;
//}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//void reload()
//{
//    if (isReloading == true)
//    {
//        currentReloadTime -= Time.deltaTime;
//        if (currentReloadTime <= 0)      //are we finished reloading? Yes? then...
//        {
//            isReloading = false;        //say we are not reloading anymore
//        }
//    }

//    //only start reload if: 1) Player presses 'R' 2) Our current clip is not full 3) We have some reserve ammo
//    if ((Input.GetKeyDown(KeyCode.R)) && (currentClipSize < maxClipSize) && (reserveAmmo > 0) && (isReloading == false))
//    {
//        isReloading = true;
//        currentReloadTime = reloadTime;

//        print("Reload iniated");
//        //start the reload
//        if (reserveAmmo >= maxClipSize)
//        {
//            int numberOfBulletsToChamber = maxClipSize - currentClipSize;
//            currentClipSize += numberOfBulletsToChamber;
//            reserveAmmo -= numberOfBulletsToChamber;
//        }
//        else
//        {
//            currentClipSize += reserveAmmo;
//            reserveAmmo = 0;
//        }

//    }
//}