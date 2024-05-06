using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asset : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public string _asset;
    public string _category;
    public Mesh _prefab;
    private GameObject model;
    private Camera maincamera;
    public GameObject interior;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        maincamera = Camera.main;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perform actions when a new scene is loaded
        maincamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData e)
    {
        States.isWalk = false;
        States.isRuler = false;
        States.removeRuler = true;
        States.isArMove = false;

        States.doRotate = false;
        if (_asset.Split("_")[0] == "lighting")
        {
            model = Instantiate(AssetManager.lightPrefab, transform.position, Quaternion.identity);
        } else
        {
            model = Instantiate(AssetManager.modelPrefab, transform.position, Quaternion.identity);
        }
        Vector3 modelScale = model.transform.localScale;
        Vector3 interiorScale = interior.transform.localScale;
        model.transform.localScale = new Vector3(modelScale.x * interiorScale.x, modelScale.y * interiorScale.y, modelScale.z * interiorScale.z);
        model.GetComponent<MeshCollider>().enabled = false;
        model.transform.parent = interior.transform;
        MeshFilter meshF = model.GetComponent<MeshFilter>();
        meshF.mesh = _prefab;
        MeshRenderer renderer =  model.GetComponent<MeshRenderer>();
        MeshCollider collider = model.GetComponent<MeshCollider>();
        collider.sharedMesh = _prefab;
        Material furnit = new Material(Shader.Find("Standard"));
        Texture2D texture = Resources.Load<Texture2D>("Models/Furnitures/" + _category + "/" + _asset + "/" + _asset + "_texture");
        furnit.mainTexture = texture;
        if (model.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0] == "wall")
        {
            model.tag = "wall";
            model.transform.localScale = new Vector3(1f, States.roomHeight / 2.44f, 1f);
            furnit.mainTextureScale = new Vector2((States.roomHeight / 2.44f) * 18, 8f);
            Debug.Log("Setting wall");
        }
        renderer.material = furnit;
        Furniture f = model.GetComponent<Furniture>();
        f.category = category;
        f.texture = texture;
        Furniture.removeMaterialOnSelect(States.selected);
    }

    public void OnPointerUp(PointerEventData e)
    {
        model.GetComponent<MeshCollider>().enabled = true;
        States.selected = model;
        Debug.Log("Set by assets");
        States.doRotate = true;
    }

    public void OnDrag(PointerEventData e)
    {
        if (_prefab != null)
        {
            Ray ray = maincamera.ScreenPointToRay(e.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string name = model.GetComponent<MeshFilter>().sharedMesh.name.Split("_")[0];
                if ((hit.collider.gameObject.tag == "wall" || hit.collider.gameObject.tag == "ceiling") && (name == "lighting" || name == "door" || name == "painting" || name == "window" || name == "curtainblinds" || name == "hangingkitchencabinet" || name == "mirror" || name == "socketswitches" || name == "suspended" || name == "clock" || name == "bathsshower"))
                {
                    model.transform.position = hit.point;
                }
                else if ((name == "householdappliance" || name == "kitchenware" || name == "videotv" || name == "vasesandflowers" || name == "plant" || name == "candle" || name == "computer" || name == "curtainblinds") && (hit.collider.gameObject.tag != "floor"))
                {
                    model.transform.position = hit.point;
                    model.transform.parent = hit.collider.gameObject.transform;
                } 
                else if ((name != "lighting") && (hit.collider.gameObject.tag == "floor" || name == "rug" || name == "bathsshower") && (name != "lighting" && name != "painting" && name != "window"))
                {
                    model.transform.position = hit.point;
                }
            }
        }
    }

    //Set the Asset name
    public string asset
    {
        get
        {
            return _asset;
        }
        set
        {
            _asset = value;
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
        }
    }

    public Mesh prefab
    {
        get
        {
            return _prefab;
        }
        set
        {
            _prefab = value;
        }
    }
    void Update()
    {
        
    }
}
