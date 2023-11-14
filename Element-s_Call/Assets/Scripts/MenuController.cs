using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private TouchController touchController;
    private int option = 0;
    private float inputDelay = 0;

    private void Awake()
    {
        touchController = FindObjectOfType<TouchController>();
    }
    public void Update()
    {
        if (touchController.touches.Length == 2 && inputDelay < 0)
        {
            if ((Mathf.Abs(touchController.touches[0].direction.normalized.y) < Mathf.Abs(touchController.touches[0].direction.normalized.x)) &&
            (Mathf.Abs(touchController.touches[1].direction.normalized.y) < Mathf.Abs(touchController.touches[1].direction.normalized.x)) &&
            touchController.touches[0].direction.x < -30 && touchController.touches[1].direction.x < -30)
            {
                Application.Quit();
                inputDelay = 0.5f;
            }
        }
        if (touchController.touches.Length == 1 && inputDelay < 0)
        {
            if (touchController.touches[0].direction.x > 30 && (Mathf.Abs(touchController.touches[0].direction.normalized.y) < Mathf.Abs(touchController.touches[0].direction.normalized.x))
               && inputDelay < 0)
            {
                switch(option)
                {
                    case 0:
                        play();
                        break;
                    case 1:
                        newGame();
                        break;
                    case 2:
                        Application.Quit();
                        break;
                }
                inputDelay = 0.5f;
            }
            if (touchController.touches[0].direction.y < 30 && (Mathf.Abs(touchController.touches[0].direction.normalized.x) < Mathf.Abs(touchController.touches[0].direction.normalized.y))
                && inputDelay < 0)
            {
                option++;
                if (option == 3) option = 0;
                inputDelay = 0.5f;
            }
        }
        inputDelay -= Time.deltaTime;
    }

    public void newGame()
    {
        PlayerPrefs.SetInt("levelCompleted", 1);
        play();
    }

    public void play()
    {
        int progress = PlayerPrefs.GetInt("levelCompleted");

        SceneManager.LoadScene(progress);
    }
}
