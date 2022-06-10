using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    void Update()
    {
        if(GameManager.instance.PlayerList[0].playerState != PLAYERSTATE.WATING
            && GameManager.instance.PlayerList[1].playerState != PLAYERSTATE.WATING)
        {
            if (GameManager.instance.ThisTurnPlayer == 0)
            {
                transform.position = GameManager.instance.PlayerList[0].gameObject.transform.position;

            }
            else if (GameManager.instance.ThisTurnPlayer == 1)
            {
                transform.position = GameManager.instance.PlayerList[1].gameObject.transform.position;
            }
        }


    }
}
