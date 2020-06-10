using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{

    //Player position related
    Transform player;
    
    CharacterController controller;
    public Vector3 startingPos;
    Vector3 posDiff;

    void Start()
    {
        player = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        checkPosDiff();
        controller.Move(posDiff);
    }

    void Update()
    {
    }

    void checkPosDiff()
    {
        //x
        if (startingPos.x < player.transform.position.x)
        {
            posDiff.x = startingPos.x - player.transform.position.x;
        }
        else if (startingPos.x > player.transform.position.x)
        {
            posDiff.x = player.transform.position.x - startingPos.x;
            if (player.transform.position.x < 0)
                posDiff.x *= -1;
        }

        //y
        if (startingPos.y < player.transform.position.y)
        {
            posDiff.y = startingPos.y - player.transform.position.y;
        }
        else if (startingPos.y > player.transform.position.y)
        {
            posDiff.y = player.transform.position.y - startingPos.y;
            if (player.transform.position.y < 0)
                posDiff.y *= -1;
        }

        //z
        if (startingPos.z < player.transform.position.z)
        {
            posDiff.z = startingPos.z - player.transform.position.z;
        }
        else if (startingPos.z > player.transform.position.z)
        {
            posDiff.z = player.transform.position.z - startingPos.z;
            if (player.transform.position.z < 0)
                posDiff.z *= -1;
        }
    }
}
