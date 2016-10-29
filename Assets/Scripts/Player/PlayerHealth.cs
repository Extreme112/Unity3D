using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    int maxHealth = 100;
    int currentHealth;
    int healthPerTick = 5;

    public float timeTillPlayerCanRegen = 4;
    float lastTimeTakenDamage;

    bool RegenHealthAfterDelay_isRunning = false;

    //for debugging
    public float timeCounter;
    
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        timeCounter = Time.time - lastTimeTakenDamage;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            TakeDamage(10);
        }
	}

    //When RegenHealth is called, start incrementing health
    IEnumerator RegenHealth() {
        while(currentHealth < maxHealth && (Time.time - lastTimeTakenDamage) > timeTillPlayerCanRegen) {
            if ((maxHealth - currentHealth) < healthPerTick) {
                currentHealth += (maxHealth - currentHealth);
            } else {
                currentHealth += healthPerTick;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator RegenHealthAfterDelay(float delay) {
        RegenHealthAfterDelay_isRunning = true;
        yield return new WaitForSeconds(delay);
        if ((Time.time - lastTimeTakenDamage) > timeTillPlayerCanRegen) {
            RegenHealth();
        }
        RegenHealthAfterDelay_isRunning = false;
    }

    public void TakeDamage(int dmg) {
        print("Player took" + dmg + "damage.");
        currentHealth -= dmg;
        lastTimeTakenDamage = Time.time;

        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
        RegenHealthAfterDelay(5);
       
    }
}
