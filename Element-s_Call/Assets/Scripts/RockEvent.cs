using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEvent : MonoBehaviour
{
    private DynamicJoystick joystick;
    public AudioClip actionSound;
    public AudioClip failSound;
    public float pos = 0;

    private float inputDelay = 0;
    private bool passes = false;

    void Awake()
    {
        joystick = FindObjectOfType<DynamicJoystick>();

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
        if (joystick.Horizontal != 0 && inputDelay < 0)
        {
            inputDelay = 2;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(actionSound);

            passes = (joystick.Horizontal * pos) < 0;
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
    }
    public void Fail()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(failSound);
        Handheld.Vibrate();
    }
}
