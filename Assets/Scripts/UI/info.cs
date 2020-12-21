using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info : MonoBehaviour
{
    public Text townIncome;
    float timer;
    public List<GameObject> houseList = new List<GameObject>();
    int houseCount;
    int windmillCount;
    public int foodIncome;
    public int population;
    public int income;
    public string townName;

    //This section determines town morale, meaning the town will surrender if morale is too low
    public int numDeadSoldiers;
    public int numDestroyedBarracks;
    public int numDestroyedHouses;
    public int numDestroyedCastles;
    public int capturedZones;
    public int numDestroyedEconomic;
    public int numDestroyedCivic;
    public float maxTownMorale;

    public float surrenderNum;
    public float townMorale;

    public bool surrender;


    // Start is called before the first frame update
    void Start()
    {   //sets town's morale
        setTownMorale(townMorale);

        surrender = false;

        //sets the number to which the town will surrender if morale goes too low
        setSurrenderNum(maxTownMorale);


        townIncome = GameObject.Find("income").GetComponent<Text>();
        timer = Time.fixedTime + 30;
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("House"))
        {
            houseList.Add(fooObj);
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Church"))
        {
            houseList.Add(fooObj);
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Tower"))
        {
            houseList.Add(fooObj);
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Stone House"))
        {
            houseList.Add(fooObj);

        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Windmill"))
        {
            houseList.Add(fooObj);

        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Farm"))
        {
            houseList.Add(fooObj);

        }
    }

    // Update is called once per frame
    void Update()
    {
        updateMorale();

        if (getTownMorale() < getSurrenderNum()) {

            surrender = true;
          
            }




        int i = 0;
        int f = 0;
        for (int x = 0; x < houseList.Count; x++) {
            if (houseList[x] == null)
            {
                houseList.Remove(houseList[x]);
                break;

            }
            if (houseList[x].tag == "House" || houseList[x].tag == "Stone House")
                i++;
            else if (houseList[x].tag == "Windmill") {
                f++;
            }
            else if (houseList[x].tag == "Farm")
            {
                f += 3;
            }



        }
        houseCount = i;


        population = houseCount * 5;
        foodIncome = (f * 10);
        income = (population * 5) * ((foodIncome / 3) + 1);

        townIncome.text = "Town income: " + (income).ToString() + "G";




    }
    //town morale setter
    void setTownMorale(float morale) {
        townMorale = maxTownMorale - morale;

    }

    //town morale getter
    float getTownMorale() {
        return townMorale;
     
       }

    void setSurrenderNum(float maxMorale) {
        float surrenderPercentage = 0.3f;
        surrenderNum = maxMorale * surrenderPercentage;
     
      
        }
    float getSurrenderNum() {

        return surrenderNum;
    }


    void updateMorale() {
        //Sets a level of importance whenever the player destroys or captures parts of the town
        int soldierWeight = numDeadSoldiers * 2;
        int barracksWeight = numDestroyedBarracks * 4;
        float housesWeight = numDestroyedHouses / 2f;
        int castleWeight = numDestroyedCastles * 50;
        int zoneWeight = capturedZones * 20;
        float economicWeight = numDestroyedEconomic / 2;
        float civicWeight = numDestroyedCivic * 10;

        float total = soldierWeight + barracksWeight + housesWeight + castleWeight + zoneWeight + economicWeight+civicWeight;

        setTownMorale(total);


        
    
    
    }


}

