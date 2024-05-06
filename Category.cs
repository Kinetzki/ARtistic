using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Category : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string _category;
    private Button b;
    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
    }
    public void OnPointerDown(PointerEventData e)
    {
        States.doRotate = false;
    }

    public void OnPointerUp(PointerEventData e)
    {
        //States.doRotate = true;
    }
    void OnEnable()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        //Debug.Log(_category);
        AssetManager.showAssets(_category);
        States.activeCategory = _category;
        MenuPanelManager.showBackButton = true;

    }
    public string category { get { return _category; } set { _category = value; } }

    void Update()
    {
        
    }
}
