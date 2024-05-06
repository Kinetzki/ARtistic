using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallDimension : MonoBehaviour
{
    [SerializeField]
    private GameObject lengthInput;
    [SerializeField]
    private GameObject heightInput;
    [SerializeField]
    private GameObject submitBtn;
    private float length;
    private float height;
    void Start()
    {
        lengthInput.GetComponent<InputField>().onValueChanged.AddListener(handleChange);
        heightInput.GetComponent<InputField>().onValueChanged.AddListener(handleHeight);
        submitBtn.GetComponent<Button>().onClick.AddListener(submit);
        height = States.roomHeight / 2.44f;
    }
    private void submit()
    {
        States.selected.transform.localScale = new Vector3(length, height, 1f);
        States.selected.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(length, height);
        States.selected.GetComponent<Furniture>().wallLength = length;
        States.selected.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(States.selected.transform.localScale.y * 18, States.selected.transform.localScale.x * 8);
        transform.gameObject.SetActive(false);
    }
    private void handleChange(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            length = floatValue;
            //offsetting posiion
        }
    }

    private void handleHeight(string l)
    {
        if (float.TryParse(l, out float floatValue))
        {
            Debug.Log("Input field value changed to float: " + floatValue);
            height = floatValue / 2.44f;
            //offsetting posiion
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
