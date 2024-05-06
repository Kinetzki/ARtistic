using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARhandler : MonoBehaviour
{
    // Start is called before the first frame update
    private float initialDistance; // Distance between two touches at the beginning
    private Vector3 initialScale;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(States.isShowInterior);
        if (Input.touchCount == 2)
        {
            States.selected = null;
            States.isArMove = false;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            if (States.isAr)
            {
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touch0.position, touch1.position);
                    initialScale = transform.localScale;
                }
                else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                    float scaleFactor = currentDistance / initialDistance;

                    // Scale the object proportionally based on pinch
                    transform.localScale = initialScale * scaleFactor;
                    States.arScale = transform.localScale;
                }
            }
        }
    }
}
