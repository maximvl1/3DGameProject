using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBehaviour : MonoBehaviour
{
    //Footstep related
    public AudioClip footstepsSound, slideSound, deathSound;
    public AudioSource audio;

    bool playedSlideSound = false, playedDeathSound = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GetComponent<PlayerMovement>().playFootstep)
            audio.PlayOneShot(footstepsSound, 0.4f);

        if (GetComponent<PlayerMovement>().isSliding)
        {
            if (!playedSlideSound)
            {
                playedSlideSound = true;
                audio.PlayOneShot(slideSound, 0.4f);
            }
        }
        else
            playedSlideSound = false;

        if (GetComponent<PlayerMovement>().triggerDeath)
        {
            if (!playedDeathSound)
            {
                playedDeathSound = true;
                audio.PlayOneShot(deathSound, 0.4f);
            }
        }
        else
            playedDeathSound = false;
    }
}