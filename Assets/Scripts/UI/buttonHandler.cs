using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class buttonHandler : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    
    public GameObject CanvasHUD;
    CanvasHUD HUD;
    public static int healthLvl;
    public static int speedLvl;
    public static int strengthLvl;
    public static int fireLvl;
    public static int defenseLvl;
    public static int rageLvl;
    public static int evolveLvl;

    string[] attributes = {"Health","Speed", "Defense", "Fire","Strength","Rage","Evolve" };
    public static Dictionary<string,List<int>> requirements=new Dictionary<string, List<int>>();

    GameObject playerInformation;
    public int humanSacrifice;
    public int gold;

    GameObject childText;
    private bool mouseHovering;
    UIDescription button;

    private string name;
    private string cost;
    private string descritption;

    int incrementGold;
    int incrementSacrifices;

    string attribute = "";
    void Start()
    {
        incrementGold = 100;
        incrementSacrifices = 10;
        HUD = CanvasHUD.GetComponent<CanvasHUD>();
        mouseHovering = false;
        childText = GameObject.Find("Text");
        humanSacrifice = transferInfoToOtherScene.availableSacrifice;
        gold = transferInfoToOtherScene.totalIncome;

        Text txt = transform.Find("Text").GetComponent<Text>();
        if (gameObject.tag == "infoManager")
        {
            txt.text = "Gold Available: " + gold + "\n" + "Human Sacrifices: " + humanSacrifice;
        }
        if (gameObject.tag == "Health")
        {
            attribute = "Health";
            name = "Increased Health";
            descritption = "Increase your dragon's hitpoints";
            txt.text = healthLvl.ToString();
        }
        else if (gameObject.tag == "Speed")
        {
            attribute = "Speed";
            descritption = "Increase your dragon's speed";
            name = "Increase Speed";
       
            txt.text = speedLvl.ToString();
        }
        else if (gameObject.tag == "Attack")
        {
            attribute = "Strength";
            descritption = "Increase your dragon's attack";
            name = "Increase Attack Power";
            txt.text = strengthLvl.ToString();
        }
        else if (gameObject.tag == "Fire")
        {
            attribute = "Fire";
            descritption = "Increase your dragon's fire power and rate of spreading";
            name = "Increase Fire Power";
            txt.text = fireLvl.ToString();
        }
        else if (gameObject.tag == "Defense")
        {
            attribute = "Defense";
            descritption = "Reduces chances of being hit";
            name = "Increase Defense";
            txt.text = defenseLvl.ToString();
        }
        else if (gameObject.tag == "Rage")
        {
            attribute = "Rage";
            descritption = "Dragon is less likely to rage";
            name = "Reduce Rage Chance";
            txt.text = rageLvl.ToString();
        }
        else if (gameObject.tag == "Evolve")
        {
            attribute = "Evolve";
            descritption = "Dragon becomes stronger overall";
            name = "Evolve to next stage";
            txt.text = evolveLvl.ToString();
        }




        if (!requirements.ContainsKey("Evolve")) {

            foreach (string att in attributes) {
                List<int> listGoldSacrifice = new List<int>();
                listGoldSacrifice.Add(100);
                listGoldSacrifice.Add(10);
                requirements.Add(att, listGoldSacrifice);
            
            
            
            }
          

        }
        cost = "Requirements: \n Gold: " + requirements[attribute][0].ToString() + "Sacrifices:" + requirements[attribute][1].ToString();


    }







    public void setAttribute(string attribute) {

        int lvl = 1;
        Text txt = transform.Find("Text").GetComponent<Text>();
        gold = transferInfoToOtherScene.totalIncome;
        Debug.Log("setting attribute...");
        if (gold >= requirements[attribute][0] && humanSacrifice >= requirements[attribute][1])
        {
       
            if (attribute == "Health")
            {
               
                healthLvl += lvl;
                txt.text = healthLvl.ToString();
                updateRequirements(healthLvl);

            }
            else if (attribute == "Speed")
            {
                speedLvl += lvl;
                txt.text = speedLvl.ToString();
                updateRequirements(speedLvl);
            }
            else if (attribute == "Strength")
            {
                strengthLvl += lvl;
                txt.text = strengthLvl.ToString();
                updateRequirements(strengthLvl);
            }
            else if (attribute == "Fire")
            {
                fireLvl += lvl;
                txt.text = fireLvl.ToString();
                updateRequirements(fireLvl);
            }
            else if (attribute == "Defense")
            {
                defenseLvl += lvl;
                txt.text = defenseLvl.ToString();
                updateRequirements(defenseLvl);
            }
            else if (attribute == "Rage")
            {
                rageLvl += lvl;
                txt.text = rageLvl.ToString();
                updateRequirements(rageLvl);
            }
            else if (attribute == "Evolve")
            {

                evolveLvl += lvl;
                updateRequirements(evolveLvl);
                txt.text = evolveLvl.ToString();
            }





            //Debug.Log("Updating total Income + sacrifices...");
            transferInfoToOtherScene.totalIncome -= requirements[attribute][0];
            transferInfoToOtherScene.availableSacrifice -= requirements[attribute][1];


            //This breaks ui buttons on overworld menu, need to fix
            /*
            for(int x= requirements[attribute][1]; x > 0; ) { 
                foreach(string key in transferInfoToOtherScene.townInformation.Keys) {
                    transferInfoToOtherScene.townInformation[key][1] -= x;
                    x--;
                    //Debug.Log("Iterating through town keys...");
                    if (x <= 0)
                        break;
                
                }



            }
            */

            HUD.HideToolTip();


            //Debug.Log("Adding requirements...");
            //requirements[attribute][0] += requirements[attribute][0];
            //requirements[attribute][1] += requirements[attribute][1];
            cost = "Requirements: \n Gold: " + requirements[attribute][0].ToString() + "Sacrifices:" + requirements[attribute][1].ToString();
            HUD.ShowToolTip(transform.position, name, cost, descritption);
        }


    
    }


    void Update()
    {
      

        humanSacrifice = transferInfoToOtherScene.availableSacrifice;
        gold = transferInfoToOtherScene.totalIncome;
        //Debug.Log(gold);

        Text txt = transform.Find("Text").GetComponent<Text>();
        if (gameObject.tag == "infoManager")
        {
            txt.text = "Gold Available: " + gold + "\n" + "Human Sacrifices: " + humanSacrifice;
        }
        if (gameObject.tag != "infoManager"&& gameObject.tag != "Tool Tip")
        {
            RawImage image = gameObject.GetComponent<RawImage>();
            var tempColor = image.color;


            if (gameObject.tag == "Health")
            {
                txt.text = healthLvl.ToString();

            }
            if (gameObject.tag == "Speed")
            {

                txt.text = speedLvl.ToString();
            }
            if (gameObject.tag == "Attack")
            {

                txt.text = strengthLvl.ToString();
            }
            if (gameObject.tag == "Fire")
            {

                txt.text = fireLvl.ToString();
            }
            if (gameObject.tag == "Defense")
            {
                txt.text = defenseLvl.ToString();
            }
            if (gameObject.tag == "Rage")
            {

                txt.text = rageLvl.ToString();
            }
            if (gameObject.tag == "Evolve")
            {

                txt.text = evolveLvl.ToString();
            }


            if (healthLvl >= 1 && gameObject.name == "HealthU")
            {
                updateRequirements(healthLvl);
                tempColor.a = 1f;
                
            }
            else if (defenseLvl >= 1 && gameObject.name == "DefenseU")
            {
                updateRequirements(defenseLvl);
                tempColor.a = 1f;
            }
            else if (rageLvl >= 1 && gameObject.name == "RageU")
            {
                updateRequirements(rageLvl);
                tempColor.a = 1f;
            }
            else if (evolveLvl >= 1 && gameObject.name == "evolve")
            {
                updateRequirements(evolveLvl);
                tempColor.a = 1f;
            }
            else if (strengthLvl >= 1 && gameObject.name == "AttackU")
            {
                updateRequirements(strengthLvl);
                tempColor.a = 1f;
            }
            else if (fireLvl >= 1 && gameObject.name == "fireUpgrade")
            {
                updateRequirements(fireLvl);
                tempColor.a = 1f;
            }
            else if (speedLvl >= 1 && gameObject.name == "SpeedU")
            {
                updateRequirements(speedLvl);
                tempColor.a = 1f;
            }
            else
            {

                tempColor.a = 0.0f;

            }










            cost = "Requirements: \n Gold: " + requirements[attribute][0].ToString() + "Sacrifices:" + requirements[attribute][1].ToString();


            //Debug.Log("Setting Raw image to new color...");
            gameObject.GetComponent<RawImage>().color = tempColor;
            //Debug.Log("Raw Image set...");
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        HUD.ShowToolTip(transform.position,name,cost,descritption);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HUD.HideToolTip();
    }

 

    void updateRequirements(int lvl)
    {




        requirements[attribute][0] = incrementGold * (lvl+1);
        requirements[attribute][1] = incrementSacrifices * (lvl+1);

    }


}
