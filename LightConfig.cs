using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;

    private void setLight(Color color)
    {
        if (States.selected != null)
        {
            Light l = States.selected.transform.GetChild(0).GetComponent<Light>();
            l.color = color;
        }
    }
    void Start()
    {
        light1.GetComponent<Button>().onClick.AddListener(() =>
        {
            Color color = new Color(248f / 255f, 255f / 255f, 183f / 255f, 1f);
            setLight(color);
        });
        light2.GetComponent<Button>().onClick.AddListener(() =>
        {
            Color color = new Color(255f / 255f, 218f / 255f, 122f / 255f, 1f);
            setLight(color);
        });
        light3.GetComponent<Button>().onClick.AddListener(() =>
        {
            Color color = new Color(255f / 255f, 155f / 255f, 23f / 255f, 1f);
            setLight(color);
        });
        light4.GetComponent<Button>().onClick.AddListener(() =>
        {
            Color color = new Color(255f / 255f, 214f / 255f, 170f / 255f, 1f);
            setLight(color);
        });
        light5.GetComponent<Button>().onClick.AddListener(() =>
        {
            Color color = new Color(255f / 255f, 214f / 255f, 170f / 255f, 1f);
            setLight(color);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
