using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> specificObjective=new List<GameObject>();
    public GameObject enemyManager;
    public GameObject enemy;
    public GameObject archers;
    GameObject player;
    public int waves=0;
    public int numberEnemiesPerWave = 0;
    //enemyMode: if its zero, enemy will chase and attack the dragon
    //if its one, enemy will defend the spawn area
    //if its two, the enemy will spawn once the player is closeby
    public int enemyMode = 0;
    public float range = 50.0f;
    Vector3 objective;
    public bool playerInAmbushArea;
    public bool spawnArchers;
    public bool alarmSounded=false;

    public GameObject[] watchtowers;

    /*this list will signal the spawn if all the spawned soldiers are killed so
     * that the next wave may form  
     */
    List<GameObject> enemySquad = new List<GameObject>();
    List<float> distance = new List<float>();
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        watchtowers = GameObject.FindGameObjectsWithTag("Watchtower");

        enemyManager = GameObject.FindWithTag("GameManager");
        playerInAmbushArea = false;
      
    }

    // Update is called once per frame
    void Update()
    {

        if (House.alarm == true)
            alarmSounded = true;




        for (int x = 0; x < enemySquad.Count; x++)
        {
            if (enemySquad[x].GetComponent<EnemySoldiers>().alive == false)
            {
                Destroy(enemySquad[x]);
                enemySquad.Remove(enemySquad[x]);
                break;
            }
        }


        if (alarmSounded == true) { 
            if (enemySquad.Count == 0 && waves > 0)
            {

                bool spawned = false;


                switch (enemyMode)
                {

                    //soldiers will simply spawn and go after the player
                    case 0:
                        Spawn();
                        spawned = true;
                        break;

                    //soldiers will go to protect an area
                    case 1:
                        SortDefendPositions();
                        Spawn();
                        spawned = true;
                        break;


                    //soldiers will ambush the player depending of set distance
                    case 2:
                        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
                        if (playerDistance < range)
                        {
                            Spawn();
                            spawned = true;
                        }
                        break;

                    //will head and defend a specific area
                    case 3:
                        for (int x = 0; x < specificObjective.Count; x++)
                        {
                            if (specificObjective[x] != null)
                            {
                                //Debug.Log(specificObjective[0].transform.position);
                                objective = specificObjective[0].transform.position;
                                break;

                            }


                        }
                        if (specificObjective == null || specificObjective.Count == 0)
                        {
                            SortDefendPositions();
                            enemyMode = 1;
                        }

                        Spawn();
                        spawned = true;

                        break;

                    //Will spawn if player is in an ambushing area;
                    case 4:
                        if (playerInAmbushArea)
                        {
                            Debug.Log("Heading to ambushArea");
                            for (int x = 0; x < specificObjective.Count; x++)
                            {
                                if (specificObjective[x] != null)
                                {

                                    objective = specificObjective[0].transform.position;
                                    break;

                                }


                            }
                            if (specificObjective == null || specificObjective.Count == 0)
                            {
                                SortDefendPositions();
                                enemyMode = 1;
                            }

                            Spawn();
                            spawned = true;


                        }
                        break;

                }


                if (spawned == true)
                {
                    waves -= 1;
                    spawned = false;
                }


            }
        }

    }

    void Spawn()
    {
        var offset = 0;

        //Will iterate through numberenemiesPerWave to spawn enemies
        for (int x = 0; x < numberEnemiesPerWave; x++)
        {
            GameObject soldier;

            Vector3 location = new Vector3(gameObject.transform.position.x+offset, gameObject.transform.position.y,gameObject.transform.position.z);
            if (spawnArchers)
            {
                soldier = Instantiate(archers, location, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                soldier = Instantiate(enemy, location, Quaternion.Euler(0, 0, 0));
            }
            soldier.GetComponent<EnemySoldiers>().mode = enemyMode;
            soldier.GetComponent<EnemySoldiers>().goalPosition = objective;
            enemySquad.Add(soldier);
            offset += 3;

        }
    }

    void SortDefendPositions() {
        GameObject[] defendPositions = enemyManager.GetComponent<EnemyManager>().defendPoints;
        List<GameObject> defendPositions2 = new List<GameObject>();
        defendPositions2 = enemyManager.GetComponent<EnemyManager>().defendPoints2;

        float closest = 5000.0f;
        var closestposition = defendPositions2[0];
        for (int x = 0; x < defendPositions2.Count; x++)
        {
            distance.Add(Vector3.Distance(defendPositions2[x].transform.position, player.transform.position));

            if (distance.Min() < closest)
            {
                closest = distance.Min();
                closestposition = defendPositions2[x];
            }
            if (defendPositions2[x].GetComponent<ObjectivePointInfo>().isImportant == true)
            {
                closestposition = defendPositions2[x];
                break;
            }


        }
        objective = closestposition.transform.position;



    }
}
