using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    NavMeshAgent nav;
    GameObject player;
    bool isTriggered = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        //Set enemy to move towards the player        
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered == true)
        {   
            //enemy will now follow player
            nav.SetDestination(player.transform.position);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerIsInFOV() == true)
        {
            isTriggered = true;
        }
    }
    
    //this function will tell us if the player is is view of the enemy
    bool playerIsInFOV()
    {
        Vector3 playerLinePOS = player.transform.position - transform.position;
        if(Vector3.Angle(transform.forward,playerLinePOS) <= 45f)
        {
            //player is in FOV
            return true;
        }
        //player NOT in FOV
        return false;
    }
}
