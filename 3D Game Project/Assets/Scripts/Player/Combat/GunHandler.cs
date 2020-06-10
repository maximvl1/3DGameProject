using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour
{

    enum GunState
    {
        Grappling,
        Bullet
    };

    GunState gunState;
    bool gunStateChanged = true;

    Image gunIcon;
    Image grapplingIcon;

    public Sprite gunIconSprite;
    public Sprite gunIconActiveSprite;
    public Sprite grapplingIconSprite;
    public Sprite grapplingIconActiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        gunState = GunState.Grappling;
        grapplingIcon = GameObject.Find("GrapplingIcon").GetComponent<Image>();
        gunIcon = GameObject.Find("GunIcon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunState = GunState.Grappling;
            gunStateChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunState = GunState.Bullet;
            gunStateChanged = true;

        }

        if (gunStateChanged)
        {
            switch (gunState)
            {
                case GunState.Bullet:
                    this.gameObject.GetComponentInChildren<GunBehaviour>().enabled = true;
                    this.gameObject.GetComponentInChildren<GrapplingGun>().enabled = false;

                    gunIcon.sprite = gunIconActiveSprite;
                    grapplingIcon.sprite = grapplingIconSprite;
                    break;
                case GunState.Grappling:
                    this.gameObject.GetComponentInChildren<GunBehaviour>().enabled = false;
                    this.gameObject.GetComponentInChildren<GrapplingGun>().enabled = true;

                    gunIcon.sprite = gunIconSprite;
                    grapplingIcon.sprite = grapplingIconActiveSprite;
                    break;
                default:
                    break;
            }

            gunStateChanged = false;
        }
    }
}
