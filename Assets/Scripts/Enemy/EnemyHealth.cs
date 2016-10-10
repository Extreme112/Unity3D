using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    int enemyHealth;
	// Use this for initialization
	void Start () {
        enemyHealth = 100;
	}
	// Update is called once per frame
	void Update () {
	
	}
    public void displayHitMessage()
    {
        print("Thats hurts!" + gameObject);
    }
    public void dealDamage(int damageToDeal)
    {
        enemyHealth -= damageToDeal;
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
