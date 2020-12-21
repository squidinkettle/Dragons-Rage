using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    float timer;
    Player player;
    List<House> buildings=new List<House>();
    // Start is called before the first frame update
    void Start()
    {
 
        timer = Time.fixedTime + 0.25f;

        player = FindObjectOfType<Player>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        int damage = player.GetComponent<Player>().strength * 2;
      
        if (other.gameObject.GetComponent<House>())
        {
            other.gameObject.GetComponent<House>().SetHealth(damage);
        }


        //if the dragon's fire makes contact with a house, it will lower its health
        //and will start spreading fire

        if (other.gameObject.GetComponent<EnemySoldiers>()) {
            other.gameObject.GetComponent<EnemySoldiers>().health -= damage / 2;
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.fixedTime) {
            Destroy(gameObject);
        
        }
    }
}
