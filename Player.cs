using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    WATING,
    PLAY,
    PLAY2, //�������� �̱��
    ISOL
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
    public bool Isdie = false;

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

        //Isattack = true;
        isMove = true;
        int otherplayeridx = GameManager.instance.OtherPlayerIndex(playerNumber);
        Debug.Log("Move to num : " + otherplayeridx);

        Vector3 startPos = savedLastPos;
        Vector3 middlePos = GameManager.instance.PlayerList[otherplayeridx].transform.position;
        Vector3 targetPos = GameManager.instance.LastClickedPlace.transform.position; //�̰Ŷ� ������ ����

        

        float time = 0;

        time = 0;
        Debug.Log("moving begin");
        while (1 > time)
        {
            GameManager.instance.SetIsmove(true);

            time += Time.deltaTime * 4;

            
            switch(playerState)
            {
                case PLAYERSTATE.PLAY:
                    if (GameManager.instance.PlayerList[otherplayeridx].playerState == PLAYERSTATE.ISOL)
                    {
                        //transform.position = Vector3.Lerp(startPos, targetPos, time);
                        GameManager.instance.PlayerList[otherplayeridx].SetPlayerState(PLAYERSTATE.PLAY);
                        Debug.Log("isol -> play");
                    }

                    transform.position = Vector3.Lerp(startPos, targetPos, time);

                    break;
                case PLAYERSTATE.PLAY2:
                    transform.position = Vector3.Lerp(startPos, targetPos, time);
                    break;
            }

            GameManager.instance.SetIsmove(false);
            yield return null;
        }
        Debug.Log("moving end");
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

                int otherplayeridx = GameManager.instance.OtherPlayerIndex(playerNumber);

                if (GameManager.instance.PlayerList[otherplayeridx].playerState == PLAYERSTATE.ISOL)
                {
                    //StartMoveCoroutine();
                    Invoke("StartMoveCoroutine", 0.5f);
                    break;
                }
                Isattack = true;
                GameManager.instance.PlayerList[otherplayeridx].Isdeffence = true;

                Invoke("StartMoveCoroutine", 0.5f);

                break;
            case PLAYERSTATE.PLAY2:
            
                Debug.Log("move " + this);

                Invoke("StartMoveCoroutine", 0.5f);

                //if (!isMove)
                //{
                //    StartCoroutine(Move()); //�����̴� �����߿� ���ٺҰ�        
                //}

                GameManager.instance.HookSound();
                break;
            case PLAYERSTATE.ISOL:
                transform.position = GameManager.instance.LastClickedPlace.transform.position;
                Debug.Log("move isol state");
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

    
    public void SetPlayerState(PLAYERSTATE P)
    {
        playerState = P;
    }

    public void StartMoveCoroutine()
    {
        if (!isMove)
        {
            StartCoroutine(Move()); //�����̴� �����߿� ���ٺҰ�        
        }
    }
}
