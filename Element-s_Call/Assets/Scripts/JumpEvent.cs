using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEvent : MonoBehaviour
{
    private TouchController touchController;
    public AudioClip actionSound;
    public AudioClip failSound;
    public float initialDelay = 0;
    public float height = 20;

    private float inputDelay = 0;
    public float downSp = 0;

    void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
    }

    public void Update()
    {
        if (initialDelay < 0)
        {
            if (touchController.touches.Length > 0)
            {
                if (touchController.touches[0].direction.y > 30 &&
                    (Mathf.Abs(touchController.touches[0].direction.normalized.x) < Mathf.Abs(touchController.touches[0].direction.normalized.y))
                    && inputDelay < 0)
                {
                    inputDelay = 0.5f;
                    height += 2;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(actionSound);
                }
            }
            inputDelay -= Time.deltaTime;
            if (height > 0)
            {
                height -= Time.deltaTime * downSp;
            }
            if (height < 0)
            {
                Fail();
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

        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(failSound);
        cont.failCont = cont.maxFails - 1;
        cont.Fail();
        Handheld.Vibrate();
        this.enabled = false;   
    }
}