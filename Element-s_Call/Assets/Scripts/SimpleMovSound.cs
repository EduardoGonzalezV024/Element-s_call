using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovSound : MonoBehaviour
{
    [SerializeField] private Vector3 dir = new Vector3(0,0,0);
    [SerializeField] private float time = 0;
    [SerializeField] private float initialDelay = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke("move", initialDelay);
        Destroy(this.gameObject, time);
    }

    private void move()
    {
        this.GetComponent<Rigidbody>().velocity = dir;
    }
}
