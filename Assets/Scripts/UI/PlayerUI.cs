using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] Text rageDisplay; //Text object displaying the chance the dragon will rage
    [SerializeField] Text timerText;  //Text object that will display the time

    [Header("Images")]
    [SerializeField] Image health;    //Health object that will show the player's health
    [SerializeField] RawImage damageWarning; //Will appear if the player has taken damage
    [SerializeField] Color flashColor;


    private bool showImage=false;
    private float startTime;
    int playerHealth;
    Player player;     //player object
    // Start is called before the first frame update
    void Start()
    {

        startTime = Time.time;
        player = FindObjectOfType<Player>();
        playerHealth = player.GetHealth();   //will fetch the player's health


    }

    public void ToggleImage(bool set)
    {
        showImage = set;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();

        SetUpTimer();

        DisplayRageChance();
    }



    private void SetUpTimer()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        //Displays timer
        timerText.text = minutes + ":" + seconds;
    }

    private void DisplayHealth()
    {
        playerHealth = player.GetHealth();   //updates player's health
        health.fillAmount = 0.01f * playerHealth;         //controlls how the bar is filled based on player's health

        DisplayDamageScreen();
    }

    private void DisplayDamageScreen()
    {
        if (showImage)
        {

            damageWarning.color = flashColor;
            showImage = false;

        }
        else
        {
            damageWarning.color = Color.Lerp(damageWarning.color, Color.clear, 3 * Time.deltaTime);
        }
    }

    private void DisplayRageChance()
    {
        string rchance = player.rageChance.ToString("f2");
        rageDisplay.text = "Rage: " + rchance;
    }
}
