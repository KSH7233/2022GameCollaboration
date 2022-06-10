using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator animator;
    Player GetPlayer;

    public string attack_anim_name;
    public string deffence_anim_name;
    public string standing_anim_name;

    void Start()
    {
        animator= gameObject.GetComponent<Animator>();
        GetPlayer = gameObject.GetComponentInParent<Player>();
    }

    void Update()
    {
        if(GetPlayer.Isattack==true)
        {
            animator.SetTrigger(attack_anim_name);
            //Debug.LogWarning("���ݸ�� on");
            GetPlayer.Isattack = false;
        }

        if (GetPlayer.Isdeffence==true)
        {
            animator.SetTrigger(deffence_anim_name);
            //Debug.LogWarning("����� on");
            GetPlayer.Isdeffence = false;
        }

        if(GetPlayer.Isattack==false || GetPlayer.Isdeffence==false)
        {
            animator.SetTrigger(standing_anim_name);
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
