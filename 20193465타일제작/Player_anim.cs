using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_anim : MonoBehaviour
{
    Animator animator;

    //인스펙터 창에서 애니메이션 이름을 입력받음.
    //인스펙터 창에서 애니메이션 이름을 할당해주면, 캐릭터별 애니메이션 스크립트를 여러 개 만들필요x.
    //현재 기사 캐릭터의 사망,방어 모션이 안만들어졌으므로 일단 attack애니메이션만 받도록 함.
    public string attack_anim_name;
    //public string die_anim_name;
    //public string deffence_anim_name;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //캐릭터 애니메이션 트리거.
        //조건은 임의로 정함.
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
