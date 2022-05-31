using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    WATING,
    PLAY
}

public class Player : MonoBehaviour
{
    public int playerNumber { get; private set; }
    public bool isattack { get; protected set; } = false;
    public int hp { get; private set; }
    public bool isActivate { get; private set; } = false;

    public bool isMove { get; private set; } = false;

    public PLAYERSTATE playerState { get; private set; } = PLAYERSTATE.WATING;

    Vector3 savedLastPos;

    //���
    public bool Isattack = false;
    public bool Isdeffence = false;
    public bool Isinisland = false;

    public int getgoldnum = 0;

    private Animator animator;
   

    void Start()
    {
        savedLastPos = transform.position;

        animator = gameObject.GetComponentInChildren<Animator>();
        
    }

    IEnumerator Move() //play ������ ��
    {
        if (isMove) yield break;

        isMove = true;
        int otherplayeridx = GameManager.instance.OtherPlayerIndex(playerNumber);
        Debug.Log("Move to num : " + otherplayeridx);

        Vector3 startPos = savedLastPos;
        Vector3 middlePos = GameManager.instance.PlayerList[otherplayeridx].transform.position;
        Vector3 targetPos = GameManager.instance.LastClickedPlace.transform.position;

        float time = 0;

        //while (1 > time)
        //{
        //    time += Time.deltaTime * 2; 
        //    transform.position = Vector3.Lerp(startPos, middlePos, time);
        //    Debug.Log("move11");
        //}

        time = 0;
        while (1 > time)
        {
            
            time += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(middlePos, targetPos, time);
            yield return null;
        }
        Isattack = true;
        isMove = false;
        savedLastPos = targetPos;

    }


    public void Movement()//�̵� �ڵ�� �÷��̾ update ȣ���ϴ� ���� �ƴ� gm�� update���� ȣ���ؾ���
    {
        switch (playerState) //case�� �ϳ��� ���Ƶ� �Ǵ°�
        {
            case PLAYERSTATE.WATING:
                transform.position = GameManager.instance.LastClickedPlace.transform.position;
                playerState = PLAYERSTATE.PLAY;
                gameObject.SetActive(true);               
                break;

            case PLAYERSTATE.PLAY:

                Debug.Log("move " + this);

                if (!isMove)
                {
                    StartCoroutine(Move()); //�����̴� �����߿� ���ٺҰ�        
                }
                break;
        }
        GameManager.instance.AllNoticeOff();
    }



    public void StartPosSet(int playerIndex)
    {
        Isdeffence = false;
        Isattack = false;
        transform.position = new Vector3(-10 + (20 * playerIndex), 0, 0);
        gameObject.SetActive(false);
        playerNumber = playerIndex;
    }

    public void setActivate(bool onoff)
    {
        gameObject.SetActive(onoff);
    }


    public void ChangeUnitState(PLAYERSTATE state)
    {
        playerState = state;
    }

    




}
