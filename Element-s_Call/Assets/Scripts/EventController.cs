using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    public float minSecs, maxSecs, totalDuration, initialDelay, failCont, maxFails;

    void Start()
    {
        StartCoroutine(Event());
    }

    private void Update()
    {
        if(initialDelay == 0) totalDuration -= Time.deltaTime;
    }

    IEnumerator Event()
    {
        yield return new WaitForSecondsRealtime(initialDelay);

        initialDelay = 0;

        if(spawnable != null)Instantiate(spawnable, this.transform);

        float wait = Random.Range(minSecs, maxSecs);

        yield return new WaitForSecondsRealtime(wait);

        if (totalDuration > 0) StartCoroutine(Event());

        else if(failCont < maxFails) FindObjectOfType<SceneController>().nextScene();
    }

    public void Fail()
    {
        failCont++;

        if(failCont == maxFails)
        {
            StopCoroutine(Event());
            FindObjectOfType<DialogController>().failSq();
        }
    }
}
