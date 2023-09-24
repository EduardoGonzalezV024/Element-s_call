using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private TouchController touchController;

    private void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
    }
    public void Update()
    {
        if (touchController.touches.Length == 2)
        {
            if ((Mathf.Abs(touchController.touches[0].direction.normalized.y) < Mathf.Abs(touchController.touches[0].direction.normalized.x)) &&
                (Mathf.Abs(touchController.touches[1].direction.normalized.y) < Mathf.Abs(touchController.touches[1].direction.normalized.x)) &&
                touchController.touches[0].direction.x < -30 && touchController.touches[1].direction.x < -30)
            {
                int index = SceneManager.GetActiveScene().buildIndex;

                if (index == 0) index = SceneManager.sceneCountInBuildSettings;

                SceneManager.LoadScene(index - 1);
            }
        }
    }
}
