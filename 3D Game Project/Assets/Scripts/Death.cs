using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject PlayerObj;
    void OnTriggerEnter(Collider other)
    {
        PlayerObj=GameObject.Find("Player");
        PlayerObj.GetComponent<PlayerHealth>().AdjustCurrentHealth(-100);
    }
}
