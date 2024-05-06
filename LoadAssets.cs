using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssets : MonoBehaviour
{
    public GameObject content;
    public GameObject button;
    public string _category;

    public string category
    {
        get
        {
            return _category;
        }
        set
        {
            Debug.Log(value);
            _category = value;
        }
    }
    private void OnEnable()
    {
        // Load all assets icon
        if(_category != null)
        {
            int childCount = content.transform.childCount;

            // Loop through all children except the first one
            for (int i = childCount - 1; i > 0; i--)
            {
                // Destroy the child GameObject
                Destroy(content.transform.GetChild(i).gameObject);
            }
            button.gameObject.SetActive(true);
            Debug.Log("Loading Assets for " + _category);
            foreach (Mesh mesh in AssetManager.getMeshes(_category))
            {
                GameObject asset = Instantiate(button, content.transform);
                Sprite icon = Resources.Load<Sprite>("Models/Furnitures/" + _category + "/" + mesh.name + "/" + mesh.name + "_icon");
                Text t = asset.GetComponentInChildren<Text>();
                t.text = mesh.name;
                Asset a = asset.GetComponent<Asset>();
                asset.transform.GetChild(1).GetComponent<Image>().sprite = icon;
                a.category = _category;
                a.asset = mesh.name;
                a.prefab = mesh;
            }
            button.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
