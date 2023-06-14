using UnityEngine;

public class DenemeController : MonoBehaviour
{
    public float minSwipeDistance = 50f;
    public static  bool swipeLeft, swipeRight, swipeUp, swipeDown;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;

                float xDelta = Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
                float yDelta = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);

                if (xDelta > minSwipeDistance || yDelta > minSwipeDistance)
                {
                    if (xDelta > yDelta) // Horizontal swipe
                    {
                        if (fingerDownPosition.x - fingerUpPosition.x > 0) // Swipe left
                        {
                            swipeLeft = true;
                        }
                        else // Swipe right
                        {
                            swipeRight = true;
                        }
                    }
                    else // Vertical swipe
                    {
                        if (fingerDownPosition.y - fingerUpPosition.y > 0) // Swipe down
                        {
                            swipeDown = true;
                        }
                        else // Swipe up
                        {
                            swipeUp = true;
                        }
                    }
                }
            }
        }
    }
}
