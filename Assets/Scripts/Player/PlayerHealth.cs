using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    int maxHealth = 100;
    public int currentHealth;
    int healthPerTick = 5;
    public float currentTime;

    public float timeTillPlayerCanRegen = 4;
    public float lastTimeTakenDamage;

    bool CheckWhen2Regen_isRunning = false;
    bool RegenHealth_isRunning = false;

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
        currentTime = Time.time;

    }

    //When RegenHealth is called, start incrementing health
    IEnumerator RegenHealth() {
        RegenHealth_isRunning = true;
        yield return new WaitForSeconds(1);
        print("Health regen started");
        while(currentHealth < maxHealth && Time.time - lastTimeTakenDamage > timeTillPlayerCanRegen) {  //Do not use timeSinceLastDamageTaken, instead use Time.time - lastTimeTakenDamage
            currentHealth += healthPerTick;
            yield return new WaitForSeconds(1);
        }
        RegenHealth_isRunning = false;
        print("Health regen stopped");
    }

    IEnumerator CheckWhen2Regen() {
        print("Starting CheckWhen2Regen");
        //keep checking timeCounter. If time counter > timetillplayer can regen. Call RegenHealth()
        CheckWhen2Regen_isRunning = true;
        while (true) {
            if(Time.time - lastTimeTakenDamage > timeTillPlayerCanRegen) {              //Do not use timeSinceLastDamageTaken, instead use Time.time - lastTimeTakenDamage
                if (RegenHealth_isRunning == false) StartCoroutine(RegenHealth());
                CheckWhen2Regen_isRunning = false;
                print("Starting Health regen");
                print("Stopping CheckWhen2Regen");
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
        if(CheckWhen2Regen_isRunning == false) StartCoroutine(CheckWhen2Regen());
       
    }
}
