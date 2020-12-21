using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
 
    int fireRes;
    int fireLvl;
    GameObject player;
    GameObject mapInfo;
    public GameObject prefab;
    public GameObject prefab2;
    public bool fire_spread = false;
    List<GameObject> fire = new List<GameObject>();
    public bool destroy = false;
    int fire_intensity_limit = 30;
    bool big_fire = false;
    int number_of_big_flames = 3;
    float timer;
    int big_flame_counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        mapInfo = GameObject.FindWithTag("MapInfo");
        player = GameObject.FindWithTag("PlayerC");

        timer = Time.fixedTime + 1;


        fireRes = GetComponent<House>().fireResistance;
        fireLvl = player.GetComponent<Player>().fireLvl;
    }

    public void OnParticleCollision(GameObject other)
    {
        


        //if the dragon's fire makes contact with a house, it will lower its health
        //and will start spreading fire
        if (other.tag == "Fire Attack Particle" &&  fireRes <= fireLvl)
        {

            GetComponent<House>().SetHealth(10);
            if (GetComponent<House>().fire_intensity < fire_intensity_limit)
            {
                Vector3 location = Fire_Spawn_Location();
                Generate_Flames(location,0);


                GetComponent<House>().is_on_fire = true;
                GetComponent<House>().fire_intensity++;
            }

            if (GetComponent<House>().is_on_fire == false) {
                Debug.Log(gameObject.tag);
                //Debug.Log("Player:"+player.GetComponent<Player>().fireLvl+"\n House:"+GetComponent<House>().fireResistance );
            
            
            }



        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<House>() == null) { Destroy(gameObject); }

        //will instantiate fire to a house if true while limiting the number
        //of flames to 10 per house
        if (fire_spread == true) {

            GetComponent<House>().SetHealth(10);
            if (GetComponent<House>().fire_intensity < fire_intensity_limit/2)
            {
                //returns a location so fire spawns on the house
                Vector3 location = Fire_Spawn_Location();

                //spawns fire on said location
                Generate_Flames(location,0);


                GetComponent<House>().is_on_fire = true;

            }
            GetComponent<House>().fire_intensity++;
            fire_spread = false;

        }
        if (GetComponent<House>().fire_intensity >= fire_intensity_limit && big_fire==false)
        {

            for (int x = 0; x < fire.Count; x++)
            {
                Destroy(fire[x]);

            }
            if (transform.localScale.x < 16 && gameObject.tag=="House") {
                number_of_big_flames = 1;
                 }
            if (gameObject.tag == "Wood Wall")
                number_of_big_flames = 2;
            if (gameObject.tag == "Farm")
                number_of_big_flames = 5;
            if (gameObject.tag == "Church")
                number_of_big_flames = 15;
            if (gameObject.tag == "Windmill")
                number_of_big_flames = 5;


            for (int x = 0; x < number_of_big_flames; x++)
            {
                Vector3 location = Fire_Spawn_Location();
                Generate_Flames(location, 1);
            }


            big_fire = true;


        }

        //this will destroy both the house and flames once the house has less
        //than 0 hp
        if (GetComponent<House>().GetHealth() < 0)
        {
            for (int x = 0; x < fire.Count; x++)
            {
                Destroy(fire[x]);

            }

            //Each building destroyed impacts town's morale
            if (gameObject.tag == "Castle") {

                mapInfo.GetComponent<info>().numDestroyedCastles++;
            }
            else if (gameObject.tag == "House"|| gameObject.tag == "Stone House")
            {
                mapInfo.GetComponent<info>().numDestroyedHouses++;
            }
            else if (gameObject.tag == "Windmill"|| gameObject.tag == "Farm"|| gameObject.tag == "Market")
            {
                mapInfo.GetComponent<info>().numDestroyedEconomic++;
            }
            else if (gameObject.tag == "Barracks")
            {
                mapInfo.GetComponent<info>().numDestroyedBarracks++;
            }
            else if (gameObject.tag == "Church")
            {
                mapInfo.GetComponent<info>().numDestroyedCivic++;
            }



        }


    }
    public void Generate_Flames(Vector3 location, int size)
    {
        if (size == 1)
        {
            //Generates a big flame
            GameObject flame1 = Instantiate(prefab, location, Quaternion.Euler(-90, 0, 0));
            GameObject flame2 = Instantiate(prefab2, location, Quaternion.Euler(-90, 0, 0));

            flame1.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            flame2.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            flame1.transform.parent = gameObject.transform;
            flame2.transform.parent = gameObject.transform;
            flame1.transform.position = location;
            flame2.transform.position = location;
            fire.Add(flame1);
            fire.Add(flame2);
        }
        else
        {
            //generates a small flame
            GameObject flame1 = Instantiate(prefab, location, Quaternion.Euler(-90, 0, 0));
            GameObject flame2 = Instantiate(prefab2, location, Quaternion.Euler(-90, 0, 0));
            flame1.transform.parent = gameObject.transform;
            flame2.transform.parent = gameObject.transform;
            flame1.transform.position = location;
            flame2.transform.position = location;

            fire.Add(flame1);
            fire.Add(flame2);
        }



    }

    public Vector3 Fire_Spawn_Location()
    {
        //Will spawn fire on the cube's perimeter
        //it works fine on cubes, but wont work well on asymetrical shapes
        float x= 0.0f;
        float y = 0.0f;
        float z = 0.0f;
        Vector3 location=new Vector3(0.0f,0.0f,0.0f);
        float modifier;
        float divisionModifier = 0.1f;
  
        if (gameObject.tag == "house")
        {
            modifier = 100.0f;
            divisionModifier = 0.1f;
        }
        else
        {
            modifier = 1.5f;
            divisionModifier = 1.5f;
        }
        int side=Random.Range(0, 3);



        if (gameObject.tag == "Farm") {
            side = 2;

        }


        if (side == 0)
        {
           

            y = Random.Range(transform.position.y - transform.localScale.y / divisionModifier, transform.position.y+(transform.localScale.y/ divisionModifier));
            z = Random.Range(transform.position.z - transform.localScale.z / divisionModifier, transform.position.z+transform.localScale.z/ divisionModifier);
            int x_value = Random.Range(0, 2);

            if (x_value == 0)
            {
                x = transform.position.x + transform.localScale.x / divisionModifier + modifier;
            }
            else
            {
                x = transform.position.x - transform.localScale.x / divisionModifier - modifier;
            }

        }
        if (side == 1)
        {

            x = Random.Range(transform.position.x - transform.localScale.x, transform.position.x+transform.localScale.x);
            if (gameObject.tag == "Church")
            {

                y = transform.position.y + transform.localScale.y+15;
            }
            else if(gameObject.tag=="House")
            {
                y = transform.position.y + transform.localScale.y+15;
            }


            z = Random.Range(transform.position.z - transform.localScale.z / divisionModifier, transform.position.z+transform.localScale.z/ divisionModifier);

        }
        if (side == 2)
        {

            x = Random.Range(transform.position.x - transform.localScale.x / divisionModifier, transform.position.x+transform.localScale.x/ divisionModifier);
            y = Random.Range(transform.position.y - transform.localScale.y / divisionModifier, transform.position.y+transform.localScale.y/ divisionModifier);
            int z_value = Random.Range(0, 2);


            if (z_value == 0)
            {
                z = transform.position.z + transform.localScale.z / divisionModifier + modifier;
            }
            else
            {
                z = transform.position.z - transform.localScale.z / divisionModifier - modifier;
            }

        }
        location = new Vector3(x, y, z);

        return location;


    }
}
