using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldiers : MonoBehaviour
{

    public GameObject flames;   //used to spawn flames once soldier catches fire 
    public GameObject mapInfo; 
    GameObject player;          //initializes player object in order to get player's location
    private AudioSource audioSource;
    public AudioClip[] fireDeath;
    public bool flee;


    public bool isOnFire=false; //checks if soldier is on fire
    public int health = 15;     //soldier's health
    public bool alive = true;   //checks if soldier is alive (used to control squads)

    public int mode;            //will be used to indicate how the soldiers will act once they spawn
    bool reachedGoal = false;   //will turn true if soldier reaches his goal
    public Vector3 goalPosition;//assigns the soldier's goal
    public NavMeshAgent agent;  //pathfinding object

    float speed = 20.0f;        //how fast the soldier is
    public float speedMod;
    float originalSpeed;

    float attackRange = 50.0f;  //how far soldiers have to be in order to attack the dragon

    //Variables that setup soldier's location while running and panicking
    float randomx;
    float randomz;
    Vector3 panicRun = new Vector3();

    //Timers 
    float timer;    //used for damage the soldier each second he's on fire
    float timer2;   //used to change the direction the soldier is running when he's on fire

    public bool stop = false;   //used for debugging, stops the soldier from moving around
    private GameObject flame1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("PlayerC");  //will fetch the player object
        mapInfo = GameObject.FindWithTag("MapInfo");

        //setup timers
        timer = Time.fixedTime + 1;
        timer2 = Time.fixedTime + 5;
        audioSource = GetComponent<AudioSource>();
        speedMod = 1f;
        originalSpeed = speed;
        float timerCO = 2f;
        StartCoroutine(EnemyLogic(timerCO));


    }


    //Checks if soldier is colliding with dragon's fire
    public void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire Attack Particle" && isOnFire == false)
        {
            flame1 = Instantiate(flames, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            flame1.transform.parent = gameObject.transform;

            isOnFire = true;
            int randomNum = Random.Range(0, fireDeath.Length);
            audioSource.clip = fireDeath[randomNum];
            audioSource.Play();
        }
    }

    IEnumerator EnemyLogic(float timeWait)
    {
        while (true) {

            updateStats();
            float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            //Soldier will behave this way if he's not on fire
            if (isOnFire == false && stop == false)
            {
                if (gameObject.tag == "Ranged Enemy")
                {
                    rangedUnit(distanceToPlayer);

                }
                else
                {
                    distanceToPlayer = HarassPlayer(); //Will check players position and deterimine if it will chase or attack
                    while (distanceToPlayer < 200.0f && attackRange< distanceToPlayer &&isOnFire==false)
                    {
                        distanceToPlayer= Vector3.Distance(gameObject.transform.position, player.transform.position);
                        //Debug.Log("Following"+distanceToPlayer);
                        agent.SetDestination(player.transform.position);
                        yield return null;

                    }
                }

                /*
              * enemyMode: 
              * 0 = will chase and attack the dragon
              * 1 = will head towards the closest 'objective point' object near the dragon
              * 2 = will ambush (not in this script, refer to SpawnEnemy.cs)         
               */
                if (distanceToPlayer >= 50.0f && gameObject.tag != "Ranged Enemy")
                {


                    switch (mode)
                    {
                        case 0:
                            agent.SetDestination(player.transform.position);
                            break;
                        case 1:
                            agent.SetDestination(goalPosition);
                            break;
                        case 3:
                            agent.SetDestination(goalPosition);
                            //Debug.Log(transform.position + "----" + goalPosition);
                            break;
                        case 4:
                            agent.SetDestination(goalPosition);
                            break;
                    }

                }

            }
            //This segment will control the enemy soldiers once they've caught fire
            else if(isOnFire==true)
            {
                while (health > 0 || isOnFire==true)
                {

                    Vector3 position = Timers(); // Will determine the soldier's next position and deplete health with the setup timers


                    Run(position);//Will make soldier run to parameter position

                    Vector3 dir = transform.position - player.transform.position;
                    //Debug.Log(dir);
                    agent.SetDestination(player.transform.position);

                    //checks if soldier is alive
                    if (health <= 0)
                    {
                        mapInfo.GetComponent<info>().numDeadSoldiers++;
                        alive = false;
                        stop = true;
                        Destroy(gameObject, 5.5f);
                    }

                    yield return null;
                }

            } //Soldier will stop whatever he's doing
            else if (stop == true)
            {
                agent.SetDestination(transform.position);
                if (gameObject.tag == "Ranged Enemy")
                {
                    rangedUnit(distanceToPlayer);

                }

            }



            yield return new WaitForSeconds(timeWait);
        }





    }

    // Update is called once per frame
    void Update()
    {





    }





    //Returns distance to player, will pursuit and attack if range is close enough
    float HarassPlayer()
    {

        int damage = 5;
        //Will determine if player is close enough to chase
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);





        //Will attack the player once it gets close enough. Every 5 seconds, there is a 5% it will damage the dragon
        if (distanceToPlayer < attackRange)
        {
            float savingThrow = Random.Range(0.0f, 1.0f);

            if (timer2 < Time.fixedTime)
            {

                if (savingThrow <= 0.20-player.GetComponentInChildren<Player>().defense)
                {
                    player.GetComponent<Player>().SetHealth(damage - (buttonHandler.defenseLvl / 2));
                    player.GetComponentInChildren<Player>().attacked = true;

                }
                timer2 = Time.fixedTime + 5;
            }
        }
        if (timer2 < Time.fixedTime)
        {
            timer2 = Time.fixedTime + 5;
        }

        return distanceToPlayer;
    }

    //Timers to control health depletion and generate new position to run 
    Vector3 Timers()
    {
        //Will generate random positions every 5 seconds
        if (timer2 < Time.fixedTime)
        {
            randomx = Random.Range(-100.00f, 100.00f);
            randomz = Random.Range(-100.00f, 100.00f);
            timer2 = Time.fixedTime + 5;

        }

        //Timer that will deplete soldier's health every second
        if (timer < Time.fixedTime)
        {
            health -= 1;

            timer = Time.fixedTime + 1;
        }

        return new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

    }

    void Run(Vector3 run)
    {
        float speed2 = speed * 2.5f;    //multiplies soldier speed *1.5
        Vector3 runDestination = Vector3.MoveTowards(transform.position, (run), speed2 * Time.deltaTime / 1.5f);

        agent.speed=speed2;
        agent.SetDestination(runDestination);

        


    }



    void rangedUnit(float pDistance)
    {

        if (pDistance < 30)
        {
            stop = false;
            flee = true;
            Vector3 dir = transform.position - player.transform.position;
            agent.SetDestination(dir);

        }else if (pDistance <= 45)
        {
            stop = true;
            flee = false;

        }else if (pDistance <= 75) {
            stop = false;
            flee = false;
            agent.SetDestination(player.transform.position);
            
          
            }

        if (isOnFire) {

            stop = false;
            flee = false;
        }


    }

    void updateStats() {

        speed = originalSpeed * speedMod;
    
    
    }


}


