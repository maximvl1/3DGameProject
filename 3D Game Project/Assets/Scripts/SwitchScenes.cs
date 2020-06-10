using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public int curScene;
    public int maxScene;
    void OnTriggerEnter(Collider other)
    {
        if(curScene<maxScene)
        {
        curScene+=1;
        SceneManager.LoadScene(curScene);
        }
    }
}