using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static string[] folders = new string[] {"Walls",
"Doors",
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
"Mirrors"};
    public static GameObject assets;
    public static GameObject categories;
    public static GameObject modelPrefab;
    public static GameObject lightPrefab;

    void Start()
    {
        modelPrefab = Resources.Load<GameObject>("Models/Prefabs/Cube");
        lightPrefab = Resources.Load<GameObject>("Models/Prefabs/light_object");
        assets = transform.GetChild(2).gameObject;
        categories = transform.GetChild(1).gameObject;
    }

    //Get assets from a folder
    public static Mesh[] getMeshes(string folder)
    {
        Mesh[] meshes = Resources.LoadAll<Mesh>("Models/Furnitures/" + folder);
        return meshes;
    }

    //Show assets
    public static void showAssets(string category)
    {
        Debug.Log("Show Assets " + category);
        categories.SetActive(false);
        LoadAssets l = assets.GetComponent<LoadAssets>();
        l.category = category;
        assets.SetActive(true);
    }
    
    void Update()
    {
        
    }
}
