using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioManager : MonoBehaviour
{

    public AudioSource Boom_long_1;
    public AudioSource Boom_long_2;
    public AudioSource Boom_medium_1;
    public AudioSource Boom_short2;
    public AudioSource Boom_short_1;
    public AudioSource shit_is_burning;
    public AudioSource Super_shot_fire_roar;

    //Dragon Audio
    //Please use a "d" when you are adding a sound that
    //comes from the dragon
    public AudioSource dAttackEmpty;
    public AudioSource dEnemiesClose;
    public AudioSource dHeavyDamage;
    public AudioSource dFire_short_1;
    public AudioSource dLong_and_short_fire_roar_combined;
    public AudioSource dRoar_fire_long_1;
    public AudioSource dRoar_fire_short_1;
    public AudioSource dRoar_fire_short_2;
    public AudioSource dRoar_fire_short_3;
    public AudioSource dRoar_only;

    //Enemy Audio
    //Please use an "e" when you are adding a sound that
    //comes from the enemies



    // Start is called before the first frame update
    void Start()
    {
        dAttackEmpty.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
