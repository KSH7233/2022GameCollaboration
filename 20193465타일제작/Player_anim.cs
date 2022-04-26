using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_anim : MonoBehaviour
{
    Animator animator;

    //�ν����� â���� �ִϸ��̼� �̸��� �Է¹���.
    //�ν����� â���� �ִϸ��̼� �̸��� �Ҵ����ָ�, ĳ���ͺ� �ִϸ��̼� ��ũ��Ʈ�� ���� �� �����ʿ�x.
    //���� ��� ĳ������ ���,��� ����� �ȸ���������Ƿ� �ϴ� attack�ִϸ��̼Ǹ� �޵��� ��.
    public string attack_anim_name;
    //public string die_anim_name;
    //public string deffence_anim_name;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //ĳ���� �ִϸ��̼� Ʈ����.
        //������ ���Ƿ� ����.
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(attack_anim_name, true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool(attack_anim_name, false);

        }
    }
}
