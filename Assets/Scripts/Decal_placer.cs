using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decal_placer : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public GameObject decalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {



        Vector3 pos=new Vector3(0.0f,0.0f,0.0f);
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        BoxCollider bx = other.GetComponent<BoxCollider>();
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (bx)
            {
                pos = collisionEvents[i].intersection;
            }
            i++;
        }
        SpawnDecal(pos);



    }



    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
    }


    void SpawnDecal(Vector3 position) {

        var decal = Instantiate(decalPrefab);
        decal.transform.position = position;
    
    
    }

    // Update is called once per frame
    void Update()
    {

    }

}