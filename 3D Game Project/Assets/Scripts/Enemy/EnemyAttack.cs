using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform playerTransform;
    //public GameObject target;
    UnityEngine.AI.NavMeshAgent myNavmesh;
    public float checkRate = 0.01f;
    float nextCheck;
    public float range;
    public float hit;
    public Transform Player;
    public GameObject PlayerObj;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        myNavmesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextCheck)
        {
            nextCheck = Time.time + checkRate;
            float distance = Vector3.Distance(Player.transform.position, transform.position);
            if(distance<= range){
            FollowPlayer();
            }
            if (distance<= hit){
            AttackPlayer();
            }
        }
    }
    void FollowPlayer()
    {
        myNavmesh.transform.LookAt(Player);
        myNavmesh.destination = Player.transform.position;
    }
    void AttackPlayer(){
        PlayerObj=GameObject.Find("Player");
        PlayerObj.GetComponent<PlayerHealth>().AdjustCurrentHealth(-100);
    }
}
