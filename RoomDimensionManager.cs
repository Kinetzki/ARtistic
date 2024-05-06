using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDimensionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject floor;
    public GameObject ceiling;
    private float roomX;
    private float roomY;
    private float roomElevation;
    public GameObject roomLengthInput;
    public GameObject roomWidthInput;
    public GameObject roomElevationInput;
    public GameObject submitButton;
    //private GameObject Menu;
    void Start()
    {
        //Hide menu
        //Menu = transform.parent.transform.GetChild(0).gameObject;
        //Menu.SetActive(false)
        States.isShowMenu = false;
        InputField lengthField = roomLengthInput.GetComponent<InputField>();
        InputField widthField = roomWidthInput.GetComponent<InputField>();
        InputField heightField = roomElevationInput.GetComponent<InputField>();
        lengthField.onValueChanged.AddListener(onLengthChange);
        widthField.onValueChanged.AddListener(onWidthChange);
        heightField.onValueChanged.AddListener(onHeightChange);
        submitButton.GetComponent<Button>().onClick.AddListener(submit);
    }

    private void submit()
    {
        Debug.Log("Submit");
        floor.transform.localScale = new Vector3(roomX, 1f, roomY);
        ceiling.transform.localScale = new Vector3(roomX, 1f, roomY);
        Debug.Log(roomElevation);
        ceiling.transform.localPosition = new Vector3(ceiling.transform.localPosition.x, roomElevation, ceiling.transform.localPosition.z);
        //unhide menu
        //Menu.SetActive(true);
        States.isShowMenu = true;
        States.roomLength = roomY;
        States.roomWidth = roomX;
        States.roomHeight = roomElevation;
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        States.isShowRoom = false;
    }
    private void onLengthChange(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            roomY = floatValue;
        }
    }
    private void onHeightChange(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            roomElevation = floatValue;
        }
    }

    private void onWidthChange(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            roomX = floatValue;
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(States.isShowRoom);
    }
}
