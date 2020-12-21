using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OverworldManager : MonoBehaviour
{
    Vector3 newPosition;            //Directs the player to a new position
    Vector3 playerCityPosition;     //Current player position
    public GameObject playerIcon;   //Player's icon
    public int playerPosition;      //sets what city's posotion will define player's position
    public List<GameObject> cities= new List<GameObject>(); //List of cities in map
    public int turns;               //Turns in game
    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        playerPosition = 0;
        playerIcon.transform.position = cities[playerPosition].transform.position;
        newPosition = cities[playerPosition].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetMouseButtonDown(0))
        {


            if (!isMoving)
            {
                newPosition = Input.mousePosition;
                GameObject selectedCity;
                selectedCity = IsPointerOnGUI();
                CanPlayerTravel(selectedCity);
                canPlayerAttack();
            }



        }
        newPosition = cities[playerPosition].transform.position;
        if (playerIcon.transform.position != newPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        playerIcon.transform.position = Vector2.MoveTowards(playerIcon.transform.position,newPosition,100*Time.deltaTime);
    }

    //Searches for clicked game object
    GameObject IsPointerOnGUI()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, hits); //Raycast into GUI

        foreach (RaycastResult hit in hits) //iterate through each hit from raycast
        {
            GameObject hitGOB = hit.gameObject; //get the gameobject of what was hit.
            Debug.Log(hitGOB.GetComponent<CityControl>().cityName);
            return hitGOB;
           
        }

        return null;
    }
    void canPlayerAttack() {
        var cityOBJ = cities[playerPosition];
        var nameOfPlayerPositionCity = cities[playerPosition].GetComponent<CityControl>().cityName;
        var cityInformation = transferInfoToOtherScene.townInformation;

        Debug.Log("City:" + nameOfPlayerPositionCity);
        Debug.Log("TraverseStatus:"+cityOBJ.GetComponent<CityControl>().isTraversable);
       

        if (cityInformation.ContainsKey(nameOfPlayerPositionCity)){
            if (cityInformation[nameOfPlayerPositionCity][2] == 0)
            {
                cities[playerPosition].GetComponent<CityControl>().isTraversable = true;
            }

        }
        else {
            cities[playerPosition].GetComponent<CityControl>().isTraversable = true;
        }



    }

    //Views if player can go to the next game object
    void CanPlayerTravel(GameObject selectedCity)
    {
        //iterates through the nodes of the current city. The next city should have current city as a node
        for(int x = 0; x < cities[playerPosition].GetComponent<CityControl>().nodes.Count; x++)
        {

            var cityWhereThePlayerIs = cities[playerPosition].GetComponent<CityControl>().nodes[x];
            var nameOfCityWherePlayerIs = cities[playerPosition].GetComponent<CityControl>().cityName;

            if (cityWhereThePlayerIs == selectedCity)
            {
                //Debug.Log(selectedCity.GetComponent<CityControl>().isTraversable);
                int checkIfcityWherePlayerIsSurrendered = 0;
                int checkifSelectedCityHasSurrendered = 0;
                if (!transferInfoToOtherScene.townInformation.ContainsKey(nameOfCityWherePlayerIs)){

                  
                    checkIfcityWherePlayerIsSurrendered = 0;
                }else
                {

                    checkIfcityWherePlayerIsSurrendered = transferInfoToOtherScene.townInformation[nameOfCityWherePlayerIs][2];
                }
                if (!transferInfoToOtherScene.townInformation.ContainsKey(selectedCity.GetComponent<CityControl>().cityName))
                {
                    //Debug.Log("No key was found on other position");
                    checkifSelectedCityHasSurrendered = 0;
                }
                else
                {

                    checkifSelectedCityHasSurrendered = transferInfoToOtherScene.townInformation[selectedCity.GetComponent<CityControl>().cityName][2];

                }


                if (checkIfcityWherePlayerIsSurrendered == 1 || checkifSelectedCityHasSurrendered == 1)
                {
                    //Updates the player position integer
                    for (int y = 0; y < cities.Count; y++)
                    {

                        if (cities[y] == selectedCity)
                        {

                            playerPosition = y;

                        }

                    }
            }
            }


        }


   
    }




}
