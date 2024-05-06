using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isMenuHidden = false;
    [SerializeField]
    private GameObject settingsPanel;
    public GameObject hideBtn;
    public GameObject resetBtn;
    public GameObject backBtn;
    public GameObject interior;
    public GameObject menuPanel;
    public GameObject roomConfig;
    public GameObject eventManager;
    void Start()
    {
        eventManager.SetActive(true);
        hideBtn.GetComponent<Button>().onClick.AddListener(handleHide);
        gameObject.GetComponent<Button>().onClick.AddListener(handleSettingsPanel);
        resetBtn.GetComponent<Button>().onClick.AddListener(handleReset);
        backBtn.GetComponent<Button>().onClick.AddListener(handleBack);
    }
    private void handleBack()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("interior");
        if (objects.Length > 0)
        {
            // Destroy each object
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
        States.isAr = false;
        States.selected = null;
        States.isArMove = false;
        States.isArRotate = false;
        States.isShowMenu = false;
        States.isShowRoom = false;
        States.isShowInterior = false;
        States.isWalk = false;
        eventManager.SetActive(false);
        States.isShowSettings = false;
        //handleReset();
        SceneManager.LoadScene(0);
    }
    private void handleReset ()
    {
        int children = interior.transform.childCount;
        for (int i = 3; i < children; i++)
        {
            Destroy(interior.transform.GetChild(i).gameObject);
        }
    }
    private void handleSettingsPanel ()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    private void handleHide()
    {
        isMenuHidden = !isMenuHidden;
        if (isMenuHidden)
        {
            States.isWalk = true;
        } else
        {
            States.isWalk = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!roomConfig.activeSelf)
        {
            menuPanel.SetActive(!isMenuHidden);
        }
        gameObject.SetActive(States.isShowSettings);
    }
}
