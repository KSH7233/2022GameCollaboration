using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // 이 스크립트에 애니메이션, 캐릭터 효과음을 조정함.

    AudioSource audioSource;
    Animator animator;
    Player GetPlayer;

    public string attack_anim_name;
    public string deffence_anim_name;
    public string standing_anim_name;
    public string die_anim_name;

    public AudioClip hit;
    public AudioClip damaged;

    void Start()
    {
        animator= gameObject.GetComponent<Animator>();
        GetPlayer = gameObject.GetComponentInParent<Player>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void AudioPlay()
    {
        audioSource.Play();
    }

    void Update()
    {
        if(GetPlayer.Isattack==true)
        {
            animator.SetTrigger(attack_anim_name);
            Debug.LogWarning("공격모션 on");

            audioSource.clip = hit;
            audioSource.volume = 1.0f;
            Invoke("AudioPlay", 0.5f);

            GetPlayer.Isattack = false;
        }

        if (GetPlayer.Isdeffence==true)
        {
            animator.SetTrigger(deffence_anim_name);
            Debug.LogWarning("방어모션 on");

            audioSource.clip = damaged;
            audioSource.volume = 0.8f;
            Invoke("AudioPlay", 0.5f);

            GetPlayer.Isdeffence = false;
        }

        if(GetPlayer.Isattack==false || GetPlayer.Isdeffence==false)
        {
            animator.SetTrigger(standing_anim_name);
        }

        if(GetPlayer.Isdie==true)
        {
            animator.SetBool(die_anim_name, true);
        }
        //if (players[GetComponent<GameManager>().turnplayerindex()].Isattack == true)
        //{

        //}

        //if(Player.isattack==true)
        //{
        //    animator.SetBool(attack_anim_name, true);
        //    Player.isattack=false;
        //}
        //else if(Player.isattack==false)
        //{
        //    animator.SetBool(attack_anim_name, false);
        //}
    }
}
