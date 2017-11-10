using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXBoard : MonoBehaviour
{


    public AudioClip Move;

    public AudioClip[] EnemyAttackSounds = new AudioClip[] { (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Goblin/Hit_1"),
                                                             (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Goblin/Hit_2"),
                                                             (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Goblin/Hit_3"),
                                                             (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Goblin/Hit_4")};

    public AudioClip[] PlayerAttackSounds = new AudioClip[]{(AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack"),
                                                      (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack2"),
                                                      (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack3"),
                                                      (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack4"),
                                                      (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack5"),
                                                      (AudioClip)Resources.Load("Assets/TEMP_ASSETS/Voice Pack/Man_1/Attack6") };

    public AudioClip Damage;
    public AudioClip Death;


    void Start()
    {

    }

}