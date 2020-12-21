using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    //Fire Attack (FA) script
    //Checks to see if the FA cooldown timer is running
    public bool FACDTimerOn = false;
    //The time used inside the FA methods
    public float gameTime;
    //The cooldown bool that is triggered if the player stops using the FA
    //before it reaches the FAMaxUse time
    public bool preCDCD = false;
    //The max amount of time that the FA can be used
    public float FAMaxUse = 5.0f;


    [SerializeField] FireAttack fireAttack;
    [SerializeField] Player player;
    [SerializeField] PlayerUI playerUI;

    void Awake()
    {
        if (instance == null) {
            instance = this;


        }

        else if(instance!=this) {
            Destroy(gameObject);
        
        }

        fireAttack = FindObjectOfType<FireAttack>();
        player = FindObjectOfType<Player>();
        playerUI = FindObjectOfType<PlayerUI>();

    }


    // Start is called before the first frame update
    void Start()
    {




    }

    //Method that is used when the player is using the FA
    public void FATimerMethod()
    {
        //Adds time when the FA is being used
        gameTime += Time.deltaTime;
        //Bool in the FA script that is set to true when in use
        fireAttack.SetFireAttackStart(true);
        if (player.GetRage() == true) {
            FAMaxUse = 10.0f;
        }
        else {
            FAMaxUse = 5.0f;
        }
        //Bools that are set based on the max amount of time the
        //player can use the FA

        if (gameTime >= FAMaxUse)
        {
            int fireDamage = 5;
            player.SetHealth(fireDamage);
            player.attacked = true;
            playerUI.ToggleImage(true);
            player.SetRage(true);
            Debug.Log("ImageTrueGM");
            //FA cooldown timer
            FACDTimerOn = true;
            //FA in use
            fireAttack.fireAStarted = false;
            //Cooldown bool that checks to see if the max cooldown 
            //FAMaxUse has not been met yet
            preCDCD = false;
        }
    }

    //If player reaches the FAMaxUse time
    //or the FA is not being used
    //Method not used is the gametime hits zero
    public void FACDTimerMethod()
    {
        //FA in use bool set to false
        fireAttack.fireAStarted = false;
        //Constrains the gameTime to not go less than zero
        //and subtracts the current time down to zero
        gameTime = Mathf.Max(0.0f, gameTime - Time.deltaTime);

        //When the gameTime hits zero
        if (gameTime <= 0.0f)
        {
            //FA in use bool set to salse
            fireAttack.fireAStarted = false;
            //FA cooldown timer set to false
            FACDTimerOn = false;
        }
        //If the gameTime is not less then FAMaxUse and
        //the FA cooldown timer has not started
        else if (gameTime <= FAMaxUse && FACDTimerOn == false)
        {
            //Set the pre cooldown cooldown timer to true
            preCDCD = true;
        }
    }

    void FixedUpdate()
    {
        if (player == null) {
            player = FindObjectOfType<Player>();

        }
        //Debug.Log(gameTime);
        //If the user presses the fire button (space) and
        //the FA cooldown timer has not started

       
        if (player!=null&&((Input.GetKey(KeyCode.Space) && FACDTimerOn == false) || player.GetRage()==true))
        {


            //Run the FA not in use method
            FATimerMethod();
        }
        //Else if the FA cooldown timer
        //Pre cooldown cooldown timer or
        //the user lets go of the fire button (space)
        else if (FACDTimerOn == true || preCDCD == true || Input.GetKeyUp(KeyCode.Space))
        {
            //if the gameTime is not equl to zero
            if (gameTime != 0.0f)
            {
                //Run the FA cooldown timer method
                FACDTimerMethod();
            }
            //Else stop the method
            else
            {
                return;
            }
        }
    }
}
