using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    Player player;
    public ParticleSystem fire_ps;
    public float fireRange;
    float range;
    public int fireLvl;
    float buildingDistance;
    ManageHouses buildingList;


    List<House> listBuildings = new List<House>();
    // Start is called before the first frame update
    void Start()
    {
        buildingList = FindObjectOfType<ManageHouses>();
        player = FindObjectOfType<Player>();

        fireLvl = player.fireLvl;
        fireRange = 30 * ((fireLvl * 0.33f) + 1);
        range = fireRange;

        float fireWait = 5f;
        StartCoroutine(FireSpread(fireWait));

    }

    public House GetHouse(int index)
    {
        return listBuildings[index];
    }



    IEnumerator FireSpread(float timeWait)
    {
        while (true)
        {

            Queue<House> nearbyBuildings = new Queue<House>();
            nearbyBuildings =buildingList.GetDistanceFromObjectToBuilding(gameObject.transform, range);

            if (nearbyBuildings == null) 
            { 
            print("queue is null"); 
          
            }

            while (nearbyBuildings != null && nearbyBuildings.Count > 0)
            {
                House building = nearbyBuildings.Dequeue();
                CheckIfFireSpreads(building);
                DamageBuilding(building);
            }


            yield return new WaitForSeconds(timeWait);
        }
    }

    private void CheckIfFireSpreads(House building)
    {
        //checks if house is in range of the flame
        if (building == null) { return; }

        float saving_throw = Random.Range(0.0f, 1.0f);
        float fire_intensity = building.fire_intensity;
        float base_number = (0.02f * fireLvl) + (fire_intensity * 0.05f);

        //Debug.Log("Saving Throw:" + saving_throw + " base Number:" + base_number);
        if (saving_throw < base_number)
        {

            int fireResistance = building.fireResistance;
            int playerFire = player.fireLvl;

            //compares the building's fire resistance vs the player's
            if (fireResistance <= playerFire)
            {
                building.GetComponent<Attacked>().fire_spread = true;
                //Debug.Log("Spread");

            }
        }

        
    }

    private void SetRangeOnBuildingType(House building)
    {
        if (building.tag == "Tower")
        {
            range = fireRange * 2;
        }
        if (building.tag == "Church")
        {
            range = fireRange * 1.5f;

        }
        else
        {
            range = fireRange;
        }
    }

    private void DamageBuilding(House damagedBuilding)
    {
        if (damagedBuilding == null) { return; }

        if (damagedBuilding.fire_intensity > 7)
        {
            damagedBuilding.SetHealth(10);
        }
    }




}
