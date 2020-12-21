using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballista : MonoBehaviour
{
    float timerAim;     //Time that takes to aim and then shoot at player
    float timerReload;  //Time that takes to reload
    bool hasShot;       //Checks if ballista has shot
    public float range; //Determines ballista's range
    public RawImage crossAirs;
    GameObject player;
    public GameObject arrow;

    float reloadSpeed;  //Determines speed reload
    float aimSpeed;     //Determines speed of aiming
    Color ogColor;
    Vector3 ogPos;

    bool isBusy;         //Determines if crossbowman is too busy to shoot;

    // Start is called before the first frame update
    void Start()
    {
        isBusy = false;
        crossAirs= GameObject.Find("CrossAirs").GetComponent<RawImage>();
        ogColor = crossAirs.color;
        ogPos = crossAirs.rectTransform.position;
       

        player = GameObject.FindWithTag("PlayerC");
        range = 100;
        hasShot = false;
        reloadSpeed = 0.5f;
        aimSpeed = 1f;

        timerAim = Time.fixedTime + aimSpeed;
        timerReload = Time.fixedTime + reloadSpeed;
        float ballistaWait = 2f;
        StartCoroutine(ballistaBehaviour(ballistaWait));
    }




    IEnumerator ballistaBehaviour(float time)
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        while (true)
        {
            playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            

            while (playerDistance<range+10) {
          
                playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
                isBusy = parentFleeOrReloading();
                if (!isBusy)
                {

                    if (!hasShot)
                    {
                        aimAtPlayer();
                    }
                    else
                    {

                        reload();
                    }
                }
                yield return null;
            }
            yield return new WaitForSeconds(time);
        }
    }


    // Update is called once per frame
    void Update()
    {


    }

    void aimAtPlayer()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distanceToPlayer < range)
        {
            gameObject.transform.LookAt(player.transform);
            //gameObject.transform.Rotate(-90,90,0);
            animateCrossairs();


            if(timerAim< Time.fixedTime) {

                shoot();
              
                }


        }
        else {
            //crossAirs.color = ogColor;
            //crossAirs.color= Color.Lerp(crossAirs.color, Color.clear, 3 * Time.deltaTime);
           


            timerAim = Time.fixedTime + aimSpeed;
            timerReload = Time.fixedTime + reloadSpeed;
        }



    }
    void shoot()
     {
        Vector3 target = getPlayerPosition();
        GameObject shotArrow= Instantiate(arrow, getBallistaPosition(), Quaternion.Euler(0, 0, 0)) as GameObject;

        shotArrow.GetComponent<projectile>().destination = target;
        shotArrow.transform.LookAt(player.transform);
        hasShot = true;
        timerReload = Time.fixedTime + reloadSpeed;


    }

    void reload()
    {
        crossAirs.CrossFadeAlpha(0.0f, 0.0f, false);
        crossAirs.color = ogColor;
        crossAirs.rectTransform.position = ogPos;
        if (timerReload < Time.fixedTime) {

            hasShot = false;
            timerAim = Time.fixedTime + aimSpeed;

        }


    }

    void animateCrossairs() {
        crossAirs.CrossFadeAlpha(1.0f, 0.0f, false);
    
        if (timerAim > Time.fixedTime)
        {
            crossAirs.color = Color.Lerp(crossAirs.color, Color.red, 0.4f * Time.deltaTime);


           Vector3 newPosition = new Vector3(1.0f,1.0f,0.0f);
            crossAirs.rectTransform.position -= newPosition;
        }
        else
        {
            crossAirs.color = Color.Lerp(crossAirs.color, Color.clear,999);


            //crossAirs.transform.position.x = 20f;
        }



    }

    Vector3 getBallistaPosition() {

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y+10;
        float z = gameObject.transform.position.z;
        Vector3 location1 = new Vector3(x, y, z);

        return location1;


    }

    Vector3 getPlayerPosition() {
        float x = player.transform.position.x;
        float y = player.transform.position.y + 10;
        float z = player.transform.position.z;
        Vector3 location1 = new Vector3(x, y, z);

        return location1;


    }


    bool parentFleeOrReloading() {
    
        if(gameObject.tag=="Heavy Enemy") {
            return false;
        
        }else if (gameObject.tag=="Ranged Enemy"&& (GetComponentInParent<EnemySoldiers>().isOnFire==true || GetComponentInParent<EnemySoldiers>().flee == true))
        {
            return true;

        }
        else
        {
            return false;
        }



    }



}
