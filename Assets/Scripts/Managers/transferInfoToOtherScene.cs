using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transferInfoToOtherScene : MonoBehaviour
{
    public static int playerHealth;

    public static int fireLevel;
    public static float rageModifier;
    public static float playerDefense;
    public static int playerStrength;
    public static float playerSpeed;

    public List<int> popIncome;
    public List<GameObject> transferObjects;
    
    public static Dictionary<string, List<int>> townInformation= new Dictionary<string,List<int>>();

    public static int totalIncome;
    public static int availableSacrifice;

    public float sacrificeModifier;
    float totalPop;


    // Start is called before the first frame update
    void Awake()
    {

        sacrificeModifier = 1f;
        transferObjects.Add(GameObject.FindGameObjectWithTag("PlayerC"));
        transferObjects.Add(GameObject.FindGameObjectWithTag("MapInfo"));
        transferObjects.Add(GameObject.FindGameObjectWithTag("GameManager"));

        if (transferObjects[1] != null)
        {
            if (townInformation.ContainsKey(transferObjects[1].GetComponent<info>().townName) == false)
            {
                popIncome.Add(0);
                popIncome.Add(0);
                popIncome.Add(0);
                townInformation.Add(transferObjects[1].GetComponent<info>().townName, popIncome);
            }
        }


        DontDestroyOnLoad(this);
        //DontDestroyOnLoad(transferObjects[1]);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transferObjects[0] == null)
            transferObjects[0] = GameObject.FindGameObjectWithTag("PlayerC");
        if (transferObjects[1] == null)
            transferObjects[1] = GameObject.FindGameObjectWithTag("MapInfo");

        if (transferObjects[1] != null)
        {
            
            //Player information
            playerHealth = transferObjects[0].GetComponent<Player>().GetMaxHealth();


            fireLevel= transferObjects[0].GetComponentInChildren<Player>().fireLvl;
            rageModifier= transferObjects[0].GetComponentInChildren<Player>().rageModifier;
            playerDefense= transferObjects[0].GetComponentInChildren<Player>().defense;
            playerStrength= transferObjects[0].GetComponentInChildren<Player>().strength;
            playerSpeed= transferObjects[0].GetComponentInChildren<Player>().playerSpeed;



            //Town information
            popIncome[0] = transferObjects[1].GetComponent<info>().population;
            popIncome[1]=transferObjects[1].GetComponent<info>().income;


            if (transferObjects[1].GetComponent<info>().surrender)
            {

                popIncome[2] = 1;
            }
            else
            {
                popIncome[2] = 0;
            }


            townInformation[transferObjects[1].GetComponent<info>().townName] = popIncome;

            totalIncome = 0;
            totalPop=0;

            foreach(string key in townInformation.Keys) {
                totalIncome += townInformation[key][1];
                 totalPop += (float)townInformation[key][0];


            }
            //Debug.Log("Modifier==" + sacrificeModifier);

            totalPop *= sacrificeModifier;
            //Debug.Log("Total pop==" + totalPop);
            availableSacrifice = (int)totalPop;


        }

    }
}
