using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LibraryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject categoriesPanel;
    public GameObject categoriesContent;
    public GameObject assetsPanel;
    public GameObject assetsContent;
    public GameObject categoryButton;
    public GameObject assetButton;
    public GameObject displayModel;
    private string[] folders = new string[] {"Doors",
"Windows",
"Lightings",
 "CoffeeTables",
"DiningTables",
 "Cabinets",
"DressingTables",
 "Hangers",
"Shelves",
"ShoeCabinets",
"TvBenches",
"Desks",
"OfficeChairs",
"BarChairs",
"Chairs",
 "RockingChairs",
"SoftArmChairs",
"CornerSofas",
"RoundSofas",
"SofaSets",
"StraightSofas",
"UshapedSofas",
 "ChildrensBeds",
"DoubleSizeBeds",
"SingleBeds",
"BathsAndShowers",
"Toilets",
"Sinks",
"StandingSinks",
"HangingKitchenCabinets",
"KitchenCabinets",
"Kitchenwares",
 "KitchenSets",
"SocketSwitches",
"HouseholdAppliances",
"VideoTvs",
"Computers",
"Paintings",
"Clocks",
"Vasesandflowers",
"Plants",
"Candles",
"CurtainBlinds",
 "Rugs",
"Mirrors" };
    private float rotation;
    private bool isDragging;
    private double prevVal;
    private double currentX;

    public GameObject homeButton;
    public GameObject backButton;
    private float initialDistance; // Distance between two touches at the beginning
    private Vector3 initialScale;
    private void loadCategories()
    {
        assetsPanel.SetActive(false);
        foreach (string category in folders)
        {
            Debug.Log(category);
            GameObject b = Instantiate(categoryButton, categoriesContent.transform);
            Sprite sp = Resources.Load<Sprite>("CategoryIcons/" + category);
            Image icon = b.transform.GetChild(1).GetComponent<Image>();
            icon.sprite = sp;
            b.transform.GetChild(0).GetComponent<Text>().text = States.SeparateWords(category);
            b.GetComponent<Button>().onClick.AddListener(()=> {
                backButton.SetActive(true);
                loadAssets(category);
            });
        }
        categoryButton.SetActive(false);
    }
    private void removeAssets()
    {
        for (int i = 1; i < assetsContent.transform.childCount; i++)
        {
            Destroy(assetsContent.transform.GetChild(i).gameObject);
        }
    }
    void loadAssets(string category)
    {
        Debug.Log("Button Clicked with argument: " + category);
        removeAssets();
        categoriesPanel.SetActive(false);
        assetsPanel.SetActive(true);
        assetButton.SetActive(true);
        Mesh[] meshes = Resources.LoadAll<Mesh>("Models/Furnitures/" + category);
        foreach (Mesh mesh in meshes)
        {
            Debug.Log(mesh.name);
            GameObject b = Instantiate(assetButton, assetsContent.transform);
            b.transform.GetChild(0).GetComponent<Text>().text = mesh.name;
            Sprite icon = Resources.Load<Sprite>("Models/Furnitures/" + category + "/" + mesh.name + "/" + mesh.name + "_icon");
            b.transform.GetChild(1).GetComponent<Image>().sprite = icon;
            b.GetComponent<Button>().onClick.AddListener(() =>
            {
                Texture2D texture = Resources.Load<Texture2D>("Models/Furnitures/" + category + "/" + mesh.name + "/" + mesh.name + "_texture");
                displayModel.GetComponent<MeshFilter>().mesh = mesh;
                Material m = displayModel.GetComponent<MeshRenderer>().material;
                m.mainTexture = texture;
            });
        }
        assetButton.SetActive(false);
    }

    void Start()
    {
        loadCategories();
        homeButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        backButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            assetsPanel.SetActive(false);
            categoriesPanel.SetActive(true);
            backButton.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = displayModel.transform.localScale;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float scaleFactor = currentDistance / initialDistance;

                // Scale the object proportionally based on pinch
                displayModel.transform.localScale = initialScale * scaleFactor;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                prevVal = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
            if (isDragging)

            {
                currentX = Input.mousePosition.x;
                if (currentX > prevVal)
                {
                    //Debug.Log("Dragging right");
                    rotation -= 150f * Time.deltaTime;
                }
                else if (currentX < prevVal)
                {
                    // Debug.Log("Dragging left");
                    rotation += 150f * Time.deltaTime;
                }
                prevVal = currentX;
            }
            else
            {
                rotation += 40f * Time.deltaTime;
            }
        }
        Vector3 displayRotation = displayModel.transform.localEulerAngles;
        displayRotation = new Vector3(displayRotation.x, rotation, displayRotation.z);
        displayModel.transform.localEulerAngles = displayRotation;
    }
}
