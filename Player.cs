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
    public int attack { get; private set; }
    public int hp { get; private set; }
    public bool isActivate { get; private set; } = false;

    public bool isMove { get; private set; } = false;

    public PLAYERSTATE playerState { get; private set; } = PLAYERSTATE.WATING;



    IEnumerator Move()
    {
        if (isMove) yield break;

        //count = Mathf.Clamp(count, 1, 5); //count�� ����1-5 ���̷� ����
        isMove = true;
        int otherplayeridx = GameManager.instance.OtherPlayerIndex(playerNumber);

        Vector3 startPos = transform.position;
        Vector3 targetPos = GameManager.instance.PlayerList[otherplayeridx].transform.position;
        //Place targetPlace = standingPlace.PostPlace[0];

        //for (int i = 0; count > i; i++)
        //{
        //    Vector3 targetPos = targetPlace.transform.position;


        //    float time = 0;

        //    while (1 > time)
        //    {
        //        time += Time.deltaTime * 3.333f; // ������ 0.3f

        //        transform.position = Vector3.Lerp(startPos, targetPos, time);

        //        yield return null;
        //    }
        //    startPos = targetPos;
        //    //standingPlace = targetPlace;
        //}
        isMove = false;
        //GameManager.instance.removeStepsAtList();

        //Place standingPlace = GameManager.instance.FindStandingPlace(transform.position);

    }


    public void Movement(int rand1to5)//�̵� �ڵ�� �÷��̾ update ȣ���ϴ� ���� �ƴ� gm�� update���� ȣ���ؾ���
    {
        switch (playerState) //case�� �ϳ��� ���Ƶ� �Ǵ°�
        {
            case PLAYERSTATE.WATING:
                
                break;
            case PLAYERSTATE.PLAY:                

                Debug.Log("move " + this);

                if (!isMove)
                {
                    StartCoroutine(Move()); //�����̴� �����߿� ���ٺҰ�
                }

                break;
        }
    }



    public void StartPosSet(int playerIndex)
    {
        transform.position = new Vector3(-10 + (20 * playerIndex), 0, 0);
        gameObject.SetActive(false);
        playerNumber = playerIndex;
    }

    public void setActivate(bool onoff)
    {
        gameObject.SetActive(onoff);
    }



    //private void OnMouseUpAsButton() //������ ���� ������ Ŭ���ȵ�
    //{
    //    Debug.Log(unitNumber + " is clicked");
    //    GameManager.instance.ClickedUnitControl(unitNumber);
    //}

    public void ChangeUnitState(PLAYERSTATE state)
    {
        playerState = state;
    }






}
