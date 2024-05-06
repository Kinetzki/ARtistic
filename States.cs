using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class States : MonoBehaviour
{
    public Camera maincamera;
    private bool isDragging;
    public static string _activeCategory;
    public static GameObject _selected;
    private double prevVal;
    public static bool _doRotate;
    public static bool _isWalk = false;
    public static bool _isRuler = false;
    public static bool _isArRotate = false;
    public static bool _isArMove = false;
    public static bool _isAr = false;
    public static GameObject _lastClicked;
    public static float _roomWidth;
    public static float _roomLength;
    public static float _roomHeight;
    private static bool _isShowSettings = true;
    private static bool _isShowMenu = false;
    private static bool _isShowRoom = false;
    private static bool _isShowInterior = true;
    public GameObject interior;
    [SerializeField]
    private float speed = 5f;
    //camera movement
    public float rotationSpeed = 4f;
    private Vector3 lastMousePosition;
    private GameObject sphere;
    public static GameObject s1;
    public static GameObject s2;
    public static bool _removeRuler;
    //ar placement distance
    [SerializeField]
    private float distance = 500.00f;
    public static Vector3 _arScale = new Vector3(1f, 1f, 1f);
    private double currentX;
    public static float _yRotation = 0f;
    public static float roomWidth
    {
        get
        {
            return _roomWidth;
        }
        set
        {
            _roomWidth = value;
        }
    }
    public static float roomHeight
    {
        get
        {
            return _roomHeight;
        }
        set
        {
            _roomHeight = value;
        }
    }
    public static float yRotation
    {
        get
        {
            return _yRotation;
        }
        set
        {
            _yRotation = value;
        }
    }
    public static Vector3 arScale
    {
        get
        {
            return _arScale;
        }
        set
        {
            _arScale = value;
        }
    }
    public static float roomLength
    {
        get
        {
            return _roomLength;
        }
        set
        {
            _roomLength = value;
        }
    }

    public static bool removeRuler
    {
        get
        {
            return _removeRuler;
        }
        set
        {
            _removeRuler = value;
            Destroy(s1);
            Destroy(s2);
        }
    }
    public static bool isShowSettings
    {
        get
        {
            return _isShowSettings;
        }
        set
        {
            _isShowSettings = value;
        }
    }

    public static bool isAr
    {
        get
        {
            return _isAr;
        }
        set
        {
            _isAr = value;
        }
    }

    public static bool isArMove
    {
        get
        {
            return _isArMove;
        }
        set
        {
            _isArMove = value;
        }
    }
    public static bool isShowInterior
    {
        get
        {
            return _isShowInterior;
        }
        set
        {
            _isShowInterior = value;
        }
    }
    public static bool isShowRoom
    {
        get
        {
            return _isShowRoom;
        }
        set
        {
            _isShowRoom = value;
        }
    }
    public static bool isShowMenu
    {
        get
        {
            return _isShowMenu;
        }
        set
        {
            _isShowMenu = value;
        }
    }
    public static GameObject lastClicked
    {
        get
        {
            return _lastClicked;
        }
        set
        {
            _lastClicked = value;
        }
    }

    public static bool isArRotate
    {
        get
        {
            return _isArRotate;
        }
        set
        {
            _isArRotate = value;
        }
    }
    public static bool isWalk
    {
        get
        {
            return _isWalk;
        }
        set
        {
            _isWalk = value;
            selected = null;
        }
    }

    public static bool doRotate { get
        {
            return _doRotate;
        } 
        set
        {
            Debug.Log(value + " Set to");
            _doRotate = value;
        }
    }

    public static string activeCategory
    {
        get
        {
            return _activeCategory;
        }
        set
        {
            _activeCategory = value;
            Debug.Log(value);
        }
    }
    public static string SeparateWords(string input)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsUpper(c))
            {
                result.Append(' ');
            }
            result.Append(c);
        }
        return result.ToString().Trim();
    }

    public static GameObject selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            if (value == null)
            {
                MenuPanelManager.showVariations = false;
                MenuPanelManager.msg = "";
            }
            else
            {
                Debug.Log(selected.name);
                MenuPanelManager.showVariations = false;
                MenuPanelManager.showVariations = true;
                MeshFilter f = value.GetComponent<MeshFilter>();
                string msg = f.sharedMesh.name + "\nDimensions: " + Math.Round(f.sharedMesh.bounds.size.x, 2) + " by " + Math.Round(f.sharedMesh.bounds.size.z, 2) + " by " + Math.Round(f.sharedMesh.bounds.size.y, 2);
                MenuPanelManager.msg = msg;
                yRotation = _selected.transform.localEulerAngles.y;
            }
            
        }
    }

    public static bool isRuler
    {
        get
        {
            return _isRuler;
        }
        set
        {
            _isRuler = value;
        }
    }
    void Start()
    {
        sphere = Resources.Load<GameObject>("Models/Prefabs/ruler");
        SceneManager.sceneLoaded += OnSceneLoaded;
        maincamera = Camera.main;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perform actions when a new scene is loaded
        maincamera = Camera.main;
    }
    private void rotateSelected()
    {
        if (_selected != null)
        {
            if (doRotate)
            {
                if ((_selected.GetComponent<MeshFilter>().sharedMesh.name == "floor") && !isArRotate)
                {
                    return;
                }
                if ((_selected.GetComponent<MeshFilter>().sharedMesh.name == "floor") && isArRotate)
                {
                    interior.transform.localEulerAngles = new Vector3(interior.transform.localEulerAngles.x, yRotation, interior.transform.localEulerAngles.z);
                } else
                {
                    selected.transform.localEulerAngles = new Vector3(selected.transform.localEulerAngles.x, yRotation, selected.transform.localEulerAngles.z);
                    Debug.Log(selected.name + " Rotating");
                    if (Mathf.Abs((90f - yRotation)) <= 20f)
                    {
                        Debug.Log((Mathf.Abs(90f - yRotation)));
                        selected.transform.localEulerAngles = new Vector3(selected.transform.localEulerAngles.x, 90f, selected.transform.localEulerAngles.z);
                    }

                    if (Mathf.Abs((0f - yRotation)) <= 20f)
                    {
                        Debug.Log((Mathf.Abs(90f - yRotation)));
                        selected.transform.localEulerAngles = new Vector3(selected.transform.localEulerAngles.x, 0f, selected.transform.localEulerAngles.z);
                    }

                    if (Mathf.Abs((270f - yRotation)) <= 20f)
                    {
                        Debug.Log((Mathf.Abs(90f - yRotation)));
                        selected.transform.localEulerAngles = new Vector3(selected.transform.localEulerAngles.x, 270f, selected.transform.localEulerAngles.z);
                    }

                    if (Mathf.Abs((180f - yRotation)) <= 20f)
                    {
                        Debug.Log((Mathf.Abs(90f - yRotation)));
                        selected.transform.localEulerAngles = new Vector3(selected.transform.localEulerAngles.x, 180f, selected.transform.localEulerAngles.z);
                    }
                    //selected.transform.Rotate(Vector3.up, (float)(yRotation * Time.deltaTime));

                    MenuPanelManager.msg = _selected.transform.localEulerAngles.y.ToString() + " degrees";
                }
            }
        }
    }

    private void showDistance()
    {
        if (isRuler)
        {
            Vector3 pos1 = s1.transform.position;
            Vector3 pos2 = s2.transform.position;
            float v = Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2));
            float dist = v;
            MenuPanelManager.msg = dist.ToString() + " meters";
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            prevVal = Input.mousePosition.x;
            lastMousePosition = Input.mousePosition;
            //if isRuler instantiate 2 spheres
            if (isRuler)
            {
                if (s1 == null && s2 == null)
                {
                    Debug.Log("triggered");
                    s1 = Instantiate(sphere);
                    s2 = Instantiate(sphere);
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("clicked on gameobject");
                    s1.transform.position = hit.point;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse let go");
            isDragging = false;
        }
        if (isDragging)
        {
            currentX = Input.mousePosition.x;
            if (_isWalk && !isArRotate && !isArMove)
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

                // Rotate the camera based on mouse movement
                float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime * 0.6f;
                float rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime * 0.6f;

                Camera.main.transform.Rotate(Vector3.left, rotationX);
                Camera.main.transform.Rotate(Vector3.up, rotationY, Space.World);

                // Update last mouse position for the next frame
                lastMousePosition = Input.mousePosition;
            } 
            if (isRuler)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("clicked on gameobject");
                    s2.transform.position = hit.point;
                    showDistance();
                }
            }
            //if (isArMove)
            //{
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Vector3 placementPosition = ray.origin + ray.direction * 2f;
                //placementPosition.z = placementPosition.z + (interior.transform.localScale.z * 5);
                //placementPosition.y = placementPosition.y - (interior.transform.localScale.y * 0.5f);
                //interior.transform.position = placementPosition;
            //}
            else
            {
                if(yRotation > 360f)
                {
                    yRotation = 0f;
                }
                if (yRotation < 0f)
                {
                    yRotation = 360f;
                }
                if (currentX > prevVal)
                {
                    //Debug.Log("Dragging right");
                    yRotation -= 2f;
                    rotateSelected();
                }
                else if (currentX < prevVal)
                {
                    // Debug.Log("Dragging left");
                    yRotation += 2f;
                    rotateSelected();
                }
                prevVal = currentX;
            }
            
        }
    }
}
