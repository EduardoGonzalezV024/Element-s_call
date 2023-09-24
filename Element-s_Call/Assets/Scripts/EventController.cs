using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] GameObject spawnable;

    public float minSecs, maxSecs, totalDuration, initialDelay;

    void Start()
    {
        StartCoroutine(Event());
    }

    private void Update()
    {
        totalDuration -= Time.deltaTime;
    }

    IEnumerator Event()
    {
        yield return new WaitForSecondsRealtime(initialDelay);

        initialDelay = 0;

        Instantiate(spawnable, this.transform);

        float wait = Random.Range(minSecs, maxSecs);

        yield return new WaitForSecondsRealtime(wait);

        if (totalDuration > 0) StartCoroutine(Event());
    }
}
