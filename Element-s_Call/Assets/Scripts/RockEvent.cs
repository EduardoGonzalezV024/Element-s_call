using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEvent : MonoBehaviour
{
    private TouchController touchController;
    public AudioClip actionSound;
    public AudioClip[] failSounds;
    public AudioClip[] successSounds;
    public float pos = 0;

    private float inputDelay = 0;
    private bool passes = false;

    void Awake()
    {
        touchController = FindObjectOfType<TouchController>();

        while (pos == 0)
        {
            pos = (int)Random.Range(-1, 2);
        }

        this.transform.position = new Vector3((pos * 5), 15, 0);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);

        StartCoroutine(Action());

        Destroy(this.gameObject, 6);
    }

    public void Update()
    {
        if (touchController.touches.Length > 0)
        {
            if (Mathf.Abs(touchController.touches[0].direction.x) > 30 && 
                (Mathf.Abs(touchController.touches[0].direction.normalized.y) < Mathf.Abs(touchController.touches[0].direction.normalized.x))
                && inputDelay < 0)
            {
                inputDelay = 2;
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(actionSound);

                passes = (touchController.touches[0].direction.x * pos) < 0;
            }
        }
        inputDelay -= Time.deltaTime;
    }

    IEnumerator Action()
    {
        yield return new WaitForSecondsRealtime(3.5f);

        if (!passes)
        {
            Fail();
        }
        else if(Random.Range(-1, 2) > 0)
        {
            GameObject.FindGameObjectWithTag("Aeris").GetComponent<AudioSource>().PlayOneShot(successSounds[(int)Random.Range(0, successSounds.Length)]);
        }
    }
    public void Fail()
    {
        EventController cont = GameObject.FindObjectOfType<EventController>();

        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(failSounds[0]);
        if (cont.failCont > ((cont.maxFails / 3) * 2) && cont.failCont < cont.maxFails && Random.Range(-1, 2) > 0)
        {
            GameObject.FindGameObjectWithTag("Aeris").GetComponent<AudioSource>().PlayOneShot(failSounds[(int)Random.Range(1, failSounds.Length)]);
        }
        cont.Fail();
        Handheld.Vibrate();
    }
}
