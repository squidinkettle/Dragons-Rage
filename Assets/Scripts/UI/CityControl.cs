using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CityControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string cityName;
    public string income;
    public bool isTraversable=false;
    public string population;
    public GameObject CanvasHUD;
    CanvasHUD HUD;
    public List<GameObject>nodes=new List<GameObject>();
    public GameObject lastTownInfo;




    // Start is called before the first frame update
    void Start()
    {
        HUD = CanvasHUD.GetComponent<CanvasHUD>();
        lastTownInfo=GameObject.FindWithTag("GameManager");

        var ifNameMatchesDictionary = transferInfoToOtherScene.townInformation.ContainsKey(cityName);

        if (ifNameMatchesDictionary)
        {
            population = transferInfoToOtherScene.townInformation[cityName][0].ToString();
            income = transferInfoToOtherScene.townInformation[cityName][1].ToString();



        }
        else
        {
            population = "??";
            income = "??";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");

        string incomePopulation = "Income: " + income + "\n Population:" + population;
        HUD.ShowToolTip(transform.position, cityName, incomePopulation, "");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUD.HideToolTip();
    }

    public void nextLevel(int num)
    {
        Debug.Log("is traversable" + isTraversable);
        if (isTraversable)
        {
            SceneManager.LoadScene(num);

            
        }



    }

}
