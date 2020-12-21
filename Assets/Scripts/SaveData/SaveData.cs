using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{

    [SerializeField]private PlayerData playerData=new PlayerData();
    [SerializeField] private UpradeRequirements upgradesData = new UpradeRequirements();

    //Saves Game Data
    public void SaveGameData()
    {
        //Saves Player Data
        playerData.healthLvl = buttonHandler.healthLvl;
        playerData.speedLvl = buttonHandler.speedLvl;
        playerData.strengthLvl = buttonHandler.strengthLvl;
        playerData.fireLvl = buttonHandler.fireLvl;
        playerData.defenseLvl = buttonHandler.defenseLvl;
        playerData.evolutionLvl = buttonHandler.evolveLvl;
        playerData.rageLvl = buttonHandler.rageLvl;
        playerData.playerGold = transferInfoToOtherScene.totalIncome;
        playerData.humanSacrifice = transferInfoToOtherScene.availableSacrifice;
        string playerJson = JsonUtility.ToJson(playerData);


        upgradesData.requirements = buttonHandler.requirements;
        string upgradesJson = JsonUtility.ToJson(upgradesData);
        File.WriteAllText(Application.dataPath + "/playerData.txt", playerJson);
        File.WriteAllText(Application.dataPath + "/upgradeData.txt", upgradesJson);
        Debug.Log("Saved!");


}

    public void LoadGameData() {
        //Loads Player Data
        if (File.Exists(Application.dataPath + "/playerData.txt"))
        {
            //Loads Player Info
            string playerJson = File.ReadAllText(Application.dataPath + "/playerData.txt");

            PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(playerJson);

            buttonHandler.healthLvl = loadedPlayerData.healthLvl;
            buttonHandler.speedLvl = loadedPlayerData.speedLvl;
            buttonHandler.strengthLvl = loadedPlayerData.strengthLvl;
            buttonHandler.fireLvl = loadedPlayerData.fireLvl;
            buttonHandler.defenseLvl = loadedPlayerData.defenseLvl;
            buttonHandler.evolveLvl = loadedPlayerData.evolutionLvl;
            buttonHandler.rageLvl = loadedPlayerData.rageLvl;
            transferInfoToOtherScene.totalIncome = loadedPlayerData.playerGold;
            transferInfoToOtherScene.availableSacrifice = loadedPlayerData.humanSacrifice;

            string[] attributes = { "Health", "Speed", "Defense", "Fire", "Strength", "Rage", "Evolve" };
            buttonHandler.requirements["Health"][0] *= buttonHandler.healthLvl;


            //Loads upgrades info
            string upgradeJson= File.ReadAllText(Application.dataPath + "/upgradeData.txt");
            UpradeRequirements loadedUpgradeData = JsonUtility.FromJson<UpradeRequirements>(upgradeJson);

            //buttonHandler.requirements = loadedUpgradeData.requirements;
            Debug.Log("Loaded!");
            Debug.Log(transferInfoToOtherScene.totalIncome);
            Debug.Log(buttonHandler.requirements.Count);


        }
        else
        {
            Debug.Log("No Save File!");
        }

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


[System.Serializable]
public class PlayerData {
    public int healthLvl;
    public int speedLvl;
    public int strengthLvl;
    public int fireLvl;
    public int defenseLvl;
    public int evolutionLvl;
    public int rageLvl;
    public int playerGold;
    public int humanSacrifice;



}

[System.Serializable]
public class UpradeRequirements
{
    public Dictionary<string, List<int>> requirements = new Dictionary<string, List<int>>();


}


