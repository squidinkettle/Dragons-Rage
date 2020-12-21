using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    //Game manager
    public GameManager gameMan;
    //Fire Attack (FA) partical
    public ParticleSystem fireAttack;
    //Is the FA being used?
    public bool fireAStarted;
    //Dragons FA animation
    Animator dragon_animation;

    //Sound control
    private AudioSource audioSource;
    public AudioClip[] clips;
    private int clipNum=0;
    bool firstCall = false;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        //Initializes dragon's animator
        dragon_animation = GetComponent<Animator>();
        //Stops FA partical from starting when game starts
        fireAttack.Stop();
        //Initializes audio
        audioSource = GetComponent<AudioSource>();
        timer = Time.fixedTime + 1.5f;
    }

    public void SetFireAttackStart(bool set)
    {
        fireAStarted = set;
    }
    public bool GetFireAttackStart()
    {
        return fireAStarted;
    }

    // Update is called once per frame
    void Update()
    {
        //If the FA is being used
        //FA particle starts
        //Dragon FA starting animation plays
       

        if (fireAStarted == true)
        {
            playAudioClip(clipNum);

            if (timer < Time.fixedTime)
            {
                clipNum = 1;
            }



            fireAttack.Play();
            dragon_animation.SetBool("is_attacking", true);
     


        }else
        {
            fireAttack.Stop();
            dragon_animation.SetBool("is_attacking", false);


            if(Input.GetKeyUp(KeyCode.Space))
                playAudioClip(2);
            clipNum = 0;
            timer = Time.fixedTime + 1.5f;

        }
    }

    //Plays attack animation
    void playFireAttackClip()
    {
        audioSource.clip = clips[0];

        audioSource.Play();


    }

    void continueAttackingClip() {
        audioSource.clip = clips[1];
        if (!audioSource.isPlaying)
        {

            audioSource.Play();
        }
    }
    void endAttackingClip() {
        audioSource.clip = clips[2];
        if (!audioSource.isPlaying)
        {

            audioSource.Play();
        }
    }
    void playAudioClip(int x)
    {
        audioSource.clip = clips[x];

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
         

        }
   


    }

}


