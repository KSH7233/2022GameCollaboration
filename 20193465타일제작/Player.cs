using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();      
    }

    private void Update()
    {
        //ĳ���� �ִϸ��̼�
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Isattack", true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Isattack", false);

        }
    }

}