using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private TouchController touchController;

    private void Awake()
    {
        int progress = PlayerPrefs.GetInt("levelCompleted", 0);
        if (progress < SceneManager.GetActiveScene().buildIndex) PlayerPrefs.SetInt("levelCompleted", SceneManager.GetActiveScene().buildIndex);

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
                SceneManager.LoadScene(0);
            }
        }
    }

    public void nextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (index == SceneManager.sceneCountInBuildSettings-1) index = -1;

        SceneManager.LoadScene(index + 1);
    }
    public void reloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(index);
    }

    public void prevScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0) index = SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(index - 1);
    }

}
