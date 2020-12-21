using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuButtonHandler : MonoBehaviour
{

    public Button victory;
    public GameObject mapInfo;
    void Start()
    {
        if(victory!=null)
            victory.gameObject.SetActive(false);

    }
     void Update()
    {
        if (victory != null)
        {
            bool surrenderNum = mapInfo.GetComponent<info>().surrender;
            //Debug.Log(surrenderNum);
            if (surrenderNum)
            {
               // Debug.Log("Active");
                victory.gameObject.SetActive(true);
            }
        }
    }


    public void nextLevel(int num) {
        Debug.Log("Pressed");
        SceneManager.LoadScene(num);


    }
    public void exitGame()
    {


            Application.Quit();


    }
   



}
