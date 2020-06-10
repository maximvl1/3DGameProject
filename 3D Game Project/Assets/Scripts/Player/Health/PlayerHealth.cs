using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] Transform player;
    [SerializeField] Transform respawnPoint;
    public int maxHealth = 100;
    public int curHealth = 100;
    public float checkRate = 0.01f;
    float nextCheck;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (curHealth ==0){
                respawn();
                curHealth = maxHealth;
            }
            if (GetComponent<PlayerMovement>().triggerDeath)
            {
            GetComponent<PlayerMovement>().fallDamageTriggered = false;
            GetComponent<PlayerMovement>().triggerDeath = false;
            respawn();
            }
        }
        
    }
    public void AdjustCurrentHealth(int adj){
        curHealth += adj;

        if(curHealth < 0){
            curHealth = 0;
        }
        if(curHealth > maxHealth){
            curHealth = maxHealth;
        }
        if(maxHealth < 1){
            maxHealth = 1;
        }
    }
    public void respawn()
    {
        player.transform.position = respawnPoint.transform.position;
    }
    
}
