using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEvent : MonoBehaviour
{
    private TouchController touchController;
    public AudioClip actionSound;
    public AudioClip failSound;
    public AudioClip readySound;
    public AudioClip attackSound;

    private float inputDelay = 0;
    private bool passes = false;

    void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
        this.transform.position = new Vector3(0, 1.3f, 12);

        StartCoroutine(Action());
    }

    public void Update()
    {
        if (touchController.touches.Length > 0)
        {
            if (touchController.touches[0].direction.y > 30 &&
                (Mathf.Abs(touchController.touches[0].direction.normalized.x) < Mathf.Abs(touchController.touches[0].direction.normalized.y))
                && inputDelay < 0)
            {
                inputDelay = 2;
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(actionSound);

                passes = touchController.touches[0].direction.y > 0;
            }
        }
        inputDelay -= Time.deltaTime;
    }

    IEnumerator Action()
    {
        yield return new WaitForSecondsRealtime(2f);

        this.GetComponent<AudioSource>().PlayOneShot(readySound);

        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);

        yield return new WaitForSecondsRealtime(3f);

        passes = false;

        this.GetComponent<AudioSource>().PlayOneShot(attackSound);

        yield return new WaitForSecondsRealtime(0.75f);

        if (!passes)
        {
            Fail();
        }

        Destroy(this.gameObject, 1);
    }
    public void Fail()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(failSound);
        Handheld.Vibrate();
    }
}
