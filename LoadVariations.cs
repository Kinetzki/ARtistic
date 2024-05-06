using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadVariations : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    private Texture2D[] vars;
    private string meshName;
    public GameObject variation;
    public GameObject variationContainer;

    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData e)
    {
        States.doRotate = false;
    }

    public void OnPointerUp(PointerEventData e)
    {
        States.doRotate = true;
    }
    private void OnEnable()
    {
        RemoveAllChildren();
        variation.SetActive(true);
        if (States.selected != null)
        {
            string cat = "";
            if (States.selected.tag == "floor" || States.selected.tag == "ceiling")
            {
                vars = Resources.LoadAll<Texture2D>("Models/Furnitures/Floor/variations");
                meshName = "floor";
                cat = "Floor";
            } else
            {
                meshName = States.selected.GetComponent<MeshFilter>().sharedMesh.name.Split(" ")[0];
                cat = States.selected.GetComponent<Furniture>().category;
                vars = Resources.LoadAll<Texture2D>("Models/Furnitures/" + cat + "/" + meshName + "/variations");
            }
            Debug.Log(meshName);
            //Debug.Log("Models/Furnitures/" + States.activeCategory + "/" + meshName + "/variations");
            foreach (Texture2D texture in vars)
            {
                //Debug.Log(texture.name);
                GameObject v = Instantiate(variation, variationContainer.transform);
                Sprite icon = Resources.Load<Sprite>("Models/Furnitures/" + cat + "/" + meshName + "/" + "icons/" + texture.name + "_icon");
                //Debug.Log("Models/Furnitures/" + States.activeCategory + "/" + meshName + "/" + "icons/" + texture.name + "_icon");
                v.transform.GetChild(0).GetComponent<Image>().sprite = icon;
                Variation vv = v.GetComponent<Variation>();
                vv.variation = texture;
            }
        }
        variation.SetActive(false);
    }
    private void RemoveAllChildren()
    {
        for (int i = 1; i < variationContainer.transform.childCount; i++)
        {
            //Debug.Log(i);
            Destroy(variationContainer.transform.GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
