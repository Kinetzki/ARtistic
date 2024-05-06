using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Variation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public Texture2D _variation;
    public Texture2D variation
    {
        get
        {
            return _variation;
        }
        set
        {
            _variation = value;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log(_variation.name);
        States.doRotate = false;
        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = _variation;
        if (States.selected.GetComponent<MeshFilter>().sharedMesh.name == "floor")
        {
            material.mainTextureScale = new Vector2(States.roomWidth, States.roomLength);
        }
        if (States.selected.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "wall")
        {
            material.mainTextureScale = new Vector2(States.selected.transform.localScale.y * 13, States.selected.transform.localScale.x * 4);
        }
        States.selected.GetComponent<Furniture>().texture = _variation;
        States.selected.GetComponent<MeshRenderer>().material = material;
    }
    public void OnPointerUp(PointerEventData e)
    {
        Debug.Log("Set by variation");
        //States.doRotate = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
