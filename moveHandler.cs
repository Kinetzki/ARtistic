using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class moveHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Start is called before the first frame update
    public string direction;
    public GameObject interior;
    public Vector3 vectorValues = new Vector3(15f, 15f, 15f);
    private float prevpoint;
    private float currentpoint;
    void Start()
    {
        
    }
    public void OnPointerDown(PointerEventData e)
    {
        States.selected = null;
        prevpoint = Input.mousePosition.y;
    }

    public void OnPointerUp(PointerEventData e)
    {

    }

    public void OnDrag(PointerEventData e)
    {
        currentpoint = Input.mousePosition.y;
        if (direction == "up")
        {
            
            if (currentpoint > prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x, initials.y + vectorValues.y * Time.deltaTime, initials.z);
            }
            else if (currentpoint < prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x, initials.y - vectorValues.y * Time.deltaTime, initials.z);
            }
        }
        else if (direction == "side")
        {
            if (currentpoint > prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x + vectorValues.x * Time.deltaTime, initials.y, initials.z);
            }
            else if (currentpoint < prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x - vectorValues.x * Time.deltaTime, initials.y, initials.z);
            }
        }
        else if (direction == "forward")
        {
            if (currentpoint > prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x, initials.y, initials.z + vectorValues.z * Time.deltaTime);
            }
            else if (currentpoint < prevpoint)
            {
                Vector3 initials = interior.transform.position;
                interior.transform.position = new Vector3(initials.x, initials.y, initials.z - vectorValues.z * Time.deltaTime);
            }
        }
        prevpoint = currentpoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
