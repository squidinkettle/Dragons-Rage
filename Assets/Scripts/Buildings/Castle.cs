using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int numBallisae; //Determines the number of ballistas spawned
    public GameObject ballista;


    // Start is called before the first frame update
    void Start()
    {
        spawnBallistae();
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 getPosition() {

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        Vector3 location1 = new Vector3(x, y, z);
        return location1;

    }

    //Determines the location of ballistaes
    void spawnBallistae() {

        Vector3 location = getPosition();
        GameObject ballistaObj;

        int UP=0;
        int UPWARD = 97;

        for(int x = 0; x < numBallisae; x++)
        {

            switch (x) {

                case 0: ballistaObj = new GameObject();
                    ballistaObj=Instantiate(ballista, location, Quaternion.Euler(0, 0, 0));
                    ballistaObj.transform.parent = gameObject.transform;
                    //ballistaObj.transform.rotation = gameObject.transform.rotation;
                    ballistaObj.transform.position=location + (gameObject.transform.forward * 35) + (gameObject.transform.right * -35) + (gameObject.transform.up * UPWARD);
                    break;
                case 1:
                    ballistaObj = new GameObject();
                    ballistaObj = Instantiate(ballista, location, Quaternion.Euler(0, 0, 0));
                    ballistaObj.transform.parent = gameObject.transform;
                    //ballistaObj.transform.rotation = gameObject.transform.rotation;
                    ballistaObj.transform.position = location + (gameObject.transform.forward * 35) + (gameObject.transform.right * 35) + (gameObject.transform.up * UPWARD);
                    break;
                case 2:
                    ballistaObj = new GameObject();
                    ballistaObj = Instantiate(ballista, location, Quaternion.Euler(0, 0, 0));
                    ballistaObj.transform.parent = gameObject.transform;
                    //ballistaObj.transform.rotation = gameObject.transform.rotation;
                    ballistaObj.transform.position = location + (gameObject.transform.forward * -35) + (gameObject.transform.right * -35) + (gameObject.transform.up * UPWARD);
                    break;
                case 3:
                    ballistaObj = new GameObject();
                    ballistaObj = Instantiate(ballista, location, Quaternion.Euler(0, 0, 0));
                    ballistaObj.transform.parent = gameObject.transform;
                    //ballistaObj.transform.rotation = gameObject.transform.rotation;
                    ballistaObj.transform.position = location + (gameObject.transform.forward * -35) + (gameObject.transform.right * 35) + (gameObject.transform.up * UPWARD);
                    break;
                default:
                    break;


            }








        }





    }



}
