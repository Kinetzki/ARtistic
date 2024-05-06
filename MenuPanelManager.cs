using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Start is called before the first frame update
    public static bool _showCategories;
    public static bool _showFurnitures;
    public static bool _showVariations;
    public static bool _showWallConfig;
    public static bool _showWBackButton;
    public static GameObject categories;
    public static GameObject variations;
    public static GameObject furnitures;
    public static string _msg;
    public static GameObject textt;
    public static GameObject backButton;
    public static GameObject deleteButton;
    public static GameObject wallSetButton;
    public static GameObject lightSetButton;
    public static GameObject sizeConfigButton;
    public static GameObject wallConfig;
    public static GameObject lightConfig;
    public static GameObject sizeConfig;

    public static GameObject tools;
    public GameObject ceiling;
    public GameObject interior;
    public GameObject floor;
    //tools
    public static GameObject walkTool;
    public static GameObject rulerTool;
    public static GameObject arTool;
    public GameObject moveTool;
    public GameObject designTool;
    public void OnPointerDown (PointerEventData e)
    {
        Debug.Log("Clicked menu");
        States.doRotate = false;
    }
    public void OnDrag (PointerEventData e)
    {
        Debug.Log("Clicked menu");
        States.doRotate = false;
    }
    public void OnPointerUp (PointerEventData e)
    {
        Debug.Log("Set by menu");
        //States.doRotate = true;
    }
    public static bool showCategories
    {
        get
        {
            return _showCategories;
        }
        set
        {
            _showCategories = value;
            categories.SetActive(value);
        }
    }

    public static bool showBackButton
    {
        get
        {
            return _showWBackButton;
        }
        set
        {
            _showWBackButton = value;
            backButton.SetActive(value);
        }
    }

    public static bool showVariations
    {
        get
        {
            return _showVariations;
        }
        set
        {
            _showVariations = value;
            variations.SetActive(value);
        }
    }

    public static bool showFurnitures
    {
        get
        {
            return _showFurnitures;
        }
        set
        {
            _showFurnitures = value;
            furnitures.SetActive(value);
        }
    }

    public static string msg
    {
        get
        {
            return _msg;
        }
        set
        {
            _msg = value;
            textt.GetComponent<Text>().text = _msg;
        }
    }
    void Start()
    {
        categories = transform.GetChild(1).gameObject;
        variations = transform.GetChild(0).gameObject;
        furnitures = transform.GetChild(2).gameObject;
        wallConfig = transform.GetChild(6).gameObject;
        lightConfig = transform.GetChild(9).gameObject;
        sizeConfig = transform.GetChild(10).gameObject;
        tools = transform.GetChild(3).gameObject;
        //set tools onclick
        walkTool = tools.transform.GetChild(0).gameObject;
        rulerTool = tools.transform.GetChild(1).gameObject;
        arTool = tools.transform.GetChild(2).gameObject;

        walkTool.GetComponent<Button>().onClick.AddListener(handleWalk);
        rulerTool.GetComponent<Button>().onClick.AddListener(handleRuler);
        arTool.GetComponent<Button>().onClick.AddListener(handleAr);
        //moveTool.GetComponent<Button>().onClick.AddListener(handleMove);
        designTool.GetComponent<Button>().onClick.AddListener(handleDesign);



        variations.SetActive(false);
        textt = transform.parent.GetChild(2).gameObject;
        backButton = transform.GetChild(4).gameObject;
        deleteButton = transform.GetChild(7).gameObject;
        wallSetButton = transform.GetChild(5).gameObject;
        lightSetButton = transform.GetChild(8).gameObject;
        sizeConfigButton = transform.GetChild(11).gameObject;
        lightSetButton.GetComponent<Button>().onClick.AddListener(handleLight);
        backButton.GetComponent<Button>().onClick.AddListener(handleBack);
        wallSetButton.GetComponent<Button>().onClick.AddListener(handleWallSet);
        deleteButton.GetComponent<Button>().onClick.AddListener(handleDelete);
        sizeConfigButton.GetComponent<Button>().onClick.AddListener(handleSize);
    }

    //private void handleMove()
    //{
        //Debug.Log("Ground detection");
        //States.selected = null;
        //States.isArMove = !States.isArMove;
    //}
    private void handleSize()
    {
        sizeConfig.SetActive(!sizeConfig.activeSelf);
    }

    private void handleDesign()
    {
        interior.transform.localPosition = new Vector3(-1.7453f, 0.8763885f, -4.982351f);
        interior.transform.localScale = new Vector3(1f, 1f, 1f);
        States.isArMove = false;
        States.isAr = false;
        States.isArRotate = false;
        SceneManager.LoadScene(3);
    }
    private void handleAr()
    {
        //walkTool.SetActive(false);
        //arTool.SetActive(false);
        //moveTool.SetActive(true);
        // designTool.SetActive(true);
        States.isAr = true;
        States.isArRotate = true;
        //ceiling.SetActive(false);
        States.isShowRoom = false;
        States.isShowMenu = true;
        interior.transform.localScale = States.arScale;
        SceneManager.LoadScene(2);
    }

    private void handleDelete()
    {
        if ((States.selected != null) && (States.selected.tag != "floor" && States.selected.tag != "ceiling"))
        {
            Destroy(States.selected);
            States.selected = null;
        }
    }
    private void handleLight()
    {
        lightConfig.SetActive(!lightConfig.activeSelf);
    }
    //Tools functions
    private void handleWalk()
    {
        Debug.Log("Walking activated");
        States.isWalk = !States.isWalk;
        States.isRuler = false;
        States.removeRuler = true;
    }
    private void handleRuler()
    {
        Debug.Log("Ruler implemented");
        States.isRuler = !States.isRuler;
        States.isWalk = false;
        States.removeRuler = true;
    }

    private void handleBack()
    {
        furnitures.SetActive(false);
        categories.SetActive(true);
        showBackButton = false;
    }

    private void handleWallSet()
    {
        wallConfig.SetActive(!wallConfig.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        moveTool.SetActive(States.isAr);
        designTool.SetActive(States.isAr);

        walkTool.SetActive(!States.isAr);
        arTool.SetActive(!States.isAr);

        ceiling.SetActive(!States.isAr);

        gameObject.SetActive(States.isShowMenu);
        if (States.selected != null)
        {
            deleteButton.SetActive(true);
            if (States.selected.tag == "wall")
            {
                wallSetButton.SetActive(true);
            } else
            {
                wallSetButton.SetActive(false);
            }
            if (States.selected.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "lighting")
            {
                lightSetButton.SetActive(true);
            }
            else
            {
                lightSetButton.SetActive(false);
            }
            if ((States.selected.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "window") || (States.selected.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "door"))
            {
                sizeConfigButton.SetActive(true);
            }
            else
            {
                sizeConfigButton.SetActive(false);
            }
        }
        else
        {
            deleteButton.SetActive(false);
            wallSetButton.SetActive(false);
        }

        if(States.isWalk)
        {
            walkTool.GetComponent<Image>().color = new Color(36f / 255f, 229f / 255f, 31f / 255f, 0.6f);
        } else
        {
            walkTool.GetComponent<Image>().color = new Color(36f, 229f, 31f, 0f);

        }

        if (States.isRuler)
        {
            rulerTool.GetComponent<Image>().color = new Color(36f / 255f, 229f / 255f, 31f / 255f, 0.6f);
        }
        else
        {
            rulerTool.GetComponent<Image>().color = new Color(36f, 229f, 31f, 0f);
        }

        if (States.isArMove)
        {
            moveTool.GetComponent<Image>().color = new Color(36f / 255f, 229f / 255f, 31f / 255f, 0.6f);
        }
        else
        {
            moveTool.GetComponent<Image>().color = new Color(36f, 229f, 31f, 0f);

        }
    }
}
