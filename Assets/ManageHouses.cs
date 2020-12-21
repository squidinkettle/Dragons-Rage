using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHouses : MonoBehaviour
{

    List<House> buildingsInMap=new List<House>();

    private void Awake()
    {
        House[] buildingArray=FindObjectsOfType<House>();
        foreach(House building in buildingArray)
        {
            buildingsInMap.Add(building);
        }
    }

    public void RemoveBuildingFromMap(GameObject destroyedHouse)
    {
      
        var destroyHouse = destroyedHouse;
        buildingsInMap.Remove(destroyedHouse.GetComponent<House>());
        Destroy(destroyHouse);
            

    }
    public Queue<House> GetDistanceFromObjectToBuilding(Transform objectPos, float range)
    {
        Queue<House> buildingsInRange = new Queue<House>();
        foreach(House building in buildingsInMap)
        {
            if(building == null) { continue; }
            var distance = Vector3.Distance(objectPos.position, building.transform.position);
            if (distance < range)
            {
                buildingsInRange.Enqueue(building);

            }
        }
        if (buildingsInRange.Count == 0) { return null; }

        return buildingsInRange;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
