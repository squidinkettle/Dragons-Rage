using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    GameObject mapInfo;
    bool isCaptured = false;
    float timeToCapture;
    float countDown;
    bool done;
    bool countStart;

    // Start is called before the first frame update
    void Start()
    {
        countStart = false;
        mapInfo= GameObject.FindWithTag("MapInfo");
        timeToCapture = 2f;
        countDown=Time.fixedTime + timeToCapture;
        done = false;
    }
 

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="PlayerC" && collision.gameObject.tag!="Light Enemy") {
            countStart = true;


        }
       
        if (collision.gameObject.tag == "Light Enemy")
        {
            //Debug.Log("Enemy In");
            countStart = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerC") {

            countStart = false;
        
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!countStart) {

            resetTimer();
        }
        else
        {
            startCountDown();
        }

        setMapInfo(isCaptured);


    }

    void startCountDown()
    {
        if (countDown < Time.fixedTime) {

            isCaptured = true;
            Debug.Log("CAPTURED!");
           
        
        
        }



    }

    void resetTimer() {
        countDown = Time.fixedTime + timeToCapture;
    
    }

    void setMapInfo(bool captured) { 
    
        if (captured && !done)
        {
            mapInfo.GetComponent<info>().capturedZones++;
            done = true;
            Debug.Log("Added!");
            Destroy(gameObject);
        }


    }


}
