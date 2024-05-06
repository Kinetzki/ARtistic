using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private float initialDistance; // Distance between two touches at the beginning
    public float speed = 5f;
    public float maxZPosition = 10f; // Maximum allowed Z position
    public float minZPosition = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            States.selected = null;
            States.doRotate = false;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            float movement = speed * Time.deltaTime;
            // Calculate the new position after movement
            float newPosition = transform.position.z + movement;

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float scaleFactor = currentDistance / initialDistance;
                if (newPosition > maxZPosition)
                {
                    movement = maxZPosition - transform.position.z;
                }
                else if (newPosition < minZPosition)
                {
                    movement = minZPosition - transform.position.z;
                }
                if (movement != 0f) {
                    if (scaleFactor >= 1)
                    {
                        transform.Translate(Vector3.forward * movement * Time.deltaTime * 0.2f);
                    }
                    else if (scaleFactor < 1)
                    {
                        transform.Translate(Vector3.forward * (-1 * movement) * Time.deltaTime * 0.2f);
                    }
                }
                
            }
        }
    }
}
