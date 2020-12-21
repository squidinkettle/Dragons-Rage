using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;


    //Base stats for player
    public int fireLvl = 1;

    [SerializeField] int maxHealth=100;
    [SerializeField]int health;

    [SerializeField]bool rage;
    public float rageModifier;
    public bool attacked;
    public float rageChance;
    public float defense = 0.0f;
    float armorRating;
    public int strength=5;
    public float playerSpeed=1.0f;


    //Temporal stat modifications
    public float playerSpeedMod;
    public float playerArmorRMod;
    float originalSpeed;
    float originalArmor;

    public bool attackedByProjectile=false;



    float timer;
    Vector3 playerMovement = new Vector3(0.0f, 0.0f, 0.0f);
    public Animator dragon_animation;

    // Start is called before the first frame update
    void Start()
    {
        float rageTimer = 3.5f;
        StartCoroutine(RageControl(rageTimer));
        playerSpeedMod = 1.0f;
        playerArmorRMod = 1.0f;
        SetBaseStats();

        originalArmor = armorRating;
        originalSpeed = playerSpeed;
        //Gets dragon's animator
        dragon_animation = GetComponent<Animator>();

        timer = Time.fixedTime + 0.5f;


        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator RageControl(float rageTime)
    {
        while (true)
        {
            if (rage == true)
            {
                yield return new WaitForSeconds(rageTime);
            }
            rage = false;
            yield return null;
        }
    }


    private void SetBaseStats()
    {
        defense = (buttonHandler.defenseLvl * 0.05f);
        armorRating = defense * 0.1f;
        health = maxHealth + (buttonHandler.healthLvl * 25);
        strength = 5 * (buttonHandler.strengthLvl + 1);
        rageModifier = (buttonHandler.rageLvl * 0.05f);
        playerSpeed = 1.0f + (buttonHandler.speedLvl * 0.05f);
        fireLvl = 1 + buttonHandler.fireLvl;
    }

    public void SetHealth(int change)
    {
        health -= change;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetRage(bool isBerserk)
    {
        rage = isBerserk;
    }
    public bool GetRage()
    {
        return rage;
    }
    // Update is called once per frame
    void Update()
    {
        updateStats();
        if (health < 0)
            health = 0;


        if (attackedByProjectile) {

            projectileDamage();
            attackedByProjectile = false;
        }

        Vector3 null_movement = new Vector3(0.0f, 0.0f);

        //Will determine the chance of dragon going on a rampage
        rageChance = (Mathf.Pow(health, -1f)*(Mathf.Round((-health/10))+11))-rageModifier;


        //if the player is attacked, this will determine of the dragon goes on a rampage
        if (attacked == true) {
            gameObject.GetComponent<PlayerUI>().ToggleImage(true);
            float savingThrow = Random.Range(0.0f, 1.0f);
            if (savingThrow < rageChance)
            {
                rage = true;    //burn them all
            }
            else
            {
                attacked = false;
            }

        }



    }



    void playFireAttackClip() {
        audioSource.clip = clips[0];
        if (!audioSource.isPlaying)
        {

            audioSource.Play();
        }
    
    }
    void projectileDamage() {
        float savingThrow = Random.Range(0.0f, 1.0f);
        if (savingThrow >= 0.80 + armorRating)
        {

            health -= 10;
            attacked = true;
        }
        else {

            audioSource.clip = clips[0];
            audioSource.Play();
         
           }
        attackedByProjectile = false;

    }


    void updateStats() {
        armorRating = originalArmor * playerArmorRMod;
    
    }


}
