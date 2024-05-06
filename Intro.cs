using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private GameObject designBtn;
    private GameObject assetsBtn;
    private GameObject arDesignBtn;
    void Start()
    {
        designBtn = transform.GetChild(0).gameObject;
        assetsBtn = transform.GetChild(2).gameObject;
        arDesignBtn = transform.GetChild(1).gameObject;
        designBtn.GetComponent<Button>().onClick.AddListener(handleDesign);
        arDesignBtn.GetComponent<Button>().onClick.AddListener(handleAr);
        assetsBtn.GetComponent<Button>().onClick.AddListener(handleAssets);
    }
    private void handleDesign()
    {
        States.isShowSettings = true;
        States.isShowRoom = true;
        States.isShowInterior = true;
        SceneManager.LoadScene(1);
    }

    private void handleAr ()
    {
        States.isShowSettings = true;
        States.isShowRoom = true;
        States.isShowInterior = true;
        States.isArMove = false;
        States.isArRotate = true;
        States.isAr = true;
        //walkTool.SetActive(false);
        //arTool.SetActive(false);
        //moveTool.SetActive(true);
        //designTool.SetActive(true);
        //ceiling.SetActive(false);
        States.isShowMenu = false;

        SceneManager.LoadScene(5);
    }
    private void handleAssets()
    {
        SceneManager.LoadScene(6);
    }

    void Update()
    {
        
    }
}
