using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] GameObject touchPrefab;
    [SerializeField] bool joystickMode = false;
    public TouchClass[] touches = new TouchClass[0];
    public GameObject[] touchesUI = new GameObject[0];

    void Update()
    {
        Touch[] currentTouches = Input.touches;

        if (currentTouches.Length != touches.Length)
        {
            touches = new TouchClass[currentTouches.Length];
            foreach (GameObject i in touchesUI) Destroy(i);
            touchesUI = new GameObject[currentTouches.Length];
        }

        for (int i = 0; i<currentTouches.Length; i++)
        {
            if (touches[i] == null)
            {
                touches[i] = new TouchClass();
                touches[i].initialPos = new Vector2 (currentTouches[i].position.x, currentTouches[i].position.y);
                touchesUI[i] = createTouchUi();
            }

            if (currentTouches[i].phase == TouchPhase.Ended)
            {
                Destroy(touchesUI[i]);
            }

            if ((touches[i].initialPos - currentTouches[i].position).magnitude > 10)
            {
                touches[i].currentPos = new Vector2(currentTouches[i].position.x, currentTouches[i].position.y);

                touches[i].direction = new Vector2(
                    touches[i].currentPos.x - touches[i].initialPos.x,
                    touches[i].currentPos.y - touches[i].initialPos.y
                );

                if(!joystickMode)touches[i].initialPos = new Vector2(currentTouches[i].position.x, currentTouches[i].position.y);
            }
            else
            {
                touches[i].direction = new Vector2(0,0);
            }

            if (touchesUI[i] != null)
            {
                RectTransform rectTransform = touchesUI[i].GetComponent<RectTransform>();

                Vector2 touchPosition = currentTouches[i].position;

                Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(touchPosition);

                rectTransform.anchorMin = viewportPosition;
                rectTransform.anchorMax = viewportPosition;
            }


            touches[i].duration += Time.deltaTime;
        }
    }

    GameObject createTouchUi()
    {
        GameObject t = Instantiate(touchPrefab, canvas);
        return t;
    }
}
