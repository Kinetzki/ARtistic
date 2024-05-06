using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorWindowManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject heightInput;
    public GameObject widthInput;
    public GameObject submit;

    private float height;
    private float width;

    void Start()
    {
        InputField heightField = heightInput.GetComponent<InputField>();
        InputField widthField = widthInput.GetComponent<InputField>();

        heightField.onValueChanged.AddListener(onHeightChange);
        widthField.onValueChanged.AddListener(onWidthChange);

        submit.GetComponent<Button>().onClick.AddListener(handleSubmit);
    }
    private void onHeightChange (string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            height = floatValue;
        }
    }

    private void onWidthChange(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            width = floatValue;
        }
    }
    private void handleSubmit()
    {
        if (States.selected != null)
        {
            States.selected.transform.localScale = new Vector3(width, height / 2.44f, States.selected.transform.localScale.z);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
