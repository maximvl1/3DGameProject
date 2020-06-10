using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustceneTest : MonoBehaviour
{

    public GameObject cutsceneCamera;
    public GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneCamera.transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation); 
        cutsceneCamera.SetActive(true);
        playerCamera.SetActive(false);
        StartCoroutine(finishCutscene());
    }

    IEnumerator finishCutscene()
    {
        yield return new WaitForSeconds(8);
        playerCamera.SetActive(true);
        cutsceneCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cutsceneCamera.transform.SetPositionAndRotation(playerCamera.transform.position, playerCamera.transform.rotation); 
    }
}
