using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    int maxHealth = 100;
    public int currentHealth;
    int healthPerTick = 5;

    public float timeTillPlayerCanRegen = 4;
    public float lastTimeTakenDamage;

    bool RegenHealthAfterDelay_isRunning = false;

    //for debugging
    public float timeSinceLastDamageTaken;
    
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            TakeDamage(10);
        }
        timeSinceLastDamageTaken = Time.time - lastTimeTakenDamage;

    }

    //When RegenHealth is called, start incrementing health
    IEnumerator RegenHealth() {
        print("Regenning Health");
        while(currentHealth < maxHealth && timeSinceLastDamageTaken > timeTillPlayerCanRegen) {
            if ((maxHealth - currentHealth) < healthPerTick) {
                currentHealth += (maxHealth - currentHealth);
            } else {
                currentHealth += healthPerTick;
            }
            yield return new WaitForSeconds(1);
        }
    }

    //IEnumerator RegenHealthAfterDelay(float delay) {
    //    RegenHealthAfterDelay_isRunning = true;
    //    yield return new WaitForSeconds(delay);
    //    if ((Time.time - lastTimeTakenDamage) > timeTillPlayerCanRegen) {
    //        RegenHealth();
    //    }
    //    RegenHealthAfterDelay_isRunning = false;
    //}

    IEnumerator CheckWhen2Regen(float delay) {
        print("Starting CheckWhen2Regen");
        //keep checking timeCounter. If time counter > timetillplayer can regen. Call RegenHealth()
        RegenHealthAfterDelay_isRunning = true;
        while (true) {
            if(timeSinceLastDamageTaken > timeTillPlayerCanRegen) {
                StartCoroutine(RegenHealth());
                RegenHealthAfterDelay_isRunning = false;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void TakeDamage(int dmg) {
        print("Player took" + dmg + "damage.");
        currentHealth -= dmg;
        lastTimeTakenDamage = Time.time;

        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
        if(RegenHealthAfterDelay_isRunning == false) StartCoroutine(CheckWhen2Regen(5));
       
    }
}
