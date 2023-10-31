using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof (BoxCollider), typeof (AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private float initialDelay;

    [SerializeField] private float _moveSpeed;

    private TouchController touchController;

    private void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
    }
    private void FixedUpdate()
    {
        if (initialDelay > 0) initialDelay -= Time.deltaTime;

        Vector3 currentVector = new Vector3(0,0,0);

        if (initialDelay <= 0)
        {
            if (touchController.touches.Length > 0)
            {
                currentVector = new Vector3(touchController.touches[0].direction.normalized.x * _moveSpeed, rb.velocity.y, touchController.touches[0].direction.normalized.y * _moveSpeed);
            }

            rb.velocity = currentVector;

            if (currentVector.x != 0 || currentVector.y != 0)
            {
                if (!footsteps.isPlaying)
                {
                    footsteps.Play();
                }
            }
            else
            {
                footsteps.Pause();
            }
        }
    }
}