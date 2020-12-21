using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    float velocity;

    float destroyArrowTimer;
    public Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 100;

        destroyArrowTimer = Time.fixedTime + 10;

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Building"))
        {

            gameObject.GetComponent<Rigidbody>().detectCollisions=false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            velocity = 0;
            gameObject.GetComponent<ParticleSystem>().Stop();
        }

       
        else if (other.gameObject.layer == LayerMask.NameToLayer("PlayerC") && velocity>0) {

            other.gameObject.GetComponentInChildren<Player>().attackedByProjectile = true;

            Destroy(gameObject);


        }


    }


  



    // Update is called once per frame
    void Update()
    {
        projectileTrayectory();
        if (destroyArrowTimer < Time.fixedTime) {

            Destroy(gameObject);
        
        }
    }


    void projectileTrayectory() {
        gameObject.transform.position += transform.forward * Time.deltaTime * velocity;

    }


}
