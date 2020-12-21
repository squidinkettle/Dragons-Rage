using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaOfEffect : MonoBehaviour
{

    [SerializeField] List<GameObject> barracks;
    [Header("Type of terrains")]
    [SerializeField] bool swamp;
    [SerializeField] bool ambushTrigger;
    [SerializeField] bool water;

    [Header("Speed Debuffs")]
    [SerializeField] float swampSpeedDebuff;
    [SerializeField] float waterSpeedDebuff;
    [SerializeField] float waterArmorDebuff;
    [SerializeField] float enemySwampSpeedDebuff;

    // Start is called before the first frame update
    void Start()
    {
        swampSpeedDebuff = 0.60f;
        enemySwampSpeedDebuff = 0.85f;
        waterSpeedDebuff = 0.5f;
        waterArmorDebuff=0.80f;
    }


    public void OnTriggerEnter(Collider collision)
    {
        var playerMovement = FindObjectOfType<PlayerMovement>();
        if (collision != null) { 
        if (collision.gameObject.tag == "PlayerC")
            {
        

                if (swamp)
                {


                    playerMovement.SetSpeedModifier(swampSpeedDebuff);
                 


                }
                if (water)
                {
                    playerMovement.SetSpeedModifier(waterSpeedDebuff);

                
                }
                if (ambushTrigger)
                {
                    Debug.Log("AmbushTrigger");
                    for (int x = 0; x < barracks.Count; x++)
                    {
                        barracks[x].GetComponentInChildren<SpawnEnemy>().playerInAmbushArea = true;


                    }

                }



            }
        if (collision.gameObject.tag == "Light Enemy")
        {
            collision.gameObject.GetComponent<EnemySoldiers>().speedMod = enemySwampSpeedDebuff;




        }

    }
    }
    public void OnTriggerExit(Collider other)
    {
        var playerMovement = FindObjectOfType<PlayerMovement>();

        Debug.Log("Exit swamp");
        if (other.gameObject.tag=="PlayerC")
        {
            float defaultStatus = 1.0f;
            Debug.Log("Exit swamp SS");

            playerMovement.SetSpeedModifier(defaultStatus);
            other.GetComponentInChildren<Player>().playerSpeedMod = defaultStatus;
            other.GetComponentInChildren<Player>().playerArmorRMod = defaultStatus;

        }
        if (other.gameObject.tag == "Light Enemy")
        {
            float defaultStatus = 1f;
            other.gameObject.GetComponent<EnemySoldiers>().speedMod = defaultStatus;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
