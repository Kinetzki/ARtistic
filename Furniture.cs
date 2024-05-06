using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Furniture : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Camera maincamera;
    public Texture2D _texture;
    public string _category;
    public float _wallLength;

    public Texture2D texture
    {
        get
        {
            return _texture;
        }
        set
        {
            _texture = value;
            //Debug.Log(value.name);
        }
    }

    public string category
    {
        get
        {
            return _category;
        }
        set
        {
            _category = value;
            //Debug.Log(value.name);
        }
    }

    public float wallLength
    {
        get
        {
            return _wallLength;
        }
        set
        {
            _wallLength = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        wallLength = 1f;
        maincamera = Camera.main;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perform actions when a new scene is loaded
        maincamera = Camera.main;
    }
    //Add glow on selected
    public static void addMaterialOnSelect(GameObject ob)
    {
        if (ob != null)
        {
            Material m = Resources.Load<Material>("Materials/Selected");
            MeshRenderer renderer = ob.GetComponent<MeshRenderer>();
            renderer.material = m;
        }
        
    }
    //remove glow on selected
    public static void removeMaterialOnSelect(GameObject ob)
    {
        if (ob != null)
        {
            Texture2D t = ob.GetComponent<Furniture>().texture;
            Material m = new Material(Shader.Find("Standard"));
            m.mainTexture = t;
            if (ob.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "wall")
            {
                m.mainTextureScale = new Vector2((States.roomHeight / 2.44f) * 18, ob.transform.localScale.x * 8f);
                Debug.Log(States.roomHeight);
                Debug.Log(ob.GetComponent<Furniture>().wallLength);
                Debug.Log("Defaulting");
            }
            if (ob.tag == "floor")
            {
                m.mainTextureScale = new Vector2(States.roomWidth, States.roomLength);
            }
            MeshRenderer renderer = ob.GetComponent<MeshRenderer>();
            renderer.material = m;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log("clicked object");
        if (!States.isWalk && !States.isRuler && !States.isArMove)
        {
            //Debug.Log("Clicked");
            removeMaterialOnSelect(States.selected);
            transform.GetComponent<MeshCollider>().enabled = false;
            //Set active object
            if (States.selected == transform.gameObject)
            {
                States.selected = null;
                Debug.Log("Deselected Object");
                removeMaterialOnSelect(transform.gameObject);
            }
            else
            {
                if (transform.GetComponent<MeshFilter>().sharedMesh.name == "floor")
                {
                    Debug.Log("Clicked on floor, resetting object");
                    States.lastClicked = States.selected;
                }
                States.selected = transform.gameObject;
                addMaterialOnSelect(transform.gameObject);
                Debug.Log("Selected Object");
            }
            States.doRotate = false;
        }
    }

    public void OnDrag(PointerEventData e)
    {
        string n = transform.GetComponent<MeshFilter>().sharedMesh.name;
        if (!States.isWalk && !States.isRuler && n != "floor" && !States.isArMove)
        {
            States.selected = transform.gameObject;
            Debug.Log("Dragging");
            Ray ray = maincamera.ScreenPointToRay(e.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string name = gameObject.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0];
                if ((hit.collider.gameObject.tag == "wall" || hit.collider.gameObject.tag == "ceiling") && (name == "lighting" || name == "door" || name == "painting" || name == "window" || name == "curtainblinds" || name == "hangingkitchencabinet" || name == "mirror" || name == "socketswitches" || name == "suspended" || name == "clock" || name == "bathsshower"))
                {
                    if (!States.isWalk)
                    {
                        transform.position = hit.point;
                    }

                }
                else if ((name == "householdappliance" || name == "kitchenware" || name == "videotv" || name == "vasesandflowers" || name == "plant" || name == "candle" || name == "computer" || name == "curtainblinds") && (hit.collider.gameObject.tag != "floor"))
                {
                    if (!States.isWalk)
                    {
                        transform.position = hit.point;
                    }
                }
                else if((name != "lighting") && (hit.collider.gameObject.tag == "floor" || name == "rug" || name == "bathsshower") && (name != "lighting" && name != "painting" && name != "window")) {
                    if (!States.isWalk)
                    {
                        transform.position = hit.point;
                    }
                }
            }
        } else
        {
            if ( n == "floor" && (States.selected != States.lastClicked) && !States.doRotate && !States.isArMove)
            {
                States.selected = States.lastClicked;
                Debug.Log("Dragging floor");
                States.doRotate = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (!States.isWalk && !States.isRuler && !States.isArMove)
        {
            transform.GetComponent<MeshCollider>().enabled = true;
            Debug.Log("Set by furniture");
            States.doRotate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
