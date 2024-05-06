using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class handleMove : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public GameObject xyz;
    public GameObject interior;
    private bool isCenter = false;
    void Start()
    {
        xyz = transform.GetChild(2).gameObject;
    }

    private void OnEnable()
    {
        xyz.SetActive(false);
    }
    public void OnPointerDown (PointerEventData e)
    {
        xyz.SetActive(!xyz.activeSelf);
        if (!isCenter)
        {
            Vector2 centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);
            Vector3 placementPosition = ray.origin + ray.direction * 3f;
            placementPosition.z = placementPosition.z + (interior.transform.localScale.z * 5);
            placementPosition.x = placementPosition.x - (interior.transform.localScale.x * 2);
            placementPosition.y = placementPosition.y - (interior.transform.localScale.y * 0.5f);
            interior.transform.position = placementPosition;
            isCenter = true;
        } else
        {
            isCenter = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
