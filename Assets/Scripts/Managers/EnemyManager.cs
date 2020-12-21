using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Light enemy object
    public GameObject eLightEnemy;
    //Average enemy object
    public GameObject eAverageEnemy;
    //Heavy enemy object
    public GameObject eHeavyEnemy;
    //All enemy spawn points
    public GameObject[] eSpawnPoints;
    //The total amount of enemy spawn points
    public int eSpawnPointTotal;

    public GameObject[] defendPoints;
    public List<GameObject> defendPoints2 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("DefendPoint"))
        {
            defendPoints2.Add(fooObj);
        }

        eSpawnPointTotal = eSpawnPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
