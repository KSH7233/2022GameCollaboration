using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERSTATE
{
    WATING,
    PLAY,
    GOAL
}

public class Player : MonoBehaviour
{
    public int attack { get; private set; }
    public int hp { get; private set; }
    public bool destination { get; private set; }
    public bool isActivate { get; private set; } = false;

    public Vector3 stanbyPos { get; private set; }
    public bool isMove { get; private set; } = false;

    public PLAYERSTATE playerState { get; private set; } = PLAYERSTATE.WATING;

    private int unitNumber;


    IEnumerator Move(int count)
    {
        if (isMove) yield break;

        count = Mathf.Clamp(count, 1, 5); //count를 범위1-5 사이로 제한
        isMove = true;

        Place standingPlace = GameManager.instance.FindStandingPlace(transform.position);
        if (standingPlace)
        {          
            Vector3 startPos = transform.position;
            Place targetPlace = standingPlace.PostPlace[0];

            for (int i = 0; count > i; i++)
            {
                if(standingPlace.PostPlace.Length == 0)
                {
                    Debug.Log("Can`t find PostPlace");
                }
                else if (i == 0 && standingPlace.PostPlace.Length > 1)//count가 0일 조건, 도착해있는 지점이 코너인지 확인해서 [1]로 넘겨줄것, postplace 크기가 1일경우 0번에만 접근
                {
                    Debug.Log("shortcut");
                    targetPlace = standingPlace.PostPlace[1];
                }
                else
                {
                    targetPlace = standingPlace.PostPlace[0];                    
                }                    

                Vector3 targetPos = targetPlace.transform.position;


                float time = 0;

                while (1 > time)
                {
                    time += Time.deltaTime * 3.333f; // 나누기 0.3f

                    transform.position = Vector3.Lerp(startPos, targetPos, time);

                    yield return null;
                }
                startPos = targetPos;
                standingPlace = targetPlace;
            }
            isMove = false;
            //GameManager.instance.removeStepsAtList();
        }
    }


    public void Movement(int rand1to5)//이동 코드는 플레이어가 update 호출하는 것이 아님 gm이 update에서 호출해야함
    {
        switch (playerState) //case를 하나만 놓아도 되는가
        {
            case PLAYERSTATE.WATING:
                
                break;
            case PLAYERSTATE.PLAY:                

                Debug.Log(rand1to5);

                if (!isMove)
                {
                    StartCoroutine(Move(rand1to5)); //움직이는 동작중에 접근불가
                }

                break;
            case PLAYERSTATE.GOAL:
                Debug.Log("fin");

                //if("특정조건만족")
                //    playerState = PLAYERSTATE.PLAY;
                break;
        }
    }

    public void ConvertActivate()
    {
        isActivate = !isActivate;
        gameObject.SetActive(isActivate);
    }

    public void PositionToEntrance()
    {
        transform.position = GameManager.instance.FindPlaceToIndex(29).transform.position;
    }

    public void unitNumbering(int playerNum, int unitNum)
    {
        unitNumber = unitNum;

        stanbyPos = new Vector3(24 * (-playerNum) + unitNumber * 2, 2, 30);
        transform.position = stanbyPos;
    }

    private void OnMouseUpAsButton() //누르는 도중 나가면 클릭안됨
    {
        Debug.Log(unitNumber + " is clicked");
        GameManager.instance.ClickedUnitControl(unitNumber);
    }

    public void ChangeUnitState(PLAYERSTATE state)
    {
        playerState = state;
    }



    public void EncountMyUnit()//GameManager에서 확인된 아군유닛의 수 만큼 능력치증가 만 작성
    {
        //능력치 추가 HOST, GUEST 따져서

        gameObject.SetActive(false);
        transform.position = stanbyPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == gameObject.tag)
        {

        }
    }

}
