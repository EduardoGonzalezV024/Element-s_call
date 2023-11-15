using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbEvent : MonoBehaviour
{
    private TouchController touchController;
    public AudioClip[] actionSounds;
    public AudioClip warningSound;
    public float initialDelay = 0;
    public float height = 500;
    public bool warningBool = false;
    public bool endBool = true;

    public float downSp = 0;
    public Transform cavesounds;

    void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
        cavesounds = GameObject.Find("CaveSounds").transform;
    }

    public void Update()
    {
        if (initialDelay < 0)
        {
            if (touchController.touches.Length > 0)
            {
                if (touchController.touches[0].duration < 1)
                {
                    if (downSp > 1)
                    {
                        GameObject.Find("Feet").GetComponent<AudioSource>().PlayOneShot(actionSounds[0], 0.5f);
                        GameObject.Find("Hands").GetComponent<AudioSource>().PlayOneShot(actionSounds[1], 0.75f);
                    }
                    if(downSp > 5)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(actionSounds[2], 0.5f);
                    }
                    downSp = 0;
                    warningBool = false;
                }
            }
            else if (height > 0)
            {
                height -= Time.deltaTime * downSp;
                downSp += Time.deltaTime * 2;

                cavesounds.position = new Vector3(0, (100 - height), -20);

                if (downSp > 6 && !warningBool)
                {
                    AudioSource aeris = GameObject.FindGameObjectWithTag("Aeris").GetComponent<AudioSource>();
                    warningBool = aeris.isPlaying;
                    if(!warningBool)aeris.PlayOneShot(warningSound);
                }
            }
            if (height < 0)
            {
                if (downSp > 6) Fail();
                else GameObject.FindObjectOfType<EventController>().totalDuration = 0;
            }
            if(height < 20 && endBool)
            {
                endBool = false;
                AudioSource aeris = GameObject.FindGameObjectWithTag("Aeris").GetComponent<AudioSource>();
                if (!aeris.isPlaying) aeris.PlayOneShot(actionSounds[actionSounds.Length-1]);
            }
        }
        else
        {
            initialDelay -= Time.deltaTime;
        }
    }

    public void Fail()
    {
        EventController cont = GameObject.FindObjectOfType<EventController>();

        cont.failCont = cont.maxFails - 1;
        cont.Fail();
        Handheld.Vibrate();
        this.enabled = false;
    }
}
