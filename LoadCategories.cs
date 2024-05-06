using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCategories : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    public GameObject content;
    void OnEnable()
    {
        int childCount = content.transform.childCount;

        // Loop through all children except the first one
        for (int i = childCount - 1; i > 0; i--)
        {
            // Destroy the child GameObject
            Destroy(content.transform.GetChild(i).gameObject);
        }
        button.gameObject.SetActive(true);
        foreach (string category in AssetManager.folders)
        {
            //Debug.Log(category);
            GameObject c = Instantiate(button, content.transform);
            Sprite sp = Resources.Load<Sprite>("CategoryIcons/" + category);
            Image icon = c.transform.GetChild(1).GetComponent<Image>();
            icon.sprite = sp;
            c.transform.localScale = button.transform.localScale;
            Text t = c.GetComponentInChildren<Text>();
            t.text = States.SeparateWords(category);
            Category cat = c.GetComponent<Category>();
            cat.category = category;
        }
        button.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
