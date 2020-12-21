using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] int health;
    public int defense;
    public int fireResistance;
    public bool is_on_fire = false;
    public int fire_intensity = 0;
    public GameObject block;
    GameObject player;
    public GameObject areaOfEffect;
    public static bool alarm= false;
    public int range;
    Color color;
    GameObject AOF;
    bool isTransparent;
    float waitTransparency = 5f;

    Renderer renderer;

    private AudioSource audioSource;
    public AudioClip[] clips;

    ManageHouses manageBuildings;

    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        SetupBuildings();
        float waitTime = 3f;
        StartCoroutine(houseLoop(waitTime));

        manageBuildings = FindObjectOfType<ManageHouses>();

        //StartCoroutine(SetDefaultTransparency(waitTransparency));

    }

    private void SetupBuildings()
    {
        if (gameObject.tag == "Church")
        {
            SetBuildingStats(5, 3, 1000);
            InstantiateChurchParts();
        }
        else if (gameObject.tag == "House")
        {
            SetBuildingStats(0, 0, 2000);
        }
        else if (gameObject.tag == "Stone House")
        {
            SetBuildingStats(2, 1, 4000);
        }
        else if (gameObject.tag == "Wood Wall")
        {
            SetBuildingStats(3, 0, 4000);
        }
        else if (gameObject.tag == "Castle")
        {
            SetBuildingStats(5, 4, 2000000);
        }
        else if (gameObject.tag == "Windmill")
        {
            SetBuildingStats(1, 2, 5000);
        }
        else if (gameObject.tag == "Farm")
        {
            SetBuildingStats(0, 0, 3000);
        }
        else if (gameObject.tag == "Watchtower")
        {
            SetBuildingStats(0, 0, 2500, 200);
            InstantiateWTowerAOF();
        }
    }
    private void SetBuildingStats(int def, int fireR, int h, int r = 0)
    {
        range = r;
        defense = def;
        fireResistance = fireR;
        health = h;
    }

    private void InstantiateWTowerAOF()
    {
        AOF = Instantiate(areaOfEffect, transform.position, Quaternion.Euler(0, 0, 0));
        AOF.transform.parent = gameObject.transform;
        AOF.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
        color = AOF.GetComponent<Renderer>().material.color;
        color.a = 0;
        AOF.GetComponent<Renderer>().material.color = color;
    }

    private void InstantiateChurchParts()
    {
        //Will instantiate the 2 towers on the church so they can catch fire
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        Vector3 location1 = new Vector3(x, y, z);

        GameObject church_tower1 = Instantiate(block, location1, Quaternion.Euler(0, 0, 0));
        GameObject church_tower2 = Instantiate(block, location1, Quaternion.Euler(0, 0, 0));
        church_tower2.transform.localScale += new Vector3(0, 10, 0);
        church_tower1.transform.localScale += new Vector3(0, 10, 0);

        church_tower1.transform.parent = gameObject.transform;
        church_tower2.transform.parent = gameObject.transform;

        church_tower1.transform.rotation = gameObject.transform.rotation;
        church_tower2.transform.rotation = gameObject.transform.rotation;

        church_tower2.transform.position = location1 + (gameObject.transform.forward * -10) + (gameObject.transform.right * -15) + (gameObject.transform.up * 31);
        church_tower1.transform.position = location1 + (gameObject.transform.forward * 12) + (gameObject.transform.right * -15) + (gameObject.transform.up * 31);
    }

  

    IEnumerator houseLoop(float timeWait)
    {
        if (gameObject.tag == "Watchtower")
        {
            while (true)
            {


                float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
                while (playerDistance < range + 200 && alarm == false)
                {
                    playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
                    if (playerDistance < range)
                    {
                        alarm = true;

                        playAlarm();
                    }

                    TransparencyControl(range, playerDistance);


                    yield return null;
                }
                if (alarm == true)
                {
                    color.a = 0f;

                    AOF.GetComponent<Renderer>().material.color = color;


                }

                yield return null;
            }

        }
    }
    public void SetHealth(int change)
    {
        health -= change;
        if (health <= 0)
        {
            manageBuildings.RemoveBuildingFromMap(this.gameObject);
        }


    }
    public int GetHealth()
    {
        return health;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTransparent)
        {
            StartCoroutine (SetDefaultTransparency(waitTransparency));
        }
    }


    public void DestroyBuilding()
    {
        Destroy(gameObject);
    }
    void playAlarm()
    {
        audioSource.clip = clips[0];
        if (!audioSource.isPlaying)
        {

            audioSource.Play();
        }

    }


    void TransparencyControl(float aRange, float playerDistance)
    {


        if (playerDistance < aRange + 200)
        {
            color.a += 0.1f;
            if (color.a > 0.5f)
                color.a = 0.5f;
     

        }
        else
        {
            color.a -= 0.1f;
            if (color.a < 0f)
                color.a = 0f;

        }

        if (alarm == true)
            color.a = 0f;

        AOF.GetComponent<Renderer>().material.color = color;

    }
    public void SetIsTransparent(bool set)
    {
        isTransparent = set;
    }


    public IEnumerator SetDefaultTransparency(float timer)
    {


        yield return new WaitForSeconds(timer);
        isTransparent = false;

        foreach(Material mat in renderer.materials)
        {
            SetAlpha(mat, 1f);
        }

            


    }

    void SetAlpha(Material mat, float alphaVal)
    {

        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);


    }
}
